using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager composer;

    public AudioSource menuMusic;
    public Slider menuMSlider;

    public AudioSource gameplayMusic;
    public Slider gameplaySlider;

    // Start is called before the first frame update
    private void Start()
    {
        menuMSlider.value = menuMusic.volume;
        gameplaySlider.value = gameplayMusic.volume;
    }

    private void Awake()
    {
        if (composer == null)
        {
            DontDestroyOnLoad(gameObject);
            composer = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMenu()
    {
        gameplayMusic.Stop();
        if (!menuMusic.isPlaying) menuMusic.Play();
    }

    public void PlayGameplay()
    {
        menuMusic.Stop();
        if (!gameplayMusic.isPlaying) gameplayMusic.Play();
    }

    public void AdjustMenuMusicVolume()
    {
        menuMusic.volume = menuMSlider.value;
    }

    public void AdjustGameMusicVolume()
    {
        gameplayMusic.volume = gameplaySlider.value;
    }
}
