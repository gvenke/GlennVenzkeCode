
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WeatherReporting.Startup))]
namespace WeatherReporting
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
