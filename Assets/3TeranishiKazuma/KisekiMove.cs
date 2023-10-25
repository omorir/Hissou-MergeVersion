using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KisekiMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Generater.nowHit + new Vector3(0f, 0.5f, 0f), 10);

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }
}
