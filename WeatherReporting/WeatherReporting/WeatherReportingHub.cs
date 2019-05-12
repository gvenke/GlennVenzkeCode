using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace WeatherReporting
{
    public class WeatherReportingHub : Hub
    {
        private readonly WeatherReporter _weatherReporter;
        private WeatherReportingHub(WeatherReporter weatherReporter)
        {
            _weatherReporter = weatherReporter;
        }
        public WeatherReportingHub() : this(WeatherReporter.Instance) { }

        public WeatherData GetWeatherData(string zipCode)
        {
            return _weatherReporter.GetWeatherData(zipCode);
        }

        public WeatherData Init()
        {
            string zipCode = Clients.CallerState.zipCode;
            Groups.Add(Context.ConnectionId, zipCode);
            return _weatherReporter.InitClient(zipCode);
        }

        //public override Task OnConnected()
        //{
        //    return base.OnConnected();
        //}

        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    return base.OnDisconnected(stopCalled);
        //}
    }
}