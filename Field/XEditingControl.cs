using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ProView
{
    public class XEditingControl : TextBox, IDataGridViewEditingControl
    {
        bool valueChanged;
        readonly SearchBox searchBox = new SearchBox();
        public XEditingControl()
        {
            CausesValidation = true;
            searchBox.DeleteItemRequest += delegate(string item)
            {
                Field.Source.Remove(item);
                UpdateSearchBox();
            };
            searchBox.SelectItemRequest += delegate { CopyValueFromSearchBox(); };

            // Don't want the default right-click menu because PVDataGridVewer uses a custom right-click menu
            // [Ctrl]+[A] [Ctrl]+[C] [Ctrl]+[V] [Ctrl]+[Z] shortucts are implemented through ProcessCmdKey()
            ShortcutsEnabled = false;
        }

        public Field Field
        {
            get
            {
                return EditingCol.Field;
            }
        }

        public int EditingControlRowIndex { get; set; }

        DataGridView editingControlDataGridView;
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return editingControlDataGridView;
            }
            set
            {
                editingControlDataGridView = value;
                if (editingControlDataGridView != null)
                {
                    // Hoping this code only runs once
                    editingControlDataGridView.Scroll += delegate { searchBox.Visible = false; };
                    editingControlDataGridView.Resize += delegate { searchBox.Visible = false; };
                    editingControlDataGridView.FindForm().LocationChanged += delegate { searchBox.Visible = false; };
                }
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }
        public object EditingControlFormattedValue
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value as string;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
        }

        // DataGridView handles [Enter] and [Tab] keys.
        // These functions give this editing control a chance to handle them first.
        public bool HandledEnterKey()
        {
            if (searchBox.Visible)
            {
                CopyValueFromSearchBox();
                //searchBox.Visible = false;
                return true;
            }
            return false;
        }

        public bool HandledTabKey()
        {
            if (searchBox.Visible)
            {
                CopyValueFromSearchBox();
                //searchBox.Visible = false;
                return true;
            }
            return false;
        }

        void CopyValueFromSearchBox()
        {
            Text = searchBox.SelectedItem;
            searchBox.Visible = false;
            SelectAll();
        }

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            /*
            if (searchBox.Visible)
            {
                switch (key & Keys.KeyCode)
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Escape:
                        return true;
                    default:
                        return !dataGridViewWantsInputKey;
                }
            }
             * */
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            BackColor = EditingRow.DefaultCellStyle.BackColor;
            if (selectAll)
                SelectAll();
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return Cursors.Default;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Debug.WriteLine("Editing Control eyData {0}", keyData);
            if (searchBox.Visible)
            {
                switch (keyData)
                {
                    case Keys.Escape:
                        searchBox.Visible = false;
                        return true;
                    case Keys.Up:
                        searchBox.SelectedIndex--;
                        return true;
                    case Keys.Down:
                        searchBox.SelectedIndex++;
                        return true;
                }
            }
            switch (keyData)
            { 
                // Insert underscore when user presses [Shift]+[NumPadMinus]
                case (Keys.Shift | Keys.Subtract):
                    Paste("_");
                    return true;
                // Reimplement Copy and Paste functionality since ShortcutsEnabled = false
                case (Keys.Control | Keys.C):
                    Clipboard.SetText(SelectedText);
                    return true;
                case (Keys.Control | Keys.V):
                    Paste(Clipboard.GetText());
                    return true;
                case (Keys.Control | Keys.A):
                    SelectAll();
                    return true;
                case (Keys.Control | Keys.Z):
                    Undo();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnValidating(System.ComponentModel.CancelEventArgs e)
        {
            Field.Add(Text);
            searchBox.Visible = false;
            base.OnValidating(e);
        }

        DataGridViewCell EditingCell
        {
            get
            {
                return EditingControlDataGridView.CurrentCell;
            }
        }

        UserColumn EditingCol
        {
            get
            {
                return EditingCell.OwningColumn as UserColumn;
            }
        }

        DataGridViewRow EditingRow
        {
            get
            {
                return EditingCell.OwningRow;
            }
        }

        Rectangle EditingCellRect
        {
            get
            {
                var cellRect = EditingControlDataGridView.GetCellDisplayRectangle(EditingCell.ColumnIndex, EditingCell.RowIndex, false);
                return new Rectangle(EditingControlDataGridView.PointToScreen(cellRect.Location), cellRect.Size);
            }
        }

        void UpdateSearchBox()
        {
            int bestMatchIndex;
            var results = Field.Source.Search(Text, true, out bestMatchIndex);
            if (results.Count > 0)
            {
                searchBox.DataSource = results;
                searchBox.Show(EditingCellRect);
                if (bestMatchIndex >= 0)
                    searchBox.SelectedIndex = bestMatchIndex;
            }
            else
            {
                searchBox.Visible = false;
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (Field.SourceMode != SourceMode.None)
                UpdateSearchBox();
            valueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(e);
        }
    }
}
