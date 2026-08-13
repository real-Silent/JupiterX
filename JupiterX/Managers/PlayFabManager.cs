using System.Net;
using System;
using JupiterX.Notifications;
using Valve.Newtonsoft.Json;
using System.Text;
using Valve.Newtonsoft.Json.Linq;

namespace JupiterX.Managers
{
    public class PlayFabManager
    {
        public static void CreateAccount(CreateAccountRequest caRequest, Action<CreateAccountResponse> callback)
        {
            if (caRequest == null)
                NotificationManager.SendNotification("Your createaccountrequest is null somehow im a dumbass.", 2f);
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/json");
            string url = $"https://{caRequest.TitleId}.playfabapi.com/Client/LoginWithCustomID";
            string json = JsonConvert.SerializeObject(caRequest);
            byte[] requestBytes = Encoding.UTF8.GetBytes(json);
            byte[] responseBytes = client.UploadData(url, "POST", requestBytes);
            string responseJson = Encoding.UTF8.GetString(responseBytes);
            JObject responseData = JObject.Parse(responseJson);
            CreateAccountResponse response = new CreateAccountResponse
            {
                PlayFabId = (string)responseData["data"]["PlayFabId"],
                SessionTicket = (string)responseData["data"]["SessionTicket"],
                EntityId = (string)responseData["data"]["EntityToken"]["Entity"]["Id"],
                EntityToken = (string)responseData["data"]["EntityToken"]["EntityToken"],
                EntityType = (string)responseData["data"]["EntityToken"]["Entity"]["Type"]
            };
            callback?.Invoke(response);
        }

        public class CreateAccountRequest
        {
            public string TitleId;
            public bool CreateAccount;
            public string CustomId;
        }
        public class CreateAccountResponse
        {
            public string PlayFabId;
            public string SessionTicket;
            public string EntityId;
            public string EntityToken;
            public string EntityType;
        }
    }
}