using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BardChase : MonoBehaviour
{
    //CharacterController Controller;
    Transform Target;
    GameObject Player;

    //�I�u�W�F�N�g�̃X�s�[�h
    public float Speed = 8f;

    //�㉺����X�s�[�h
    public float upspeed = 4f;
    public float downspeed = -1f;
    public float returnspeed = 1f;


    //�d������
    //���x�������鎞��
    public float waitTime = 2f;
    //�U���O�̍d��
    public float stanceTime = 3f;
    //�U�����[�V�����̎���
    public float attackTime = 1f;
    //�U����Ɋ��S�Ɏ~�܂鎞��
    public float stoppedTime = 0.5f;
    //�U����̍d��(�㏸)
    public float freezeTime = 0.5f;
    //�U���ォ��ҋ@�܂ł̎���
    public float roopTime = 2f;
    //�d��������������
    public float elapsedTime;
    public float atktime;
    public float stoptime;
    //�U����߂镝
    public float backmove = -0.5f;

    bool stop = false;

    bool found = false;
    public static bool despone = false;


    //�I�u�W�F�N�g�̍��G�͈�
    //�ǂ�������悤�ɂȂ鋗��
    [SerializeField] float chaseDistance = 15f;
    //�v���C���[�ɋ߂Â����Ƃ��Ɏ~�܂鋗��
    [SerializeField] float stayDistance = 3f;
    //�U�����鋗��
    [SerializeField] float waitDistance = 5f;

    public float foundDistance = 20f;


    public BirdMotionscript birdMotion;

    public EnemyStates EnemyDie; //narama

    //�v���C���[�������Ȃ���������ł��鎞��
    float desTime;
    public float desponeTime = 5f;

    //enemy�̏�ԑJ��
    float state = 0;

    //enemy���U�����Ă���߂�Ƃ��̎���
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

        //�v���C���[�^�O�̎擾
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

        //�v���C���[�̕���������

        if (found == false)
        {
            if (transform.position.z > 25 || transform.position.z < -25 || transform.position.x > 30 || transform.position.x < -30)
            {
                //�v���C���[�̕���������
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
                    Debug.Log("�U���O�d��");
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

    //�ǂ��������[�h
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



        //chaseDistance�͈͈̔ȉ�����stayDistance�͈͈̔ȏ�̏ꍇ���点��
        if (distance < chaseDistance && distance > stayDistance)
        {
            Debug.Log("�ǂ������Ă���");
            Speed = 1.5f;


            transform.position = transform.position + transform.forward * Speed * Time.deltaTime;
        }
        else
        {
            Speed = 0f;
        }
    }

    //�U���ҋ@���[�h
    void Wait()
    {
        Vector3 tarPos = Target.position;
        tarPos.y = transform.position.y;
        //�v���C���[�̕���������
        transform.LookAt(tarPos);

        Speed = 0f;


        elapsedTime += Time.deltaTime;

        if (transform.position.y > 1)
        {


            transform.position = transform.position + transform.up * downspeed * Time.deltaTime;
        }



        Debug.Log("���x��������");

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
        //�v���C���[�̕���������
        //transform.LookAt(tarPos);

        elapsedTime += Time.deltaTime;

        Debug.Log("�ːi�̍\��");

        if (elapsedTime > waitTime)
        {

            elapsedTime = 0f;
            state = 2;
            transform.tag = "enemy";
        }


    }

    //�U�����[�h
    void Attack()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //�v���C���[�̕���������
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

            //�U���d�����[�h�ڍs
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

    //�U���d�����[�h
    void freeze()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //�v���C���[�̕���������
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

        Debug.Log("�U���d����");

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

        //�v���C���[�̕���������
        transform.LookAt(tarPos);

        elapsedTime += Time.deltaTime;



        if (elapsedTime > roopTime)
        {
            elapsedTime = 0;
            state = 0;

        }
    }
}