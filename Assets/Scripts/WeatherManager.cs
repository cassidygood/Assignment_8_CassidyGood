using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public enum WeatherType { Sunny, Rainy, Foggy }
    public WeatherType currentWeather;

    public ParticleSystem rainEffect;
    public GameObject clouds;
    public Light directionalLight;

    public Material sunnySkybox;
    public Material cloudySkybox;

    void Start()
    {
        SetWeather(currentWeather);
    }

    public void SetWeather(WeatherType weather)
    {
        currentWeather = weather;

        switch (currentWeather)
        {
            case WeatherType.Sunny:
                RenderSettings.skybox = sunnySkybox;
                RenderSettings.fog = false;
                if (rainEffect != null) rainEffect.Stop();
                if (clouds != null) clouds.SetActive(false);
                directionalLight.intensity = 1f;
                break;

            case WeatherType.Rainy:
                RenderSettings.skybox = cloudySkybox;
                RenderSettings.fog = true;
                RenderSettings.fogColor = Color.gray;
                if (rainEffect != null) rainEffect.Play();
                if (clouds != null) clouds.SetActive(true);
                directionalLight.intensity = 0.6f;
                break;

            case WeatherType.Foggy:
                RenderSettings.skybox = cloudySkybox;
                RenderSettings.fog = true;
                RenderSettings.fogColor = new Color(0.6f, 0.6f, 0.6f, 1f);
                if (rainEffect != null) rainEffect.Stop();
                if (clouds != null) clouds.SetActive(true);
                directionalLight.intensity = 0.4f;
                break;
        }
    }

    // Example usage: Randomly switch weather every 30 seconds
    public void ChangeWeatherRandomly()
    {
        int random = Random.Range(0, 3); // 0 - Sunny, 1 - Rainy, 2 - Foggy
        SetWeather((WeatherType)random);
    }
}

