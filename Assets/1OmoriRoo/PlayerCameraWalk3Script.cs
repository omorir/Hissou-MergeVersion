using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerCameraWalk3Script : MonoBehaviour
{
    [SerializeField] Camera UpCamera;
    [SerializeField] Camera FPSCamera;
    [SerializeField] Camera SideCamera;
    [SerializeField] Camera NCamera;
    [SerializeField] private CinemachineVirtualCamera vcam1_1;

    public static bool walk_motion = false;
    public static bool run_motion = false;
    public float walk_run_switch = 280;
    public float rotateSpeed = 10.0F;
    public float speed;
    private float speedmain;
    public float thrust;
    public static Rigidbody rb;
    public Vector3 moveDirection;
    private Transform m_Cam;
    public static Vector3 direction;
    public static bool iRun;
    public static bool isRun;
    public float moveTime = 20.0f;
    float elapsedTime = 0f;  //経過時間
    float rate;
    private float boss_movetime = 0f;

    private Transform player;

    public AudioClip sound2;
    AudioSource walkAudio;

    Vector3 mousePos, worldPos, WorldPlayerPos;

    public float testscoreX,testscoreZ;

    private Vector3 latestPos;  //前回のPosition

    private CharacterController controller;

    private Animator animator;

    //private float Blink_Time = 0;

    //private float speedmainX;
    //private float speedmainY;
    //float moveSpeed = 3f;
    //private Vector3 m_CamForward;
    //private Vector3 m_Move;

    // Use this for initialization
    void Start()
    {

        animator = GetComponent<Animator>(); //アニメーターの取得

        rb = GetComponent<Rigidbody>();　//リジットボディーの取得

        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
        }

        walkAudio = GetComponent<AudioSource>();

        isRun = true;

        controller = GetComponent<CharacterController>();

        mousePos = new Vector3(0,0,0);

        walk_motion = false;
        run_motion = false;

        boss_movetime = 0f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if(WaveFlag.Boss_ED == true)
        {
            Generater.CharaSlide = false;
            Generater.Blink = false;
        }

        if (WaveFlag.Boss_former == true)  //ボスOP
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Vector3 pos = player.position;
                pos.z = -17.0f;    // z座標を17に
                player.position = pos;  // 座標を設定
            }

            if (boss_movetime <= 11.0f)
            {
                Debug.Log("中にも来てる");

                boss_movetime += Time.deltaTime;
                Vector3 pos = player.position;
                pos.z += 3.0f * Time.deltaTime;    // z座標へ加算3.0
                player.position = pos;  // 座標を設定
            }
            return;
        }

        if (Generater.Blink == true)  // ブリンク！！！
        {
            if (elapsedTime < 1.0f)  // 距離に限らず1秒間で移動する
            {
                PlayerStatesScript.MutekiB = true;

                //Debug.Log(elapsedTime);
                elapsedTime += Time.deltaTime;  // 経過時間の加算
                rate = Mathf.Clamp01(elapsedTime / 2.0f);   // 割合計算
                player.transform.position = Vector3.Lerp(player.transform.position, Generater.BlinkPoint, rate); //移動

                Vector3 diff = transform.position - latestPos;   // ベクトルで取得
                latestPos = transform.position;  // 更新
                transform.rotation = Quaternion.LookRotation(diff); // 向きを変更する
            }
            else
            {
                PlayerStatesScript.MutekiB = false;

                Generater.BlinkPoint = Vector3.zero;
                rb.velocity = Vector3.zero;

                Debug.Log(rb.velocity);

                elapsedTime = 0.0f;
                Debug.Log(elapsedTime);

                Generater.Blink = false;
                Movetest.animator.SetInteger("isAttack", 0);
                StartCoroutine(BlinkMuteki());
            }
            return;
        }

        // こっからキャラ操作(マウス、WASD)

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Generater.CharaSlide == true)                                  // [マウスクリックによるキャラ移動]
        {
            mousePos = Input.mousePosition;

            if (CameraButtonScript.UPCameraWalkSwitch == true) // 各カメラを基準にマウス座標を調整
            {
                worldPos = UpCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 100.0f));
            }
            else if (CameraButtonScript.FPSCameraWalkSwitch == true)
            {
                worldPos = FPSCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 100.0f));
            }
            else if (CameraButtonScript.SIDECameraWalkSwitch == true)
            {
                worldPos = SideCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 100.0f));
            }
            //else if (WaveFlag.Boss_stage == true && vcam1_1.Priority == 11)
            //{
            //    worldPos = NCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 150.0f));
            //}
            else
            {
                worldPos = NCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 200.0f));
            }

            if (WaveFlag.Boss_stage == true)  // ボス用のカメラを基準にマウス座標を調整
            {
                //WorldPlayerPos = worldPos;
                //WorldPlayerPos = NCamera.WorldToScreenPoint(new Vector3(player.position.x, player.position.y, player.position.z));
            }
            else
            {
                WorldPlayerPos = player.position;
            }

            //speedmain = (worldPos - WorldPlayerPos).magnitude;


            direction.x = worldPos.x - WorldPlayerPos.x;
            direction.y = (worldPos.y - WorldPlayerPos.y) * 10;
            direction.z = worldPos.z - WorldPlayerPos.z;

            //if (WaveFlag.Boss_stage == false)
            //{
            SpeedLimit(ref direction.x, ref direction.z);


            if (-250 <= direction.x && direction.x < -10)
            {
                transform.RotateAround(player.transform.position, Vector3.up, -5f);
            }
            if (-250 <= direction.z && direction.z < -10)
            {
                transform.RotateAround(player.transform.position, Vector3.up, -5f);
            }
            //}


            testscoreX = direction.x; //デバッグ
            testscoreZ = direction.z;


            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotateSpeed);

            //Vector3 velocity = direction;  //キャラクターコントローラー用
            //velocity.y -= gravity;
            //velocity = transform.transform.TransformDirection(velocity);
            //controller.Move(velocity);

            if (WaveFlag.Boss_stage == true && vcam1_1.Priority == 11)
            {
                //direction.x = (worldPos.x - WorldPlayerPos.x) / 10;
                rb.AddForce(direction.x, 0, direction.y); //リジットボディー用
            }
            else
            {
                if (testscoreX <= -walk_run_switch || testscoreX >= walk_run_switch || testscoreZ <= -walk_run_switch || testscoreZ >= walk_run_switch)  // 走るモーションに切り替え
                {
                    run_motion = true;
                }
                else if (testscoreX >= -walk_run_switch || testscoreX <= walk_run_switch || testscoreZ >= -walk_run_switch || testscoreZ <= walk_run_switch)  // 歩くモーションに切り替え
                {
                    walk_motion = true;
                    run_motion = false;
                }

                rb.AddForce(direction.x, 0, direction.z); //リジットボディー用
            }

            Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
            latestPos = transform.position;  //前回のPositionの更新

            //ベクトルの大きさが0.01以上の時に向きを変える処理をする
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
            }
        }
        else
        {
            if (WaveFlag.Boss_former == false)
            {
                walk_motion = false;
                run_motion = false;
            }
        }


        //アナログスティックのグラつきを想定して±0.01以下をはじく
        if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 0.1F)                    // [キーボードとスティックによるキャラ移動]
        {
            Debug.Log("WASD移動");

            Vector3 camToPlayer = player.position - Camera.main.transform.position;
            float inputAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, inputAngle, 0);

            speedmain = speed;
            direction = targetRotation * Vector3.forward * speedmain;
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotateSpeed);

            rb.AddForce(direction.x, 0, direction.z);

            Vector3 diff = transform.position - latestPos;   //前回からどこに進んだかをベクトルで取得
            latestPos = transform.position;  //前回のPositionの更新

            //ベクトルの大きさが0.01以上の時に向きを変える処理をする
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(diff); //向きを変更する
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                rb.velocity = Vector3.zero;
            }

        }
        else if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) <= 0.1F)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            //this.animator.SetBool(key_isRun, false); // 左スティックを止めたらRunアニメーターがオフ
            iRun = false;
        }
        
    }


    private static void SpeedLimit(ref float x, ref float z)
    {
        z -= 100;

        if (-350 <= z && z < -10)
        {
            z *= 6.4f;
        }
        else if(10 <= z && z <= 350)
        {
            z *= 4.0f;
        }

        if (-350 <= x && x < -10)
        {
            x *= 4.0f;
        }
        else if (10 <= x && x <= 350)
        {
            x *= 4.0f;
        }

        if (x < -350)
        {
            x = -450;
        }
        else if(x > 350)
        {
            x = 450;
        }

        if(z < -350)
        {
            z = -450;
        }
        else if(z > 350)
        {
            z = 450;
        }

        //x /= 10000;
        //z /= 10000;

    }

    private IEnumerator BlinkMuteki()
    {
        yield return new WaitForSeconds(1);

        PlayerStatesScript.Muteki = false;
    }

}