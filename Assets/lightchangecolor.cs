using UnityEngine;

public class LightColorChanger : MonoBehaviour
{
    public Light targetLight; // Assign the Light component in the Inspector
    public float duration = 3f; // Duration of one color cycle

    public Color color1 = new Color(140f / 255f, 51f / 255f, 0f / 255f); // RGB 140, 51, 0
    public Color color2 = new Color(0f / 255f, 71f / 255f, 140f / 255f); // RGB 0, 71, 140
    private float timer;

    void Start()
    {
        if (targetLight == null)
            targetLight = GetComponent<Light>(); // Automatically assign if not set
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.PingPong(timer / duration, 1); // Normalized time for the cycle
        targetLight.color = Color.Lerp(color1, color2, t); // Interpolate between colors
    }
}