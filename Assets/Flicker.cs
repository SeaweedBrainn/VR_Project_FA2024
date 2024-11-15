using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light lightSource;
    public float minIntensity = 0.2f;
    public float maxIntensity = 1.0f;
    public float flickerSpeed = 0.1f;

    void Update()
    {
        lightSource.intensity = Random.Range(minIntensity, maxIntensity);
        lightSource.enabled = Random.value > flickerSpeed; // Toggle on/off
    }
}