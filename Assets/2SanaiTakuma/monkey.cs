using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monkey : MonoBehaviour
{
    [SerializeField] GameObject stone;
    [SerializeField] GameObject bom;

    //CharacterController Controller;
    Transform Target;
    GameObject Player;

    //オブジェクトのスピード
    public float speed;
    public float stepspeed;

    //硬直時間
    //攻撃前の硬直
    public float waitTime = 2f;
    //攻撃モーションの時間
    public float attackTime = 3f;
    public float attack2Time = 5f;
    //攻撃後の硬直
    public float freezeTime = 2f;
    //ステプさせる時間
    public float stepTime = 1f;

    //硬直が解かす時間
    public float elapsedTime;
    public float atktime;


    //オブジェクトの索敵範囲
    //追いかけるようになる距離
    [SerializeField] float chaseDistance = 15f;
    //プレイヤーに近づいたときに止まる距離
    [SerializeField] float stayDistance = 3f;
    //攻撃する距離
    [SerializeField] float waitDistance = 5f;

    public float foundDistance = 20f;


    //プレイヤーを見つけなかったら消滅する時間
    float desTime;
    public float desponeTime = 5f;

    //Enemyの状態
    float state = 0;

    bool found = false;
    public static bool despone = false;

    public float shotpos_y = 0.5f;

    public MonkeyMotionscript monkeyMotion;
    public EnemyStates EnemyDie;

    [SerializeField] GameObject DieEffect;
    float die_effect_count;
    public float die_effect_time = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤータグの取得
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;

        found = false;

        despone = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyDie.EnemyDie == true)
        {
            //diemortion
            monkeyMotion.animator.SetInteger("isDie", 1);

            //die_effect_count += Time.deltaTime;

            //if (die_effect_count > die_effect_time)
            //{
            //    //GameObject effect = Instantiate(DieEffect, transform.position, Quaternion.identity);
            //}
            return;
        }
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        //transform.LookAt(tarPos);

        if (found == false)
        {
            if (transform.position.z > 25 || transform.position.z < -25 || transform.position.x > 30 || transform.position.x < -30)
            {
                //プレイヤーの方向を向く
                transform.LookAt(tarPos);
                speed = 3f;
                transform.position = transform.position + transform.forward * speed * Time.deltaTime;

                //monkeyMotion.animator.SetInteger("isNageru", 0);
                return;
            }

            desTime += Time.deltaTime;

            if (desTime > desponeTime)
            {
                despone = true;
                Destroy(this.gameObject);
            }
        }

        
        float distance = Vector3.Distance(transform.position, Target.position);

        if (distance > foundDistance)
        {
            found = false;
            
        }
        else
        {
            found = true;
            desTime = 0f;
        }

        if (distance >= waitDistance)
        {
            transform.LookAt(tarPos);
            Chase();

        }
        else
        {
            switch (state)
            {
                case 0:
                    Debug.Log("攻撃前硬直");
                    Wait();
                    break;
                case 1:
                    Attack1();
                    break;
                case 2:
                    Attack2();
                    break;
                case 3:
                    freeze();
                    break;
                case 4:
                    Step();
                    break;
                default:
                    Step2();
                    break;
            }



        }

    }

    //追いかけモード
    void Chase()
    {
        atktime = 0f;//投げるまでの時間初期化 
        monkeyMotion.animator.SetInteger("isNageru", 0);//投げモーション中断

        state = 0;
        elapsedTime = 0;

        float distance = Vector3.Distance(transform.position, Target.position);

        //chaseDistanceの範囲以下かつstayDistanceの範囲以上の場合走らせる
        if (distance < chaseDistance && distance > stayDistance)
        {
            Debug.Log("追いかけている");
            speed = 1.5f;

            monkeyMotion.animator.SetInteger("isMwalk", 1);

            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        }

        
        //monkeyMotion.animator.SetInteger("isNageru", 0);
    }

    //攻撃待機モード
    void Wait()
    {
        monkeyMotion.animator.SetInteger("isMwalk", 0);

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);



        speed = 0f;



        elapsedTime += Time.deltaTime;

        if (elapsedTime < waitTime)
        {
            return;

        }

        int attackstate = Random.Range(0, 2);

        if (attackstate == 0)
        {
            Debug.Log("攻撃");
            elapsedTime = 0;
            state = 1;
            //transform.tag = "enemy";
            monkeyMotion.animator.SetInteger("isNageru", 1);
        }
        if (attackstate == 1)
        {
            Debug.Log("攻撃");
            elapsedTime = 0;
            state = 2;
            //transform.tag = "enemy";
            monkeyMotion.animator.SetInteger("isNageru", 1);
        }



    }

    //攻撃モード
    void Attack1()
    {
        

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);



        atktime += Time.deltaTime;

        //speed = 5f;





        if (atktime > attackTime)
        {
            Vector3 shotpos = new Vector3(transform.position.x, shotpos_y, transform.position.z);
            GameObject Stone = Instantiate(stone, shotpos, Quaternion.identity);

            //攻撃硬直モード移行
            Debug.Log("攻撃硬直中");
            atktime = 0f;
            state = 3;
            //transform.tag = "Not_Attack_Enemy";
            monkeyMotion.animator.SetInteger("isNageru", 0);
        }



    }
    void Attack2()
    {
        

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);



        atktime += Time.deltaTime;

        //speed = 5f;





        if (atktime > attack2Time)
        {
            Vector3 shotpos = new Vector3(transform.position.x, shotpos_y, transform.position.z);
            GameObject Bom = Instantiate(bom, shotpos, Quaternion.identity);

            //攻撃硬直モード移行
            Debug.Log("攻撃硬直中");
            atktime = 0f;
            state = 3;
            //transform.tag = "Not_Attack_Enemy";
            monkeyMotion.animator.SetInteger("isNageru", 0);
        }



    }

    //攻撃硬直モード
    void freeze()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);

        atktime = 0f;
        float distance = Vector3.Distance(transform.position, Target.position);
        speed = 0f;
        elapsedTime += Time.deltaTime;



        if (elapsedTime < freezeTime)
        {
            return;
        }



        if (distance < waitDistance)
        {
            elapsedTime = 0;
            int stepstate = Random.Range(0, 2);

            if (stepstate == 0)
            {
                state = 4;
            }
            else
            {
                state = 5;
            }

        }

    }

    void Step()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);

        elapsedTime += Time.deltaTime;


        stepspeed = 1f;
        transform.position = transform.position + transform.right * stepspeed * Time.deltaTime;



        if (elapsedTime > stepTime)
        {

            elapsedTime = 0;
            state = 0;

        }
    }
    void Step2()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);

        elapsedTime += Time.deltaTime;


        stepspeed = -1f;
        transform.position = transform.position + transform.right * stepspeed * Time.deltaTime;



        if (elapsedTime > stepTime)
        {

            elapsedTime = 0;
            state = 0;

        }
    }
}