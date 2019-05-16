using UnityEngine;
using UnityEngine.UI;

public class HeatMonitor : MonoBehaviour
{
    public Text heatDisplayText;
    
    public TireHeatMonitor FLTireHeatMonitor;
    public TireHeatMonitor FRTireHeatMonitor;
    public TireHeatMonitor RLTireHeatMonitor;
    public TireHeatMonitor RRTireHeatMonitor;

    void Update()
    {
        heatDisplayText.text = $"FL Tire Heat: {FLTireHeatMonitor.Heat:F1}\nFR Tire Heat: {FRTireHeatMonitor.Heat:F1}\nRL Tire Heat: {RLTireHeatMonitor.Heat:F1}\nRR Tire Heat: {RRTireHeatMonitor.Heat:F1}";
    }
}
