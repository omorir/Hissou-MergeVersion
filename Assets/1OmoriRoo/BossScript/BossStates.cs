using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStates : MonoBehaviour
{

    [SerializeField] private GameObject Boss_Canvas;
    [SerializeField] private Slider HPUI_Boss_S;
    [SerializeField] private Image HPUI_Fill;

    [SerializeField] private GameObject Boss2;
    [SerializeField] private GameObject Boss3;
    [SerializeField] private GameObject Boss4;

    public ParticleSystem HitEffect;//攻撃受けたのエフェクト
    public static bool Boss_wave2 = false;
    public static bool Boss_wave3 = false;

    public static float Boss_HP = 100;
    public static float former_movetime = 0f;

    public float Attack_interval = 3.0f;
    private float A_i;

    private bool damageFlag = false;
    int JumpAttack = 3;

    private float tenmetu_count = 0;

    public float former_movetime_view = 0;

    private bool Attack5_first = false;

    private bool Motionresetflag = false;

    private int Attack0_count = 0;

    private int motionresetcount = 0;
    public static bool Boss_Attack0 = false;

    public static bool textdelayflag = false;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(0, 20, 5);
        former_movetime = 0f;
        former_movetime_view = 0;

        BossAttack1.atk = false;
        Boss_Attack2.Boss_Attack_2 = false;
        enemyattack3.trigger = false;
        BossAttack4.Attack4 = false;
        BossAttack5.Attack5 = false;

        damageFlag = false;

        Boss_Canvas.SetActive(false);
        Boss_HP = HPUI_Boss_S.maxValue;

        Boss_wave2 = false;
        Boss_wave3 = false;

        A_i = Attack_interval;

        Attack5_first = false;

        Boss2.SetActive(false);
        Boss3.SetActive(false);
        Boss4.SetActive(false);

        Attack0_count = 0;
        Boss_Attack0 = false;

        textdelayflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        HPUI_Boss_S.value = Boss_HP;

        if (WaveFlag.Boss_ED == true)   // ボスクリア時
        {
            if (motionresetcount < 5)
            {
                MotionReset();

                motionresetcount++;
            }

            Boss2.SetActive(false);
            Boss3.SetActive(false);
            Boss4.SetActive(false);

            Vector3 worldAngle = this.transform.eulerAngles;
            worldAngle.x = 0;
            worldAngle.y = 180;
            worldAngle.z = 0;
            this.transform.eulerAngles = worldAngle;

            BossAttack1.atk = false;
            Boss_Attack2.Boss_Attack_2 = false;
            enemyattack3.trigger = false;
            BossAttack4.Attack4 = false;
            BossAttack5.Attack5 = false;

            this.transform.position = new Vector3(0, 1.5f, 8);

            if(FixedCameraScript.dead4_time >= 20.0f)
            {
                this.gameObject.SetActive(false);
            }

            return;
        }

        if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.Alpha0))
        {
            Attack0_count++;
            if (Attack0_count % 2 == 1)
            {
                Boss_Attack0 = true;
                BossAttack1.atk = false;
                Boss_Attack2.Boss_Attack_2 = false;
                enemyattack3.trigger = false;
                BossAttack4.Attack4 = false;
                BossAttack5.Attack5 = false;

                this.transform.position = new Vector3(0, 0f, 0);

                return;
            }
            else
            {
                Boss_Attack0 = false;
            }
        }
        else if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.Alpha1))  // ボス攻撃1のコマンド
        {
            Boss_Attack2.Boss_Attack_2 = false;
            enemyattack3.trigger = false;
            BossAttack4.Attack4 = false;
            BossAttack5.Attack5 = false;
            BossAttack1.AttackCount = 10;
            BossAttack1.atk = true;
        }
        else if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.Alpha2))  // ボス攻撃2のコマンド
        {
            BossAttack1.atk = false;
            enemyattack3.trigger = false;
            BossAttack4.Attack4 = false;
            BossAttack5.Attack5 = false;
            Boss_Attack2.Boss_Attack_2 = true;
        }
        else if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.Alpha3))  // ボス攻撃3のコマンド
        {
            BossAttack1.atk = false;
            Boss_Attack2.Boss_Attack_2 = false;
            BossAttack4.Attack4 = false;
            BossAttack5.Attack5 = false;
            enemyattack3.trigger = true;

        }
        else if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.Alpha4))  // ボス攻撃4のコマンド
        {
            BossAttack1.atk = false;
            Boss_Attack2.Boss_Attack_2 = false;
            enemyattack3.trigger = false;
            BossAttack5.Attack5 = false;
            BossAttack4.Attack4 = true;


        }
        else if (Input.GetKey(KeyCode.B) && Input.GetKeyDown(KeyCode.Alpha5))  // ボス攻撃5のコマンド
        {
            BossAttack1.atk = false;
            Boss_Attack2.Boss_Attack_2 = false;
            enemyattack3.trigger = false;
            BossAttack4.Attack4 = false;
            BossAttack5.Attack5 = true;

        }

        if (Input.GetKeyDown(KeyCode.S))  // ボスOPスキップのコマンド
        {
            Boss_Canvas.SetActive(true);
            Boss_HP = HPUI_Boss_S.maxValue;
        }
        if (Input.GetKey(KeyCode.Space) && Input.GetKeyDown(KeyCode.Alpha1)) // ボスのHPを10ずつ減らすコマンド
        {
            Boss_HP -= 10;
        }

        if ((WaveFlag.Boss_former == true || WaveFlag.Boss_stage == true) && Input.GetKeyDown(KeyCode.S))  // ボスOPがスキップされたらの処理
        {
            Vector3 pos = this.transform.position;
            pos.y = 3.0f;    // y座標へ減算3.0
            this.transform.position = pos;
        }

        if (12 < Boss_HP && Boss_HP <= 30 && Boss_wave2 == false)  // HP50%以下でWave2に移行
        {
            Boss_wave2 = true;
            BossAttack1.atk = false;
            Boss_Attack2.Boss_Attack_2 = false;
            enemyattack3.trigger = false;
            BossAttack4.Attack4 = false;
            BossAttack5.Attack5 = false;
        }
        else if(0 < Boss_HP && Boss_HP <= 12 && Boss_wave3 == false)  // HP20%以下でWave3に移行
        {
            Boss_wave2 = false;
            Boss_wave3 = true;
            BossAttack1.atk = false;
            Boss_Attack2.Boss_Attack_2 = false;
            enemyattack3.trigger = false;
            BossAttack4.Attack4 = false;
            if (Attack5_first == false)
            {
                Attack5_first = true;
                BossAttack5.Attack5 = true;
            }
        }
        else if(Boss_HP <= 0)
        {
            BossAttack5.Attack5 = false;
            WaveFlag.Boss_stage = false;
            WaveFlag.Boss_ED = true;

        }

        if(Boss_wave2 == true)   // Wave2の処理
        {
            HPUI_Fill.color = new Color32(255, 255, 0, 255);

            Boss2.SetActive(true);
            Boss3.SetActive(true);
            Boss4.SetActive(true);

            if (BossAttack1.atk == false && Boss_Attack2.Boss_Attack_2 == false && enemyattack3.trigger == false && BossAttack4.Attack4 == false && BossAttack5.Attack5 == false)
            {
                A_i -= Time.deltaTime;
                MotionReset();
            }

            if (A_i <= 0)
            {
                RamdomAttackMethod(5);
            }
        }
        else if(Boss_wave3 == true)  // Wave3の処理
        {
            if (BossAttack1.atk == false && Boss_Attack2.Boss_Attack_2 == false && enemyattack3.trigger == false && BossAttack4.Attack4 == false && BossAttack5.Attack5 == false)
            {
                A_i -= Time.deltaTime;
                MotionReset();
            }

            if (A_i <= 0)
            {
                RamdomAttackMethod(6);
            }

            Color color = HPUI_Fill.color;
            if(color.a <= 0.15f)
            {
                tenmetu_count += Time.deltaTime * 10.0f;
            }
            else if(color.a <= 0.5f)
            {
                tenmetu_count += Time.deltaTime * 2.0f;
            }
            else
            {
                tenmetu_count += Time.deltaTime;
            }

            color.a = Mathf.Sin(tenmetu_count) * 0.5f + 0.5f;
            if (color.a <= 0.15f)
            {
                color.a = 0.15f;
            }

            HPUI_Fill.color = color;
        }
        else if(WaveFlag.Boss_former == false)                            // Wave1の通常行動
        {
            if(BossAttack1.atk == false && Boss_Attack2.Boss_Attack_2 == false && enemyattack3.trigger == false && BossAttack4.Attack4 == false && BossAttack5.Attack5 == false)
            {
                A_i -= Time.deltaTime;
                MotionReset();
            }

            if (A_i <= 0)
            {
                RamdomAttackMethod(5);
            }

        }



        if (BossAttack1.atk == false && Boss_Attack2.Boss_Attack_2 == false && enemyattack3.trigger == false && BossAttack4.Attack4 == false && BossAttack5.Attack5 == false && this.transform.position.y < 3.00f) //攻撃していない時
        {
            Vector3 pos = this.transform.position;
            pos.y += 2.4f * Time.deltaTime;    // y座標を浮かせる
            this.transform.position = pos;  // 座標を設定
        }

        if (WaveFlag.Boss_former == true && FixedCameraScript.Boss_Former_count >= 0.014f)  // ボスOP時の下降処理
        {
            former_movetime += Time.deltaTime;
            former_movetime_view = former_movetime;

            if (this.transform.position.y >= 3.00f)
            {
                Vector3 pos = this.transform.position;
                pos.y -= 2.4f * Time.deltaTime;    // y座標を2.4減算
                this.transform.position = pos;  // 座標を設定
            }
            if(this.transform.position.y < 3.00f)
            {
                Debug.Log("BOSSのHP表示");
                StartCoroutine(HPUI_BossDelayMethod());
            }
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

        if(BossAttack5.Attack5 == true)
        {
            Boss2.SetActive(false);
            Boss3.SetActive(false);
            Boss4.SetActive(false);
        }
        else if(Boss_wave2 || Boss_wave3)
        {
            Boss2.SetActive(true);
            Boss3.SetActive(true);
            Boss4.SetActive(true);
        }
    }
    private void RamdomAttackMethod(int max)   // ランダムで攻撃を出すメソッド
    {
        int rnd = Random.Range(1, max); // ※ 1～max-1の範囲でランダムな整数値が返る
        switch (rnd)
        {
            case 1:
                BossAttack1.atk = true;
                Vector3 pos1 = this.transform.position;
                pos1.y = 1.5f * Time.deltaTime;    // y座標を浮かせる
                this.transform.position = pos1;  // 座標を設定
                break;
            case 2:
                Boss_Attack2.Boss_Attack_2 = true;
                break;
            case 3:
                enemyattack3.trigger = true;
                Vector3 pos3 = this.transform.position;
                pos3.y = 1.5f;    // y座標を浮かせる
                this.transform.position = pos3;  // 座標を設定
                break;
            case 4:
                BossAttack4.Attack4 = true;
                break;
            case 5:
                BossAttack5.Attack5 = true;
                break;
        }

        A_i = Attack_interval;
    }

    private IEnumerator HPUI_BossDelayMethod()
    {
        yield return new WaitForSeconds(2.0f);
        Boss_Canvas.SetActive(true);

        Boss_HP = HPUI_Boss_S.maxValue;
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
            //if(PlayerStatesScript.ComboCPS_Boss <= PlayerStatesScript.ComboCPS)
            //{
            //    PlayerStatesScript.ComboCPS_Boss = PlayerStatesScript.ComboCPS;
            //}

            Boss_HP -= PlayerStatesScript.ComboCount;

            Debug.Log("BossHP = " + Boss_HP);

            damageFlag = true;

            Effect();
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "JumpAttack")
        {
            Boss_HP -= JumpAttack;
            HPUI_Boss_S.value = Boss_HP;
            Debug.Log("ジャンプヒット:" + Boss_HP);
            if (BossAttack4.Attack4 == true)
            {
                BossAttack4.stanhit = true;
            }
            if (BossAttack5.Attack5 == true)
            {
                BossAttack5.stanhit = true;
            }
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

    void MotionReset()
    {
        TenguMotionScript.animator.SetInteger("isBossAttack", 0);
        if (BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
        {
            BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            BunsinMotion3.animator.SetInteger("isBossAttack", 0);
        }
    }
}
