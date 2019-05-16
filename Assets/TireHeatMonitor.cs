using UnityEngine;

public class TireHeatMonitor : MonoBehaviour
{
    private FrictionMonitor frictionMonitor;
    public float maxHeat;
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

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
            Color.Lerp(Color.black, Color.white, FrictionMonitor.heat / maxHeat));
    }
}
