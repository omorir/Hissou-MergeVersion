using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumipointtagchange : MonoBehaviour
{

    GameObject[] sumipoint1Object;
    GameObject[] sumipoint2Object;
    GameObject[] sumipoint3Object;
    GameObject[] sumipoint4Object;
    GameObject[] sumipoint5Object;



    // Start is called before the first frame update
    void Start()
    {


        sumipoint1Object = GameObject.FindGameObjectsWithTag("sumipoint1");
        sumipoint2Object = GameObject.FindGameObjectsWithTag("sumipoint2");
        sumipoint3Object = GameObject.FindGameObjectsWithTag("sumipoint3");
        sumipoint4Object = GameObject.FindGameObjectsWithTag("sumipoint4");
        sumipoint5Object = GameObject.FindGameObjectsWithTag("sumipoint5");
        

        if (sumipoint1Object.Length == 2)
        {
            this.tag = ("sumipoint2");

            if (sumipoint2Object.Length == 1)
            {
                this.tag = ("sumipoint3");

                if (sumipoint3Object.Length == 1)
                {
                    this.tag = ("sumipoint4");

                    if (sumipoint4Object.Length == 1)
                    {
                        this.tag = ("sumipoint5");

                        if (sumipoint5Object.Length == 1)
                        {
                            this.tag = ("sumipoint6");


                        }

                    }
                }
                
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
       

    }
    
}
