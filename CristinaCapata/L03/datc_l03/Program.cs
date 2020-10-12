using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;

namespace datc_l03
{
    class Program
    {
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "DATC lab3";
        static DriveService service;
        static UserCredential credential;
        static void Main(string[] args)
        {
            Initialize();
            GetAllFiles();
        }

        static void Initialize()
        {

            using (var stream =
                new FileStream("client_secret_895548607176-vs08lug4mc0r3g6u2k4uhnn1viggk16u.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
        static void GetAllFiles()
        {
            var request=(HttpWebRequest) WebRequest.Create("https://www.googleapis.com/drive/v3/files?q='root'%20in%20parents");
            request.Headers.Add(HttpRequestHeader.Authorization,"Bearer "+ credential.Token.AccessToken);
            
            using (var response =request.GetResponse())
            {
                using (Stream data = response.GetResponseStream())
                using (var reader = new StreamReader(data))
                {
                    string text =reader.ReadToEnd();
                    var myData=JObject.Parse(text);
                    foreach (var file in myData["files"])
                    {
                        if (file["mimeType"].ToString()!="application/vnd.google-apps.folder")
                        {
                            Console.WriteLine("File name: "+file["name"]);
                        }
                    }
                }
            }

        }
    }
}
