using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectdelet : MonoBehaviour
{
    public float delettime = 1f;
    float deletcount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deletcount += Time.deltaTime;

        if (deletcount > delettime)
        {
            Destroy(this.gameObject);
        }
        
    }
}
