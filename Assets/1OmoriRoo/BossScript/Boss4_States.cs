using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4_States : MonoBehaviour
{
    [SerializeField] private GameObject Boss;

    private bool damageFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BossStates.Boss_Attack0 == true)
        {
            this.transform.position = new Vector3(-5.0f, 0f, 0);
        }

        if (BossAttack1.atk == false && Boss_Attack2.Boss_Attack_2 == false && enemyattack3.trigger == false && BossAttack5.Attack5 == false)
        {
            Vector3 pos = Boss.transform.position;
            pos.x = pos.x + 2.5f;
            pos.z = pos.z + -2.5f;
            this.transform.position = pos;
        }

        if (BossAttack1.atk == true)
        {
            Vector3 pos = Boss.transform.position;
            pos.z = pos.z + -8.0f;
            this.transform.position = pos;
        }

        if (enemyattack3.trigger == true)
        {
            Vector3 pos = Boss.transform.position;
            pos.x = pos.x + 2.5f;
            pos.z = pos.z + 2.5f;
            this.transform.position = pos;
        }

        if (Generater.AttackCount >= Generater.PointCount)
        {
            PlayerStatesScript.ComboFlag = false;
            if (Generater.Blink == false)
            {
                damageFlag = false;
                PlayerStatesScript.ComboCount = 0;
            }

        }
    }

    public void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "attack" || ((other.gameObject.tag == "Player" || other.gameObject.tag == "Collider_Brink") && Generater.Blink == true)) && PlayerStatesScript.ComboFlag == true && damageFlag == false)
        {
            if (PlayerStatesScript.ComboCount >= 5)
            {
                PlayerStatesScript.ComboCount = 5;
            }
            else
            {
                PlayerStatesScript.ComboCount++;
            }

            PlayerStatesScript.ComboCPS++;

            damageFlag = true;


        }
    }
}
