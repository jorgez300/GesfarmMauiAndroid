namespace GesfarmMauiAndroid.Domain.Utilities
{
    public static class PreferencesHelper
    {
        public static void Set<T>(string name, T value)
        {
            Preferences.Default.Set(name, value);
        }

        public static T Get<T>(string name, T def)
        {
            T value = Preferences.Default.Get<T>(name, def);
            return value;
        }

        public static void Remove(string name)
        {
            Preferences.Default.Remove(name);
        }

        public static void Clear(string name)
        {
            Preferences.Default.Clear();
        }
    }
}
