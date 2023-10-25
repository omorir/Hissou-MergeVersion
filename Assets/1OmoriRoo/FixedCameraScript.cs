using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Cinemachine;
//using UniRx;

public class FixedCameraScript : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;      //カメラとの相対距離
    private Vector3 state;

    [SerializeField] GameObject Wave1FinCamera;
    [SerializeField] GameObject Doa1;
    [SerializeField] GameObject Doa2;
    [SerializeField] GameObject kannuki;
    //[SerializeField] GameObject Boss;
    [SerializeField] private CinemachineVirtualCamera vcam1;
    [SerializeField] private CinemachineVirtualCamera vcam3;
    [SerializeField] private CinemachineVirtualCamera vcam4;
    [SerializeField] private CinemachineVirtualCamera vcam4_1;
    [SerializeField] private CinemachineVirtualCamera vcam4_2;
    [SerializeField] private CinemachineVirtualCamera vcam5;
    [SerializeField] private CinemachineVirtualCamera vcam5_1;
    [SerializeField] private CinemachineVirtualCamera vcam5_2;
    [SerializeField] private CinemachineVirtualCamera vcam5_3;
    [SerializeField] private CinemachineVirtualCamera vcam6;
    [SerializeField] private GameObject cam6_1;
    [SerializeField] private CinemachineVirtualCamera vcam6_1;
    [SerializeField] private CinemachineVirtualCamera vcam6_2;
    [SerializeField] private GameObject DesUI;
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject PauseButton;
    [SerializeField] private GameObject HPUI;
    [SerializeField] private GameObject WhiteOut_obj;
    [SerializeField] private GameObject BOSS_HPUI;
    [SerializeField] private Image WhiteOut;

    private float whiteout_alpha = 0;

    private CinemachineTrackedDolly dolly;
    public static float Boss_Former_count = 0;

    private bool deadlycam1 = false;
    public static bool deadlycam2 = false;
    private bool deadlycam3 = false;
    public static bool deadlycam4 = false;

    public static float dead4_time = 0;
    private float win_motion_time = 0;

    private Rigidbody p_Rigidbody;

    public static bool winmotion_flag = false;
    //public static bool BlueFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        dolly = vcam3.GetCinemachineComponent<CinemachineTrackedDolly>();
        dolly.m_PathPosition = 0f;

        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        p_Rigidbody = player.GetComponent<Rigidbody>();
        //メインカメラ(自身のオブジェクト)とキャラクターとトランスフォームの相対距離を算出
        offset = transform.position - player.transform.position;

        if(WaveFlag.Boss_former == true)
        {
            dolly = vcam5.GetCinemachineComponent<CinemachineTrackedDolly>();
            vcam5.Priority = 11;
            deadlycam3 = true;
        }

        win_motion_time = 0;
        winmotion_flag = false;

        GameObject[] objects = GameObject.FindGameObjectsWithTag("EnemyHP");
        foreach (GameObject HP in objects)
        {
            HP.SetActive(true);
        }

        //BlueFlag = false;

        whiteout_alpha = 0;
        WhiteOut.color = new Color(1, 1, 1, whiteout_alpha);
        WhiteOut_obj.SetActive(false);
        PlayerStatesScript.TimeFlag = false;
        PlayerCameraWalk3Script.walk_motion = false;
        dolly.m_PathPosition = 0.0f;

        deadlycam2 = false;
        deadlycam4 = false;

        Boss_Former_count = 0;
        dead4_time = 0;

        cam6_1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(WaveFlag.wave1 == true && PlayerStatesScript.killcount >= 50)  // 第一ステージクリア
        {
            Generater.Blink = false;
            deadlycam2 = true;
            PlayerStatesScript.TimeFlag = false;
        }
        else if(WaveFlag.wave2 == true && PlayerStatesScript.killcount >= 100)  // 第二ステージクリア
        {
            Generater.Blink = false;
            deadlycam2 = true;
            PlayerStatesScript.TimeFlag = false;
        }
        if(WaveFlag.Boss_ED == true && deadlycam4 == false)  // ボスステージクリア
        {
            Generater.Blink = false;
            deadlycam4 = true;
            PlayerStatesScript.TimeFlag = false;

            Movetest.animator.SetInteger("isRun", 0);
            Movetest.animator.SetInteger("isAttack", 0);
            Movetest.animator.SetInteger("isHit", 0);
            Movetest.animator.SetInteger("isRoll", 0);

            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 0);
        }

        if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.Alpha1) && deadlycam1 == false && deadlycam2 == false)
        {
            dolly = vcam3.GetCinemachineComponent<CinemachineTrackedDolly>();
            vcam3.Priority = 13;
            deadlycam1 = true;
        }
        else if (Input.GetKey(KeyCode.H) && Input.GetKeyDown(KeyCode.Alpha2) && deadlycam1 == false && deadlycam2 == false)
        {
            if (WaveFlag.wave1 == true)
            {
                PlayerStatesScript.killcount = 50;
            }
            else if(WaveFlag.wave2 == true)
            {
                PlayerStatesScript.killcount = 100;
            }
            deadlycam2 = true;
        }
        else if (WaveFlag.Boss_former == true && deadlycam1 == false && deadlycam2 == false)
        {
            dolly = vcam5.GetCinemachineComponent<CinemachineTrackedDolly>();
            deadlycam3 = true;
        }

        if (deadlycam1 == true)       // 必殺技1
        {
            if (dolly.m_PathPosition >= 2.0f && dolly.m_PathPosition <= 2.6f)
            {
                dolly.m_PathPosition += Time.deltaTime / 1.5f;
            }
            else if (dolly.m_PathPosition >= 0.0f && dolly.m_PathPosition <= 0.1f)
            {
                dolly.m_PathPosition += Time.deltaTime / 20;
            }
            else
            {
                dolly.m_PathPosition += Time.deltaTime / 2;
            }

            if (dolly.m_PathPosition >= 4.0f)
            {
                vcam3.Priority = 9;
                StartCoroutine(vcamDelateMethod());
            }
        }

        else if (deadlycam2 == true)       // 必殺技2
        {
            Movetest.animator.SetInteger("isRun", 0);
            Movetest.animator.SetInteger("isAttack", 0);
            Movetest.animator.SetInteger("isHit", 0);

            result.resultFlag = true;
            Generater.CharaSlide = false;
            PlayerStatesScript.Muteki = true;

            PauseButton.SetActive(false);
            DesUI.SetActive(false);
            minimap.SetActive(false);
            HPUI.SetActive(false);

            GameObject[] objects = GameObject.FindGameObjectsWithTag("EnemyHP");
            foreach (GameObject HP in objects)
            {
                HP.SetActive(false);
            }

            if (dolly.m_PathPosition == 0.0f)
            {
                state = player.transform.position;
                state.x = 0f;
                state.y = 0f;
                state.z = -5f;
                player.position = state;  // 座標を設定
                Vector3 worldAngle;
                worldAngle.x = 0.0f; // ワールド座標を基準に、x軸を軸にした回転を0度に変更
                worldAngle.y = 0.0f; // ワールド座標を基準に、y軸を軸にした回転を0度に変更
                worldAngle.z = 0.0f; // ワールド座標を基準に、z軸を軸にした回転を0度に変更
                player.eulerAngles = worldAngle; // 回転角度を設定

                vcam4.Priority = 13;
                dolly = vcam4.GetCinemachineComponent<CinemachineTrackedDolly>();
            }


            if (dolly.m_PathPosition >= 0.95f && dolly.m_PathPosition <= 1.0f)  //最初のスローモーション
            {
                dolly.m_PathPosition += Time.deltaTime / 20f;
                Movetest.animator.SetInteger("isJamp", 1);
            }
            else if (dolly.m_PathPosition >= 1.9f && dolly.m_PathPosition <= 2.0f)  //空中でのスローモーション
            {
                dolly.m_PathPosition += Time.deltaTime / 20f;
            }
            else if (dolly.m_PathPosition >= 2.0f && dolly.m_PathPosition <= 3.0f)
            {
                dolly.m_PathPosition += Time.deltaTime / 1.0f;
                Movetest.animator.SetInteger("isJamp", 2);
            }
            else // その他ベースとなる速度
            {
                dolly.m_PathPosition += Time.deltaTime / 2.0f;
            }


            if (dolly.m_PathPosition >= 1.0f && dolly.m_PathPosition <= 1.5f)  //プレイヤーが宙に上がる
            {
                p_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                Vector3 pos = player.transform.position;
                pos.y += Time.deltaTime * 22.8f * 1.2156f;
                player.transform.position = pos;
            }
            else if (dolly.m_PathPosition >= 2.0f)
            {
                if (player.transform.position.y <= 0)  //地面に降りたら
                {
                    player.transform.position = state;
                    p_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation
                 | RigidbodyConstraints.FreezePositionY;

                    if (WaveFlag.wave1 == true)
                    {
                        WaveFlag.wave1 = false;
                        WaveFlag.wave1fin = true;
                        Title.wave2_flag = true;

                        //BlueFlag = true;
                    }
                    else if (WaveFlag.wave2 == true)
                    {
                        WaveFlag.wave2 = false;
                        WaveFlag.wave2fin = true;
                        Title.Boss_flag = true;

                        //BlueFlag = true;
                    }
                }
                else   //地面に落ちる
                {
                    p_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    Vector3 pos = player.transform.position;
                    pos.y -= Time.deltaTime * 19.85f;
                    player.transform.position = pos;
                }
            }

            if (dolly.m_PathPosition >= 4.0f)
            {
                Movetest.animator.SetInteger("isJamp", 0);
                vcam4.Priority = 9;
                StartCoroutine(vcamDelateMethod());
            }
        }

        else if (deadlycam3 == true)                             // ボスステージのOP
        {
            if (Input.GetKeyDown(KeyCode.S))                     // OPスキップ
            {
                WaveFlag.Boss_former = false;
                WaveFlag.Boss_stage = true;
                Boss_Former_count = 2.0f;
                BossStates.former_movetime = 11.0f;

                vcam5.Priority = 9;
                vcam5_1.Priority = 9;
                vcam5_2.Priority = 9;
                vcam5_3.Priority = 9;

            }

            if (0.0f <= Boss_Former_count && Boss_Former_count <= 0.014f)
            {
                Boss_Former_count += Time.deltaTime / 1000f;
                if (player.transform.position.z <= -17.0f)     //歩きモーション
                {
                    PlayerCameraWalk3Script.walk_motion = true;
                }
                else
                {
                    PlayerCameraWalk3Script.walk_motion = false;
                }


                if (Boss_Former_count >= 0.005f && Boss_Former_count <= 0.008f)
                {
                    vcam5_1.Priority = 13;  //vcam5_1起動
                }
                else if (Boss_Former_count >= 0.008f && Boss_Former_count <= 0.011f)
                {
                    vcam5_1.Priority = 9;   //vcam5_1ダウン
                    vcam5_2.Priority = 13;  //vcam5_2起動
                }
                else if (Boss_Former_count >= 0.011f)
                {
                    vcam5_2.Priority = 9;   //vcam5_2ダウン
                }
            }
            else if (0.014f <= Boss_Former_count && Boss_Former_count <= 0.8f)
            {
                Boss_Former_count += Time.deltaTime;
            }
            else if(0.8f <= Boss_Former_count && Boss_Former_count <= 1.9f)
            {
                vcam5_3.Priority = 13;
                Boss_Former_count += Time.deltaTime;
            }
            else if (1.9f <= Boss_Former_count && Boss_Former_count <= 2.0f)
            {
                vcam5.Priority = 9;
                Boss_Former_count += Time.deltaTime / 50f;
            }

            dolly.m_PathPosition = Boss_Former_count;
            //Debug.Log("dolly.m_PathPosition：" + dolly.m_PathPosition);

            if(BossStates.former_movetime >= 11.00f)  //ボスが降りきって数秒でformer終了
            {
                Debug.Log("BossOP終わり");
                vcam5_3.Priority = 9;
                WaveFlag.Boss_former = false;
                WaveFlag.Boss_stage = true;
                deadlycam3 = false;

                PlayerStatesScript.TimeFlag = true;
            }
        }

        if(deadlycam4 == true)                            // ボスを倒したら
        {
            PauseButton.SetActive(false);
            DesUI.SetActive(false);
            minimap.SetActive(false);
            HPUI.SetActive(false);
            BOSS_HPUI.SetActive(false);
            Movetest.AttackLifecount = 1;
            PlayerStatesScript.Muteki = true;

            if (dead4_time == 0)
            {
                player.transform.position = new Vector3(0f, 0.25f, 0);
                Vector3 worldAngle = player.transform.eulerAngles;
                worldAngle.x = 0;
                worldAngle.y = 0;
                worldAngle.z = 0;
                player.transform.eulerAngles = worldAngle;
                vcam6_1.Priority = 13;
                vcam1.Priority = 9;
            }

            dead4_time += Time.deltaTime;

            if (2.5f <= dead4_time && dead4_time < 5.5f) // ボススタン
            {
                cam6_1.SetActive(true);
                vcam6_1.Priority = 9;
                vcam6_2.Priority = 13;
                TenguMotionScript.animator.SetInteger("isStan", 1);
            }
            else if (5.5f <= dead4_time && dead4_time < 7.0f) // チフデカメラ
            {
                cam6_1.SetActive(false);
            }
            else if (7.0f <= dead4_time && dead4_time < 9.5f)  // チフデ溜め
            {
                //Vector3 pos = player.transform.position;
                //pos.z += Time.deltaTime * 2;
                //player.transform.position = pos;
                Movetest.animator.SetInteger("isRoll", 1);
            }
            else if (9.5f <= dead4_time && dead4_time < 11.0f)
            {
                cam6_1.SetActive(true);
                vcam6_2.Priority = 9;
                vcam6_1.Priority = 13;
                TenguMotionScript.animator.SetInteger("isStan", 0);
            }
            else if (11.0f <= dead4_time && dead4_time < 13.5f) // ボス溜め
            {
                TenguMotionScript.animator.SetInteger("isBossAttack", 8);
                cam6_1.SetActive(false);

            }
            else if (13.5f <= dead4_time && dead4_time < 15.0f) // カメラが引いて
            {
                vcam6_1.Priority = 9;
                vcam6.Priority = 13;
            }
            else if (17.0f <= dead4_time && dead4_time < 18.5f) // 回転衝突
            {
                Movetest.animator.SetInteger("isRoll", 2);
                TenguMotionScript.animator.SetInteger("isBossAttack", 9);
            }
            else if (18.5f <= dead4_time && dead4_time < 21.0f)
            {
                WhiteOut_obj.SetActive(true);
                WhiteOut.color = new Color(1, 1, 1, whiteout_alpha);
                whiteout_alpha += Time.deltaTime;

                if (whiteout_alpha >= 1)
                {
                    Movetest.animator.SetInteger("isRoll", 0);
                    Vector3 pos = player.transform.position;
                    pos.y = 0.25f;
                    player.transform.position = pos;
                    vcam6.Priority = 9;
                    vcam4_2.Priority = 13;

                    Vector3 worldAngle = player.transform.eulerAngles;
                    worldAngle.x = 0;
                    worldAngle.y = 180;
                    worldAngle.z = 0;
                    player.transform.eulerAngles = worldAngle;
                }
            }
            else if (21.0f <= dead4_time)
            {
                WhiteOut_obj.SetActive(false);
            }
        }

        // メインカメラに相対距離を反映させた新しいトランスフォームの値をセットする
        //transform.position = this.player.transform.position + offset;

        if (PlayerStatesScript.killcount >= 50 && WaveFlag.wave1fin == true && dolly.m_PathPosition >= 4.0f)
        {
            vcam4_2.Priority = 13;
        }
        else if (PlayerStatesScript.killcount >= 100 && WaveFlag.wave2fin == true && dolly.m_PathPosition >= 4.0f)
        {
            vcam4_2.Priority = 13;
        }
        else if(WaveFlag.Boss_ED == true && dead4_time >= 21.0f)
        {
            vcam4_2.Priority = 13;
        }

        if(vcam4_2.Priority == 13)
        {
            if(win_motion_time == 0)
            {
                winmotion_flag = true;
            }

            win_motion_time += Time.deltaTime;
            if (win_motion_time >= 7.0f)
            {
                vcam4_2.Priority = 9;
                vcam4_1.Priority = 13;

                Movetest.AttackLifecount = 0;
            }
        }

        if(result.result_go == true)
        {
            vcam4_1.Priority = 9;
            StartCoroutine(SwitchMethod(Wave1FinCamera));
        }
    }

    private IEnumerator SwitchMethod(GameObject Cam)
    {
        Cam.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        gatescript.isBuckOpen = true;

        Stage1BGM.audioSource.Play();

        //GateDelateMethod(Doa1, Doa2, kannuki);
        yield return new WaitForSeconds(3);
        Cam.SetActive(false);

        result.result_go = false;

        yield return new WaitForSeconds(2.3f);
        DesUI.SetActive(true);
        minimap.SetActive(true);
        PauseButton.SetActive(true);
        HPUI.SetActive(true);
    }
    private void GateDelateMethod(GameObject Gate1, GameObject Gate2, GameObject bar)
    {
        Gate1.SetActive(false);
        Gate2.SetActive(false);
        bar.SetActive(false);
    }

    private IEnumerator vcamDelateMethod()
    {
        yield return new WaitForSeconds(3.0f);

        dolly.m_PathPosition = 0f;

        deadlycam1 = false;
        deadlycam2 = false;
        //PlayerStatesScript.Muteki = false;
    }
}