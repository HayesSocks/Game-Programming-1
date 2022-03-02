using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject checkpointOFF;
    [SerializeField] GameObject checkpointON;

    [SerializeField] Vector3 positionOffset = Vector3.zero; 
    // Start is called before the first frame update
    void Start()
    {
        checkpointOFF.SetActive(true);
        checkpointON.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameController.Instance.RespawnPosition = transform.position + positionOffset;
            Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
            foreach(Checkpoint cp in checkpoints)
            {
                cp.checkpointOFF.SetActive(true);
                cp.checkpointON.SetActive(false); 
            }
            checkpointOFF.SetActive(false);
            checkpointON.SetActive(true); 
        }
    }
}
