using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

public enum AuthenticationStatus { None, Ok, Invalid, Failed }

public static class ASPNetAPIHelper 
{
    static public string BaseWebAddress = "https://localhost:44310/";
    static public string GameToken = "";
    static public AuthenticationStatus StoreStatus = AuthenticationStatus.None;
    static public string ErrorMessage = "";

    static public bool Login(string username, string password)
    {

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var content = new FormUrlEncodedContent(
                new[]
                {
                        new KeyValuePair<string, string>("grant_type","password"),
                        new KeyValuePair<string, string>("username",username),
                        new KeyValuePair<string, string>("password",password),
                });

            var result = client.PostAsync(BaseWebAddress + "Token", content).Result;
            
            try
            {
                var resultContent = result.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                string ServerError = string.Empty;

                if (!String.IsNullOrEmpty(resultContent.access_token))
                {
                    Debug.Log(resultContent.access_token);
                    GameToken = resultContent.access_token;
                    StoreStatus = AuthenticationStatus.Ok;
                    return true;
                }
                else
                {
                    GameToken = "Invalid Login";
                    StoreStatus = AuthenticationStatus.Invalid;
                    ErrorMessage = "Invalid Credentials";
                    Debug.Log("Invalid Credentials");
                    return false;
                }

            }
            catch (Exception exception)
            {
                StoreStatus = AuthenticationStatus.Failed;
                GameToken = "Server Error -> " + exception.Message;
                Debug.Log(exception.Message);
                return false;
            }

        }
    }

    static public bool RegisterUser(UserDTO userDTO)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.PostAsJsonAsync(BaseWebAddress + "api/Account/Register", userDTO).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            ErrorMessage = response.Content.ReadAsStringAsync().Result;

            if (ErrorMessage.Contains("Username already exists."))
                ErrorMessage = "Username already exists.";
            else if (ErrorMessage.Contains("The Password must be at least"))
                ErrorMessage = "Password must be at least 6 characters.";
            else
                ErrorMessage = "Unexpected error! Please contact support.";

            return false;
        }
    }
}


