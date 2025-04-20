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
        float sunAngle = (currentTime / dayDuration) * 360f;
        sun.transform.rotation = Quaternion.Euler(sunAngle - 90, 170, 0);

        float t = Mathf.InverseLerp(0f, dayDuration / 2f, currentTime);
        if (currentTime > dayDuration / 2f)
            t = Mathf.InverseLerp(dayDuration, dayDuration / 2f, currentTime);

        sun.intensity = Mathf.Lerp(0.05f, 1f, t);
        sun.color = Color.Lerp(Color.gray, Color.white, t);

        if (proceduralSkybox != null)
        {
            proceduralSkybox.SetColor("_SkyTint", Color.Lerp(nightSkyTint, daySkyTint, t));
            proceduralSkybox.SetFloat("_Exposure", Mathf.Lerp(minExposure, maxExposure, t));
        }
    }

    // This method checks if it's night and updates the sleep state for animals
    void UpdateAnimalSleepState()
    {
        // Nighttime is the second half of the day cycle (currentTime > half of dayDuration)
        bool isNight = currentTime > dayDuration / 2f;

        // Set all animals' sleep state based on the time of day
        foreach (var animal in animals)
        {
            animal.SetSleeping(isNight);
        }
    }
}
