using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioClip defaultBGM;
    public AudioClip gameBGM;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = defaultBGM;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // public void narasu()
    // {
    //     audioSource.PlayOneShot(sound1);
    // }
    // public void startDefaultBGM()
    // {
    //     bgmAudioSource.Play();
    // }
    public void stopDefaultBGM()
    {
        audioSource.Stop();
    }

    public  void startGameBGM()
    {
        audioSource.clip = gameBGM;
        audioSource.Play();
    }

    public void stopGameBGM()
    {
        audioSource.Stop();
    }

}
