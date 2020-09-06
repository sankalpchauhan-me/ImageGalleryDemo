namespace Image_Galery_Demo
{
    partial class ImageGallery
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            //this.tile1 = new C1.Win.C1Tile.Tile();
            //this.tile2 = new C1.Win.C1Tile.Tile();
            //this.tile3 = new C1.Win.C1Tile.Tile();
            //((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            //this.mainSplitContainer.Panel2.SuspendLayout();
            //this.mainSplitContainer.SuspendLayout();
            //this.statusStrip.SuspendLayout();
            this.SuspendLayout();
           
            /**
            // 
            // tile1
            // 
            this.tile1.BackColor = System.Drawing.Color.LightCoral;
            this.tile1.Name = "tile1";
            this.tile1.Text = "Tile 1";
            // 
            // tile2
            // 
            this.tile2.BackColor = System.Drawing.Color.Teal;
            this.tile2.Name = "tile2";
            this.tile2.Text = "Tile 2";
            // 
            // tile3
            // 
            this.tile3.BackColor = System.Drawing.Color.SteelBlue;
            this.tile3.Name = "tile3";
            this.tile3.Text = "Tile 3";
            **/
            // 
            // ImageGallery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(810, 810);
            this.Name = "ImageGallery";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Image Gallery";
            //this.mainSplitContainer.Panel2.ResumeLayout(false);
            //this.mainSplitContainer.Panel2.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            //this.mainSplitContainer.ResumeLayout(false);
            //this.statusStrip.ResumeLayout(false);
            //this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        //private C1.Win.C1Tile.Tile tile1;
        //private C1.Win.C1Tile.Tile tile2;
        //private C1.Win.C1Tile.Tile tile3;
        System.ComponentModel.ComponentResourceManager resources;
    }
}

