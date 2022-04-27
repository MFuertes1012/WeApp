using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherConcurrencyApp.Common;
using WeatherConcurrencyApp.Infrastructure.OpenWeatherClient;
using WeatherConcurrentApp.Domain.Entities;

namespace WeatherConcurrencyApp
{
    public partial class FrmMain : Form
    {
        public HttpOpenWeatherClient httpOpenWeatherClient;
        public OpenWeather openWeather;
        public FrmMain()
        {
            httpOpenWeatherClient = new HttpOpenWeatherClient();
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Run(Request).Wait();
                if(openWeather == null)
                {
                    throw new NullReferenceException("Fallo al obtener el objeto OpeWeather.");
                }
                
                WeatherPanel weatherPanel = new WeatherPanel();
                weatherPanel.lblCity.Text = txtCity.Text;
                weatherPanel.lblTemperature.Text = openWeather.Main.Temp.ToString() + "C";
                weatherPanel.lblWeather.Text = openWeather.Weather[0].Main;
                weatherPanel.picIcon.ImageLocation = $"{AppSettings.ApiIcon}" + openWeather.Weather[0].Icon + ".png";
                flpContent.Controls.Add(weatherPanel);
                
            }
            catch (Exception)
            {
                
            }
           
        }

        public async Task Request()
        {
            //txtCity es temporal para mientras hago lo otro
           openWeather = await httpOpenWeatherClient.GetWeatherByCityNameAsync(txtCity.Text);
        }
    }
}
