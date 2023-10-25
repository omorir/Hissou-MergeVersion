using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverBGM : MonoBehaviour
{
    public AudioClip GameOverClip;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(GameOverClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
