using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Time Settings")]
    public float dayDuration = 120f;
    private float currentTime = 30f; // Start around sunrise

    [Header("Sun Settings")]
    public Light sun;

    [Header("Skybox Settings")]
    public Material proceduralSkybox;
    public Color daySkyTint = Color.cyan;
    public Color nightSkyTint = new Color(0.05f, 0.05f, 0.1f);
    public float minExposure = 0.2f;
    public float maxExposure = 1f;

    [Header("Animals")]
    public AnimalWander[] animals; // Drag all your animals here in the Inspector

    void Start()
    {
        UpdateLighting(); // Ensure sky isn't black at start
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= dayDuration)
            currentTime = 0f;

        UpdateLighting();
        UpdateAnimalSleepState(); // Check if animals should sleep
    }

    void UpdateLighting()
    {
        // Sun angle calculation
        float sunAngle = (currentTime / dayDuration) * 360f;
        sun.transform.rotation = Quaternion.Euler(sunAngle - 90, 170, 0);

        // Lerp intensity and color based on the time of day
        float t = Mathf.InverseLerp(0f, dayDuration / 2f, currentTime);
        if (currentTime > dayDuration / 2f)
            t = Mathf.InverseLerp(dayDuration, dayDuration / 2f, currentTime);

        // Update sun intensity and color (sun intensity decreases more at night)
        sun.intensity = Mathf.Lerp(0.05f, 1f, t);
        sun.color = Color.Lerp(Color.gray, Color.white, t);

        // Ensure it gets darker during the night
        if (currentTime > dayDuration / 2f) // It's night
        {
            sun.intensity = Mathf.Lerp(0f, 0.05f, Mathf.InverseLerp(dayDuration / 2f, dayDuration, currentTime)); // Ensure sun is very low
        }

        // Skybox adjustments based on day/night cycle
        if (proceduralSkybox != null)
        {
            proceduralSkybox.SetColor("_SkyTint", Color.Lerp(nightSkyTint, daySkyTint, t));
            proceduralSkybox.SetFloat("_Exposure", Mathf.Lerp(minExposure, maxExposure, t));
        }
    }

    void UpdateAnimalSleepState()
    {
        bool isNight = currentTime > dayDuration / 2f;

        foreach (var animal in animals)
        {
            animal.SetSleeping(isNight);
        }
    }
}
