using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Metrosun.Classes;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Metrosun
{
    public partial class APIPage : PhoneApplicationPage
    {
        // Needed for the character limit in APIBox
        private bool suppressTextChanged = false;

        public APIPage()
        {
            InitializeComponent();
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

        private async void NextButton_Click(object sender, EventArgs e)
        {
            var api = new Metrosun.Classes.API();
            ShowProgressIndicator(true, "Validating API key...");

            // Start the process of checking the API key
            SettingsMgr.APIKey = APIBox.Text;
            Debug.WriteLine($"Written APIKey value, here's the reported value: {SettingsMgr.APIKey}");

            // This is not the best thing I could test with, but whatever.
            await api.SendAPI($"/geo/1.0/direct?q=Ireland&limit=1&appid={SettingsMgr.APIKey}", "GET", null, async (response) =>
            {
                // I'm using a delay here because if you have a really good Wi-Fi connection it will just immediately remove the progress indictator which looks ugly
                // I'm very nitpicky about things like this and I'm not sure why.
                await Task.Delay(600);
                ShowProgressIndicator(false, null);
                Debug.WriteLine($"Response from OWM's API: {response}");
                if (response.Contains("Ireland"))
                {
                    Debug.WriteLine("Valid API key, continuing to LocationPage.");
                    NavigationService.Navigate(new Uri("/LocationPage.xaml", UriKind.Relative));
                }
                else
                {
                    Debug.WriteLine("Invalid API key, cannot continue.");
                    MessageBox.Show("Your API key is invalid, if you have just created one you may have to wait a few hours for it to activate.", "invalid API key", MessageBoxButton.OK);
                }
            }, null);
        }
        
        // Why does this thing even work..?
        private void APIBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (suppressTextChanged) return;

            int maxLength = 32;
            string text = APIBox.Text;
            if (text.Length > maxLength)
            {
                int caretIndex = APIBox.SelectionStart;
                suppressTextChanged = true;

                APIBox.Text = text.Substring(0, maxLength);
                APIBox.SelectionStart = Math.Min(caretIndex, maxLength);
                suppressTextChanged = false;
            }

            int textLength = APIBox.Text.Length;
            if (textLength == maxLength)
            {
                APILetterCounter.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 242, 158));
            }
            else
            {
                APILetterCounter.Foreground = new SolidColorBrush(Colors.White);
            }

            APILetterCounter.Text = $"{textLength} / {maxLength}";
            ((ApplicationBarIconButton)ApplicationBar.Buttons[1]).IsEnabled = textLength == maxLength;
        }

        private void GoBackButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/WelcomePage.xaml", UriKind.Relative));
        }
    }
}