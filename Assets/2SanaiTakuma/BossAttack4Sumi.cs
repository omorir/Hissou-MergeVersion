using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack4Sumi : MonoBehaviour
{
    [SerializeField] GameObject Sumi;

    GameObject sumi_obj;

    public float speed = 0.1f;

    public float pos_x, pos_y, pos_z;


    public float AttackTime = 4f;
    float AttackCount;

    bool SumiShot = false;

    public static int sumicount = 0;

    public float DeletDistance = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        AttackCount = 0f;

        SumiShot = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (BossAttack4.isStan == true)
        {
            speed = 0f;

            Destroy(sumi_obj);
            Destroy(this.gameObject);
            return;
        }
        if (SumiShot == false)
        {

            AttackCount += Time.deltaTime;

            if (AttackCount > AttackTime)
            {
                sumicount++;

                Vector3 sumipos = new Vector3(pos_x, pos_y, pos_z);

                sumi_obj = Instantiate(Sumi, sumipos, Quaternion.identity);
                sumi_obj.transform.parent = transform;
                SumiShot = true;
            }
            return;
        }

        sumi_obj.transform.position = Vector3.MoveTowards(sumi_obj.transform.position, transform.position, speed);


        float distance = Vector3.Distance(transform.position, sumi_obj.transform.position);
        if (distance < DeletDistance || BossAttack4.Attack4 == false)
        {
            speed = 0f;

            Destroy(sumi_obj.gameObject);
            Destroy(this.gameObject);
        }
    }
}