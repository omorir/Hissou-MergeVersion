using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TegakiButtonScript : MonoBehaviour
{
    //[SerializeField] Animator anim;
    //public bool BigMap = false;

    private int TegakiCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        TegakiCount++;

        if (TegakiCount % 2 == 1)
        {
            PlayerCameraWalk3Script.isRun = false;

            //anim.SetBool("Centermove", true);
            //BigMap = true;
        }
        else
        {
            PlayerCameraWalk3Script.isRun = true;
        }

        
    }

}
