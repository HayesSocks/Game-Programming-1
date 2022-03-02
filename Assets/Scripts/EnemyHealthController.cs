using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    #region Health Data
    [SerializeField] int healthMax = 1; 
    public int CurrentHealth { get; private set; }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = healthMax; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealth(int update)
    {
        CurrentHealth += update; 
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Destroy(gameObject);
        }
    }
}
