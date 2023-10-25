using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack4 : MonoBehaviour
{
    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;
    [SerializeField] GameObject Sumi;
    [SerializeField] GameObject attackpoint;


    //public static Transform[] Sumipoint;

    //int ap;//attackpoint�̐�
    public static int point = 0;

    public static int state = 0;//�G�̍s��

    public float move_x, move_y, move_z;//�G����Z1���g�p���邽�߂Ɉړ�����ꏊ

    public float speed = 1f;
    float speedcopy;

    private float motioncount;//���[�V�����p


    public static int pointcount = 0;//�n�̗\���n�_�̌�
    public int Maxpointcount = 4;//�n�̗\���n�_�̍ő��
    public float pointspawn = 0;//�n�̗\���n�_���o������
    public float pointspawntime = 0.5f;//�n�̗\���n�_����o�����߂̎���

    public float rsvtime;//���ߍU���܂ł̗��ߎ���
    public float reservtime = 3f;//���ߎ��Ԃ̍ő厞��

    public float atktime;//�U�����鎞��
    public float attacktime = 4f;//�U������ő厞��


    public static bool pointchange;//�\���n�_����n�ɕύX����t���O

    public float changetime = 4f;//�n�𔭎˂��鎞��
    //public float sumichange = 1f;//�n�𔭎˂���ő�̎���

    float changecount;//�n�𔭎˂�����
    //int Maxchangecount;//���˂ł���n�̍ő吔�iMaxpointcount�Ɠ����j

    public static bool Attack4 = false;
    public bool Attack4_view = false;

    public float AttackPos_y;//�n���΂��Ƃ��̍���

    public float stantime = 4f;//�X�^���ő厞��//���[�V�����̕b��
    float stancount;
    public float stan_pos_y = 0f;
    public float stan_pos_speed = 0.1f;
    float stan_pos_speed_copy;

    public static bool stanhit = false;
    public static bool isStan = false;

    //���g���Ă鎞�͖n���΂�����4�{
    public int bunsin_Maxpointcount = 16;
    public float bunsin_pointspawntime = 0.1f;



    // Start is called before the first frame update
    void Start()
    {

        speedcopy = speed;
        stan_pos_speed_copy = stan_pos_speed;
    }

    // Update is called once per frame
    void Update()
    {
        Attack4_view = Attack4;

        if (Attack4 == false)
            return;

        //AttackPoint[ap] = attackpoint[ap];

        switch (state)
        {
            //�|�W�V�����ړ��˗��߁i3s�j�ˍU���i4s�j�ˌ��̈ʒu�ɖ߂�

            case 0:
                positioning();
                break;
            case 1:
                reservoir();
                break;
            case 2:
                attack();
                break;
            case 3:
                bunsin_reservoir();
                break;
            case 4:
                bunsin_attack();
                break;
            default:
                reposition();
                break;

        }


    }


    /// <summary>
    /// �|�W�V�����ړ�
    /// </summary>
    void positioning()
    {
        Vector3 position = new Vector3(move_x, move_y, move_z);
        //position.y = transform.position.y;

        float posdistance = Vector3.Distance(transform.position, position);

        speed = speedcopy;

        //transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, speed * 0.1f);

        if (posdistance < 0.1f)
        {
            speed = 0f;
            //���g���[�h�ɂ͂�������U�����@�̕ύX
            if (BossStates.Boss_wave2 == true)
            {
                state = 3;
            }
            else
            {
                state = 1;
            }
        }

    }


    /// <summary>
    /// ���ߍU���̗��߂Ă��鏈���i�R�b�j
    /// ���̂Ƃ��ɔ�΂��n�̗����n�_�̕\����������
    /// </summary>
    void reservoir()
    {

        if (stanhit == true)
        {//�X�^���̃��[�V����

            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }

        rsvtime += Time.deltaTime;

        //�\���n�_�ipointcount�j���ő�l(Maxpointcount)�i�S�j�ȉ��̏ꍇ���s
        if (pointcount < Maxpointcount)
        {

            pointspawn += Time.deltaTime;

            //pointspawntime(4s)�̊Ԋu�Ń����_���ʒu�ɗ\���n�_���o��
            if (pointspawn > pointspawntime)
            {
                float x = Random.Range(Point1.position.x, Point2.position.x);
                float y = Random.Range(Point1.position.y, Point2.position.y);
                float z = Random.Range(Point1.position.z, Point2.position.z);

                //AttackPoint[ap] = attackpoint[ap];
                //Instantiate(attackpoint[ap], new Vector3(x, y, z), attackpoint[ap].transform.rotation);
                Instantiate(attackpoint, new Vector3(x, y, z), attackpoint.transform.rotation);

                //ap++;
                pointspawn = 0f;

                pointcount++;
                //TenguMotionScript.animator.SetInteger("isBossAttack",1);
            }
        }

        //reservtime�i3s�j����������U�����[�h�ɕύX
        if (rsvtime > reservtime)
        {
            pointcount = 0;
            state = 2;
            rsvtime = 0f;
            //ap = 0;
        }

    }

    /// <summary>
    /// ���ߍU���̍U���̏���
    /// �S�b�łS�A�̖n���΂�
    /// </summary>
    void attack()
    {
        if (stanhit == true)
        {//�X�^���̃��[�V����
            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }



        if (BossAttack4Sumi.sumicount % 2 == 0)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 6);
        }
        else
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 7);
        }


        changecount += Time.deltaTime;

        if (changetime < changecount)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            changecount = 0;
            state = 5;
            BossAttack4Sumi.sumicount = 0;
        }

    }

    /// <summary>
    /// ���ߍU���̗��߂Ă��鏈���i�R�b�j
    /// ���̂Ƃ��ɔ�΂��n�̗����n�_�̕\����������
    /// </summary>
    void bunsin_reservoir()
    {


        if (stanhit == true)
        {//�X�^���̃��[�V����
            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            BunsinMotion3.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }

        rsvtime += Time.deltaTime;

        //�\���n�_�ipointcount�j���ő�l(Maxpointcount)�i�S�j�ȉ��̏ꍇ���s
        if (pointcount < bunsin_Maxpointcount)
        {

            pointspawn += Time.deltaTime;

            //pointspawntime(4s)�̊Ԋu�Ń����_���ʒu�ɗ\���n�_���o��
            if (pointspawn > bunsin_pointspawntime)
            {
                float x = Random.Range(Point1.position.x, Point2.position.x);
                float y = Random.Range(Point1.position.y, Point2.position.y);
                float z = Random.Range(Point1.position.z, Point2.position.z);

                //AttackPoint[ap] = attackpoint[ap];
                //Instantiate(attackpoint[ap], new Vector3(x, y, z), attackpoint[ap].transform.rotation);
                Instantiate(attackpoint, new Vector3(x, y, z), attackpoint.transform.rotation);

                //ap++;
                pointspawn = 0f;

                pointcount++;
                //TenguMotionScript.animator.SetInteger("isBossAttack", 1);
            }
        }

        //reservtime�i3s�j����������U�����[�h�ɕύX
        if (rsvtime > reservtime)
        {
            pointcount = 0;
            state = 4;
            rsvtime = 0f;
            //ap = 0;
        }

    }

    /// <summary>
    /// ���ߍU���̍U���̏���
    /// �S�b�łS�A�̖n���΂�
    /// </summary>
    void bunsin_attack()
    {
        if (stanhit == true)
        {//�X�^���̃��[�V����
            isStan = true;
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            BunsinMotion3.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }



        if (BossAttack4Sumi.sumicount % 2 == 0)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 6);
            BunsinMotion1.animator.SetInteger("isBossAttack", 6);
            BunsinMotion2.animator.SetInteger("isBossAttack", 6);
            BunsinMotion3.animator.SetInteger("isBossAttack", 6);
        }
        else
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 7);
            BunsinMotion1.animator.SetInteger("isBossAttack", 7);
            BunsinMotion2.animator.SetInteger("isBossAttack", 7);
            BunsinMotion3.animator.SetInteger("isBossAttack", 7);
        }


        changecount += Time.deltaTime;

        if (changetime < changecount)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            if(BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
            {
                BunsinMotion1.animator.SetInteger("isBossAttack", 0);
                BunsinMotion2.animator.SetInteger("isBossAttack", 0);
                BunsinMotion3.animator.SetInteger("isBossAttack", 0);
            }
            
            changecount = 0;
            state = 5;
            BossAttack4Sumi.sumicount = 0;
        }

    }

    /// <summary>
    /// ���̈ʒu�ɖ߂�
    /// </summary>
    void reposition()
    {
        //�����ő�Z1�t���O��؂�
        Attack4 = false;
        state = 0;
        TenguMotionScript.animator.SetInteger("isBossAttack", 0);
        if (BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
        {
            BunsinMotion1.animator.SetInteger("isBossAttack", 0);
            BunsinMotion2.animator.SetInteger("isBossAttack", 0);
            BunsinMotion3.animator.SetInteger("isBossAttack", 0);
        }
        
    }

    /// <summary>
    /// ���ߒ��ɍU����H������Ƃ��ɃX�^��
    /// </summary>
    void stan()
    {

        stancount += Time.deltaTime;

        //
        Vector3 stan_pos = new Vector3(transform.position.x, stan_pos_y, transform.position.z);

        float stan_distance = Vector3.Distance(transform.position, stan_pos);

        transform.position = Vector3.MoveTowards(transform.position, stan_pos, stan_pos_speed_copy);

        if (stan_distance < 0.1f)
        {
            stan_pos_speed_copy = 0f;
        }

        if (stancount > stantime)
        {
            //�X�^�������炻�̍U���͏I��
            Attack4 = false;
            state = 0;
            stancount = 0f;
            pointspawn = 0f;
            pointcount = 0;
            rsvtime = 0f;
            stanhit = false;
            isStan = false;
            changecount = 0f;
            stan_pos_speed_copy = stan_pos_speed;
            BossAttack4Sumi.sumicount = 0;
            TenguMotionScript.animator.SetInteger("isStan", 0);
        }
    }

}
