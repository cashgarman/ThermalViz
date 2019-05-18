using UnityEngine;
using UnityEngine.UI;

public class VehicleStats : MonoBehaviour
{
    public Text statsText;
    
    void Update()
    {
        if (statsText != null)
        {
            statsText.text = $"{GetComponent<Rigidbody>().velocity.magnitude:F1} mph";
        }
    }
}
