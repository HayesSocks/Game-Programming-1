using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIController : MonoBehaviour
{
    #region Singleton Pattern
    public static UIController Instance { get; private set; }
    #endregion

    #region UI Data
    Image fadeScreen;

    public Text healthText;
    public Slider healthSlider;
    public Text wealthText;
    #endregion

    #region Fade Data
    public enum FadeState { IDLE, FROM_DARK, TO_DARK }
    public FadeState Fading { get; set; }
    [SerializeField] float fadeTime = 2.0f;
    #endregion

    private void Awake()
    {
        Instance = this; 
    }

    void Start()
    {
        //fadeScreen = GetComponentInChildren<Image>();
        fadeScreen = Instance.transform.Find("Fade Screen").GetComponent<Image>(); 
        Fading = FadeState.FROM_DARK; 
    }

    // Update is called once per frame
    void Update()
    {
        switch(Fading)
        {
            case FadeState.FROM_DARK:
                fadeScreen.color = new Color(
                    fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                    Mathf.MoveTowards(
                        fadeScreen.color.a, 0.0f, Time.deltaTime)); 
                if(fadeScreen.color.a.Equals(0.0f))
                {
                    Fading = FadeState.IDLE; 
                }
                break;
            case FadeState.TO_DARK:
                fadeScreen.color = new Color(
                    fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                    Mathf.MoveTowards(
                        fadeScreen.color.a, 1.0f, Time.deltaTime)); 
                if(fadeScreen.color.a.Equals(1.0f))
                {
                    Fading = FadeState.IDLE; 
                }
                break; 
        }
    }
}
