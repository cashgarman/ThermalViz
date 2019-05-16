using UnityEngine;

public class TireHeatMonitor : MonoBehaviour
{
    private FrictionMonitor frictionMonitor;
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
        Debug.Log($"{name} Heat: {FrictionMonitor.heat}");
    }
}
