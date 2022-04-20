using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace cmArt.LibIntegrations.ApiCallerService
{
    public class ApiCallerBase
    {
        public ApiConnectorData _ApiConnectorData { get; set; }
        public ApiCallerBase()
        {
            init(new ApiConnectorData());
        }
        public void init(ApiConnectorData data)
        {
            _ApiConnectorData = data ?? new ApiConnectorData();
        }
        private string MakeApiCall(HttpClient client, HttpRequestMessage requestMessage, int maxAttempts)
        {
            int attempts = 0;
            int _maxAttempts = maxAttempts;
            string messages = string.Empty;
            while (attempts < _maxAttempts)
            {
                try
                {
                    attempts++;
                    var task = client.SendAsync(requestMessage);
                    var response = task.Result;
                    //response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result ?? string.Empty;
                    return responseBody;
                }
                catch (Exception e)
                {
                    System.Threading.Thread.Sleep(100 * attempts);
                    // try again. but maybe do some logging
                    messages += e.Message + Environment.NewLine;
                }
            }
            return $"Quitting after {_maxAttempts} tries to get MakeApiCall. Messages: {messages}. " +
                $"RequestUri: {requestMessage.RequestUri}. Content: {requestMessage.Content}. Method: {requestMessage.Method}";
        }
        private async Task<string> MakeApiCallAsync(HttpClient client, HttpRequestMessage requestMessage, int maxAttempts)
        {
            int attempts = 0;
            int _maxAttempts = maxAttempts;
            string messages = string.Empty;
            while (attempts < _maxAttempts)
            {
                try
                {
                    attempts++;
                    var response = await client.SendAsync(requestMessage);
                    //response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result ?? string.Empty;
                    return responseBody;
                }
                catch (Exception e)
                {
                    System.Threading.Thread.Sleep(100 * attempts);
                    // try again. but maybe do some logging
                    messages += e.Message + Environment.NewLine;
                }
            }
            return $"Quitting after {_maxAttempts} tries to get MakeApiCall. Messages: {messages}. " +
                $"RequestUri: {requestMessage.RequestUri}. Content: {requestMessage.Content}. Method: {requestMessage.Method}";
        }
        protected async Task<string> MakeApiPostCallAsync(ApiCallData data, Func<string, int> MakeLogEntry)
        {
            string urlCommand = data.UrlCommand;
            string content = data.Body;
            try
            {
                MakeLogEntry("urlCommand: " + urlCommand);
                MakeLogEntry("content: " + content);
            }
            catch (Exception e)
            {
                return "Error in MapApiPostCall_Unsecured while trying to MakeLogEntry. Message: " + e.Message;
            }

            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(_ApiConnectorData.Url + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = _ApiConnectorData.UserName;
            string clientSecret = _ApiConnectorData.Password;

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);
            requestMessage.Content = new StringContent(content);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Headers.Add("args", data.Args);

            string responseBody = await MakeApiCallAsync(client, requestMessage, 10);
            ////make the request
            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            ////response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result ?? string.Empty;

            return responseBody;
        }
        protected string MakeApiPostCall(ApiCallData data, Func<string, int> MakeLogEntry)
        {
            string urlCommand = data.UrlCommand;
            string content = data.Body;
            try
            {
                MakeLogEntry("urlCommand: " + urlCommand);
                MakeLogEntry("content: " + content);
            }
            catch (Exception e)
            {
                return "Error in MapApiPostCall_Unsecured while trying to MakeLogEntry. Message: " + e.Message;
            }

            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(_ApiConnectorData.Url + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = _ApiConnectorData.UserName;
            string clientSecret = _ApiConnectorData.Password;

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);
            requestMessage.Content = new StringContent(content);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Headers.Add("args", data.Args);

            string responseBody = MakeApiCall(client, requestMessage, 10);
            ////make the request
            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            ////response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result ?? string.Empty;

            return responseBody;
        }
        protected string MapApiPostCall_Unsecured(ApiCallData data, Func<string, int> MakeLogEntry)
        {
            string urlCommand = data.UrlCommand;
            string content = data.Body;
            try
            {
                MakeLogEntry("urlCommand: " + urlCommand);
                MakeLogEntry("content: " + content);
            }
            catch (Exception e)
            {
                return "Error in MapApiPostCall_Unsecured while trying to MakeLogEntry. Message: " + e.Message;
            }
            string results = MakeApiPostCall_Unsecured(urlCommand, content);
            MakeLogEntry("results: " + results);
            return results;
        }
        protected string MakeApiPostCall_Unsecured(string urlCommand, string content)
        {
            Console.WriteLine("urlCommand: " + urlCommand);
            Console.WriteLine("content: " + content);
            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(_ApiConnectorData.Url + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //string clientId = "ed84bfc1c2687d7d6f357717fe977dd6";
            //string clientSecret = "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";

            //var authenticationString = $"{clientId}:{clientSecret}";
            //var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri);
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            requestMessage.Content = new StringContent(content);

            string responseBody = MakeApiCall(client, requestMessage, 10);
            ////make the request
            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            ////response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine("responseBody: " + responseBody);
            return responseBody;
        }

        protected string MakeApiGetCall_Unsecured(string urlCommand)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(10); // 1000 tics per second * 60 Seconds is a minute * 10 is 10 minutes

            Uri baseUri = new Uri(_ApiConnectorData.Url + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            //string clientId = "ed84bfc1c2687d7d6f357717fe977dd6";
            //string clientSecret = "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";

            //var authenticationString = $"{clientId}:{clientSecret}";
            //var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            ////requestMessage.Content = content;

            string responseBody = MakeApiCall(client, requestMessage, 10);
            ////make the request
            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            ////response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result;

            return responseBody;
        }
        public string MakeApiGetCall(string urlCommand)
        {
            HttpClient client = new HttpClient();

            Uri baseUri = new Uri(_ApiConnectorData.Url + urlCommand);
            client.BaseAddress = baseUri;
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.ConnectionClose = true;

            string clientId = _ApiConnectorData.UserName;
            string clientSecret = _ApiConnectorData.Password;

            var authenticationString = $"{clientId}:{clientSecret}";
            var base64EncodedAuthenticationString = 
                Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, baseUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            //requestMessage.Content = content;

            string responseBody = MakeApiCall(client, requestMessage, 10);
            ////make the request
            //var task = client.SendAsync(requestMessage);
            //var response = task.Result;
            ////response.EnsureSuccessStatusCode();
            //string responseBody = response.Content.ReadAsStringAsync().Result;

            return responseBody;
        }

    }
}
