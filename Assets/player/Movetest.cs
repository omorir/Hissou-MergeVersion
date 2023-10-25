using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movetest : MonoBehaviour
{

    public static Animator animator;
    public static bool hit = false;
    public static int AttackLifecount = 0;

    private bool AttackFlag = false;
    public static bool SumiAttack = false;
    private bool Reset = false;
    private int AttackCount = 0;
    private int AttackMotion = 0;
    private int Jampcount = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        SumiAttack = false;
        hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (PlayerCameraWalk3Script.walk_motion == true)
        {
            if (PlayerCameraWalk3Script.run_motion == true)
            {
                animator.SetInteger("isRun", 2);

                AttackLifecount = 0;
            }
            else
            {
                animator.SetInteger("isRun", 1);

                
               
            }
        }
        else
        {
            //animator.SetInteger("isWalk", 0);
            animator.SetInteger("isRun", 0);


        }

        if (AttackLifecount > 0)
        {
            AttackLifecount--;
            if (AttackLifecount == 1) Debug.Log("�U���^�C���A�E�g");
        }
        else if (Reset)
        {
            animator.SetInteger("isAttack", 0);
            AttackCount = 0;
            Reset = false;
        }
        if (Input.GetKeyDown(KeyCode.L)) //�p�����Ԓ��ɒǉ����͂Ŏ��̃��[�V����
        {

            AttackMotion = AttackCount % 4 + 1;
            animator.SetInteger("isAttack", AttackMotion);
            Debug.Log("�U��:" + AttackMotion);
            AttackCount++;
            AttackLifecount = 300;
        }

        if (SumiAttack)
        {
            AttackMotion = AttackCount % 4 + 1;
            animator.SetInteger("isAttack", AttackMotion);
            Debug.Log("�U��:" + AttackMotion);
            AttackCount++;
            AttackLifecount = 300;
            Reset = true;
        }

        //���[�V�����f�o�b�N�p
        //���������L�[�ŕ���
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            // Wait����Walk�ɑJ�ڂ���
            animator.SetInteger("isRun", 1);


        }
        else
        {
            // Walk����Wait�ɑJ�ڂ���
            //animator.SetInteger("isWalk", 0);
        }
        //WASD�ő���
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            // Wait or Walk����Run�ɑJ�ڂ���
            animator.SetInteger("isRun", 2);


        }
        else
        {
            // Run����Wait or Walk�ɑJ�ڂ���
            //animator.SetInteger("isRun", 0);
        }
        if (Input.GetKeyDown(KeyCode.H) || hit == true)
        {
            //Wait or Walk����Hit�ɑJ�ڂ���
            animator.SetInteger("isHit", 1);
            hit = false;

        }
        else
        {
            // Hit����Wait or Walk�ɑJ�ڂ���
            animator.SetInteger("isHit", 0);

        }

        if (Input.GetKey(KeyCode.J) || FixedCameraScript.winmotion_flag == true)
        {
            //Wait ����Win1�ɑJ�ڂ���
            StartCoroutine(winmotionMethod());
        }
        else
        {
            // Win1����Wait�ɑJ�ڂ���
            animator.SetInteger("isWin1", 0);

        }
        if (Input.GetKeyDown(KeyCode.P))
        {

            Jampcount++;

            if (Jampcount >= 3)
            {
                Jampcount = 0;
            }

            animator.SetInteger("isRoll", Jampcount);
            //AttackLifecount = 300;
        }

    }

    private IEnumerator winmotionMethod()
    {
        yield return new WaitForSeconds(1.95f);

        if(WaveFlag.wave1fin == true)
        {
            animator.SetInteger("isWin1", 1);
        }
        else if(WaveFlag.wave2fin == true)
        {
            animator.SetInteger("isWin1", 3);
        }
        else if (WaveFlag.Boss_ED)
        {
            animator.SetInteger("isWin1", 2);
        }
    }

    void AttackStop()
    {
        SumiAttack = false;
        AttackLifecount = 0;
        AttackCount = 0;
        Reset = false;
    }
    void Hude()
    {
        HudeTrail.Hude = true;
    }
}