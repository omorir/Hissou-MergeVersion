using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointdelet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (BossAttack5.dangerdelet == true)
        {
            Destroy(this.gameObject);
            BossAttack5.dangerdelet = false;
        }
        if (BossAttack5.stanhit == true)
        {

            Destroy(this.gameObject);
            return;
        }
        if (BossStates.Boss_HP < 1)
        {
            Destroy(this.gameObject);
        }
    }
}