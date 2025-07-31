using System;
using System.Collections.Generic;
using Microsoft.Phone.Controls;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Windows;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using Metrosun.Classes;

namespace Metrosun
{
    public partial class WeatherPage : PhoneApplicationPage
    {
        private string APIKey = SettingsMgr.APIKey;
        private List<weatherItem> weatherItems = new List<weatherItem>();

        public WeatherPage()
        {
            InitializeComponent();
            WeatherPivot.Title = SettingsMgr.LocationName;
            LoadWeather();
            Loaded += WeatherPage_Loaded;
        }

        private void WeatherPage_Loaded(object sender, RoutedEventArgs e)
        {
            // what a shitty hack icl
            for (int i = 0; i < 5; i++)
            {
                NavigationService.RemoveBackEntry();
            }
        }

        private async void LoadWeather()
        {
            var api = new Metrosun.Classes.API();
            await api.SendAPI($"/data/2.5/onecall?lat={SettingsMgr.Latitude}&lon={SettingsMgr.Longitude}&units=metric&exclude=daily,hourly,minutely,alerts&appid={APIKey}", "GET", null, (response) =>
            {
                Debug.WriteLine($"Response from OWM's API: {response}");
                var weather = JsonConvert.DeserializeObject<WeatherRep>(response);

                double roundedTemp = Math.Ceiling(weather.Current.Temp);
                double roundedLike = Math.Ceiling(weather.Current.FeelsLike);
                string capsDesc = Capitalize(weather.Current.Weather[0].Description);

                var desc = capsDesc.ToLower();

                if (desc.Contains("thunderstorm"))
                    WeatherBG.Source = new BitmapImage(new Uri("Images\\WBGs\\thunder.jpg", UriKind.RelativeOrAbsolute));
                else if (desc.Contains("drizzle") || desc.Contains("rain"))
                    WeatherBG.Source = new BitmapImage(new Uri("Images\\WBGs\\rainy-day.jpg", UriKind.RelativeOrAbsolute));
                else if (desc.Contains("snow"))
                    WeatherBG.Source = new BitmapImage(new Uri("Images\\WBGs\\snowy.jpg", UriKind.RelativeOrAbsolute));
                else if (desc.Contains("fog"))
                    WeatherBG.Source = new BitmapImage(new Uri("Images\\WBGs\\foggy.jpg", UriKind.RelativeOrAbsolute));
                else if (desc.Contains("clear"))
                    WeatherBG.Source = new BitmapImage(new Uri("Images\\WBGs\\sunny.jpg", UriKind.RelativeOrAbsolute));
                else if (desc.Contains("clouds"))
                    WeatherBG.Source = new BitmapImage(new Uri("Images\\WBGs\\cloudy.jpg", UriKind.RelativeOrAbsolute));

                WeatherTemp.Text = $"{roundedTemp}";
                WeatherDesc.Text = $"{capsDesc}";

                LoadDailyWeather();
            }, null);
        }

        // Note this is only for the UserControls in the current page
        public async void LoadDailyWeather()
        {
            var api = new Metrosun.Classes.API();
            await api.SendAPI($"/data/2.5/onecall?lat={SettingsMgr.Latitude}&lon={SettingsMgr.Longitude}&units=metric&exclude=current,hourly,minutely,alerts&appid={APIKey}", "GET", null, (response) =>
            {
                Debug.WriteLine($"Response from OWM's API: {response}");
                var weather = JsonConvert.DeserializeObject<WeatherRep>(response);

                // Main weather
                double roundedTemp = Math.Ceiling(weather.Daily[0].Temp.MaxTemp);
                string weatherDesc = Capitalize(weather.Daily[0].Weather[0].Description);

                // Extra details
                string roundedFLTemp = $"{Math.Ceiling(weather.Daily[0].FeelsLike.DayFLTemp)}°C";
                string HumidityPercent = $"{weather.Daily[0].Humidity}%";
                string WindSpeedKilometers = $"{weather.Daily[0].WindSpeed} km/h";

                FeelsLikeText.Text = roundedFLTemp.ToString();
                HumidityText.Text = HumidityPercent;
                WindSpeedsText.Text = WindSpeedKilometers;

                AddWeatherItem(roundedTemp, "Today", weatherDesc);
            }, null);
        }

        private void AddWeatherItem(double weatherTemp, string weatherDate, string weatherDesc)
        {
            var weatherItem = new weatherItem
            {
                WeatherTemp = weatherTemp.ToString(),
                WeatherDate = weatherDate,
                WeatherDesc = weatherDesc
            };

            weatherItems.Add(weatherItem);
            WeatherInfoBox.ItemsSource = null;
            WeatherInfoBox.ItemsSource = weatherItems;
        }

        private string Capitalize(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void ShowProgressIndicator(bool isVisible, string text = "")
        {
            ProgressIndicator progressIndicator = new ProgressIndicator
            {
                IsIndeterminate = true,
                IsVisible = isVisible,
                Text = text
            };

            SystemTray.SetProgressIndicator(this, progressIndicator);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemTray.IsVisible = false;
        }

        public class weatherItem
        {
            public string WeatherTemp { get; set; }
            public string WeatherDate { get; set; }
            public string WeatherDesc { get; set; }
        }

        public class WeatherRep
        {
            [JsonProperty("current")]
            public CurrentWeather Current { get; set; }
            [JsonProperty("daily")]
            public List<DailyWeather> Daily { get; set; }
        }

        public class CurrentWeather
        {
            [JsonProperty("temp")]
            public double Temp { get; set; }
            [JsonProperty("feels_like")]
            public double FeelsLike { get; set; }
            [JsonProperty("humidity")]
            public double Humidity { get; set; }
            [JsonProperty("weather")]
            public List<WeatherDescClass> Weather { get; set; }
        }

        public class DailyWeather
        {
            [JsonProperty("temp")]
            public WeatherTempClass Temp { get; set; }
            [JsonProperty("feels_like")]
            public WeatherFeelsLikeClass FeelsLike { get; set; }
            [JsonProperty("weather")]
            public List<WeatherDescClass> Weather { get; set; }
            [JsonProperty("humidity")]
            public int Humidity { get; set; }
            [JsonProperty("wind_speed")]
            public string WindSpeed { get; set; }
        }

        public class WeatherTempClass
        {
            [JsonProperty("max")]
            public double MaxTemp { get; set; }
        }

        public class WeatherFeelsLikeClass
        {
            [JsonProperty("day")]
            public double DayFLTemp { get; set; }
        }

        public class WeatherDescClass
        {
            [JsonProperty("description")]
            public string Description { get; set; }
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming soon! Please wait while we implement this.", "not an implemented feature", MessageBoxButton.OK);
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}