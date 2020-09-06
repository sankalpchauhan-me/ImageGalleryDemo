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
        public interface IStatus
        {
            void Status(bool b, String s);
        }

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
                    Console.WriteLine(readText);
                }
            }
            catch (Exception e1)
            {
                //readText = File.ReadAllText()
                Console.WriteLine(e1.Message);
                readText = File.ReadAllText(@"Data/sampleData.json");
                //Pass the message to view, so that it can be displayed to user
                iStatus.Status(false, e1.Message);
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


    }
}
