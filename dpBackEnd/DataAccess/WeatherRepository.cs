using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DataAccess
{
    public class WeatherRepository : ConnectionClass
    {

        public void InsertLatestWeatherUpdate(Weather w)
        {
            Entity.Weathers.Add(w);
            Entity.SaveChanges();
        }

        public Weather getLatestWeatherFromDb()
        {
            return Entity.Weathers.OrderByDescending(w => w.TimeStamp).FirstOrDefault();
        }


    }
}
