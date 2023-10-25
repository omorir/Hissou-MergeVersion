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
    float elapsedTime = 0f;  //�o�ߎ���
    float rate;
    private float boss_movetime = 0f;

    private Transform player;

    public AudioClip sound2;
    AudioSource walkAudio;

    Vector3 mousePos, worldPos, WorldPlayerPos;

    public float testscoreX,testscoreZ;

    private Vector3 latestPos;  //�O���Position

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

        animator = GetComponent<Animator>(); //�A�j���[�^�[�̎擾

        rb = GetComponent<Rigidbody>();�@//���W�b�g�{�f�B�[�̎擾

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

        if (WaveFlag.Boss_former == true)  //�{�XOP
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Vector3 pos = player.position;
                pos.z = -17.0f;    // z���W��17��
                player.position = pos;  // ���W��ݒ�
            }

            if (boss_movetime <= 11.0f)
            {
                Debug.Log("���ɂ����Ă�");

                boss_movetime += Time.deltaTime;
                Vector3 pos = player.position;
                pos.z += 3.0f * Time.deltaTime;    // z���W�։��Z3.0
                player.position = pos;  // ���W��ݒ�
            }
            return;
        }

        if (Generater.Blink == true)  // �u�����N�I�I�I
        {
            if (elapsedTime < 1.0f)  // �����Ɍ��炸1�b�Ԃňړ�����
            {
                PlayerStatesScript.MutekiB = true;

                //Debug.Log(elapsedTime);
                elapsedTime += Time.deltaTime;  // �o�ߎ��Ԃ̉��Z
                rate = Mathf.Clamp01(elapsedTime / 2.0f);   // �����v�Z
                player.transform.position = Vector3.Lerp(player.transform.position, Generater.BlinkPoint, rate); //�ړ�

                Vector3 diff = transform.position - latestPos;   // �x�N�g���Ŏ擾
                latestPos = transform.position;  // �X�V
                transform.rotation = Quaternion.LookRotation(diff); // ������ύX����
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

        // ��������L��������(�}�E�X�AWASD)

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Generater.CharaSlide == true)                                  // [�}�E�X�N���b�N�ɂ��L�����ړ�]
        {
            mousePos = Input.mousePosition;

            if (CameraButtonScript.UPCameraWalkSwitch == true) // �e�J��������Ƀ}�E�X���W�𒲐�
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

            if (WaveFlag.Boss_stage == true)  // �{�X�p�̃J��������Ƀ}�E�X���W�𒲐�
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


            testscoreX = direction.x; //�f�o�b�O
            testscoreZ = direction.z;


            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotateSpeed);

            //Vector3 velocity = direction;  //�L�����N�^�[�R���g���[���[�p
            //velocity.y -= gravity;
            //velocity = transform.transform.TransformDirection(velocity);
            //controller.Move(velocity);

            if (WaveFlag.Boss_stage == true && vcam1_1.Priority == 11)
            {
                //direction.x = (worldPos.x - WorldPlayerPos.x) / 10;
                rb.AddForce(direction.x, 0, direction.y); //���W�b�g�{�f�B�[�p
            }
            else
            {
                if (testscoreX <= -walk_run_switch || testscoreX >= walk_run_switch || testscoreZ <= -walk_run_switch || testscoreZ >= walk_run_switch)  // ���郂�[�V�����ɐ؂�ւ�
                {
                    run_motion = true;
                }
                else if (testscoreX >= -walk_run_switch || testscoreX <= walk_run_switch || testscoreZ >= -walk_run_switch || testscoreZ <= walk_run_switch)  // �������[�V�����ɐ؂�ւ�
                {
                    walk_motion = true;
                    run_motion = false;
                }

                rb.AddForce(direction.x, 0, direction.z); //���W�b�g�{�f�B�[�p
            }

            Vector3 diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
            latestPos = transform.position;  //�O���Position�̍X�V

            //�x�N�g���̑傫����0.01�ȏ�̎��Ɍ�����ς��鏈��������
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(diff); //������ύX����
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


        //�A�i���O�X�e�B�b�N�̃O������z�肵�ā}0.01�ȉ����͂���
        if (Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 0.1F)                    // [�L�[�{�[�h�ƃX�e�B�b�N�ɂ��L�����ړ�]
        {
            Debug.Log("WASD�ړ�");

            Vector3 camToPlayer = player.position - Camera.main.transform.position;
            float inputAngle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, inputAngle, 0);

            speedmain = speed;
            direction = targetRotation * Vector3.forward * speedmain;
            player.rotation = Quaternion.Slerp(player.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * rotateSpeed);

            rb.AddForce(direction.x, 0, direction.z);

            Vector3 diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
            latestPos = transform.position;  //�O���Position�̍X�V

            //�x�N�g���̑傫����0.01�ȏ�̎��Ɍ�����ς��鏈��������
            if (diff.magnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(diff); //������ύX����
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
            //this.animator.SetBool(key_isRun, false); // ���X�e�B�b�N���~�߂���Run�A�j���[�^�[���I�t
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