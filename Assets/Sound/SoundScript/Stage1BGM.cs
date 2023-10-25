using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1BGM : MonoBehaviour
{
    //private bool introflag = false;
    private bool winSEflag = false;

    public AudioClip introclip;
    public AudioClip loopclip;
    public AudioClip winclip;

    public static AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = introclip;

        audioSource.Play();

        //introflag = true;
        winSEflag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && winSEflag == true)
        {
            
            Debug.Log("ÉCÉìÉgÉçèIóπ");
            audioSource.clip = loopclip;
            audioSource.loop = audioSource.loop;

            audioSource.Play();
            //introflag = false;
        }

        if (FixedCameraScript.winmotion_flag == true && winSEflag == true)
        {
            audioSource.Stop();
           
            
            audioSource.PlayOneShot(winclip);

            winSEflag = false;

            Debug.Log("èüóòºﬁ›∏ﬁŸ");

        }else if (winSEflag == false)
        {
            //audioSource.loop = !audioSource.loop;

        
        }

    }
}
