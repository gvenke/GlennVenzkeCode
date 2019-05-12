using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace WeatherReporting
{
    public class WeatherReporter : IDisposable
    {
        private const string weatherApiUrl = "https://openweathermap.org/data/2.5/weather?units=imperial&appid=b6907d289e10d714a6e88b30761fae22&";
        private WebClient _webClient;
        private readonly static Lazy<WeatherReporter> _instance = new Lazy<WeatherReporter>(() => new WeatherReporter(GlobalHost.ConnectionManager.GetHubContext<WeatherReportingHub>().Clients));
        private readonly Timer _timer;
        private IHubConnectionContext<dynamic> _clients;
        private readonly TimeSpan _updateInterval = TimeSpan.FromMinutes(1);
        private readonly DataContractJsonSerializer _weatherSerializer;
        public ConcurrentDictionary<string, WeatherData> _clientWeatherData;
        private Mutex _mutex;

        private WeatherReporter(IHubConnectionContext<dynamic> clients)
        {
            _clients = clients;
            _timer = new Timer(BroadcastUpdatedWeatherData, null, _updateInterval, _updateInterval);
            _webClient = new WebClient();
            _weatherSerializer = new DataContractJsonSerializer(typeof(WeatherData));
            _clientWeatherData = new ConcurrentDictionary<string, WeatherData>();
            _mutex = new Mutex();
        }

        public WeatherData GetUpdatedWeatherData(string zipCode)
        {
            _mutex.WaitOne();
            
            JObject jo = JObject.Parse(GetWeatherJson(zipCode));
            var weatherData = new WeatherData {
                Description = (string)jo.SelectToken("weather[0].description"),
                CurrentTemperature = (string)jo.SelectToken("main.temp"),
                LowTemperature = (string)jo.SelectToken("main.temp_min"),
                HighTemperature = (string)jo.SelectToken("main.temp_max"),
                Humidity = (string)jo.SelectToken("main.humidity"),
                AirPressure = (string)jo.SelectToken("main.pressure")
            };
            // using (var weatherStream = new MemoryStream(Encoding.Unicode.GetBytes(GetWeatherJson(zipCode)))) {

            // weatherData = (WeatherData)_weatherSerializer.ReadObject(weatherStream);                
            //}
            _mutex.ReleaseMutex();
            return weatherData;
        }

        

        public WeatherData GetWeatherData(string zipCode)
        {
            WeatherData weatherData;
            _clientWeatherData.TryGetValue(zipCode, out weatherData);
            return weatherData;
        }

        private void BroadcastUpdatedWeatherData(object state)
        {
            WeatherData curData;
            foreach(var curZip in _clientWeatherData.Keys)
            {
                curData = GetUpdatedWeatherData(curZip);
                _clients.Group(curZip).updateWeatherData(curData);

            }
        }


        public WeatherData InitClient(string zipCode)
        {
            WeatherData weatherData;
            if (!_clientWeatherData.TryGetValue(zipCode, out weatherData)) {
                weatherData = GetUpdatedWeatherData(zipCode);
                _clientWeatherData.TryAdd(zipCode, weatherData);
            }
            
            return weatherData;
        }

        private string GetWeatherJson(string zipCode)
        {        
            
            string url = weatherApiUrl + $"zip={zipCode}";
            string json = _webClient.DownloadString(url);
            
            return json;

        }

        public static WeatherReporter Instance => _instance.Value;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _webClient.Dispose();
                    _mutex.Dispose();
                    _timer.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~WeatherReporter()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose();
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}