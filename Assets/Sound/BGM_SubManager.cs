using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_SubManager : MonoBehaviour
{
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        this.audioSource.volume = BGM_Manager.getVal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
