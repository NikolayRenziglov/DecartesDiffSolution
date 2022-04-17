using System.Net;
using System.Text;
using System.Text.Json;

namespace BlazorDiffApp1.Services
{
    /// <summary>
    /// Service for the Blazor Server application to implement the 
    /// ApiClient
    /// </summary>
    public class DiffWebApiClient
    {
        private const string baseURL = "https://localhost:44391/";
        public string LastResult;

        public async Task<bool> SendBlob(int userId, byte[] blob, bool isRight)
        {
            //Sends the binary data for the userId to the server side
            LastResult=String.Empty;
            HttpClient httpClient = new HttpClient();
            string uri = String.Concat(baseURL, "v1/Diff/", userId, "/", isRight ? "right" : "left");
            //It saves the binary data for a given user
            using HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(uri),
                Headers =
                    {
                        { HttpRequestHeader.Accept.ToString(), "application/json" }
                    }
            };
            var data = new Dictionary<string, string>
            {
                {"dataString", Convert.ToBase64String(blob)},
            };
            using HttpResponseMessage httpResponse = await httpClient.PutAsync(uri, new FormUrlEncodedContent(data));
            LastResult = String.Concat((int)httpResponse.StatusCode," ", httpResponse.StatusCode);
            return httpResponse.IsSuccessStatusCode;
        }

        public async Task<string> Compare(int userId)
        {
            //Compares binary data for the userId and returns the Json
            //string of differences as Spec references
            LastResult = String.Empty;
            HttpClient httpClient = new HttpClient();
            string uri = String.Concat(baseURL, "v1/Diff/", userId);
            //It saves the binary data for a given user
            using HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(uri),
                Headers =
                    {
                        { HttpRequestHeader.Accept.ToString(), "application/json" }
                    }
            };

            using HttpResponseMessage httpResponse = await httpClient.GetAsync(uri);
            LastResult = await httpResponse.Content.ReadAsStringAsync();
            return LastResult;
        }
    }
}
