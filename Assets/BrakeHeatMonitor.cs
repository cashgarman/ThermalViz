using UnityEngine;

public class BrakeHeatMonitor : MonoBehaviour
{
    public float heat;
    public float coolingPerSecond;
    public Color minHeatColor;
    public Color maxHeatColor;
    public float maxHeat;

    void Update()
    {
        heat = Mathf.Clamp(heat - coolingPerSecond * Time.deltaTime, 0f, maxHeat);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(minHeatColor, maxHeatColor, heat));
    }
}
