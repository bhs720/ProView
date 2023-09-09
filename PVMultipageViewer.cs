using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace ProView
{
    internal class PVImageListViewItem : ListViewItem
    {
        
    }

    internal class PVMultipageViewer  : ListView
    {
        private PVDocument _pvDocument;
        //private ListView _listView;
        private Size _imageSize = new Size(256, 256);

        public PVMultipageViewer() : base()
        {
            PVInitializeComponents();
        }

        private void PVInitializeComponents()
        {
            base.DoubleBuffered = true;
            base.OwnerDraw = true;
            //_listView = new ListView();
            base.LargeImageList = new ImageList();
            base.Dock = DockStyle.Fill;
            base.TileSize = _imageSize;
            base.VirtualMode = true;
            base.VirtualListSize = 0;
            base.View = View.LargeIcon;
            //_listView.RetrieveVirtualItem += _listView_RetrieveVirtualItem;
            //_listView.CacheVirtualItems += _listView_CacheVirtualItems;
            //_listView.SearchForVirtualItem += _listView_SearchForVirtualItem;
        }
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (e.ItemIndex > 0 && e.ItemIndex < base.LargeImageList.Images.Count)
            {
                e.Graphics.DrawImageUnscaled(base.LargeImageList.Images[e.ItemIndex], e.Bounds.Location);
            }
            
            e.DrawText();
            //base.OnDrawItem(e);
            
        }
        protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
        {
            int pageNumber = e.ItemIndex + 1;
            string imageKey = pageNumber.ToString();

            if (!base.LargeImageList.Images.ContainsKey(imageKey))
            {
                var img = PVGetPageThumbnail(pageNumber);
                base.LargeImageList.Images.Add(imageKey, img);
            }

            e.Item = new ListViewItem();
            e.Item.Text = $"Page {pageNumber}";
            e.Item.ImageKey = imageKey;
            e.Item.ImageIndex = base.LargeImageList.Images.IndexOfKey(imageKey);
            
            base.OnRetrieveVirtualItem(e);
        }

        public PVDocument PVDocument
        {
            get
            {
                return _pvDocument;
            }
            set
            {
                _pvDocument = value;
                base.VirtualListSize = _pvDocument.PageCount;
                base.LargeImageList = new ImageList();
                base.LargeImageList.ImageSize = _imageSize;
            }
        }

        private Image PVGetPageThumbnail(int pageNubmer)
        {
            //return Image.FromFile(@"C:\Users\bsmith\Downloads\2023-07-13 20_06_26-ProFile Counter.png");
            var page = _pvDocument.GetPage(pageNubmer);
            var renderDevice = page.GetRenderDevice(this);
            float scale = Math.Min(_imageSize.Width / page.Width, _imageSize.Height / page.Height);
            int width = (int)(page.Width * scale);
            int height = (int)(page.Height * scale);
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppPArgb);
            renderDevice.Render(bmp, scale, 0, 0);
            renderDevice.Release(this);
            return bmp;
        }
    }
}
