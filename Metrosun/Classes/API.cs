// Reused from CobaltSky (now with Newtonsoft.Json!)
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System;

namespace Metrosun.Classes
{
    class API
    {
        private string APIEndpoint = "https://api.openweathermap.org";
        private string APIKey = SettingsMgr.APIKey;

        private static readonly HttpClient client = new HttpClient();

        public async Task SendAPI(string owmEndpoint, string httpMethod, object jsonObj, Action<string> callback, Dictionary<string, string> Headers = null)
        {
            try
            {
                var request = new HttpRequestMessage(new HttpMethod(httpMethod.ToUpperInvariant()), APIEndpoint + owmEndpoint);

                if (Headers != null)
                {
                    foreach (var header in Headers)
                    {
                        request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                if (httpMethod.ToUpperInvariant() == "POST" && jsonObj != null)
                {
                    string jsonBody = JsonConvert.SerializeObject(jsonObj);
                    request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                }

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("[API] Request is successful!");
                    callback(responseBody);
                }
                else
                {
                    callback($"[API] Error: {response.StatusCode} | {responseBody}");
                }
            }
            catch (Exception ex)
            {
                callback($"[API] Error: {ex.Message}");
            }
        }
    }
}