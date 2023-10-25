using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour
{

    //CharacterController Controller;
    Transform Target;
    GameObject Player;

    //�I�u�W�F�N�g�̃X�s�[�h
    public float speed;
    public float attackmove = 0.5f;

    //�d������
    public float waitTime = 5f;
    public float attackTime = 5f;
    public float freezeTime = 5f;
    public float elapsedTime;
    public float atktime;

    //�I�u�W�F�N�g�̍��G�͈�
    [SerializeField] float chaseDistance;
    [SerializeField] float stayDistance;
    [SerializeField] float waitDistance;
    [SerializeField] float attackDistance;


    float state = 0;

    //Enemy�̏��


    // Start is called before the first frame update
    void Start()
    {
        //�v���C���[�^�O�̎擾
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;




    }

    // Update is called once per frame
    void Update()
    {

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //�v���C���[�̕���������
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
                    Debug.Log("�U���O�d��");
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

    //�ǂ��������[�h
    void Chase()
    {

        float distance = Vector3.Distance(transform.position, Target.position);

        //chaseDistance�͈͈̔ȉ�����stayDistance�͈͈̔ȏ�̏ꍇ���点��
        if (distance < chaseDistance && distance > stayDistance)
        {
            Debug.Log("�ǂ������Ă���");
            speed = 1.5f;



            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        }
        else
        {
            speed = 0f;
        }
    }

    //�U���ҋ@���[�h
    void Wait()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //�v���C���[�̕���������
        transform.LookAt(tarPos);

        atktime = 0f;

        speed = 0f;



        elapsedTime += Time.deltaTime;

        if (elapsedTime > waitTime)
        {
            Debug.Log("�U��");
            state = 1;

        }

    }

    //�U�����[�h
    void Attack()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //�v���C���[�̕���������
        transform.LookAt(tarPos);

        elapsedTime = 0f;

        atktime += Time.deltaTime;

        speed = 5f;





        if (atktime > attackTime)
        {
            Vector3 attack = new Vector3(0, 0, attackmove);
            transform.Translate(attack);


            //�U���d�����[�h�ڍs
            Debug.Log("�U���d����");

            state = 2;

        }



    }

    //�U���d�����[�h
    void freeze()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //�v���C���[�̕���������
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
            //�U�����[�h�ڍs
            Debug.Log("�U��");
            state = 1;


        }



    }

}