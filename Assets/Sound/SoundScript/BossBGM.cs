using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGM : MonoBehaviour
{
    public AudioClip Bloopclip;
    public AudioClip Bwinclip;

    private bool winSEflag = false;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = Bloopclip;

        audioSource.Play();

        winSEflag = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (FixedCameraScript.winmotion_flag == true && winSEflag == true)
        {
            audioSource.Stop();

            audioSource.PlayOneShot (Bwinclip);

            winSEflag = false;

            Debug.Log("èüóòºﬁ›∏ﬁŸ");

        }
        else if (winSEflag == false)
        {
            //audioSource.loop = !audioSource.loop;


        }

    }
}
