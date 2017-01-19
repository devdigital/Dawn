namespace Dawn.SampleApi.Services
{
    public interface IConfigurationService
    {
        TValue GetSetting<TValue>(string settingName, TValue defaultValue);
    }
}
