namespace ProView
{
    partial class PVImageViewerStatus
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	this.txtPage = new System.Windows.Forms.TextBox();
        	this.txtScale = new System.Windows.Forms.TextBox();
        	this.SuspendLayout();
        	// 
        	// txtPage
        	// 
        	this.txtPage.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.txtPage.Location = new System.Drawing.Point(40, 0);
        	this.txtPage.Margin = new System.Windows.Forms.Padding(0);
        	this.txtPage.Name = "txtPage";
        	this.txtPage.Size = new System.Drawing.Size(40, 19);
        	this.txtPage.TabIndex = 6;
        	this.txtPage.TabStop = false;
        	this.txtPage.Text = "1 / 5";
        	this.txtPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	this.txtPage.WordWrap = false;
        	this.txtPage.Click += new System.EventHandler(this.txtPage_Click);
        	this.txtPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPage_KeyDown);
        	this.txtPage.Leave += new System.EventHandler(this.txtPage_Leave);
        	// 
        	// txtScale
        	// 
        	this.txtScale.Font = new System.Drawing.Font("Segoe UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.txtScale.Location = new System.Drawing.Point(0, 0);
        	this.txtScale.Margin = new System.Windows.Forms.Padding(0);
        	this.txtScale.Name = "txtScale";
        	this.txtScale.Size = new System.Drawing.Size(40, 19);
        	this.txtScale.TabIndex = 7;
        	this.txtScale.TabStop = false;
        	this.txtScale.Text = "56.6%";
        	this.txtScale.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	this.txtScale.Click += new System.EventHandler(this.txtZoom_Click);
        	this.txtScale.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtZoom_KeyDown);
        	this.txtScale.Leave += new System.EventHandler(this.txtZoom_Leave);
        	// 
        	// PVImageViewerStatus
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.Controls.Add(this.txtScale);
        	this.Controls.Add(this.txtPage);
        	this.Margin = new System.Windows.Forms.Padding(0);
        	this.Name = "PVImageViewerStatus";
        	this.Size = new System.Drawing.Size(80, 20);
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPage;
        private System.Windows.Forms.TextBox txtScale;

    }
}
