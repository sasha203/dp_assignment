using Common;
using DataAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace dpBackEnd.Controllers
{
    public class WeatherController : ApiController
    {
        //Should convert from degrees to a Wind direction
        public string GetWindDirection()
        {
            //Not mandatory to implement for criteria.
            return "Implement Me PLease";
        }


       
        public async Task AddWeatherInDbRecursively()
        {

            string url = "http://api.openweathermap.org/data/2.5/weather?q=Valletta&units=metric&appid=d12e761b653064784c9d028522c92fe9";
            HttpClient client = new HttpClient();
            string result;
            JObject currentWeather;

            while (true)
            {
                result = "\0";
                result = await client.GetStringAsync(url);  //waiting to receive required data asynchronously.

                //Storing json into JObject
                currentWeather = JObject.Parse(result);
                Weather w = new Weather();

                var weather = currentWeather["weather"] as JArray;
                foreach (JObject item in weather)
                {
                    w.Main = item["main"].Value<string>();
                    w.Desc = item["description"].Value<string>();
                }

                w.Temperature = currentWeather["main"]["temp"].Value<decimal>();
                w.Humidity = currentWeather["main"]["humidity"].Value<int>();

                w.WindSpeed = currentWeather["wind"]["speed"].Value<decimal>();
                w.WindDirectionInDegrees = currentWeather["wind"]["deg"].Value<int>(); //returns wind direction in degrees

                w.W_Id = Guid.NewGuid();
                w.TimeStamp = DateTime.Now;


                w.WindDirection = GetWindDirection(); //need to implement this to change from degrees to a wind direction.


                //addToDb
               WeatherRepository wr = new WeatherRepository();
                wr.InsertLatestWeatherUpdate(w);

                UsersController uc = new UsersController();
                RequestController rc = new RequestController();
                EmailController ec = new EmailController();
                List<User> users = uc.GetUsers();
                
                if (users != null)
                {
                    foreach (User u in users)
                    {
                        
                        Request latestRequest = rc.GetLatestRequestByEmail(u.Email);
                        if(latestRequest != null) {
                            ec.SendEmail(latestRequest);
                        }
                        
                    }
                }
               
                await Task.Delay(new TimeSpan(0, 1, 0)); 
            }
        }


        //getting data from db.
        [HttpGet, Route("api/latestWeather")]
        public IHttpActionResult GetLatestWeatherUpdate()
        {
            try
            {
                WeatherRepository wr = new WeatherRepository();
                return Ok(wr.getLatestWeatherFromDb());
            }
            catch (Exception e)
            {
                return InternalServerError(new Exception(e.Message));
            }
        }



        
        [HttpPost, Route("api/getRequestedInfo")]
        public Weather GetRequestedInformation(string[] userInfo)
        {

            if (userInfo != null)
            {
                WeatherRepository wr = new WeatherRepository();
                Weather latestWeather = wr.getLatestWeatherFromDb();
                string itemLowCase = "\0";

                if (userInfo.Length == 0 || userInfo.Length > 6)
                {
                    return null;
                }
                else
                {
                    Weather filteredWeather = new Weather();
                    filteredWeather.W_Id = latestWeather.W_Id;
                    filteredWeather.TimeStamp = latestWeather.TimeStamp;

                    foreach (string item in userInfo)
                    {
                        if (item != null)
                        {

                            itemLowCase = item.ToLower();

                            if (itemLowCase.Equals("description"))
                            {
                                filteredWeather.Desc = latestWeather.Desc;
                            }

                            else if (itemLowCase.Equals("temperature"))
                            {
                                filteredWeather.Temperature = latestWeather.Temperature;
                            }

                            else if (itemLowCase.Equals("humidity"))
                            {
                                filteredWeather.Humidity = latestWeather.Humidity;
                            }

                            else if (itemLowCase.Equals("wind speed"))
                            {
                                filteredWeather.WindSpeed = latestWeather.WindSpeed;
                            }

                            else if (itemLowCase.Equals("wind direction"))
                            {
                                filteredWeather.WindDirectionInDegrees = latestWeather.WindDirectionInDegrees;
                            }
                        }
                    }

                    return filteredWeather; //Ok(filteredWeather);
                    
                }


            }
            else {
                return null;
            }
        }

    }


}
