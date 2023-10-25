using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack4 : MonoBehaviour
{
    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;
    [SerializeField] GameObject Sumi;
    [SerializeField] GameObject attackpoint;


    //public static Transform[] Sumipoint;

    //int ap;//attackpointの数
    public static int point = 0;

    public static int state = 0;//敵の行動

    public float move_x, move_y, move_z;//敵が大技1を使用するために移動する場所

    public float speed = 1f;
    float speedcopy;

    private float motioncount;//モーション用


    public static int pointcount = 0;//墨の予測地点の個数
    public int Maxpointcount = 4;//墨の予測地点の最大個数
    public float pointspawn = 0;//墨の予測地点を出す時間
    public float pointspawntime = 0.5f;//墨の予測地点を一つ出すための時間

    public float rsvtime;//溜め攻撃までの溜め時間
    public float reservtime = 3f;//溜め時間の最大時間

    public float atktime;//攻撃する時間
    public float attacktime = 4f;//攻撃する最大時間


    public static bool pointchange;//予測地点から墨に変更するフラグ

    public float changetime = 4f;//墨を発射する時間
    //public float sumichange = 1f;//墨を発射する最大の時間

    float changecount;//墨を発射した数
    //int Maxchangecount;//発射できる墨の最大数（Maxpointcountと同じ）

    public static bool Attack4 = false;
    public bool Attack4_view = false;

    public float AttackPos_y;//墨を飛ばすときの高さ

    public float stantime = 4f;//スタン最大時間//モーションの秒数
    float stancount;
    public float stan_pos_y = 0f;
    public float stan_pos_speed = 0.1f;
    float stan_pos_speed_copy;

    public static bool stanhit = false;
    public static bool isStan = false;

    //分身してる時は墨を飛ばす数が4倍
    public int bunsin_Maxpointcount = 16;
    public float bunsin_pointspawntime = 0.1f;



    // Start is called before the first frame update
    void Start()
    {

        speedcopy = speed;
        stan_pos_speed_copy = stan_pos_speed;
    }

    // Update is called once per frame
    void Update()
    {
        Attack4_view = Attack4;

        if (Attack4 == false)
            return;

        //AttackPoint[ap] = attackpoint[ap];

        switch (state)
        {
            //ポジション移動⇒溜め（3s）⇒攻撃（4s）⇒元の位置に戻る

            case 0:
                positioning();
                break;
            case 1:
                reservoir();
                break;
            case 2:
                attack();
                break;
            case 3:
                bunsin_reservoir();
                break;
            case 4:
                bunsin_attack();
                break;
            default:
                reposition();
                break;

        }


    }


    /// <summary>
    /// ポジション移動
    /// </summary>
    void positioning()
    {
        Vector3 position = new Vector3(move_x, move_y, move_z);
        //position.y = transform.position.y;

        float posdistance = Vector3.Distance(transform.position, position);

        speed = speedcopy;

        //transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, speed * 0.1f);

        if (posdistance < 0.1f)
        {
            speed = 0f;
            //分身モードにはいったら攻撃方法の変更
            if (BossStates.Boss_wave2 == true)
            {
                state = 3;
            }
            else
            {
                state = 1;
            }
        }

    }


    /// <summary>
    /// 溜め攻撃の溜めている処理（３秒）
    /// このときに飛ばす墨の落下地点の表示をしたい
    /// </summary>
    void reservoir()
    {

        if (stanhit == true)
        {//スタンのモーション

            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }

        rsvtime += Time.deltaTime;

        //予測地点（pointcount）が最大値(Maxpointcount)（４つ）以下の場合実行
        if (pointcount < Maxpointcount)
        {

            pointspawn += Time.deltaTime;

            //pointspawntime(4s)の間隔でランダム位置に予測地点を出す
            if (pointspawn > pointspawntime)
            {
                float x = Random.Range(Point1.position.x, Point2.position.x);
                float y = Random.Range(Point1.position.y, Point2.position.y);
                float z = Random.Range(Point1.position.z, Point2.position.z);

                //AttackPoint[ap] = attackpoint[ap];
                //Instantiate(attackpoint[ap], new Vector3(x, y, z), attackpoint[ap].transform.rotation);
                Instantiate(attackpoint, new Vector3(x, y, z), attackpoint.transform.rotation);

                //ap++;
                pointspawn = 0f;

                pointcount++;
                //TenguMotionScript.animator.SetInteger("isBossAttack",1);
            }
        }

        //reservtime（3s）をこえたら攻撃モードに変更
        if (rsvtime > reservtime)
        {
            pointcount = 0;
            state = 2;
            rsvtime = 0f;
            //ap = 0;
        }

    }

    /// <summary>
    /// 溜め攻撃の攻撃の処理
    /// ４秒で４連の墨を飛ばす
    /// </summary>
    void attack()
    {
        if (stanhit == true)
        {//スタンのモーション
            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }



        if (BossAttack4Sumi.sumicount % 2 == 0)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 6);
        }
        else
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 7);
        }


        changecount += Time.deltaTime;

        if (changetime < changecount)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            changecount = 0;
            state = 5;
            BossAttack4Sumi.sumicount = 0;
        }

    }

    /// <summary>
    /// 溜め攻撃の溜めている処理（３秒）
    /// このときに飛ばす墨の落下地点の表示をしたい
    /// </summary>
    void bunsin_reservoir()
    {


        if (stanhit == true)
        {//スタンのモーション
            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            BunsinMotion3.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }

        rsvtime += Time.deltaTime;

        //予測地点（pointcount）が最大値(Maxpointcount)（４つ）以下の場合実行
        if (pointcount < bunsin_Maxpointcount)
        {

            pointspawn += Time.deltaTime;

            //pointspawntime(4s)の間隔でランダム位置に予測地点を出す
            if (pointspawn > bunsin_pointspawntime)
            {
                float x = Random.Range(Point1.position.x, Point2.position.x);
                float y = Random.Range(Point1.position.y, Point2.position.y);
                float z = Random.Range(Point1.position.z, Point2.position.z);

                //AttackPoint[ap] = attackpoint[ap];
                //Instantiate(attackpoint[ap], new Vector3(x, y, z), attackpoint[ap].transform.rotation);
                Instantiate(attackpoint, new Vector3(x, y, z), attackpoint.transform.rotation);

                //ap++;
                pointspawn = 0f;

                pointcount++;
                //TenguMotionScript.animator.SetInteger("isBossAttack", 1);
            }
        }

        //reservtime（3s）をこえたら攻撃モードに変更
        if (rsvtime > reservtime)
        {
            pointcount = 0;
            state = 4;
            rsvtime = 0f;
            //ap = 0;
        }

    }

    /// <summary>
    /// 溜め攻撃の攻撃の処理
    /// ４秒で４連の墨を飛ばす
    /// </summary>
    void bunsin_attack()
    {
        if (stanhit == true)
        {//スタンのモーション
            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            BunsinMotion3.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }



        if (BossAttack4Sumi.sumicount % 2 == 0)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 6);
            BunsinMotion1.animator.SetInteger("isBossAttack", 6);
            BunsinMotion2.animator.SetInteger("isBossAttack", 6);
            BunsinMotion3.animator.SetInteger("isBossAttack", 6);
        }
        else
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 7);
            BunsinMotion1.animator.SetInteger("isBossAttack", 7);
            BunsinMotion2.animator.SetInteger("isBossAttack", 7);
            BunsinMotion3.animator.SetInteger("isBossAttack", 7);
        }


        changecount += Time.deltaTime;

        if (changetime < changecount)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            if(BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
            {
                BunsinMotion1.animator.SetInteger("isBossAttack", 0);
                BunsinMotion2.animator.SetInteger("isBossAttack", 0);
                BunsinMotion3.animator.SetInteger("isBossAttack", 0);
            }
            
            changecount = 0;
            state = 5;
            BossAttack4Sumi.sumicount = 0;
        }

    }

    /// <summary>
    /// 元の位置に戻る
    /// </summary>
    void reposition()
    {
        //ここで大技1フラグを切る
        Attack4 = false;
        state = 0;
        TenguMotionScript.animator.SetInteger("isBossAttack", 0);
        if (BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
        {
            BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            BunsinMotion3.animator.SetInteger("isBossAttack", 0);
        }
        
    }

    /// <summary>
    /// 溜め中に攻撃を食らったときにスタン
    /// </summary>
    void stan()
    {

        stancount += Time.deltaTime;

        //
        Vector3 stan_pos = new Vector3(transform.position.x, stan_pos_y, transform.position.z);

        float stan_distance = Vector3.Distance(transform.position, stan_pos);

        transform.position = Vector3.MoveTowards(transform.position, stan_pos, stan_pos_speed_copy);

        if (stan_distance < 0.1f)
        {
            stan_pos_speed_copy = 0f;
        }

        if (stancount > stantime)
        {
            //スタンしたらその攻撃は終了
            Attack4 = false;
            state = 0;
            stancount = 0f;
            pointspawn = 0f;
            pointcount = 0;
            rsvtime = 0f;
            stanhit = false;
            isStan = false;
            changecount = 0f;
            stan_pos_speed_copy = stan_pos_speed;
            BossAttack4Sumi.sumicount = 0;
            TenguMotionScript.animator.SetInteger("isStan", 0);
        }
    }

}
