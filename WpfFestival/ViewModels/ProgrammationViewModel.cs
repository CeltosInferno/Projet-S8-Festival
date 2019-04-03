using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using WpfFestival.Models.DTO;

namespace WpfFestival.ViewModels
{
    class ProgrammationViewModel
    {
        private ProgrammationNameDTO dto = new ProgrammationNameDTO();

       


        public List<ProgrammationNameDTO> GetNameFromAPI()
        {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/Programmations").Result;

                if (response.IsSuccessStatusCode)
                {

                    var readTask = response.Content.ReadAsAsync<List< ProgrammationNameDTO >>();
                    readTask.Wait();

                    return readTask.Result;

                }
            return null;

    
        }
    }
}
