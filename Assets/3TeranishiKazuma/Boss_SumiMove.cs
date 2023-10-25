using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SumiMove : MonoBehaviour
{

    private Vector3 vector3;

    // Start is called before the first frame update
    void Start()
    {
        vector3 = BossAttack1.AttackT;
    }

    // Update is called once per frame
    void Update()
    {
        if(BossAttack1.style)
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(this.gameObject.transform.position.x, 0.4f, -34), 3);
            //Invoke("VSumi", 0.5f);
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, new Vector3(-34, 0.4f, this.gameObject.transform.position.z), 3);
            //Invoke("HSumi", 0.5f);
        }
    }

    void VSumi()
    {
       
        BossAttack1.Hatk = true;
    }

    void HSumi()
    {
       
        BossAttack1.Hatk = false;
    }
}
