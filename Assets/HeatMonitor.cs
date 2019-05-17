using UnityEngine;
using UnityEngine.UI;

public class HeatMonitor : MonoBehaviour
{
    public Text heatDisplayText;

    public ChassisHeatMonitor ChassisHeatMonitor;
    public TireHeatMonitor FLTireHeatMonitor;
    public TireHeatMonitor FRTireHeatMonitor;
    public TireHeatMonitor RLTireHeatMonitor;
    public TireHeatMonitor RRTireHeatMonitor;
    public WheelDrive exaustHeatMonitor;

    void Update()
    {
        heatDisplayText.text = $"Velocity: {exaustHeatMonitor.velocity*20f*2.24f:F0}mph\nChassis Heat: {ChassisHeatMonitor.heat:F1}\nFL Tire Heat: {FLTireHeatMonitor.Heat:F1}\nFR Tire Heat: {FRTireHeatMonitor.Heat:F1}\nRL Tire Heat: {RLTireHeatMonitor.Heat:F1}\nRR Tire Heat: {RRTireHeatMonitor.Heat:F1}\nExhaust Heat: {exaustHeatMonitor.exhaustHeat:F1}";
    }
}
