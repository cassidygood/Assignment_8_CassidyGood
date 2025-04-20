using UnityEngine;

public class SmoothSkyTransition : MonoBehaviour
{
    public Light sunLight;
    public Material skyMaterial; // Procedural skybox

    [Range(0, 24)]
    public float timeOfDay = 12f; // 0 = midnight, 12 = noon

    public float fullDayLength = 120f; // Seconds for full cycle
    private float timeRate;

    void Start()
    {
        timeRate = 24f / fullDayLength;
    }

    void Update()
    {
        timeOfDay += Time.deltaTime * timeRate;
        if (timeOfDay > 24f) timeOfDay = 0f;

        UpdateLighting(timeOfDay);
    }

    void UpdateLighting(float time)
    {
        float t = Mathf.InverseLerp(6f, 18f, time); // t = 0 at 6am, 1 at 6pm

        // Sky Tint
        Color nightSky = new Color(0.05f, 0.05f, 0.1f); // dark blue
        Color daySky = Color.cyan;
        skyMaterial.SetColor("_SkyTint", Color.Lerp(nightSky, daySky, t));

        // Exposure
        float exposure = Mathf.Lerp(0.2f, 1f, t);
        skyMaterial.SetFloat("_Exposure", exposure);

        // Sun rotation
        float sunAngle = (time / 24f) * 360f - 90f;
        sunLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);

        // Sun brightness
        sunLight.intensity = Mathf.Lerp(0.05f, 1f, t);
        sunLight.color = Color.Lerp(Color.gray, Color.white, t);
    }
}
