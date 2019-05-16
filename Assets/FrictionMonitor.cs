using UnityEditor;
using UnityEngine;

public class FrictionMonitor : MonoBehaviour
{
    private float prevRotation;
    public float heatRatePerDegreeSpin;
    public float coolingRate;

    public float heat;

    void Update()
    {
        var rotationDelta = Mathf.Abs(transform.rotation.eulerAngles.x - prevRotation);
        prevRotation = transform.rotation.eulerAngles.x;

        heat += rotationDelta * heatRatePerDegreeSpin / 360f;
        heat -= coolingRate * Time.deltaTime;
        heat = Mathf.Max(heat, 0f);
    }
}
