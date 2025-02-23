using Aspire.RabbitDemo.ServiceDefaults;

namespace Aspire.RabbitDemo.Consumer.Web;

public class WeatherDataService
{
    private WeatherForecast[]? _forecasts;
    public event Action<WeatherForecast[]?>? WeatherDataUpdated;

    public WeatherForecast[]? Forecasts
    {
        get => _forecasts;
        private set
        {
            _forecasts = value;
            WeatherDataUpdated?.Invoke(_forecasts);
        }
    }

    public void UpdateWeatherData(WeatherForecast[]? forecasts)
    {
        Forecasts = forecasts;
    }
}
