using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServerHandler.Services
{
    internal class HttpWorker
    {

        internal async Task<TResponse> PostByRequestAsync<TRequest, TResponse>(string url, TRequest request, Dictionary<string, string> headers) =>
           await GetResponseAsync<TResponse>(url, JsonConvert.SerializeObject(request), headers, HttpMethod.Post);

        internal async Task<TResponse> GetResponseAsync<TResponse>(string url,
                                                                 string content,
                                                                 Dictionary<string, string> headers,
                                                                 HttpMethod httpMethod)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Content = string.IsNullOrEmpty(content) ? null :
                                                          new StringContent(content, Encoding.UTF8, "application/json"),
            };
            foreach (var header in headers)
                request.Headers.Add(header.Key, header.Value);

            var response = await new HttpClient().SendAsync(request).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"response.StatusCode:{response.StatusCode} Content:{response.Content}");

            var result = default(TResponse);
            var jsonContent = await response.Content.ReadAsStringAsync();
            try
            {
                result = JsonConvert.DeserializeObject<TResponse>(jsonContent);
            }
            catch(Exception e)
            {
                throw new Exception($"Status Code: {response.StatusCode} Content: {jsonContent}");
            }

            return result;
        }
    }
}
