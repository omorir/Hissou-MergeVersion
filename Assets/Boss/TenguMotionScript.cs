using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenguMotionScript : MonoBehaviour
{
    public static Animator animator;

    private int motioncount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.D))
        {

            motioncount++;
            if (motioncount >= 8)
            {
                motioncount = 0;
            }
            animator.SetInteger("isBossAttack", motioncount);
            Debug.Log("天狗モーション：" + motioncount);

        }
    }
}
