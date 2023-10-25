using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack4Sumi : MonoBehaviour
{
    Transform Target;
    GameObject sumipoint;


    public float speed = 15f;

    int targetpoint;

    public static int targetchange;

    // Start is called before the first frame update
    void Start()
    {
        targetchange++;
        targetpoint = targetchange;
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (targetpoint)
        {
            case 1://tag:sumipoint1‚ÉŒü‚©‚¤
                sumipoint = GameObject.FindWithTag("sumipoint1");
                Target = sumipoint.transform;
                break;
            case 2://tag:sumipoint2‚ÉŒü‚©‚¤
                sumipoint = GameObject.FindWithTag("sumipoint2");
                Target = sumipoint.transform;
                break;
            case 3://tag:sumipoint3‚ÉŒü‚©‚¤
                sumipoint = GameObject.FindWithTag("sumipoint3");
                Target = sumipoint.transform;
                break;
            case 4://tag:sumipoint2‚ÉŒü‚©‚¤
                sumipoint = GameObject.FindWithTag("sumipoint4");
                Target = sumipoint.transform;
                break;
            case 5://tag:sumipoint3‚ÉŒü‚©‚¤
                sumipoint = GameObject.FindWithTag("sumipoint5");
                Target = sumipoint.transform;
                break;
            default://tag:sumipoint4‚ÉŒü‚©‚¤
                sumipoint = GameObject.FindWithTag("sumipoint6");
                Target = sumipoint.transform;
                break;
        }

        Vector3 position = Target.position;
        

        transform.LookAt(position);

        transform.position = transform.position + transform.forward * speed * Time.deltaTime;


        float distance = Vector3.Distance(transform.position, position);

        if (distance < 0.01f)
        {
            speed = 0f;

            Destroy(sumipoint.gameObject);
            Destroy(this.gameObject);
        }
       

    }

    
}
