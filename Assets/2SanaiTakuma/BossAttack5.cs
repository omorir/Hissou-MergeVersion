using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack5 : MonoBehaviour
{
    public float posmove_x = 0f, posmove_y = 10f, posmove_z = 0f, downpos_y = 1f;//posmove_y�͐^�񒆂Ɉړ�����Ƃ���y���W�Adownpos_y�͐^�񒆂���~������Ƃ���y���W

    public float posmovespeed;//�^�񒆂Ɉړ�����Ƃ��̃X�s�[�h

    public float downspeed;//�^�񒆂���n��ɍ~���Ƃ��̃X�s�[�h
    float downspeed_copy;

    public float downtime = 2f;//�^�񒆂���~���Ƃ��̎���
    float downcount;

    public float chargetime = 3f;//���ߍU���̗��ߎ���
    float chargecount;

    bool danger = false;
    [SerializeField] GameObject dangercircle;
    public static bool dangerdelet = false;


    public float _Velocity_0,sumicount, Degree;
    float PI = Mathf.PI;
    float _theta;
    [SerializeField] GameObject sumi;
    bool shot = false;

    public int state = 0;

    public static bool Attack5 = false;
    public bool Attack5_view = false;

    public float stantime = 4f;
    float stancount;
    public float stan_pos_y = 0f;
    public float stan_pos_speed = 0.1f;
    float stan_pos_speed_copy;


    public static bool stanhit = false;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;

        downspeed_copy = downspeed;
        stan_pos_speed_copy = stan_pos_speed;
    }

    // Update is called once per frame
    void Update()
    {
        Attack5_view = Attack5;
       
        if (Attack5 == false)
        {
            
            return;
        }

        switch (state)
        {
            case 0:
                positioning();
                break;
            case 1:
                descend();
                break;
            case 2:
                charge();
                break;
            default:
                sumishot();
                break;

        }

    }

    //�X�e�[�W�̂ǐ^�񒆂Ɉړ�
    void positioning()
    {
        Vector3 posmove = new Vector3(posmove_x, posmove_y, posmove_z);


        float centerdistance = Vector3.Distance(transform.position, posmove);

       // transform.position += transform.forward * posmovespeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, posmove, posmovespeed);

        if (centerdistance < 0.5f)
        {
            state = 1;
            danger = true;
        }

    }

    //�n��ɍ~���
    void descend()
    {
        downcount += Time.deltaTime;

        Vector3 downpos = new Vector3(posmove_x, downpos_y, posmove_z);


        float downposdistance = Vector3.Distance(transform.position, downpos);

        //transform.position += transform.up * downspeed_copy * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, downpos, downspeed_copy);


        if (danger == true)
        {
            Vector3 center = new Vector3(0, 0, 0);

            Instantiate(dangercircle, center, Quaternion.identity);

            TenguMotionScript.animator.SetInteger("isBossAttack", 8);

            danger = false;

        }


        if (downposdistance < 0.5f)
        {
            downspeed_copy = 0f;
        }

        if (downtime < downcount)
        {
            state = 2;
            
            downspeed_copy = downspeed;
            downcount = 0;

        }

    }

    //���ߐ��b��
    void charge()
    {
        if (stanhit == true)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
            TenguMotionScript.animator.SetInteger("isStan", 1);
            stan();
            return;
        }

        chargecount += Time.deltaTime;

        if (chargetime - 1.2f < chargecount)
        {
            TenguMotionScript.animator.SetInteger("isBossAttack", 9);
        }
        
        if (chargetime < chargecount)
        {
            state = 3;
            chargecount = 0;
            shot = true;
            dangerdelet = true;

        }

    }


    //���ˏ�ɋʂ��΂�
    void sumishot()
    {

        if (shot == true)
        {
         
            for (int i = 0; i < sumicount; i++)
            {
                float AngleRange = PI * (Degree / 180);

                if (sumicount > 1)
                {
                    _theta = (AngleRange / (sumicount - 1)) * i + 0.5f * (PI - AngleRange);
                }
                else
                {
                    _theta = 0.5f * PI;
                }

                GameObject sumi_obj = Instantiate(sumi, transform.position, Quaternion.identity);

                BossAttack5sumi sumi_cs = sumi_obj.GetComponent<BossAttack5sumi>();
                sumi_cs.theta = _theta;
                sumi_cs.Velocity_0 = _Velocity_0;


            }

            shot = false;
            state = 0;
            Attack5 = false;

            TenguMotionScript.animator.SetInteger("isBossAttack", 0);

        }
        else
        {
            Attack5 = false;
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
            Attack5 = false;
            state = 0;
            stancount = 0f;
            stanhit = false;
            chargecount = 0;
            stan_pos_speed_copy = stan_pos_speed;
            TenguMotionScript.animator.SetInteger("isStan", 0);
        }
    }

}