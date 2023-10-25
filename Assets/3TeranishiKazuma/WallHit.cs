using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(-34.5<gameObject.transform.position.y|| gameObject.transform.position.y<34.5
            &&- 32.0 < gameObject.transform.position.x || gameObject.transform.position.x < 32.0)
        {
            gameObject.layer = 2;
        }
        else
        {
            gameObject.layer = 10;
        }
    }
}
