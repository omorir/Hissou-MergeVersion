using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDelete : MonoBehaviour
{

    public float time = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            

            //Debug.Log(EnemyHP);
        }
    }

}