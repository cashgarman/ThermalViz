using UnityEngine;

public class TireHeatMonitor : MonoBehaviour
{
    private FrictionMonitor frictionMonitor;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    public Color minColor;
    public Color maxColor;

    private FrictionMonitor FrictionMonitor
    {
        get
        {
            if (frictionMonitor == null)
            {
                frictionMonitor = GetComponentInChildren<FrictionMonitor>();
            }

            return frictionMonitor;
        }
    }

    public float Heat => FrictionMonitor.heat;

    void Update()
    {
        FrictionMonitor.GetComponent<Renderer>().material.SetColor(EmissionColor,
            Color.Lerp(minColor, maxColor, FrictionMonitor.heat / frictionMonitor.maxHeat));
    }
}
