using UnityEngine;

public class BerryPulseEffect : MonoBehaviour
{
    public float pulseSpeed = 5f; 
    public float pulseAmount = 0.0015f; 

    private Vector3 startScale;

    void Start()
    {
        
        startScale = transform.localScale;
    }

    void Update()
    {
        
        float scaleChange = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

       
        transform.localScale = startScale + new Vector3(scaleChange, scaleChange, 0);
    }
}