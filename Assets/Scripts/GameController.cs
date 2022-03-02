using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton Pattern
    public static GameController Instance { get; private set; }
    #endregion

    #region Respawn Data
    public Vector3 RespawnPosition { get; set; } = Vector3.zero; 
    [SerializeField] float respawnTime = 2.0f;
    #endregion

    #region Wealth Data
    public int CurrentWealth { get; private set; } = 0;
    #endregion

    private void Awake()
    {
        Instance = this; 
    }
    
    void Start()
    {
        // Hide Cursor during playmode
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        RespawnPosition =
            PlayerController.Instance.transform.position;
        UpdateWealth(CurrentWealth); 


        
    }

    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        CameraController.Instance.TheCinemachineBrain.enabled = false;

        UIController.Instance.Fading = UIController.FadeState.TO_DARK; 

        yield return new WaitForSeconds(respawnTime);

        HealthController.Instance.ResetHealth(); 

        UIController.Instance.Fading = UIController.FadeState.FROM_DARK; 

        PlayerController.Instance.transform.position = RespawnPosition;
        PlayerController.Instance.isGrounded = true;
        PlayerController.Instance.GetComponentInChildren<Animator>().SetBool("Grounded", true);

        yield return new WaitForEndOfFrame(); 

        PlayerController.Instance.gameObject.SetActive(true);
        CameraController.Instance.TheCinemachineBrain.enabled = true; 
    }

    public void UpdateWealth(int update)
    {
        CurrentWealth += update;
        UIController.Instance.wealthText.text = CurrentWealth.ToString();
    }



}
