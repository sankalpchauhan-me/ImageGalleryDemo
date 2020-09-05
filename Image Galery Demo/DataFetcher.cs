using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Image_Galery_Demo
{
    class DataFetcher
    {

        async Task<string> GetDataFromService(string searchstring, IStatus iStatus)
        {
            string readText = null;
            try
            {
                var azure = @"https://imagefetcher20200529182038.azurewebsites.net";
                string url = azure + @"/api/fetch_images?query=" + searchstring + "&max_count=10";

                using (HttpClient c = new HttpClient())
                {
                    readText = await c.GetStringAsync(url);
                }
            }
            catch
            {
                //readText = File.ReadAllText()
                Console.WriteLine("Error Occured");
                readText = File.ReadAllText(@"Data/sampleData.json");
                iStatus.Status(false);
                
            }

            return readText;
        }

        public async Task<List<ImageItem>> GetImageData(string search, IStatus iStatus)
        {
            string data = await GetDataFromService(search, iStatus);
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }

        public List<ImageItem> GetSampleData()
        {
            string data = File.ReadAllText(@"Data/sampleData.json");
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }

        public interface IStatus{
            void Status(bool b);
        }

    }
}
