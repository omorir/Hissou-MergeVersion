using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{

    //CharacterController Controller;
    Transform Target;
    GameObject Player;

    //オブジェクトのスピード
    public float speed;
    public float attackmove = 0.5f;

    //硬直時間
    public float waitTime = 5f;
    public float attackTime = 5f;
    public float freezeTime = 5f;
    public float elapsedTime;
    public float atktime;

    //オブジェクトの索敵範囲
    [SerializeField] float chaseDistance;
    [SerializeField] float stayDistance;
    [SerializeField] float waitDistance;
    [SerializeField] float attackDistance;


    float state = 0;

    //Enemyの状態


    // Start is called before the first frame update
    void Start()
    {
        //プレイヤータグの取得
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;




    }

    // Update is called once per frame
    void Update()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);


        float distance = Vector3.Distance(transform.position, Target.position);

        if (distance >= waitDistance)
        {

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
                default:
                    freeze();
                    break;
            }



        }

    }

    //追いかけモード
    void Chase()
    {

        float distance = Vector3.Distance(transform.position, Target.position);

        //chaseDistanceの範囲以下かつstayDistanceの範囲以上の場合走らせる
        if (distance < chaseDistance && distance > stayDistance)
        {
            Debug.Log("追いかけている");
            speed = 1.5f;



            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        }
        else
        {
            speed = 0f;
        }
    }

    //攻撃待機モード
    void Wait()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);

        atktime = 0f;

        speed = 0f;



        elapsedTime += Time.deltaTime;

        if (elapsedTime > waitTime)
        {
            Debug.Log("攻撃");
            state = 1;

        }

    }

    //攻撃モード
    void Attack()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //プレイヤーの方向を向く
        transform.LookAt(tarPos);

        elapsedTime = 0f;

        atktime += Time.deltaTime;

        speed = 5f;





        if (atktime > attackTime)
        {
            Vector3 attack = new Vector3(0, 0, attackmove);
            transform.Translate(attack);


            //攻撃硬直モード移行
            Debug.Log("攻撃硬直中");

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

        atktime = 0f;
        float distance = Vector3.Distance(transform.position, Target.position);
        speed = 0f;
        elapsedTime += Time.deltaTime;



        if (elapsedTime > freezeTime)
        {
            return;
        }

        if (distance < waitDistance)
        {
            //攻撃モード移行
            Debug.Log("攻撃");
            state = 1;


        }



    }

}