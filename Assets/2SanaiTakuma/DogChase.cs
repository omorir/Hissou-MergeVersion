using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogChase : MonoBehaviour
{
    //CharacterController Controller;
    Transform Target;
    GameObject Player;

    //オブジェクトのスピード
    public float speed;


    //硬直時間

    //攻撃前の硬直
    public float waitTime = 1f;
    //攻撃モーションの時間
    public float attackTime = 2f;
    //攻撃後の硬直
    public float freezeTime = 0.5f;
    //攻撃後から待機までの時間
    public float roopTime = 2f;

    //硬直が解かす時間
    public float elapsedTime;
    public float atktime;

    //プレイヤーを見つけなかったら消滅する時間
    float desTime;
    public float desponeTime = 5f;

    //攻撃後戻る幅
    public float backmove = -0.5f;

    public float upspeed = 4f;


    //オブジェクトの索敵範囲
    //追いかけるようになる距離
    [SerializeField] float chaseDistance = 15f;
    //プレイヤーに近づいたときに止まる距離
    [SerializeField] float stayDistance = 3f;
    //攻撃する距離
    [SerializeField] float waitDistance = 5f;
    //
    public float foundDistance = 20f;

    //enemyの状態遷移
    float state = 0;

    //enemyが攻撃してから戻るときの時間
    float counter;
    public static bool found = false;
    public static bool despone = false;

    bool Reposition = false;

    public DogMotionScript dogMotion;
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

        Reposition = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyDie.EnemyDie == true)
        {
            //diemortion
            dogMotion.animator.SetInteger("isDie", 1);

            //die_effect_count += Time.deltaTime;

            //if (die_effect_count > die_effect_time)
            //{
            //    //GameObject effect = Instantiate(DieEffect, transform.position, Quaternion.identity);
            //}
            return;
        }

        if (PlayerStatesScript.StartFlag == true || WaveFlag.Boss_former == true)
        {
            return;
        }


        if (Reposition == false)
        {
            if (transform.position.y < 1)
            {
                upspeed = 1f;

                transform.position = transform.position + transform.up * upspeed * Time.deltaTime;
                
            }
            else
            {
                Reposition = true;
            }

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
                dogMotion.animator.SetInteger("isDwalk", 1);
                return;
            }

            desTime += Time.deltaTime;
            dogMotion.animator.SetInteger("isDwalk", 0);
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
            dogMotion.animator.SetInteger("isDwalk", 0);
        }
        else
        {
            found = true;
            desTime = 0f;
        }

        if (distance >= waitDistance)
        {
            //プレイヤーの方向を向く
            transform.LookAt(tarPos);
            dogMotion.animator.SetInteger("isDwalk", 1);
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
                    Attack();
                    break;
                case 2:
                    freeze();
                    break;
                case 3:
                    back();
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
        dogMotion.animator.SetInteger("isDAttack", 0);

        state = 0;
        elapsedTime = 0;

        float distance = Vector3.Distance(transform.position, Target.position);

        //chaseDistanceの範囲以下かつstayDistanceの範囲以上の場合走らせる
        if (distance < chaseDistance && distance > stayDistance)
        {
            Debug.Log("追いかけている");
            speed = 1.3f;

            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        }
        
    }

    //攻撃待機モード
    void Wait()
    {
        dogMotion.animator.SetInteger("isDwalk", 0);
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);



        speed = 0f;




        elapsedTime += Time.deltaTime;

        if (elapsedTime > waitTime)
        {
            Debug.Log("攻撃");
            elapsedTime = 0;
            state = 1;
            transform.tag = "enemy";
            dogMotion.animator.SetInteger("isDAttack", 1);
        }

    }

    //攻撃モード
    void Attack()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        //transform.LookAt(tarPos);

        elapsedTime = 0f;

        atktime += Time.deltaTime;



        float distance = Vector3.Distance(transform.position, Target.position);

        if (distance < waitDistance)
        {
            speed = 6f;



            transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        }



        if (atktime > attackTime)
        {

            //攻撃硬直モード移行
            Debug.Log("攻撃硬直中");
            atktime = 0;
            transform.tag = "Not_Attack_Enemy";
            state = 2;

            
        }



    }

    //攻撃硬直モード
    void freeze()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);


        elapsedTime += Time.deltaTime;

        if (elapsedTime > freezeTime)
        {
            Debug.Log("攻撃");
            elapsedTime = 0;
            state = 3;
            dogMotion.animator.SetInteger("isDAttack", 2);
        }

    }



    void back()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);


        float distance = Vector3.Distance(transform.position, Target.position);

        //elapsedTime += Time.deltaTime;

        speed = 0f;


        Vector3 back = new Vector3(0, 0, backmove);
        transform.Translate(back);

        counter++;
        Debug.Log(counter);

        if (counter == 5)
        {
            counter = 0;
            state = 4;
            dogMotion.animator.SetInteger("isDAttack", 0);
        }

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