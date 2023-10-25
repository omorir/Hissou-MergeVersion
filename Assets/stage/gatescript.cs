using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gatescript : MonoBehaviour
{
    [SerializeField] MeshCollider polySurface35_B;
    [SerializeField] MeshCollider polySurface41_B;
    [SerializeField] MeshCollider polySurface35_F;
    [SerializeField] MeshCollider polySurface41_F;

    private Animator animator;
    private int count = 0;
    private int gateCount = 0;

    public static bool isBuckOpen = false;
    public static bool isOpen = false;

    private bool wave2_monFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("isBuckOpen", 0);

        isBuckOpen = false;
        if(WaveFlag.Boss_former)
        {
            isOpen = true;
        }
        else
        {
            isOpen = false;
        }

        wave2_monFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBuckOpen == true)
        {
            animator.SetInteger("isBuckOpen", 1);
            polySurface35_F.convex = true;
            polySurface35_F.isTrigger = true;
            polySurface41_F.convex = true;
            polySurface41_F.isTrigger = true;
        }
        else
        {
            animator.SetInteger("isBuckOpen", 0);
            polySurface35_F.isTrigger = false;
            polySurface35_F.convex = false;
            polySurface41_F.isTrigger = false;
            polySurface41_F.convex = false;
        }

        if(isOpen == true || WaveFlag.Boss_former || (WaveFlag.wave2 == true && PlayerStatesScript.StartFlag == true && wave2_monFlag == false))
        {
            animator.SetInteger("isOpen", 1);
            polySurface35_B.convex = true;
            polySurface35_B.isTrigger = true;
            polySurface41_B.convex = true;
            polySurface41_B.isTrigger = true;
        }
        else
        {
            wave2_monFlag = true;
            animator.SetInteger("isOpen", 0);
            polySurface35_B.isTrigger = false;
            polySurface35_B.convex = false;
            polySurface41_B.isTrigger = false;
            polySurface41_B.convex = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            gateCount = count % 3;
            
            //î‡ÇäJï¬
            animator.SetInteger("isOpen", gateCount);

            Debug.Log("î‡ÅF" + gateCount);

            count++;
        }
    }
}
