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
using System.Windows.Forms;
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

        DataFetcher datafetch = new DataFetcher();
        List<ImageItem> imagesList;
        int checkedItems = 0;

        [DllImport("wininet.dll")]
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

        public void InitUi()
        {
            //init Search EditText
            searchBox = new TextBox();
            searchBox.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right);
            searchBox.BorderStyle = BorderStyle.None;
            searchBox.Cursor = Cursors.IBeam;
            searchBox.Location = new Point(44, 9);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(235, 13);
            searchBox.TabIndex = 0;
            searchBox.Text = "Search Image";
            searchBox.TextChanged += new EventHandler(textBox1_TextChanged);
            panel1.Controls.Add(searchBox);
            //init Search Picture box

            //init init Border

            //init C1Tile

            //init Status Bar

        }

        // Perform Search if Enter Key is pressed
        private void ImageGalleryEnterPressed(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    _search_Click(this.pictureBox1, null);
                    break;
            }
        }

        //Check Internet Connection
        public bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = searchBox.Bounds;
            r.Inflate(3, 3);
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawRectangle(p, r);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void _search_Click(object sender, EventArgs e)
        {
            
                statusLabel.Visible = false;
                statusStrip1.Visible = true;
                if (IsConnectedToInternet()){
                    imagesList = await datafetch.GetImageData(searchBox.Text, this);
                }
                else{
                    imagesList = datafetch.GetSampleData();
                    MessageBox.Show("Oops! Unable to connect to Internet Please Check Your Connection. \r\n Meanwhile Enjoy some sample images", "Okay");
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
                    statusLabel.Text = "No Images Found for this search";
                }
                AddTiles(imagesList);
                statusStrip1.Visible = false;
            

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
            if (images.Count <= 0) {
                MessageBox.Show("No Images Selected", "Alert");
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
                }
                catch (Exception e1)
                {
                    Console.WriteLine(e1.StackTrace);
                    MessageBox.Show(e1.Message, "Error");
                }

            }
            else{
                Console.WriteLine("Test");
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
                }catch (Exception e){
                    MessageBox.Show(e.Message, "Error");
                }
            }
        }

        private void _exportImage_Paint(object sender, PaintEventArgs e)
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

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            statusStrip1.Visible = true;
            imagesList = await datafetch.GetImageData(searchBox.Text, this);
            AddTiles(imagesList);
            statusStrip1.Visible = false;
        }

        // Inform USer of Exception
        public void Status(bool b, String s)
        {
            if (!b){
                MessageBox.Show("Error: "+s+"\r\nMeanwhile look at these sample images.");
            }
        }
    }
}
