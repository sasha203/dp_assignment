using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using DataAccess;

namespace dpBackEnd.Controllers
{
    public class EmailController : ApiController
    {

        public static IRestResponse TestMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", ConfigurationManager.AppSettings["emailApiKey"]);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandboxb1c1293fab5847f0839a9ade8e23edb5.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxb1c1293fab5847f0839a9ade8e23edb5.mailgun.org>");
            request.AddParameter("to", "sashaattard94@gmail.com");
            request.AddParameter("subject", "nextGenAPpz");
            request.AddParameter("text", "testing once again.");
            request.Method = Method.POST;
            return client.Execute(request);
        }

        [HttpPost, Route("api/requestEmail")]
        public IRestResponse AddRequest(string[] userInfo)
        {
            if (userInfo != null)
            {
                Weather currentWeather;
                
                if (userInfo.Length > 1)
                {
                    UsersRepository ur = new UsersRepository();
                    WeatherController wc = new WeatherController();
                    RequestController rc = new RequestController();
                    string email;

                    email = userInfo[userInfo.Length - 1];

                    if (email != null)
                    {
                        if (email != "")
                        {
                            ur.setEmailRequestToTrue(email);
                        }
                        else
                        {
                            return null;
                        }


                        currentWeather = wc.GetRequestedInformation(userInfo);
                        if (currentWeather.Equals(null))
                        {
                            return null;
                        }
                        else
                        {
                            //Create Request according to currentWeather
                            Request r = new Request();
                            r.Id = Guid.NewGuid();
                            r.TimeStamp = DateTime.Now;
                            r.Email = email;
                            r.Desc = currentWeather.Desc;
                            r.Temperature = currentWeather.Temperature;
                            r.Humidity = currentWeather.Humidity;
                            r.WindSpeed = currentWeather.WindSpeed;
                            r.WindDirectionInDegrees = currentWeather.WindDirectionInDegrees;

                            rc.addRequest(r);
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;
        }


        public IRestResponse SendEmail(Request latestRequest)
        {

            string message = "\0";
            message = "Requested weather data: \n";

            if (latestRequest.Desc != null)
            {
                message += "Description: " + latestRequest.Desc + "\n";
            }

            if (latestRequest.Temperature != null)
            {
                message += "temperature: " + latestRequest.Temperature + "C\n";
            }

            if (latestRequest.Humidity != null)
            {
                message += "Humidity: " + latestRequest.Humidity + "%\n";
            }

            if (latestRequest.WindSpeed != null)
            {
                message += "WindSpeed: " + latestRequest.WindSpeed + "km/h\n";
            }

            if (latestRequest.WindDirectionInDegrees != null)
            {
                message += "WindDirectionInDegrees: " + latestRequest.WindDirectionInDegrees + "°\n";
            }

            message += "\n\nThanks for your support,\nRegards,\nMyWeather management.";



            if (latestRequest != null)
            {
                RestClient client = new RestClient();
                client.BaseUrl = new Uri("https://api.mailgun.net/v3");
                client.Authenticator = new HttpBasicAuthenticator("api", ConfigurationManager.AppSettings["emailApiKey"]);
                RestRequest request = new RestRequest();
                request.AddParameter("domain", "sandboxb1c1293fab5847f0839a9ade8e23edb5.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxb1c1293fab5847f0839a9ade8e23edb5.mailgun.org>");
                if (latestRequest.Email != null)
                {
                    request.AddParameter("to", latestRequest.Email);
                }
                else
                {
                    return null;
                }
                request.AddParameter("subject", "Weather Updates");
                request.AddParameter("text", message);
                request.Method = Method.POST;
                return client.Execute(request);
            }


            return null;
        }
            
        



            

            
    }
}
    

