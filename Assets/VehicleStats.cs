using UnityEngine;
using UnityEngine.UI;

public class VehicleStats : MonoBehaviour
{
    public Text statsText;
    
    void Update()
    {
        if (statsText != null)
        {
            statsText.text = $"{GetComponent<Rigidbody>().velocity.magnitude*1.5f:F1} mph";
        }
    }
}
