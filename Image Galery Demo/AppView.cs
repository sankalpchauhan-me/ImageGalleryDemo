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

        public static SplitContainer GetSplitContainer()
        {
            SplitContainer mainSplitContainer = new SplitContainer
            {
                Dock = DockStyle.Fill,
                IsSplitterFixed = true,
                Location = new Point(0, 0),
                Margin = new Padding(2),
                Name = "mainSplitContainer",
                Orientation = Orientation.Horizontal,
                Size = new Size(784, 661),
                SplitterDistance = 84,
                TabIndex = 0
            };
            return mainSplitContainer;
        }

        public static TextBox GetSearchBox()
        {
            TextBox searchBox = new TextBox
            {
                Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
           | AnchorStyles.Left)
           | AnchorStyles.Right),
                BorderStyle = BorderStyle.None,
                Cursor = Cursors.IBeam,
                Location = new Point(44, 9),
                Name = "searchBox",
                Size = new Size(235, 13),
                TabIndex = 0,
                Text = Properties.Resources.search_image
            };
            return searchBox;
        }

        public static PictureBox GetSearchImagePictureBox()
        {
            PictureBox searchImagePictureBox = new PictureBox
            {
                Image = Properties.Resources.downloadsearch,
                Location = new Point(493, 3),
                Name = "searchImagePictureBox",
                Size = new Size(24, 24),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabIndex = 3,
                TabStop = false
            };

            return searchImagePictureBox;

        }

        public static TableLayoutPanel GetTableLayoutPanel()
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

        public static PictureBox GetExportPDFButton()
        {
            PictureBox exportImage = new PictureBox
            {
                Anchor = (AnchorStyles.Bottom | AnchorStyles.Left),
                Image = Properties.Resources.ExportToPDF,
                Location = new Point(20, 46),
                Margin = new Padding(20, 3, 3, 10),
                Name = "exportImage",
                Size = new Size(135, 28),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabIndex = 2,
                TabStop = false,
                Visible = false
            };

            return exportImage;
        }

        public static Label GetStausLabel()
        {
            Label statusLabel = new Label
            {
                Cursor = Cursors.Default,
                Dock = DockStyle.Fill,
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = SystemColors.ControlDarkDark,
                Location = new Point(0, 2),
                Name = "statusLabel",
                Size = new Size(784, 549),
                TabIndex = 4,
                Text = Properties.Resources.initial_status_label,
                TextAlign = ContentAlignment.MiddleCenter
            };
            return statusLabel;
        }

        public static Label GetBorder()
        {
            Label label1 = new Label
            {
                BorderStyle = BorderStyle.Fixed3D,
                Dock = DockStyle.Top,
                Location = new Point(0, 0),
                Name = "label1",
                Size = new Size(784, 2),
                TabIndex = 3
            };
            return label1;
        }

        public static Group GetGroup()
        {
            Group group1 = new Group
            {
                Name = "group1",
                Visible = false
            };
            return group1;
        }

        public static C1TileControl GetTileCntrol()
        {
            C1TileControl imageTileControl = new C1TileControl
            {
                AllowChecking = true,
                AllowRearranging = true,
                CellHeight = 78,
                CellSpacing = 11,
                CellWidth = 78,
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "imageTileControl",
                Orientation = LayoutOrientation.Vertical,
                Padding = new Padding(0),
                Size = new Size(784, 573),
                SurfacePadding = new Padding(12, 4, 12, 4),
                SwipeDistance = 20,
                SwipeRearrangeDistance = 98,
                TabIndex = 1,
                TextSize = 0F
            };
            return imageTileControl;
        }

        public static ToolStripProgressBar GetProgressBarTool()
        {
            ToolStripProgressBar toolStripProgressBar1 = new ToolStripProgressBar
            {
                Name = "toolStripProgressBar1",
                Size = new Size(100, 16),
                Style = ProgressBarStyle.Marquee
            };
            return toolStripProgressBar1;
        }

        public static Panel GetPanel()
        {
            Panel panel1 = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(199, 3),
                Name = "panel1",
                Size = new Size(288, 78),
                TabIndex = 0
            };
            return panel1;
        }

        public static StatusStrip GetStatusStrip()
        {
            StatusStrip statusStrip = new StatusStrip
            {
                Location = new Point(0, 551),
                Name = "statusStrip",
                Size = new Size(784, 22),
                TabIndex = 2,
                Text = "statusStrip1",
                Visible = false
            };
            return statusStrip;
        }
    }
}
