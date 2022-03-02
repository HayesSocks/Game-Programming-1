using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    #region Singleton Pattern
    public static HealthController Instance { get; private set; }
    #endregion

    #region Health Data
    [SerializeField] int healthMax = 5;
    public int CurrentHealth { get; private set; }
    bool hasShield = false;
    [SerializeField] float hasShieldTime = 3.0f;
    float hasShieldTimeCounter;
    #endregion

    [SerializeField] int sfxToPlay = 7;

    private void Awake()
    {
        Instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        UIController.Instance.healthSlider.maxValue = healthMax; 
        ResetHealth(); 
    }

    // Update is called once per frame
    void Update()
    {
        if(hasShieldTimeCounter > Mathf.Epsilon)
        {
            hasShieldTimeCounter -= Time.deltaTime; 
        }
        else
        {
            hasShield = false; 
        }
    }

    public void UpdateHealth(int update)
    {
        if ((!hasShield) || (hasShield && (update > 0)))
        {
            CurrentHealth += update;
            if(update < 0)
            {
                hasShield = true;
                hasShieldTimeCounter = hasShieldTime;
            }
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                AudioController.Instance.PlaySFX(sfxToPlay);
                GameController.Instance.Respawn();

            }
            else if (CurrentHealth > healthMax)
            {
                ResetHealth(); 
            }
            UpdateUI();
        }
    }

    public void ResetHealth()
    {
        CurrentHealth = healthMax;
        UpdateUI(); 
    }

    public void UpdateUI()
    {
        UIController.Instance.healthText.text = CurrentHealth.ToString();
        UIController.Instance.healthSlider.value = CurrentHealth; 
    }
}
