using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public CinemachineBrain TheCinemachineBrain { get; private set; }

    private void Awake()
    {
        Instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        TheCinemachineBrain = GetComponent<CinemachineBrain>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
