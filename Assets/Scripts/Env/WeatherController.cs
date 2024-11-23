using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public enum Weather 
    { 
        Stormy,
        Rainy,
        Overcast,
        ClearDusk,
        FoggyDusk,
        Blue
    }

    [SerializeField] private ParticleSystem particleSystemRain;
    [SerializeField] private AudioSource rainSound;

    private const string fogColorBlue = "4fb3ff";
    private const string fogColorOvercast = "c5c5c5";
    private const string fogColorDawn = "CC958C";
    private const string fogColorRainy = "B4C1E7";  

    private const float lightFogDensity = 0.0015f;
    private const float moderateFogDensity = 0.0045f;
    private const float heavyFogDensity = 0.0050f;

    private bool isWindy;
    private bool isFoggy;

    public static WeatherController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("multiple weather controllers in scene");
        }
        Instance = this;
    }
    public bool SetWeatherFromString(string weather)
    {
        switch (weather)
        {
            case "blue":
                SetBlue();
                return true;
            case "overcast":
                SetOvercast();
                return true;
            case "dawn":
                SetDawn();
                return true;
            case "rainy":
                SetRainy();
                return true;
            default:
                return false;

        }
    }

    private void SetBlue()
    {
        RenderSettings.fogColor = HexToColor(fogColorBlue);
        RenderSettings.fogDensity = heavyFogDensity;
        SetRain(false);
    }
    private void SetDawn()
    {
        RenderSettings.fogDensity = moderateFogDensity;
        RenderSettings.fogColor = HexToColor(fogColorDawn);
        SetRain(false);
    }
    private void SetOvercast()
    {
        RenderSettings.fogDensity = lightFogDensity;
        RenderSettings.fogColor = HexToColor(fogColorOvercast);
        SetRain(false);
    }
    private void SetRainy()
    {
        RenderSettings.fogDensity = moderateFogDensity;
        RenderSettings.fogColor = HexToColor(fogColorRainy);
        SetRain(true);
    }

    private void SetRain(bool value) { 
        if (value)
        {
            particleSystemRain.gameObject.SetActive(true);
            rainSound.Play();
        }
        else
        {
            particleSystemRain.gameObject.SetActive(false);
            rainSound.Pause();
        }
    }

    public static Color HexToColor(string hex)
    {
        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length != 6 && hex.Length != 8)
            throw new System.ArgumentException("Hex code must be 6 or 8 characters long.");

        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte a = hex.Length == 8 ? byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) : (byte)255;

        return new Color32(r, g, b, a);
    }
}
