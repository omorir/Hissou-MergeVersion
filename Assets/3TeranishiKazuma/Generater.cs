using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Generater : MonoBehaviour
{
    public static int penTime = 0;
    public ParticleSystem ChargeEffect;//長押しのエフェクト
    public GameObject cube;//軌跡　
    public GameObject kiseki; //軌跡の線
    public GameObject jump;//ジャンプのダメージ範囲
    public GameObject area;//ジャンプの予定範囲
    public static GameObject Sen;
    public static float time = 0;
    public float jikan = 0;
    public float Max = 100;
    public float max = 0;
    public static int scale = 0;
    public int count = 0;
    public int trigger = 200;
    public static int combo = 0;
    public static float sumi = 100;//墨の残量
    public static float sumiMax = 100;
    public Slider sumiSlider;
    public bool jumparea = false;
    public static bool keep = false;
    public static bool C_Effect = false;//チャージエフェクトを出したかどうか
    public bool EffectTest = false;
    public static bool CharaSlide = false;  //ペンタブ移動
    public static bool Blink = false;  //キャラクタースクリプトにブリンク可能か伝えるフラグ
    public static bool BlinkIn = false;  //最初に床と当たったかどうか
    public static bool BlinkOut = false;  //最後に主人公に当たったかどうか
    public static bool effect = false;
    public bool write = true; // kisekiを生成したかどうか
    public bool jumptrigger = true;
    public static Vector3 direction;
    public float rotateSpeed = 10.0F;
    public static Vector3 nowHit = new Vector3(0, 0, 0);//現座標
    public Vector3 firstHit = new Vector3(0, 0, 0);//始点
    public static Vector3 BlinkPoint = new Vector3(0, 0, 0);  //ブリンク先の座標をキャラクタースクリプトに伝える
    public static List<Vector3> Ray = new List<Vector3>();//Rayの座標管理用リスト

    [SerializeField] Camera UpCamera;
    [SerializeField] Camera FPSCamera;
    [SerializeField] Camera SideCamera;
    [SerializeField] Camera NCamera;
    [SerializeField] Camera TPSCamera;
    [SerializeField] GameObject Brink_Point;

    public static int PointCount = 0;
    private int PointC = 0;
    public static int AttackCount = 0;

    void Start()
    {
        max = Max;
        sumi = sumiMax;
        sumiSlider.value = sumi;

        PointCount = 0;
        PointC = 0;
        AttackCount = 0;
        CharaSlide = false;
        Brink_Point.SetActive(false);

        Blink = false;
        BlinkIn = false;
        BlinkOut = false;
    }

    void Update()
    {
        if (WaveFlag.Boss_former == true || FixedCameraScript.deadlycam2 == true || result.resultFlag == true || FixedCameraScript.deadlycam4 == true || WaveFlag.Boss_ED == true)
        {
            Ray.Clear();
            return;
        }

        sumiSlider.value = sumi;

        if (Input.GetKey(KeyCode.Alpha1))   //墨ゲージ回復コマンド
        {
            sumi = 100;
            sumiSlider.value = sumi;
        }

        if (sumi <= 33)
        {
            sumi += Time.deltaTime;
        }
        if (sumi >= sumiMax)
        {
            sumi = sumiMax;
        }
        if (sumi <= 0)
        {
            sumi = 0;
        }

        Ray ray;

        //if (CameraButtonScript.UPCameraWalkSwitch == true)            //カメラ別に線の座標を調整
        //{
        //    ray = UpCamera.ScreenPointToRay(Input.mousePosition);
        //}
        //else if (CameraButtonScript.FPSCameraWalkSwitch == true)
        //{
        //    ray = FPSCamera.ScreenPointToRay(Input.mousePosition);
        //}
        //else if (CameraButtonScript.SIDECameraWalkSwitch == true)
        //{
        //    ray = SideCamera.ScreenPointToRay(Input.mousePosition);
        //}
        //else if (CameraButtonScript.TPSFlag == true)
        //{
        //    ray = TPSCamera.ScreenPointToRay(Input.mousePosition);
        //}
        //else
        //{
        ray = NCamera.ScreenPointToRay(Input.mousePosition);
        //}

        RaycastHit hit = new RaycastHit();

        if (Input.GetMouseButtonDown(0))
        {
            Ray.Clear();
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {

        }
        else if (Input.GetMouseButton(0) && Blink == false && Time.timeScale == 1)
        {
            time += Time.deltaTime;
            jikan = time;
            count++;
            if (Physics.Raycast(ray, out hit))
            {
                if (write && (hit.collider.CompareTag("floor") || hit.collider.CompareTag("enemy")))
                {
                    //Sen =
                    Instantiate(kiseki, hit.point + new Vector3(0f, 0.4f, 0f), Quaternion.identity);
                    write = false;
                }

                if (penTime == 0 && (hit.collider.CompareTag("Collider_Brink") || hit.collider.CompareTag("Player")))  //主人公から線が引かれる場合
                {
                    penTime++;
                    Debug.Log("Aliceと当たりました。");
                    CharaSlide = true;
                    write = false;
                }
                else if (penTime == 0 && (hit.collider.CompareTag("floor") || hit.collider.CompareTag("enemy")))  //床から線が引かれる場合
                {
                    penTime++;
                    //Debug.Log("ブリンク発動準備");
                    BlinkIn = true;
                    BlinkPoint = hit.point;

                }
                else if (nowHit != hit.point && CharaSlide == false && (hit.collider.CompareTag("Collider_Brink") || hit.collider.CompareTag("Player")) && max > 0 && keep == false)  //線を引いてる途中でプレイヤーに当たった時
                {
                    Brink_Point.SetActive(true);
                    Brink_Point.transform.position = BlinkPoint;

                    penTime++;
                    BlinkOut = true;
                    Movetest.animator.SetInteger("isAttack", 5);

                    if (C_Effect == false)
                    {
                        EffectTest = true;
                        C_Effect = true;
                        Invoke("Effect",0.1f);
                    }
                    Debug.Log("ブリンク可能");
                }
                else if (nowHit != hit.point && CharaSlide == false && hit.collider.CompareTag("floor") && keep == false && sumi >= 1)  //線を床に引いてる
                {
                    C_Effect = false;
                    EffectTest = false;

                    BlinkOut = false;

                    Brink_Point.SetActive(false);

                    if (jumptrigger)
                    {
                        firstHit = hit.point;
                        jumptrigger = false;
                        //Debug.Log("初期値");
                        //Debug.Log(firstHit);
                    }


                    if (time < 1)
                    {
                        penTime++;
                        nowHit = hit.point;
                        Ray.Add(hit.point);
                        effect = true;
                        Instantiate(cube, hit.point, Quaternion.identity);
                        PointCount++;
                        max--;
                        BlinkOut = false;

                    }

                    //コンボ計算
                    if (hit.collider.CompareTag("enemy"))//&&フラッグを取得して二度当たらないようにする
                    {
                        combo++;
                    }

                }

                if (time >= 0.5 && (Mathf.Abs(firstHit.x - hit.point.x) < 0.1 && Mathf.Abs(firstHit.y - hit.point.y) < 0.1) && keep == false && max > Max -40 && sumi >= 1)
                {
                    //Debug.Log(Mathf.Abs(firstHit.x - hit.point.x));
                    keep = true;
                }

                if (keep == true && CharaSlide == false && sumi >= 1)
                {
                    Movetest.AttackLifecount = 0;
                    Movetest.animator.SetInteger("isAttack", 7);
                    
                    if (C_Effect == false)
                    {
                        EffectTest = true;
                        C_Effect = true;
                        Invoke("Effect", 0.6f);
                    }
                    count++;
                    if (jumparea == false)
                    {
                        Instantiate(area, firstHit, Quaternion.identity);

                        jumparea = true;
                        Debug.Log(firstHit);
                    }

                    if ((count / 100) * 3 >= 10)
                    {
                        scale = 10;
                    }
                    else if ((count / 100) * 3 <= 1)
                    {
                        scale = 1;
                    }
                    else
                    {
                        scale = (count / 100) * 3;
                    }

                }

            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Generater.BlinkIn == true && Generater.BlinkOut == true)
            {
                C_Effect = false;
                EffectTest = false;

                //PointCount = PointC;
                PointC = PointCount;
                Blink = true;
                penTime = 0;
                Debug.Log("ブリンクtrue");

                PlayerStatesScript.ComboCountTime = 0;
                PlayerStatesScript.ComboCPS = 0;

                Generater.BlinkIn = false;
                Generater.BlinkOut = false;

                Movetest.AttackLifecount = 0;
                Movetest.animator.SetInteger("isAttack", 6);
            }
            else
            {
                if (CharaSlide == false)
                {
                    PointC = PointCount;
                }

                PlayerStatesScript.ComboCountTime = 0;
                PlayerStatesScript.ComboCPS = 0;

                penTime = 0;
                PlayerCameraWalk3Script.rb.velocity = Vector3.zero;
                PlayerCameraWalk3Script.direction.x = 0;
                PlayerCameraWalk3Script.direction.z = 0;
                CharaSlide = false;
            }

            Brink_Point.SetActive(false);

            if (keep)
            {
                
                jump.transform.localScale = new Vector3(scale, scale, scale);
                Instantiate(jump, firstHit, Quaternion.identity);
                sumi = sumi - scale * 2;
                sumiSlider.value = sumi;

                Movetest.AttackLifecount = 0;
                Movetest.animator.SetInteger("isAttack", 8);
                C_Effect = false;
                EffectTest = false;
               
                //Debug.Log("墨残量:"+sumi);
            }
            else
            {
                float test = 0;
                test = Mathf.Ceil(((Max - max) / (Max / 10)));
                sumi = sumi - Mathf.Ceil(((Max - max) / (Max / 10)));
                sumiSlider.value = sumi;

                //Debug.Log("墨計算:" + test);
                //Debug.Log("墨残量:" + sumi);

            }

            if (Input.GetMouseButtonDown(0))
            {
                Ray.Clear();
            }

            nowHit = new Vector3(999, 999, 999);
            penTime = 0;
            max = Max;
            count = 0;
            Invoke("KeepFalse",0.2f);
            jumparea = false;
            jumptrigger = true;
            write = true;
            effect = false;
            time = 0;
            scale = 0;
            jump.transform.localScale = new Vector3(0, 0, 0);
        }


    }

    void Effect()
    {
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(ChargeEffect);
        // パーティクルの発生場所をPlayerタグのGameObjectの場所にする。
        newParticle.transform.position = GameObject.Find("Collider").transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
    }

    void KeepFalse()
    {
        keep = false;
    }
}