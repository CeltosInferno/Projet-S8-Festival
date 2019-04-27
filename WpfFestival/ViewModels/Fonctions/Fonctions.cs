using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WpfFestival.ViewModels.Fonctions
{
    public class Fonctions
    {
        public  static bool DeleteFestival(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.DeleteAsync(uri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool DeleteProgrammation(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.DeleteAsync(uri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool DeleteArtiste(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.DeleteAsync(uri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
        public static bool DeleteScene(string uri)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5575");
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.DeleteAsync(uri).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
    }

}
