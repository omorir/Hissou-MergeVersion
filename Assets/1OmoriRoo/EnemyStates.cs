using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStates : MonoBehaviour
{
    public ParticleSystem HitEffect;//攻撃受けたのエフェクト
    private float EnemyHP = 3;
    public int SumiHeal = 5;

    [SerializeField] GameObject ThisEnemy;
    [SerializeField] private GameObject hpSlider_obj;
    [SerializeField] private Slider hpSlider;

    [SerializeField] GameObject DieEffect;

    public static bool delate = false;

    private bool damageFlag = false;
    public static bool textdelayflag = false;
    int JumpAttack = 3;

    public bool EnemyDie = false; //narama

    float del_time_count; //narama
    public float del_time = 0.5f; //narama

    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = hpSlider.maxValue;
        //AttackCount = 0;
        hpSlider.value = EnemyHP;
        //EnemyhpSlider.value = EnemyHP;

        textdelayflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hpSlider.value == hpSlider.maxValue)
        {
            hpSlider_obj.SetActive(false);
        }

        if(WaveFlag.Boss_ED == true)
        {
            Destroy(this.gameObject);
            return;
        }
        
        if (EnemyHP <= 0)
        {
            
            EnemyDie = true;

            del_time_count += Time.deltaTime;
            if (del_time_count > del_time) //やられポーズを行ってから死亡処理
            {
                Generater.sumi += SumiHeal;
                PlayerStatesScript.killcount++;
                Destroy(ThisEnemy);
                GameObject effect = Instantiate(DieEffect, transform.position, Quaternion.identity);
            }


            if (PlayerStatesScript.StartFlag == true && PlayerStatesScript.killcount == 3)
            {
                PlayerStatesScript.killcount = 0;
                tutorial.go = true;
            }
        }

        if(WaveFlag.wave1fin == true)
        {
            PlayerStatesScript.killcount++;
            Destroy(ThisEnemy);
        }
        else if(WaveFlag.wave2fin == true)
        {
            PlayerStatesScript.killcount++;
            Destroy(ThisEnemy);
        }

        if (Generater.AttackCount >= Generater.PointCount)
        {
            PlayerStatesScript.ComboFlag = false;
            if (Generater.Blink == false)
            {
                damageFlag = false;
                PlayerStatesScript.ComboCount = 0;
            }

            textdelayflag = true;
        }

        if(Generater.Blink == true)
        {
            PlayerStatesScript.ComboFlag = true;
        }
        else
        {
            PlayerStatesScript.ComboFlag = false;
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if ( (other.gameObject.tag == "attack" || ((other.gameObject.tag == "Player" || other.gameObject.tag == "Collider_Brink") && Generater.Blink == true)) && PlayerStatesScript.ComboFlag == true && damageFlag == false)
        {
            Debug.Log("当たってるよ");
            Debug.Log("コンボフラグ：" + PlayerStatesScript.ComboFlag);
            Debug.Log("ダメージフラグ：" + damageFlag); 
            Effect();//HitEffect生成

            PlayerStatesScript.ComboCPS++;
            PlayerStatesScript.ComboCount = PlayerStatesScript.ComboCPS;
            //if (PlayerStatesScript.ComboCPS_Boss <= PlayerStatesScript.ComboCPS)
            //{
            //    PlayerStatesScript.ComboCPS_Boss = PlayerStatesScript.ComboCPS;
            //}

            if (PlayerStatesScript.ComboCount >= 5)
            {
                PlayerStatesScript.ComboCount = 5;
            }

            Debug.Log("ComboCount:" + PlayerStatesScript.ComboCount);
            EnemyHP -= PlayerStatesScript.ComboCount;
            hpSlider_obj.SetActive(true);
            hpSlider.value = EnemyHP;
            damageFlag = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "JumpAttack")
        {
            Effect();//HitEffect生成
            EnemyHP -= JumpAttack;
            hpSlider_obj.SetActive(true);
            hpSlider.value = EnemyHP;
            Debug.Log("ジャンプヒット:" + EnemyHP);
        }
    }

    void Effect()
    {
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(HitEffect);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
    }
}
