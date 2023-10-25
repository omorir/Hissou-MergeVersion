using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatesScript : MonoBehaviour
{
    public static int HP;
    public static int HPmax = 100;
    public int HPheal = 50;
    public int HealKillCount = 50;
    private bool heal50 = false;
    private bool heal100 = false;
    public int ReceivedDamage = 10;
    public float MutekiTime = 2.0f; //ダメージの重複を無くす
    public static bool MutekiB = false; //ブリンク中の無敵フラグ
    public static bool MutekiD = false; //ダメージによる無敵フラグ
    public static bool Muteki = false; //コマンド用の無敵
    private int mutekicount = 0;
    public static bool StartFlag = false; //スタートエリア
    public static int ComboCPS = 0;
    public static int ComboCPS_Boss = 0;
    private int ComboCPS_Copy = 0;
    private int ComboCPS_Boss_Copy = 0;
    public static float ComboCountTime = 0;
    public static float TimeCount = 0;
    private float Purpose_time = 0;
    public static bool hit_motion = false;
    private float Blackout_alpha = 0;


    public Slider hpSlider;

    [SerializeField] private Renderer PlayerRenderer1;
    [SerializeField] private Renderer PlayerRenderer2;
    [SerializeField] private Renderer PlayerRenderer3;
    [SerializeField] private Renderer PlayerRenderer4;
    [SerializeField] private Renderer PlayerRenderer5;
    [SerializeField] private GameObject Mutekiobj;
    [SerializeField] private Text ComboText;
    [SerializeField]  Text TimeText;
    [SerializeField] Text Purpose_kill;
    [SerializeField] GameObject Purpose_kill_obj;
    [SerializeField] private GameObject BlackOut_obj;
    [SerializeField] private Image BlackOut;

    public float FlashTime = 0.5f;
    private float cycle;

    public static int killcount = 0;
    public static int ComboCount = 0; // EnemyStatesで変更

    public static bool ComboFlag = false;

    public static bool TimeFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        Purpose_kill_obj.SetActive(false);

        killcount = 0;
        HP = HPmax;
        hpSlider.value = HP;

        Purpose_time = 0;

        TimeCount = 0.00f;
        TimeText.text = "時間："+TimeCount;

        heal50 = false;
        heal100 = false;

        Mutekiobj.SetActive(false);

        ComboText.text = "";

        Color color = Purpose_kill.color;
        color.a = 0;
        Purpose_kill.color = color;

        StartFlag = false;
        TimeFlag = false;

        MutekiB = false;
        MutekiD = false;
        Muteki = false;

        ComboCPS = 0;
        ComboCPS_Boss = 0;
        ComboCPS_Copy = 0;
        ComboCPS_Boss_Copy = 0;
        ComboCountTime = 0;

        Blackout_alpha = 0;
        BlackOut.color = new Color(0, 0, 0, Blackout_alpha);
        BlackOut_obj.SetActive(false);
}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            HP -= 5;
        }

            if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            mutekicount++;
            if (mutekicount % 2 == 1)
            {
                Muteki = true;
                Mutekiobj.SetActive(true);

            }
            else
            {
                Muteki = false;
                Mutekiobj.SetActive(false);
            }
        }

        if(WaveFlag.Boss_stage == true && Purpose_time == 0)
        {
            Purpose_kill.text = "烏天狗を倒せ！";
            Purpose_time = 0.01f;
        }

        if (Purpose_time > 0 && Purpose_time <= 3)
        {
            Color color = Purpose_kill.color;
            if (color.a <= 1)
            {
                color.a += Time.deltaTime;
            }
            Purpose_kill.color = color;

            Purpose_time += Time.deltaTime;
        }
        else if (Purpose_time > 0 && Purpose_time <= 4)
        {
            Color color = Purpose_kill.color;
            color.a -= Time.deltaTime;
            Purpose_kill.color = color;

            Purpose_time += Time.deltaTime;
        }
        else
        {
            Color color = Purpose_kill.color;
            color.a = 0;
            Purpose_kill.color = color;
            Purpose_kill_obj.SetActive(false);
        }

        if (TimeFlag == true)
        {
            TimeCount += Time.deltaTime;
            TimeText.text = "時間：" + TimeCount.ToString("f2");
        }

        if(EnemyStates.textdelayflag == true || BossStates.textdelayflag == true || Generater.Blink == true)
        {
            ComboCountTime += Time.deltaTime;

            if (ComboCountTime >= 3.0f)
            {
                ComboText.text = "";
                ComboCPS = 0;
                //ComboCPS_Copy = 0;
                //ComboCPS_Boss_Copy = 0;
                EnemyStates.textdelayflag = false;

                //Debug.Log("3以上の時");
            }
            //else if(ComboCountTime == 0)
            //{
            //    ComboCPS_Copy = ComboCPS;
            //    //ComboCPS_Boss_Copy = ComboCPS_Boss;
            //    ComboCPS = 0;
            //    //ComboCPS_Boss = 0;

            //    Debug.Log("0の時");

            //}
            else
            {
                ComboText.text = ComboCPS + " 連撃！";

                if(ComboCPS == 0)
                {
                    ComboText.text = "";
                }

                //Debug.Log("その他の時");
            }

            //ComboCountTime += Time.deltaTime;
        }

        if (Generater.AttackCount >= Generater.PointCount)
        {
            ComboCount = 0;
        }

        hpSlider.value = HP;

        if (HP <= 0)
        {
            BlackOut_obj.SetActive(true);
            BlackOut.color = new Color(0, 0, 0, Blackout_alpha);
            Blackout_alpha += Time.deltaTime * 2;

            if (BlackOut.color.a >= 1)
            {
                SceneManager.LoadScene("GameOverScene");
            }
        }


        if (PlayerStatesScript.killcount >= 50 && heal50 == false)
        {
            heal50 = true;
            HP += HPheal;
            Debug.Log("koko");
        }
        if (PlayerStatesScript.killcount >= 100 && heal100 == false)
        {
            heal100 = true;
            HP += HPheal;
        }
        //if (PlayerStatesScript.killcount != 0 && PlayerStatesScript.killcount % HealKillCount == 0)
        //{
        //    HP += HPheal;
        //}

        if (HP >= HPmax)
        {
            HP = HPmax;
        }

        if (Input.GetKey(KeyCode.Alpha2))   //体力ゲージ回復コマンド
        {
            HP = HPmax;
        }

        if (MutekiTime < 0.0f)
        {
            MutekiTime = 2.0f;
            MutekiD = false;
            PlayerRenderer1.enabled = true;
            PlayerRenderer2.enabled = true;
            PlayerRenderer3.enabled = true;
            PlayerRenderer4.enabled = true;
            PlayerRenderer5.enabled = true;
        }
        else if (MutekiTime < 2.0f)
        {
            MutekiTime -= Time.deltaTime;

            //点滅処理
            cycle += Time.deltaTime;
            var repeatValue = Mathf.Repeat(cycle, FlashTime);
            // 内部時刻cycleにおける明滅状態を反映
            PlayerRenderer1.enabled = repeatValue >= FlashTime * 0.5f;
            PlayerRenderer2.enabled = PlayerRenderer1.enabled;
            PlayerRenderer3.enabled = PlayerRenderer1.enabled;
            PlayerRenderer4.enabled = PlayerRenderer1.enabled;
            PlayerRenderer5.enabled = PlayerRenderer1.enabled;
        }

    }

    //public void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "enemy" && MutekiB == false && MutekiD == false && Muteki == false)
    //    {
    //        HP -= ReceivedDamage;
    //        hpSlider.value = HP;
    //        MutekiTime -= Time.deltaTime;
    //        MutekiD = true;

    //        //Debug.Log(HP);
    //    }
    //}
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("stone") && MutekiB == false && MutekiD == false && Muteki == false)
        {
            Movetest.hit = true;
            HP -= ReceivedDamage;
            hpSlider.value = HP;
            MutekiTime -= Time.deltaTime;
            MutekiD = true;

            //Debug.Log(HP);
        }

        if (other.CompareTag("WaveMove") && WaveFlag.wave1fin == true)
        {
            WaveFlag.wave1fin = false;
            SceneManager.LoadScene("Stage1Wave2Scene");
        }
        else if (other.CompareTag("WaveMove") && WaveFlag.wave2fin == true)
        {

            WaveFlag.wave2fin = false;
            SceneManager.LoadScene("Stage1BossScene");
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("enemy") && MutekiB == false && MutekiD == false && Muteki == false)
        {
            Movetest.hit = true;
            HP -= ReceivedDamage;
            hpSlider.value = HP;
            MutekiTime -= Time.deltaTime;
            MutekiD = true;

            //Debug.Log(HP);
        }

        if (other.CompareTag("StartArea"))
        {
            StartFlag = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Purpose_kill_obj.SetActive(true);

        if (other.CompareTag("StartArea"))
        {
            if (WaveFlag.wave1 == true)
            {
                Purpose_kill.text = "敵を50体倒せ！";
                Purpose_time += Time.deltaTime;
            }
            else if (WaveFlag.wave2)
            {
                Purpose_kill.text = "敵を100体倒せ！";
                Purpose_time += Time.deltaTime;
            }
            else
            {
                Purpose_kill.text = "";
            }

            gatescript.isOpen = false;

            StartFlag = false;
            if (WaveFlag.Boss_former == false)
            {
                TimeFlag = true;
            }
        }
    }
}