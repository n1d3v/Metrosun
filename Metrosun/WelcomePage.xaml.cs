using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Metrosun
{
    public partial class WelcomePage : PhoneApplicationPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/APIPage.xaml", UriKind.Relative));
        }
    }
}