using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherConcurrencyApp.Infrastructure.OpenWeatherClient;
using WeatherConcurrentApp.Domain.Entities;

namespace WeatherConcurrencyApp
{
    public partial class WeatherPanel : UserControl
    {
        public HttpOpenWeatherClient httpOpenWeatherClient;
        public OpenWeather openWeather;
        public WeatherPanel()
        {
            httpOpenWeatherClient = new HttpOpenWeatherClient();
            InitializeComponent();
        }

        private void WeatherPanel_Load(object sender, EventArgs e)
        {
            Task.Run(Request).Wait();
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            DateTime day1 = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();

            DetailsWeather detailsWeather = new DetailsWeather();
            detailsWeather.lblDetail.Text = "Details";
            detailsWeather.lblDetailValue.Text = openWeather.Weather[0].Description;
            flpContent.Controls.Add(detailsWeather);

            DetailsWeather detailsWeather1 = new DetailsWeather();
            detailsWeather1.lblDetail.Text = "amanecer";
            day = day.AddSeconds(openWeather.Sys.Sunrise).ToLocalTime();
            detailsWeather1.lblDetailValue.Text = day.ToShortTimeString();
            flpContent.Controls.Add(detailsWeather1);

            DetailsWeather detailsWeather2 = new DetailsWeather();
            detailsWeather2.lblDetail.Text = "atardecer";
            day1 = day1.AddSeconds(openWeather.Sys.Sunset).ToLocalTime();
            detailsWeather2.lblDetailValue.Text = day1.ToShortTimeString();
            flpContent.Controls.Add(detailsWeather2);

            DetailsWeather detailsWeather3 = new DetailsWeather();
            detailsWeather3.lblDetail.Text = "Wind Speed";
            detailsWeather3.lblDetailValue.Text = openWeather.Wind.Speed.ToString() + " Km/h";
            flpContent.Controls.Add(detailsWeather3);

            DetailsWeather detailsWeather4 = new DetailsWeather();
            detailsWeather4.lblDetail.Text = "Pressure";
            detailsWeather4.lblDetailValue.Text = openWeather.Main.Pressure.ToString();
            flpContent.Controls.Add(detailsWeather4);

            DetailsWeather detailsWeather5 = new DetailsWeather();
            detailsWeather5.lblDetail.Text = "Humidity";
            detailsWeather5.lblDetailValue.Text = openWeather.Main.Humidity.ToString() + "%";
            flpContent.Controls.Add(detailsWeather5);
        }

        public async Task Request()
        {
            openWeather = await httpOpenWeatherClient.GetWeatherByCityNameAsync(lblCity.Text);
        }
    }
}
