using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardChase : MonoBehaviour
{
    //CharacterController Controller;
    Transform Target;
    GameObject Player;

    //オブジェクトのスピード
    public float Speed = 8f;

    //上下するスピード
    public float upspeed = 4f;
    public float downspeed = -1f;
    public float returnspeed = 1f;


    //硬直時間
    //高度を下げる時間
    public float waitTime = 2f;
    //攻撃前の硬直
    public float stanceTime = 3f;
    //攻撃モーションの時間
    public float attackTime = 1f;
    //攻撃後に完全に止まる時間
    public float stoppedTime = 0.5f;
    //攻撃後の硬直(上昇)
    public float freezeTime = 0.5f;
    //攻撃後から待機までの時間
    public float roopTime = 2f;
    //硬直が解かす時間
    public float elapsedTime;
    public float atktime;
    public float stoptime;
    //攻撃後戻る幅
    public float backmove = -0.5f;

    bool stop = false;

    bool found = false;
    public static bool despone = false;


    //オブジェクトの索敵範囲
    //追いかけるようになる距離
    [SerializeField] float chaseDistance = 15f;
    //プレイヤーに近づいたときに止まる距離
    [SerializeField] float stayDistance = 3f;
    //攻撃する距離
    [SerializeField] float waitDistance = 5f;

    public float foundDistance = 20f;


    public BirdMotionscript birdMotion;

    public EnemyStates EnemyDie; //narama

    //プレイヤーを見つけなかったら消滅する時間
    float desTime;
    public float desponeTime = 5f;

    //enemyの状態遷移
    float state = 0;

    //enemyが攻撃してから戻るときの時間
    float counter;

    private Rigidbody rigid;

    [SerializeField] GameObject DieEffect;
    float die_effect_count;
    public float die_effect_time = 0.2f;

    //private float CD;


    //Animator animator;

    // Start is called before the first frame update
    void Start()
    {

        //プレイヤータグの取得
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;


        rigid = GetComponent<Rigidbody>();

        found = false;

        despone = false;

        //animator = birdMotion.animator;

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyDie.EnemyDie == true)  //narama
        {
            //diemortion
            birdMotion.animator.SetInteger("isDie", 1);

            //die_effect_count += Time.deltaTime;
            
            //if (die_effect_count > die_effect_time)
            //{
            //    //GameObject effect = Instantiate(DieEffect, transform.position, Quaternion.identity);
            //}
            return;
        }


        if (stop == true)
        {
            stoptime += Time.deltaTime;

            if (stoptime > stoppedTime)
            {
                stop = false;
                state = 4;
            }

        }


        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く

        if (found == false)
        {
            if (transform.position.z > 25 || transform.position.z < -25 || transform.position.x > 30 || transform.position.x < -30)
            {
                //プレイヤーの方向を向く
                transform.LookAt(tarPos);
                Speed = 3f;
                transform.position = transform.position + transform.forward * Speed * Time.deltaTime;
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
                    stance();
                    break;

                case 2:
                    Attack();
                    break;
                case 3:
                    Stop();
                    break;
                case 4:
                    freeze();
                    break;

                default:
                    roop();
                    break;
            }



        }

    }

    //追いかけモード
    void Chase()
    {
        birdMotion.animator.SetInteger("isFly", 0);

        if (transform.position.y < 3.5)
        {
            transform.position = transform.position + transform.up * returnspeed * Time.deltaTime;
        }

        if (transform.position.z > 25 || transform.position.z < -25 || transform.position.x > 30 || transform.position.x < -30)
        {
            Speed = 3f;
            transform.position = transform.position + transform.forward * Speed * Time.deltaTime;
        }


        state = 0;
        elapsedTime = 0;

        float distance = Vector3.Distance(transform.position, Target.position);



        //chaseDistanceの範囲以下かつstayDistanceの範囲以上の場合走らせる
        if (distance < chaseDistance && distance > stayDistance)
        {
            Debug.Log("追いかけている");
            Speed = 1.5f;


            transform.position = transform.position + transform.forward * Speed * Time.deltaTime;
        }
        else
        {
            Speed = 0f;
        }
    }

    //攻撃待機モード
    void Wait()
    {
        Vector3 tarPos = Target.position;
        tarPos.y = transform.position.y;
        //プレイヤーの方向を向く
        transform.LookAt(tarPos);

        Speed = 0f;


        elapsedTime += Time.deltaTime;

        if (transform.position.y > 1)
        {


            transform.position = transform.position + transform.up * downspeed * Time.deltaTime;
        }



        Debug.Log("高度を下げる");

        if (elapsedTime > waitTime)
        {

            elapsedTime = 0f;
            state = 1;

        }

    }

    void stance()
    {
        Vector3 tarPos = Target.position;
        tarPos.y = transform.position.y;
        //プレイヤーの方向を向く
        //transform.LookAt(tarPos);

        elapsedTime += Time.deltaTime;

        Debug.Log("突進の構え");

        if (elapsedTime > waitTime)
        {

            elapsedTime = 0f;
            state = 2;
            transform.tag = "enemy";
        }


    }

    //攻撃モード
    void Attack()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        //transform.LookAt(tarPos);



        atktime += Time.deltaTime;



        float distance = Vector3.Distance(transform.position, Target.position);

        if (distance < waitDistance)
        {
            Speed = 8f;

            transform.position = transform.position + transform.forward * Speed * Time.deltaTime;

            birdMotion.animator.SetInteger("isFly", 1);


        }
        else
        {
            birdMotion.animator.SetInteger("isFly", 0);
        }



        if (atktime > attackTime)
        {

            //攻撃硬直モード移行
            transform.tag = "Not_Attack_Enemy";
            atktime = 0f;
            state = 3;

            birdMotion.animator.SetInteger("isFly", 0);   // masaki

        }



    }
    void Stop()
    {
        stop = true;
    }

    //攻撃硬直モード
    void freeze()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);


        float distance = Vector3.Distance(transform.position, Target.position);

        //elapsedTime += Time.deltaTime;

        Speed = 0f;

        if (transform.position.y < 3.5)
        {
            upspeed = 4f;

            transform.position = transform.position + transform.up * upspeed * Time.deltaTime;

        }

        elapsedTime += Time.deltaTime;

        Debug.Log("攻撃硬直中");

        if (elapsedTime > freezeTime)
        {
            elapsedTime = 0;
            state = 5;

        }

        birdMotion.animator.SetInteger("isFly", 0);   // masaki
    }

    void roop()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);

        elapsedTime += Time.deltaTime;



        if (elapsedTime > roopTime)
        {
            elapsedTime = 0;
            state = 0;

        }
    }
}