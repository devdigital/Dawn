namespace Dawn.SampleApi.Services
{
    using System;
    using System.Configuration;

    public class DefaultConfigurationService : IConfigurationService
    {
        public TValue GetSetting<TValue>(string settingName, TValue defaultValue)
        {
            var value = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrWhiteSpace(value))
            {
                return default(TValue);
            }

            try
            {
                return (TValue)Convert.ChangeType(value, typeof(TValue));
            }
            catch
            {
                return default(TValue);
            }
        }
    }
}