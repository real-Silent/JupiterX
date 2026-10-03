// ============================================================
//  JupiterX | Managers/PlayFabManager.cs
//  Copyright (c) 2026 Jupiterx (@NAuth). All rights reserved.
//
//  This software and its source code are the property of the
//  author. Unauthorized copying, redistribution, modification,
//  or reuse of any part of this project, in whole or in part,
//  without express written permission is strictly prohibited.
//
//  Version: 2.0.0 | By Silent/Ashley/Nova (@s1lnt)
// ============================================================

using Il2CppSystem.Net;
using System;
using JupiterX.Notifications;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

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