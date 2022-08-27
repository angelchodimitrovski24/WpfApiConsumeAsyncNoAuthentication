using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BackEnd.Models
{
    public class Processor
    {
        public static async Task<Model> LoadRecord (int recordNumber = 0)
        {
            string url = "";

            if (recordNumber > 0)
            {
                url = $"https://xkcd.com/{recordNumber}/info.0.json";
            } else
            {
                url = $"https://xkcd.com/info.0.json";
            }

            using (HttpResponseMessage response = await ApiConfig.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Model model = await response.Content.ReadAsAsync<Model>();

                    
                    return model;

                } else
                {
                    throw new Exception(response.ReasonPhrase);
                }

            }
        }
    }
}
