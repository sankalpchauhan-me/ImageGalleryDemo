using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using C1.Win.C1Tile;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using C1.C1Pdf;
using C1.Framework;

namespace Image_Galery_Demo
{

    /// <summary>
    /// Image Gallery class of form
    /// </summary>
    public partial class ImageGallery : Form, DataFetcher.IStatus
    {
        //UI
        private TextBox searchBox;
        private PictureBox exportImage;
        private TableLayoutPanel tableLayoutPanel;
        private PictureBox searchImagePictureBox;
        private Label label1;
        private Label statusLabel;
        private C1TileControl imageTileControl;
        private StatusStrip statusStrip;
        private ToolStripProgressBar toolStripProgressBar1;
        private Group group1;
        private SplitContainer mainSplitContainer;
        private Panel panel1;

        //Data
        DataFetcher datafetch = new DataFetcher();
        List<ImageItem> imagesList;
        int checkedItems = 0;

        [DllImport(Constants.NET_DRIVER)]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Constructor for the form
        /// </summary>
        public ImageGallery()
        {
            InitializeComponent();
            InitUi();
            this.KeyUp += ImageGalleryEnterPressed;
        }

        // Initializes the UI from the base components in AppView class
        public void InitUi()
        {
            //Panel
            panel1 = AppView.GetPanel();
            panel1.Paint += new PaintEventHandler(PanelPaint);
            //Split Container
            mainSplitContainer = AppView.GetSplitContainer();
            Controls.Add(mainSplitContainer); Controls.Add(mainSplitContainer);

            //init Search EditText
            searchBox = AppView.GetSearchBox();
            searchBox.TextChanged += new EventHandler(OnTextChanged);
            panel1.Controls.Add(searchBox);

            //init Search Picture box
            searchImagePictureBox = AppView.GetSearchImagePictureBox();
            searchImagePictureBox.Click += new EventHandler(this.OnSearchClicked);

            // Table LayoutPanel
            tableLayoutPanel = AppView.GetTableLayoutPanel();
            tableLayoutPanel.Controls.Add(panel1, 1, 0);
            tableLayoutPanel.Controls.Add(searchImagePictureBox, 2, 0);
            mainSplitContainer.Panel1.Controls.Add(tableLayoutPanel);

            //init Export PDF Btn
            exportImage = AppView.GetExportPDFButton();
            exportImage.Click += new EventHandler(this.OnExportClick);
            exportImage.Paint += new PaintEventHandler(this.ExportImagePaint);
            tableLayoutPanel.Controls.Add(this.exportImage, 0, 0);

            // init Border
            label1 = AppView.GetBorder();
            mainSplitContainer.Panel2.Controls.Add(label1);

            //init status label
            statusLabel = AppView.GetStausLabel();
            mainSplitContainer.Panel2.Controls.Add(statusLabel);

            //Groups
            group1 = AppView.GetGroup();

            //init C1Tile
            imageTileControl = AppView.GetTileCntrol();
            imageTileControl.Groups.Add(group1);
            imageTileControl.TileChecked += new EventHandler<TileEventArgs>(OnTileChecked);
            imageTileControl.TileUnchecked += new EventHandler<TileEventArgs>(OnTileUnchecked);
            mainSplitContainer.Panel2.Controls.Add(this.imageTileControl);

            //ProgressBar
            toolStripProgressBar1 = AppView.GetProgressBarTool();

            //init Status Bar
            statusStrip = AppView.GetStatusStrip();
            statusStrip.Items.AddRange(new ToolStripItem[] {
            toolStripProgressBar1});
            mainSplitContainer.Panel2.Controls.Add(statusStrip);
        }

        // Perform Search if "S" Key is pressed
        private void ImageGalleryEnterPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    OnSearchClicked(searchImagePictureBox, null);
                    break;
            }
        }

        //Check Internet Connection
        public bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        private void PanelPaint(object sender, PaintEventArgs e)
        {
            Rectangle r = searchBox.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);

        }

        private void OnTextChanged(object sender, EventArgs e)
        {

        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            statusLabel.Visible = false;
            statusStrip.Visible = true;
            if (IsConnectedToInternet())
            {
                imagesList = await datafetch.GetImageData(searchBox.Text, this);
            }
            else
            {
                imagesList = datafetch.GetSampleData();
                MessageBox.Show(Properties.Resources.no_internet, Properties.Resources.dialog_okay);
            }
            if (imagesList.Count > 0)
            {
                group1.Visible = true;
                checkedItems = 0;
                exportImage.Visible = false;
            }
            else
            {
                statusLabel.Visible = true;
                statusLabel.Text = Properties.Resources.no_images_found;
            }
            AddTiles(imagesList);
            statusStrip.Visible = false;
        }

        private void AddTiles(List<ImageItem> imageList)
        {
            imageTileControl.Groups[0].Tiles.Clear();
            Console.WriteLine(imagesList.Count);
            foreach (var imageitem in imageList)
            {
                Tile tile = new Tile();
                tile.HorizontalSize = 2;
                tile.VerticalSize = 2;
                imageTileControl.Groups[0].Tiles.Add(tile);
                Image img = Image.FromStream(new MemoryStream(imageitem.Base64));
                Template tl = new Template();
                C1.Win.C1Tile.ImageElement ie = new C1.Win.C1Tile.ImageElement();
                ie.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(ie);
                tile.Template = tl;
                tile.Image = img;
            }
        }

        private void OnTileChecked(object sender, TileEventArgs e)
        {
            checkedItems++;
            exportImage.Visible = true;
        }
        private void OnTileUnchecked(object sender, TileEventArgs e)
        {
            Console.WriteLine(checkedItems);
            checkedItems--;
            exportImage.Visible = checkedItems > 0;
        }

        private void OnTileControlPaint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }

        private void OnExportClick(object sender, EventArgs e)
        {
            C1PdfDocument imageToPdf = new C1PdfDocument();
            List<Image> images = new List<Image>();
            foreach (Tile tile in imageTileControl.Groups[0].Tiles)
            {
                if (tile.Checked)
                {
                    images.Add(tile.Image);
                }
            }
            if (images.Count <= 0)
            {
                MessageBox.Show(Properties.Resources.no_images_selected, Properties.Resources.dialog_alert);
                return;
            }
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "pdf";
            saveFile.Filter = "PDF files (*.pdf)|*.pdf*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ConvertToPdf(images, imageToPdf);
                    Console.WriteLine(saveFile.FileName);
                    imageToPdf.Save(saveFile.FileName);
                    DialogResult dialogResult = MessageBox.Show(saveFile.FileName + " " + Properties.Resources.saved_successfully, Properties.Resources.dialog_success, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveFile.FileName);
                    }
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.StackTrace);
                    MessageBox.Show(e1.Message, Properties.Resources.dialog_error);
                }
            }
            else
            {
                Console.WriteLine("Operation Cancelled");
            }
        }

        private void ConvertToPdf(List<Image> images, C1PdfDocument imageToPdf)
        {
            RectangleF rect = imageToPdf.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imageToPdf.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                try
                {
                    imageToPdf.DrawImage(selectedimg, rect);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Properties.Resources.dialog_error);
                }
            }
        }

        private void ExportImagePaint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(exportImage.Location.X, exportImage.Location.Y, exportImage.Width, exportImage.Height);
            r.X -= 29;
            r.Y -= 3;
            r.Width--;
            r.Height--;
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);
            e.Graphics.DrawLine(p, new Point(0, 43), new Point(this.Width, 43));

        }

        // Inform User of Exception
        public void Status(bool b, String s)
        {
            if (!b)
            {
                MessageBox.Show(Properties.Resources.dialog_error + ": " + s + "\r\n" + Properties.Resources.sample_text);
            }
        }
    }
}
