using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    public Light sceneLight;

    public void AdjustBrightness(float value)
    {
        if (sceneLight != null)
        {
            sceneLight.intensity = value;
        }
    }
}
