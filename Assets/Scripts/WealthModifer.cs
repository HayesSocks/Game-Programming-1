using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WealthModifer : MonoBehaviour
{
    [SerializeField] int wealthValue = 1;
    [SerializeField] int sfxToPlay = 2; 
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
            GameController.Instance.UpdateWealth(wealthValue);
            Destroy(gameObject);
            AudioController.Instance.PlaySFX(sfxToPlay);
        }
    }
}
