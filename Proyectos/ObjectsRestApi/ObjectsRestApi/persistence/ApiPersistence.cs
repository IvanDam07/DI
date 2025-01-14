using Newtonsoft.Json;
using ObjectsRestApi.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ObjectsRestApi.persistence
{
    internal class ApiPersistence
    {
        public List<Api> ApiList { get; set; }
        private string ApiURL;

        public ApiPersistence()
        {
            ApiList = new List<Api>();
            ApiURL = "https://api.restful-api.dev/objects";
        }

        public async Task<List<Api>> GetObjectAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ApiURL);

                    if(response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<List<Api>>(jsonResponse);
                    }
                    else
                    {
                        throw new Exception($"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los datos de la API", ex);
            }
        }

        public async Task<Api> AddObjectAsync(Api newObject)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonPayload = JsonConvert.SerializeObject(newObject);
                    StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(ApiURL, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Api>(jsonResponse);
                    }
                    else
                    {
                        throw new Exception($"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al añadir un nuevo objeto a la API", ex);
            }
        }
    }
}
