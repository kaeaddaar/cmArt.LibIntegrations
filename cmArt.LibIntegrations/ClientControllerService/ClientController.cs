using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace cmArt.LibIntegrations.ClientControllerService
{
    public class ClientController<T, Ts> : IClientControllerGeneric<T, Ts> where T : IXRef<Guid>, new() where Ts : new()
    {
        private static HttpClient http = new HttpClient();
        //private static string DefaultBaseAddress = "https://azurefunctionsapitimereventtracker.azurewebsites.net";
        private string? _ControllerRoute;
        private string? _FunctionKey;
        private bool _BaseAddressSet;

        private T obj;
        private Ts objData;

        public ClientController()
        {
            _ControllerRoute = null;
            _FunctionKey = null;
            _BaseAddressSet = false;
        }
        public ClientController(Uri BaseAddressUri)
        {
            _ControllerRoute = null;
            _FunctionKey = null;
            http.BaseAddress = BaseAddressUri;
            _BaseAddressSet = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ControllerRoute">ex: "/api/firefighter/" based on "/api/firefighter/{idIn?}" route for function</param>
        /// <param name="FunctionKey">ex: "code=epcME6Rx6kQ8e4qkT3FjSi24IeaAtA181FJvjXdPVQ7aF2rgL5jaJA=="</param>
        public ClientController(string ControllerRoute, string FunctionKey, Uri BaseAddressUri)
        {
            _ControllerRoute = ControllerRoute;
            _FunctionKey = FunctionKey;
            http.BaseAddress = BaseAddressUri;
            _BaseAddressSet = true;
        }

        public string ControllerRoute
        {
            get { return _ControllerRoute; }
            set { _ControllerRoute = value; }
        }
        public string FunctionKey
        {
            get { return _FunctionKey; }
            set { _FunctionKey = value; }
        }
        public void SetBaseAddress(Uri BaseAddressUri)
        {
            http.BaseAddress = BaseAddressUri;
            _BaseAddressSet = true;
        }
        private void ThrowErrIfRouteAndKeyNotSupplied()
        {
            bool Missing =
                string.IsNullOrEmpty(_ControllerRoute)
                || string.IsNullOrEmpty(_FunctionKey)
                || !_BaseAddressSet;
            if (Missing)
            {
                throw new ArgumentNullException("Controller Route and Function Key required");
            }
        }
        public async Task<T> Get(string idGuid)
        {
            Guid objID;
            Guid.TryParse(idGuid, out objID);
            T obj;
            obj = await Get(objID: objID);
            return obj;
        }
        public async Task<T> Get(Guid objID)
        {
            string response;
            string strID = HttpUtility.UrlEncode(objID.ToString());
            try
            {
                response = await http.GetStringAsync
                (
                    _ControllerRoute
                    + strID
                    + "?" + _FunctionKey
                );
            }
            catch (Exception e)
            {
                response = $"{{ \"Error processing http.GetStringAsync in Get to {_ControllerRoute} for {strID} using function key "
                    + $"{_FunctionKey}\": \"{e.Message}\"}}";
                Console.WriteLine(response);
                return new T();
            }

            try
            {
                T obj = JsonSerializer.Deserialize<T>(response);
                return obj;
            }
            catch (Exception e)
            {
                response = $"{{ \"Error processing http.GetStringAsync in Get to {_ControllerRoute} for {strID} using function key "
                    + $"{_FunctionKey}\": \"{e.Message}\"}}";
                Console.WriteLine(response);
                return new T();
            }
        }
        public async Task<string> Add(string idGuid, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded)
        {
            Guid objId;
            Guid.TryParse(idGuid, out objId);
            return await Add(objId: objId, objToAdd: objToAdd, QueryStringArgs_NotEncoded);
        }
        public async Task<string> Add(Guid objId, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded)
        {
            ThrowErrIfRouteAndKeyNotSupplied();
            Dictionary<string, string> _QueryStringArgs_NotEncoded =
                QueryStringArgs_NotEncoded ?? new Dictionary<string, string>();

            string xx = string.Empty;

            try
            {
                xx = JsonSerializer.Serialize<Ts>(objToAdd);
            }
            catch (Exception e)
            {
                string errormessage = e.Message;
            }
            StringContent strFF = new StringContent(xx);

            string QueryString = string.Empty;
            foreach (var arg in _QueryStringArgs_NotEncoded)
            {
                QueryString += $"&{arg.Key}={HttpUtility.UrlEncode(arg.Value)}";
            }
            Console.WriteLine($"ClientControllerGeneric.QueryString on call to {_ControllerRoute} is: {QueryString}");

            HttpResponseMessage resp = null;
            string response;
            try
            {
                resp = await http.PostAsJsonAsync<Ts>
                (
                    _ControllerRoute
                    + objId.ToString()
                    + "?" + _FunctionKey
                    + QueryString +
                    "&AllowUpdate=true"
                    , objToAdd
                );
                response = await resp.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                response = System.Text.Json.JsonSerializer.Serialize<Exception>(e);
            }

            Console.WriteLine("response: " + response);

            return await ResponseOrError(response);
        }
        public async Task<string> Update(string idGuid, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded)
        {
            Guid objId;
            Guid.TryParse(idGuid, out objId);
            return await Add(objId: objId, objToAdd: objToAdd, QueryStringArgs_NotEncoded);
        }
        public async Task<string> Update(Guid objId, Ts objToAdd, Dictionary<string, string> QueryStringArgs_NotEncoded)
        {
            ThrowErrIfRouteAndKeyNotSupplied();
            Dictionary<string, string> _QueryStringArgs_NotEncoded =
                QueryStringArgs_NotEncoded ?? new Dictionary<string, string>();
            string xx = string.Empty;

            try
            {
                xx = JsonSerializer.Serialize<Ts>(objToAdd);
            }
            catch (Exception e)
            {
                string errormessage = e.Message;
            }
            StringContent strFF = new StringContent(xx);

            string QueryString = string.Empty;
            foreach (var arg in _QueryStringArgs_NotEncoded)
            {
                QueryString += $"&{arg.Key}={HttpUtility.UrlEncode(arg.Value)}";
            }

            string response;
            HttpResponseMessage resp = null;
            try
            {
                resp = await http.PutAsJsonAsync<Ts>
                (
                    _ControllerRoute
                    + objId.ToString()
                    + "?" + _FunctionKey
                    + QueryString +
                    "&AllowUpdate=true"
                    , objToAdd
                );
                response = await resp.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                response = System.Text.Json.JsonSerializer.Serialize<Exception>(e);
            }
            Console.WriteLine("response: " + response);

            return await ResponseOrError(response);
        }
        public async Task<string> Delete(string idGuid)
        {
            Guid objId;
            Guid.TryParse(idGuid, out objId);
            string response = await Delete(objId: objId);
            return response;
        }
        public async Task<string> Delete(Guid objId)
        {
            ThrowErrIfRouteAndKeyNotSupplied();

            string response;
            HttpResponseMessage Resp = null;
            try
            {
                Resp = await http.DeleteAsync
                (
                    _ControllerRoute
                    + objId.ToString()
                    + "?" + _FunctionKey
                );

                response = await Resp.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                response = System.Text.Json.JsonSerializer.Serialize<Exception>(e);
            }
            Console.WriteLine("response: " + response);

            return await ResponseOrError(response);
        }
        private async Task<string> ResponseOrError(string response)
        {
            // if deserialization into an exception fails return the response, 
            //  otherwize re-throw the error from the API
            bool responseIsError = false;
            try
            {
                T tmpObj = System.Text.Json.JsonSerializer.Deserialize<T>(response);
                if (tmpObj.GetPrimraryKey() == Guid.Empty)
                {
                    responseIsError = true;
                }
            }
            catch
            {
                responseIsError = true;
            }
            Exception reThrow = null;
            if (responseIsError)
            {
                try
                {
                    reThrow = System.Text.Json.JsonSerializer.Deserialize<Exception>(response);
                }
                catch (Exception e)
                {
                    throw new Exception($"Failed to Deserialize Error: {e}");
                }
                throw reThrow;
            }
            else
            {
                return response;
            }

        }
    }

}
