using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    #region Singleton Pattern
    public static AudioController Instance { get; private set; }
    #endregion

    #region Audio Data
    [SerializeField] AudioSource[] musicList;
    [SerializeField] AudioSource[] sfxList;
    [SerializeField] int levelMusicToPlay = 0;
    #endregion

    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic(levelMusicToPlay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMusic(int musicToPlay)
    {
        foreach (var music in musicList)
        {
            music.Stop();
        }
        musicList[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfxList[sfxToPlay].Play();
    }
}