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

namespace Image_Galery_Demo
{

    /// <summary>
    /// Image Gallery class of form
    /// </summary>
    public partial class ImageGallery : Form, DataFetcher.IStatus
    {
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
            this.KeyUp += ImageGalleryEnterPressed;
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
            Rectangle r = _searchBox.Bounds;
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
                    imagesList = await datafetch.GetImageData(_searchBox.Text, this);
                }
                else{
                    imagesList = datafetch.GetSampleData();
                    MessageBox.Show("Oops! Unable to connect to Internet Please Check Your Connection. Meanwhile Enjoy some sample images", "Okay");
                }
                if (imagesList.Count > 0)
                {
                    group1.Visible = true;
                    checkedItems = 0;
                    _exportImage.Visible = false;
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
            _imageTileControl.Groups[0].Tiles.Clear();
            Console.WriteLine(imagesList.Count);
            foreach (var imageitem in imageList)
            {
                Tile tile = new Tile();
                tile.HorizontalSize = 2;
                tile.VerticalSize = 2;
                _imageTileControl.Groups[0].Tiles.Add(tile);
                Image img = Image.FromStream(new MemoryStream(imageitem.Base64));
                Template tl = new Template();
                ImageElement ie = new ImageElement();
                ie.ImageLayout = ForeImageLayout.Stretch;
                tl.Elements.Add(ie);
                tile.Template = tl;
                tile.Image = img;
            }
        }

        private void OnTileChecked(object sender, TileEventArgs e)
        {
            checkedItems++;
            _exportImage.Visible = true;
        }
        private void OnTileUnchecked(object sender, TileEventArgs e)
        {
            Console.WriteLine(checkedItems);
            checkedItems--;
            _exportImage.Visible = checkedItems > 0;
        }

        private void OnTileControlPaint(object sender, PaintEventArgs e)
        {
            Pen p = new Pen(Color.LightGray);
            e.Graphics.DrawLine(p, 0, 43, 800, 43);
        }

        private void OnExportClick(object sender, EventArgs e)
        {
            List<Image> images = new List<Image>();
            foreach (Tile tile in _imageTileControl.Groups[0].Tiles)
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
            ConvertToPdf(images);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "pdf";
            saveFile.Filter = "PDF files (*.pdf)|*.pdf*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    imagePdfDocument.Save(saveFile.FileName);
                }catch (Exception e1)
                {
                    MessageBox.Show(e1.Message, "Error");
                }
                
            }
        }

        private void ConvertToPdf(List<Image> images)
        {
            imagePdfDocument?.Clear();
            RectangleF rect = imagePdfDocument.PageRectangle;
            bool firstPage = true;
            foreach (var selectedimg in images)
            {
                if (!firstPage)
                {
                    imagePdfDocument.NewPage();
                }
                firstPage = false;
                rect.Inflate(-72, -72);
                try
                {
                    imagePdfDocument.DrawImage(selectedimg, rect);
                }catch (Exception e){
                    MessageBox.Show(e.Message, "Error");
                }
            }
        }

        private void _exportImage_Paint(object sender, PaintEventArgs e)
        {
            Rectangle r = new Rectangle(_exportImage.Location.X, _exportImage.Location.Y, _exportImage.Width, _exportImage.Height);
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
            imagesList = await datafetch.GetImageData(_searchBox.Text, this);
            AddTiles(imagesList);
            statusStrip1.Visible = false;
        }

        // Some Exception Occured Inform User
        public void Status(bool b)
        {
            if (!b){
                MessageBox.Show("OOPS! Something Went Wrong, Meanwhile enjoy these sample images.");
            }
        }
    }
}
