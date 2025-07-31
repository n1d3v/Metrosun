using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Metrosun.Classes;

namespace Metrosun
{
    public partial class StallPage : PhoneApplicationPage
    {
        public StallPage()
        {
            InitializeComponent();
            Loaded += StallPage_Loaded;
        }

        public async void StallPage_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            if (SettingsMgr.APIKey == null)
            {
                NavigationService.Navigate(new Uri("/WelcomePage.xaml", UriKind.Relative));
            }
            else
            {
                NavigationService.Navigate(new Uri("/WeatherPage.xaml", UriKind.Relative));
            }
        }
    }
}