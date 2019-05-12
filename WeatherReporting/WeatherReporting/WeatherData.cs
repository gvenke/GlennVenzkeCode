using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace WeatherReporting
{

    public class WeatherData
    {
        public string Description { get; set;
        }

        public string CurrentTemperature { get; set; }

        public string Humidity { get; set; }

        public string AirPressure { get; set; }

    
        public string LowTemperature { get; set; }


        public string HighTemperature { get; set; }

    }
}