using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Utils
{
    public class HttpTools
    {
        public static async Task<string> HttpGetAsync(string uriString, Dictionary<string, string> headers)
        {
            using (HttpClient client = new HttpClient())
            {
                AddHeaders(client, headers);
                return await client.GetStringAsync(uriString).ConfigureAwait(false);
            }
        }

        public static async Task<string> HttpPostAsync(string uri, Dictionary<string, string> headers,
            Dictionary<string, string> values)
        {
            using (HttpClient client = new HttpClient())
            {
                AddHeaders(client, headers);
                FormUrlEncodedContent content = new FormUrlEncodedContent(values);
                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
        }

        public static void AddHeaders(HttpClient client, Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        public static async Task<byte[]> HttpGetBytesAsync(string uriString, Dictionary<string, string> headers)
        {
            using (HttpClient client = new HttpClient())
            {
                AddHeaders(client, headers);
                return await client.GetByteArrayAsync(uriString).ConfigureAwait(false);
            }
        }

    }
}
