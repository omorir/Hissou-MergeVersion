using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudeTrail : MonoBehaviour
{
    TrailRenderer trail;

    public static bool Hude = false;

    // Start is called before the first frame update
    void Start()
    {
        trail = this.gameObject.GetComponent< TrailRenderer>();
        trail.enabled = false;
        Hude = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Movetest.AttackLifecount > 0 || FixedCameraScript.winmotion_flag == true || FixedCameraScript.deadlycam2 == true || Hude == true)
        {
            trail.enabled = true;
        }
        else
        {
            trail.enabled = false;
        }

    }
}
