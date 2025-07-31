using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Metrosun
{
    public partial class FinishPage : PhoneApplicationPage
    {
        public FinishPage()
        {
            InitializeComponent();
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/LocationPage.xaml", UriKind.Relative));
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/WeatherPage.xaml", UriKind.Relative));
        }
    }
}