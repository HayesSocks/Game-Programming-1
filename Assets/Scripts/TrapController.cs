using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] GameObject trapOPEN;
    [SerializeField] GameObject trapCLOSED;
    [SerializeField] int sfxToPlay = 5;

    [SerializeField] int healthValue = 0; 

    [SerializeField] Vector3 positionOffset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        trapOPEN.SetActive(true);
        trapCLOSED.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (trapOPEN.activeInHierarchy == true)
        {
            HealthController.Instance.UpdateHealth(healthValue);
        }

            trapOPEN.SetActive(false);
            trapCLOSED.SetActive(true);
        AudioController.Instance.PlaySFX(sfxToPlay);

    }
}

