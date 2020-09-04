using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Image_Galery_Demo
{
    class DataFetcher
    {

        async Task<string> GetDataFromService(string searchstring)
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
            }

            return readText;
        }

        public async Task<List<ImageItem>> GetImageData(string search)
        {
            string data = await GetDataFromService(search);
            return JsonConvert.DeserializeObject<List<ImageItem>>(data);
        }

    }
}
