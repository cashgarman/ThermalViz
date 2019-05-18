using UnityEngine;

public class ChassisHeatMonitor : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    
    private Vector3 prevPosition;
    private Renderer renderer;
    public float maxVelocity;
    public float heatPerSecondPerVelocity;

    public float heat;
    public float maxHeat;
    public float coolingPerSecond;
    public Color minHeatColor;
    public Color maxHeatColor;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        prevPosition = transform.parent.position;
    }

    private void Update()
    {
        var velocity = (transform.parent.position - prevPosition).magnitude;
        prevPosition = transform.parent.position;

        heat = Mathf.Min(maxHeat, heat + velocity * heatPerSecondPerVelocity);
//        Debug.Log($"chassis heat: {heat}");
        
        var bodyColor = Color.Lerp(minHeatColor, maxHeatColor, heat / maxHeat);
//        Debug.Log($"Body Color: {bodyColor}");
        foreach (var material in renderer.materials)
        {
            material.SetColor(EmissionColor, bodyColor);
        }

        heat -= coolingPerSecond * Time.deltaTime;
        heat = Mathf.Max(heat, 0f);
    }
}
