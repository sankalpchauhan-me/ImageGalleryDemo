using C1.Win.C1Tile;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Galery_Demo
{
    class AppView
    {

        public static SplitContainer getSplitContainer()
        {
            SplitContainer mainSplitContainer = new SplitContainer();
            mainSplitContainer.Dock = DockStyle.Fill;
            mainSplitContainer.IsSplitterFixed = true;
            mainSplitContainer.Location = new Point(0, 0);
            mainSplitContainer.Margin = new Padding(2);
            mainSplitContainer.Name = "mainSplitContainer";
            mainSplitContainer.Orientation = Orientation.Horizontal;
            mainSplitContainer.Size = new Size(784, 661);
            mainSplitContainer.SplitterDistance = 84;
            mainSplitContainer.TabIndex = 0;
            return mainSplitContainer;
        }

        public static TextBox getSearchBox()
        {
            TextBox searchBox = new TextBox();
            searchBox.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
           | AnchorStyles.Left)
           | AnchorStyles.Right);
            searchBox.BorderStyle = BorderStyle.None;
            searchBox.Cursor = Cursors.IBeam;
            searchBox.Location = new Point(44, 9);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(235, 13);
            searchBox.TabIndex = 0;
            searchBox.Text = Properties.Resources.search_image;
            return searchBox;
        }

        public static PictureBox getSearchImagePictureBox()
        {
            PictureBox searchImagePictureBox = new PictureBox();
            searchImagePictureBox.Image = Properties.Resources.downloadsearch;
            searchImagePictureBox.Location = new Point(493, 3);
            searchImagePictureBox.Name = "searchImagePictureBox";
            searchImagePictureBox.Size = new Size(24, 24);
            searchImagePictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            searchImagePictureBox.TabIndex = 3;
            searchImagePictureBox.TabStop = false;

            return searchImagePictureBox;

        }

        public static TableLayoutPanel getTableLayoutPanel()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37.5F));
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.Location = new Point(0, 0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Size = new Size(784, 84);
            tableLayoutPanel.TabIndex = 0;

            return tableLayoutPanel;
        }

        public static PictureBox getExportPDFButton()
        {
            PictureBox exportImage = new PictureBox();
            exportImage.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            exportImage.Image = Properties.Resources.ExportToPDF;
            exportImage.Location = new Point(20, 46);
            exportImage.Margin = new Padding(20, 3, 3, 10);
            exportImage.Name = "exportImage";
            exportImage.Size = new Size(135, 28);
            exportImage.SizeMode = PictureBoxSizeMode.StretchImage;
            exportImage.TabIndex = 2;
            exportImage.TabStop = false;
            exportImage.Visible = false;

            return exportImage;
        }

        public static Label getStausLabel()
        {
            Label statusLabel = new Label();
            statusLabel.Cursor = Cursors.Default;
            statusLabel.Dock = DockStyle.Fill;
            statusLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            statusLabel.ForeColor = SystemColors.ControlDarkDark;
            statusLabel.Location = new Point(0, 2);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(784, 549);
            statusLabel.TabIndex = 4;
            statusLabel.Text = Properties.Resources.initial_status_label;
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            return statusLabel;
        }

        public static Label getBorder()
        {
            Label label1 = new Label();
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(784, 2);
            label1.TabIndex = 3;
            return label1;
        }

        public static Group getGroup()
        {
            Group group1 = new Group();
            group1.Name = "group1";
            group1.Visible = false;
            return group1;
        }

        public static C1TileControl getTileCntrol()
        {
            C1TileControl imageTileControl = new C1TileControl();
            imageTileControl.AllowChecking = true;
            imageTileControl.AllowRearranging = true;
            imageTileControl.CellHeight = 78;
            imageTileControl.CellSpacing = 11;
            imageTileControl.CellWidth = 78;
            imageTileControl.Dock = DockStyle.Fill;
            imageTileControl.Location = new Point(0, 0);
            imageTileControl.Name = "imageTileControl";
            imageTileControl.Orientation = LayoutOrientation.Vertical;
            imageTileControl.Padding = new Padding(0);
            imageTileControl.Size = new Size(784, 573);
            imageTileControl.SurfacePadding = new Padding(12, 4, 12, 4);
            imageTileControl.SwipeDistance = 20;
            imageTileControl.SwipeRearrangeDistance = 98;
            imageTileControl.TabIndex = 1;
            imageTileControl.TextSize = 0F;
            return imageTileControl;
        }

        public static ToolStripProgressBar getProgressBarTool()
        {
            ToolStripProgressBar toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            toolStripProgressBar1.Size = new Size(100, 16);
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            return toolStripProgressBar1;
        }

        public static StatusStrip getStatusStrip()
        {
            StatusStrip statusStrip = new StatusStrip();
            statusStrip.Location = new Point(0, 551);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(784, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            statusStrip.Visible = false;
            return statusStrip;
        }
    }
}
