using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSound : MonoBehaviour
{

    public AudioSource titlesource;
    //public AudioSource menusource;
    //public AudioClip selsectSE;


    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        titlesource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    titlesource.Stop();
        //    //menusource.Play();
        //}
        //if (Input.GetMouseButtonDown(0))
        //{
        //    audioSource.PlayOneShot(selsectSE);
        //}
    }
}
