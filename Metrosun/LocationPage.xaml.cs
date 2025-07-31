using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Metrosun.Classes;
using System.Linq;

namespace Metrosun
{
    public partial class LocationPage : PhoneApplicationPage
    {
        public ObservableCollection<string> Suggestions { get; set; }

        // I use a debounce since I don't want to spam the OWM API.
        private DispatcherTimer debounceTimer;
        private const int DebounceMilliseconds = 500; 

        public LocationPage()
        {
            InitializeComponent();
            Suggestions = new ObservableCollection<string>();
            this.DataContext = this;

            debounceTimer = new DispatcherTimer();
            debounceTimer.Interval = TimeSpan.FromMilliseconds(DebounceMilliseconds);
            debounceTimer.Tick += DebounceTimer_Tick;
        }

        private void LocationTB_TextChanged(object sender, RoutedEventArgs e)
        {
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private async void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();
            string input = Capitalize(LocationTB.Text);

            if (string.IsNullOrWhiteSpace(input))
            {
                LocationTB.ItemsSource = null;
                LocationTB.PopulateComplete();
                return;
            }

            ShowProgressIndicator(true, "Fetching the necessary data...");
            var api = new Metrosun.Classes.API();
            await api.SendAPI($"/geo/1.0/direct?q={input}&limit=5&appid={SettingsMgr.APIKey}", "GET", null, (response) =>
            {
                var results = JsonConvert.DeserializeObject<List<OWMLocation>>(response);
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    // Make sure we don't have any dupes before showing results
                    var list = results
                        .Select(loc => $"{loc.name}, {loc.country}")
                        .Distinct()
                        .ToList();

                    LocationTB.ItemsSource = list;
                    LocationTB.PopulateComplete();
                });
                ShowProgressIndicator(false, null);
            }, null);
        }

        private async void LocationTB_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var box = sender as AutoCompleteBox;
            this.Focus();

            if (box.SelectedItem != null)
            {
                string selectedText = box.SelectedItem.ToString();

                var api = new Metrosun.Classes.API();
                await api.SendAPI($"/geo/1.0/direct?q={selectedText}&limit=1&appid={SettingsMgr.APIKey}", "GET", null, (response) =>
                {
                    var results = JsonConvert.DeserializeObject<List<OWMLocation>>(response);
                    var location = results[0];

                    SettingsMgr.LocationName = $"{location.name}, {location.country}";
                    SettingsMgr.Latitude = location.lat;
                    SettingsMgr.Longitude = location.lon;
                }, null);
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (LocationTB.SelectedItem == null)
            {
                var result = MessageBox.Show(
                    "You have selected no location, are you sure you want to continue without setting one?",
                    "no location set",
                    MessageBoxButton.OKCancel
                );

                if (result == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/FinishPage.xaml", UriKind.Relative));
                }
                else
                {
                    return;
                }
            }
            else
            {
                NavigationService.Navigate(new Uri("/FinishPage.xaml", UriKind.Relative));
            }
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

        public class OWMLocation
        {
            // We don't need local_names since this is an English only application (for now)
            public string name { get; set; } // Location name
            public string lat { get; set; } // Location latitude
            public string lon { get; set; } // Location longitude
            public string country { get; set; } // Location country
        }
    }
}