using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager composer;

    public AudioSource menuMusic;
    public AudioSource gameplayMusic;
    // Start is called before the first frame update

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
}
