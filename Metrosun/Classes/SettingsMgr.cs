using System.IO.IsolatedStorage;

namespace Metrosun.Classes
{
    static class SettingsMgr
    {
        private static string GetString(string key)
        {
            object value;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value))
                return value as string;
            return null;
        }

        private static void SetString(string key, string value)
        {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        private static bool GetBool(string key)
        {
            object value;
            if (IsolatedStorageSettings.ApplicationSettings.TryGetValue(key, out value) && value is bool)
                return (bool)value;
            return false;
        }

        private static void SetBool(string key, bool value)
        {
            IsolatedStorageSettings.ApplicationSettings[key] = value;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public static string APIKey
        {
            get { return GetString("apiKey"); }
            set { SetString("apiKey", value); }
        }

        public static string LocationName
        {
            get { return GetString("locationName"); }
            set { SetString("locationName", value); }
        }

        public static string Latitude
        {
            get { return GetString("latitude"); }
            set { SetString("latitude", value); }
        }

        public static string Longitude
        {
            get { return GetString("longitude"); }
            set { SetString("longitude", value); }
        }

        public static bool FinishedWelcome
        {
            get { return GetBool("finishedWel"); }
            set { SetBool("finishedWel", value); }
        }
    }
}