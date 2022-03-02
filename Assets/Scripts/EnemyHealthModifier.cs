using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthModifier : MonoBehaviour
{
    #region Health Data
    [SerializeField] int healthValue = -1; 
    #endregion
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
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.
                GetComponent<EnemyHealthController>().
                UpdateHealth(healthValue); 
        }
    }
}
