using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Generater : MonoBehaviour
{
    public static int penTime = 0;
    public ParticleSystem ChargeEffect;//�������̃G�t�F�N�g
    public GameObject cube;//�O�Ձ@
    public GameObject kiseki; //�O�Ղ̐�
    public GameObject jump;//�W�����v�̃_���[�W�͈�
    public GameObject area;//�W�����v�̗\��͈�
    public static GameObject Sen;
    public static float time = 0;
    public float jikan = 0;
    public float Max = 100;
    public float max = 0;
    public static int scale = 0;
    public int count = 0;
    public int trigger = 200;
    public static int combo = 0;
    public static float sumi = 100;//�n�̎c��
    public static float sumiMax = 100;
    public Slider sumiSlider;
    public bool jumparea = false;
    public static bool keep = false;
    public static bool C_Effect = false;//�`���[�W�G�t�F�N�g���o�������ǂ���
    public bool EffectTest = false;
    public static bool CharaSlide = false;  //�y���^�u�ړ�
    public static bool Blink = false;  //�L�����N�^�[�X�N���v�g�Ƀu�����N�\���`����t���O
    public static bool BlinkIn = false;  //�ŏ��ɏ��Ɠ����������ǂ���
    public static bool BlinkOut = false;  //�Ō�Ɏ�l���ɓ����������ǂ���
    public static bool effect = false;
    public bool write = true; // kiseki�𐶐��������ǂ���
    public bool jumptrigger = true;
    public static Vector3 direction;
    public float rotateSpeed = 10.0F;
    public static Vector3 nowHit = new Vector3(0, 0, 0);//�����W
    public Vector3 firstHit = new Vector3(0, 0, 0);//�n�_
    public static Vector3 BlinkPoint = new Vector3(0, 0, 0);  //�u�����N��̍��W���L�����N�^�[�X�N���v�g�ɓ`����
    public static List<Vector3> Ray = new List<Vector3>();//Ray�̍��W�Ǘ��p���X�g

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

        if (Input.GetKey(KeyCode.Alpha1))   //�n�Q�[�W�񕜃R�}���h
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

        //if (CameraButtonScript.UPCameraWalkSwitch == true)            //�J�����ʂɐ��̍��W�𒲐�
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

                if (penTime == 0 && (hit.collider.CompareTag("Collider_Brink") || hit.collider.CompareTag("Player")))  //��l����������������ꍇ
                {
                    penTime++;
                    Debug.Log("Alice�Ɠ�����܂����B");
                    CharaSlide = true;
                    write = false;
                }
                else if (penTime == 0 && (hit.collider.CompareTag("floor") || hit.collider.CompareTag("enemy")))  //����������������ꍇ
                {
                    penTime++;
                    //Debug.Log("�u�����N��������");
                    BlinkIn = true;
                    BlinkPoint = hit.point;

                }
                else if (nowHit != hit.point && CharaSlide == false && (hit.collider.CompareTag("Collider_Brink") || hit.collider.CompareTag("Player")) && max > 0 && keep == false)  //���������Ă�r���Ńv���C���[�ɓ���������
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
                    Debug.Log("�u�����N�\");
                }
                else if (nowHit != hit.point && CharaSlide == false && hit.collider.CompareTag("floor") && keep == false && sumi >= 1)  //�������Ɉ����Ă�
                {
                    C_Effect = false;
                    EffectTest = false;

                    BlinkOut = false;

                    Brink_Point.SetActive(false);

                    if (jumptrigger)
                    {
                        firstHit = hit.point;
                        jumptrigger = false;
                        //Debug.Log("�����l");
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

                    //�R���{�v�Z
                    if (hit.collider.CompareTag("enemy"))//&&�t���b�O���擾���ē�x������Ȃ��悤�ɂ���
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
                Debug.Log("�u�����Ntrue");

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
               
                //Debug.Log("�n�c��:"+sumi);
            }
            else
            {
                float test = 0;
                test = Mathf.Ceil(((Max - max) / (Max / 10)));
                sumi = sumi - Mathf.Ceil(((Max - max) / (Max / 10)));
                sumiSlider.value = sumi;

                //Debug.Log("�n�v�Z:" + test);
                //Debug.Log("�n�c��:" + sumi);

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
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
        ParticleSystem newParticle = Instantiate(ChargeEffect);
        // �p�[�e�B�N���̔����ꏊ��Player�^�O��GameObject�̏ꏊ�ɂ���B
        newParticle.transform.position = GameObject.Find("Collider").transform.position;
        // �p�[�e�B�N���𔭐�������B
        newParticle.Play();
    }

    void KeepFalse()
    {
        keep = false;
    }
}