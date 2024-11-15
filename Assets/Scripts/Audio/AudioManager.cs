using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip explodeSound;
    public AudioClip hakusyuSound;

    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void narasu()
    {
        audioSource.PlayOneShot(sound1);
    }
    public void playExplodeSound()
    {
        audioSource.PlayOneShot(explodeSound);
    }    
    public void playHakusyuSound()
    {
        audioSource.PlayOneShot(hakusyuSound);
    }
}
