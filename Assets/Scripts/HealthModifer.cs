using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifer : MonoBehaviour
{
    [SerializeField] int healthValue = 0;

    [SerializeField] int sfxToPlay = 4; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            HealthController.Instance.UpdateHealth(healthValue);
        }
        if(this.gameObject.CompareTag("Pickup"))
        {
            Destroy(this.gameObject);
            AudioController.Instance.PlaySFX(sfxToPlay);
        }
        
    }
}
