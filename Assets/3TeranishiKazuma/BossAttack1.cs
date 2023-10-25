using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : MonoBehaviour
{
    [SerializeField] GameObject Boss;

    public GameObject LinePrefab;//linerenderer
    public GameObject SumiPrefab;//�n�G�t�F�N�g
    public GameObject Attack;//�����蔻��
    public static GameObject Prefab;

    public static Vector3[] positions;

    public static bool style;

    Quaternion Vrot = Quaternion.Euler(90, 90, 0);
    Quaternion Hrot = Quaternion.Euler(90, 0, 0);
    Quaternion ATKrot = Quaternion.Euler(0, 90, 0);

    public static Transform playerT;
    public static Transform prefabT;
    public static  Vector3 AttackT;

    public bool Sumi=true;

    public static bool atk = false;
    public static bool Vatk = true;
    public static bool Hatk = false;
    private bool AtkFlag=true;

    private float bossHP;

    private Vector3 startPosition, targetPosition;
    private float startTime, distance;
    private bool atkMove = false;

    private int BAttackCount = 0;
    private int BAttackMotion = 0;

    private int LineSpace = 7;
    public static int AttackCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        atkMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown((KeyCode.I)))
        {
            atk = true;
        }
        if (Input.GetKeyDown((KeyCode.O)))
        {
            atk = false;
        }

        if (atk)
        {
            
            if (atkMove == false)
            {
                startPosition = Boss.transform.position;
                targetPosition = new Vector3(0, 1.25f, 15);
                distance = Vector3.Distance(startPosition, targetPosition);

                bossHP = BossStates.Boss_HP;

                atkMove = true;
            }
            if(Boss.transform.position != targetPosition)
            {
                //���݃t���[���̕�Ԓl���v�Z
                float interpolatedValue = (Time.time - startTime) / distance;
                //�ړ�������
                transform.position = Vector3.Lerp(startPosition, targetPosition, interpolatedValue);

                return;
            }

            if(BossStates.Boss_HP <= bossHP - 5.0f || AttackCount < 1)
            {
                atk = false;
                atkMove = false;

                BAttackCount = 0;
                BAttackMotion = 0;
                TenguMotionScript.animator.SetInteger("isBossAttack", 0);//�I���ۂɑҋ@���[�V�����ɖ߂�
                if (BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
                {
                    BunsinMotion1.animator.SetInteger("isBossAttack", 0);
                    BunsinMotion2.animator.SetInteger("isBossAttack", 0);
                    BunsinMotion3.animator.SetInteger("isBossAttack", 0);
                }
               
                Invoke("Countreset", 3.0f);
            }

            if (AttackCount >= 1)
            {
                if (BossStates.Boss_wave2 == false && BossStates.Boss_wave3 == false)
                {
                    if (Vatk && Hatk == false)
                    {
                        AtkFlag = false;
                        Vatk = false;
                        style = true;
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        playerT = player.transform;
                        Vertical(playerT.position);
                        AttackT = playerT.position;
                        StartCoroutine(VAttack(AttackT));

                        //TenguMotionScript.animator.SetInteger("isBossAttack", BAttackMotion);
                        //Debug.Log("�V�烂�[�V����:" + BAttackMotion);
                        Invoke("VSumi", 3.0f);
                    }
                    else if (Hatk && Vatk == false)
                    {
                        AtkFlag = false;
                        Vatk = true;
                        style = false;
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        playerT = player.transform;
                        Horizon(playerT.position);
                        AttackT = playerT.position;
                        StartCoroutine(HAttack(AttackT));

                        //TenguMotionScript.animator.SetInteger("isBossAttack", BAttackMotion);
                        //Debug.Log("�V�烂�[�V����:" + BAttackMotion);
                        Invoke("HSumi", 3.0f);
                    }

                }
                else
                {
                    if (Vatk && Hatk == false)
                    {
                        Vatk = false;
                        style = true;
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        playerT = player.transform;
                        Vertical(playerT.position);
                        AttackT = playerT.position;
                        StartCoroutine(VAttack(AttackT));

                        AttackT.x = AttackT.x + LineSpace;
                        Vertical(AttackT);
                        StartCoroutine(VAttack(AttackT));

                        AttackT.x = AttackT.x + LineSpace;
                        Vertical(AttackT);
                        StartCoroutine(VAttack(AttackT));

                        AttackT.x = AttackT.x - LineSpace * 3;
                        Vertical(AttackT);
                        StartCoroutine(VAttack(AttackT));

                        AttackT.x = AttackT.x - LineSpace;
                        Vertical(AttackT);
                        StartCoroutine(VAttack(AttackT));

                        //TenguMotionScript.animator.SetInteger("isBossAttack", BAttackMotion);
                        //Debug.Log("�V�烂�[�V����:" + BAttackMotion);
                        Invoke("VSumi", 3.0f);
                    }
                    else if (Hatk && Vatk == false)
                    {
                        Vatk = true;
                        style = false;
                        GameObject player = GameObject.FindGameObjectWithTag("Player");
                        playerT = player.transform;
                        Horizon(playerT.position);
                        AttackT = playerT.position;
                        StartCoroutine(HAttack(AttackT));

                        AttackT.z = AttackT.z + LineSpace;
                        Horizon(AttackT);
                        StartCoroutine(HAttack(AttackT));

                        AttackT.z = AttackT.z + LineSpace;
                        Horizon(AttackT);
                        StartCoroutine(HAttack(AttackT));

                        AttackT.z = AttackT.z - LineSpace * 3;
                        Horizon(AttackT);
                        StartCoroutine(HAttack(AttackT));

                        AttackT.z = AttackT.z - LineSpace;
                        Horizon(AttackT);
                        StartCoroutine(HAttack(AttackT));

                        //TenguMotionScript.animator.SetInteger("isBossAttack", BAttackMotion);
                        //Debug.Log("�V�烂�[�V����:" + BAttackMotion);
                        Invoke("HSumi", 3.0f);
                    }
                }
            }

        }
        
        

    }    

    void Vertical(Vector3 x)
    {
       
        positions = new Vector3[]
        {
        new Vector3(34, 0, 0),               // �J�n�_
        new Vector3(0, 0, 0),               // ���ԓ_
        new Vector3(-34, 0, 0),              // �I���_
        };

        GameObject line = Instantiate(LinePrefab, new Vector3(x.x, 0.1f, 0), Vrot) as GameObject;//line����

        LineRenderer lineL = line.GetComponent<LineRenderer>();//line�R���|�[�l���g�擾

        lineL.SetPositions(positions);
    }

    void Horizon(Vector3 x)
    {
        
        positions = new Vector3[]
        {
        new Vector3(34, 0, 0),               // �J�n�_
        new Vector3(0, 0, 0),               // ���ԓ_
        new Vector3(-34, 0, 0),              // �I���_
        };

        GameObject line = Instantiate(LinePrefab, new Vector3(0, 0.1f, x.z), Hrot) as GameObject;//line����

        LineRenderer lineL = line.GetComponent<LineRenderer>();//line�R���|�[�l���g�擾

        lineL.SetPositions(positions);
        
    }

    IEnumerator VAttack(Vector3 x)
    {
        yield return new WaitForSeconds(1.5f);

        GameObject Sumi = Instantiate(SumiPrefab, new Vector3(x.x, 0.1f, 34), Quaternion.identity);//Sumi����
        GameObject ATK = Instantiate(Attack, new Vector3(x.x, 1f, 0), Quaternion.identity);//�����蔻�萶��

        BAttackMotion = BAttackCount % 4 + 1;//�U�����[�V����1�`4�v�Z
        TenguMotionScript.animator.SetInteger("isBossAttack", BAttackMotion);
        if (BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
        {
            BunsinMotion1.animator.SetInteger("isBossAttack", BAttackMotion);
            BunsinMotion2.animator.SetInteger("isBossAttack", BAttackMotion);
            BunsinMotion3.animator.SetInteger("isBossAttack", BAttackMotion);
        }
        

        Debug.Log("�V�烂�[�V����:" + BAttackMotion);

        BAttackCount++;//���[�V�����l+1
    }

    IEnumerator HAttack(Vector3 x)
    {
        yield return new WaitForSeconds(1.5f);

        GameObject Sumi = Instantiate(SumiPrefab, new Vector3(34, 0.1f, x.z), Quaternion.identity);//Sumi����
        GameObject ATK = Instantiate(Attack, new Vector3(0, 1f, x.z), ATKrot);//�����蔻�萶��

        BAttackMotion = BAttackCount % 4 + 1;//�U�����[�V����1�`4�v�Z
        TenguMotionScript.animator.SetInteger("isBossAttack", BAttackMotion);
        if (BossStates.Boss_wave2 == true || BossStates.Boss_wave3 == true)
        {
            BunsinMotion1.animator.SetInteger("isBossAttack", BAttackMotion);
            BunsinMotion2.animator.SetInteger("isBossAttack", BAttackMotion);
            BunsinMotion3.animator.SetInteger("isBossAttack", BAttackMotion);
        }

        Debug.Log("�V�烂�[�V����:" + BAttackMotion);

        BAttackCount++;//���[�V�����l+1
    }

    void VSumi()
    {

        Hatk = true;
        AtkFlag = true;
        AttackCount--;
        Debug.Log("�A�^�b�N�J�E���g:" + AttackCount);
    }

    void HSumi()
    {

        Hatk = false;
        AtkFlag = true;
        AttackCount--;
        Debug.Log("�A�^�b�N�J�E���g:" + AttackCount);
    }

    void Countreset()
    {
        AttackCount = 10;
    }
}
