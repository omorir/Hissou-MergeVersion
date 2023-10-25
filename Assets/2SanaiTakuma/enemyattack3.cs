using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyattack3 : MonoBehaviour
{
    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;
    [SerializeField] GameObject Dog;
    [SerializeField] GameObject Bard;
    [SerializeField] GameObject Monkey;

    Transform Target;
    GameObject Player;

    
    public float spawn = 1f;
    public float spawnstop = 10f;
    public float position_z = 10f;
    public float speed = 1f;

    private float time;
    private float stoptime;
    private GameObject Enemy;


    private float state = 0;
    private bool search = true; 
    public static bool trigger = false;

    private float move_x, move_y, move_z;
    private float speedcopy;
    // Start is called before the first frame update
    void Start()
    {

        speedcopy = speed;

        trigger = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (trigger == false)
        {
            return;
        }

        if (search == true)
        {
            //プレイヤータグの取得
            Player = GameObject.FindWithTag("Player");
            Target = Player.transform;

            Vector3 pos = Target.position;
            pos.y = transform.position.y;
            move_x = pos.x;
            move_y = pos.y;
            move_z = pos.z;
            
            search = false;
        }
        
        switch (state) 
        {
            case 0:
                Move();
                break;
            default:
                Spawn();
                break;
        
        }

    }

    /// <summary>
    /// プレイヤーのz座標+10の空中に移動
    /// </summary>
    void Move()
    {
        Vector3 movepos = new Vector3(move_x, move_y, move_z + position_z); 
        
        //移動先を見る
        transform.LookAt(movepos);

        float distance = Vector3.Distance(transform.position, movepos);

        speed = speedcopy;

        transform.position = transform.position + transform.forward * speed * Time.deltaTime;

        //Debug.Log(distance);

        if (distance < 0.1f)
        {
            state = 1;
        }

    }
    void Spawn()
    {
        //プレイヤータグの取得
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;

        Vector3 pos = Target.position;
        pos.y = transform.position.y;

        transform.LookAt(pos);

        time += Time.deltaTime;
        stoptime += Time.deltaTime;

        TenguMotionScript.animator.SetInteger("isBossAttack", 10);
        //BunsinMotion1.animator.SetInteger("isBossAttack", 10);
        //BunsinMotion2.animator.SetInteger("isBossAttack", 10);
        //BunsinMotion3.animator.SetInteger("isBossAttack", 10);

        if (time < spawn)
        {
            return;
        }

        int mob = Random.Range(0, 3);

        switch (mob)
        {
            case 0:
                Enemy = Dog;
                break;
            case 1:
                Enemy = Bard;
                break;
            default:
                Enemy = Monkey;
                break;
        }

        float X, Y, Z;
        X = Random.Range(Point1.position.x, Point2.position.x);

        Y = Random.Range(Point1.position.y, Point2.position.y);

        Z = Random.Range(Point1.position.z, Point2.position.z);

        Instantiate(Enemy, new Vector3(X, Y, Z), Enemy.transform.rotation);
        time = 0f;

        if (stoptime > spawnstop)
        {
            trigger = false;
            stoptime = 0f;
            state = 0;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            //BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            //BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            //BunsinMotion3.animator.SetInteger("isBossAttack", 0);
        }
    }


}
