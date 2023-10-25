using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack5sumi : MonoBehaviour
{
    public float Velocity_0, theta;

    Rigidbody rb;


    public float delettime;
    float deletcount;

    GameObject sumi_obj;

    public float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Vector3 sumiV = rb.velocity;

        sumiV.x = Velocity_0 * Mathf.Cos(theta);
        sumiV.z = Velocity_0 * Mathf.Sin(theta);
        rb.velocity = sumiV;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (BossAttack5.stanhit == true)
        {
            speed = 0f;

            //Destroy(sumi_obj.gameObject);
            Destroy(this.gameObject);
            return;
        }
        deletcount += Time.deltaTime;

        if (deletcount > delettime)
        {
            Destroy(this.gameObject);
        }
    }
}
