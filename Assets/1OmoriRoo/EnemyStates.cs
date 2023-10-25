using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStates : MonoBehaviour
{
    public ParticleSystem HitEffect;//�U���󂯂��̃G�t�F�N�g
    private float EnemyHP = 3;
    public int SumiHeal = 5;

    [SerializeField] GameObject ThisEnemy;
    [SerializeField] private GameObject hpSlider_obj;
    [SerializeField] private Slider hpSlider;

    [SerializeField] GameObject DieEffect;

    public static bool delate = false;

    private bool damageFlag = false;
    public static bool textdelayflag = false;
    int JumpAttack = 3;

    public bool EnemyDie = false; //narama

    float del_time_count; //narama
    public float del_time = 0.5f; //narama

    // Start is called before the first frame update
    void Start()
    {
        EnemyHP = hpSlider.maxValue;
        //AttackCount = 0;
        hpSlider.value = EnemyHP;
        //EnemyhpSlider.value = EnemyHP;

        textdelayflag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(hpSlider.value == hpSlider.maxValue)
        {
            hpSlider_obj.SetActive(false);
        }

        if(WaveFlag.Boss_ED == true)
        {
            Destroy(this.gameObject);
            return;
        }
        
        if (EnemyHP <= 0)
        {
            
            EnemyDie = true;

            del_time_count += Time.deltaTime;
            if (del_time_count > del_time) //����|�[�Y���s���Ă��玀�S����
            {
                Generater.sumi += SumiHeal;
                PlayerStatesScript.killcount++;
                Destroy(ThisEnemy);
                GameObject effect = Instantiate(DieEffect, transform.position, Quaternion.identity);
            }


            if (PlayerStatesScript.StartFlag == true && PlayerStatesScript.killcount == 3)
            {
                PlayerStatesScript.killcount = 0;
                tutorial.go = true;
            }
        }

        if(WaveFlag.wave1fin == true)
        {
            PlayerStatesScript.killcount++;
            Destroy(ThisEnemy);
        }
        else if(WaveFlag.wave2fin == true)
        {
            PlayerStatesScript.killcount++;
            Destroy(ThisEnemy);
        }

        if (Generater.AttackCount >= Generater.PointCount)
        {
            PlayerStatesScript.ComboFlag = false;
            if (Generater.Blink == false)
            {
                damageFlag = false;
                PlayerStatesScript.ComboCount = 0;
            }

            textdelayflag = true;
        }

        if(Generater.Blink == true)
        {
            PlayerStatesScript.ComboFlag = true;
        }
        else
        {
            PlayerStatesScript.ComboFlag = false;
        }

    }

    public void OnTriggerStay(Collider other)
    {
        if ( (other.gameObject.tag == "attack" || ((other.gameObject.tag == "Player" || other.gameObject.tag == "Collider_Brink") && Generater.Blink == true)) && PlayerStatesScript.ComboFlag == true && damageFlag == false)
        {
            Debug.Log("�������Ă��");
            Debug.Log("�R���{�t���O�F" + PlayerStatesScript.ComboFlag);
            Debug.Log("�_���[�W�t���O�F" + damageFlag); 
            Effect();//HitEffect����

            PlayerStatesScript.ComboCPS++;
            PlayerStatesScript.ComboCount = PlayerStatesScript.ComboCPS;
            //if (PlayerStatesScript.ComboCPS_Boss <= PlayerStatesScript.ComboCPS)
            //{
            //    PlayerStatesScript.ComboCPS_Boss = PlayerStatesScript.ComboCPS;
            //}

            if (PlayerStatesScript.ComboCount >= 5)
            {
                PlayerStatesScript.ComboCount = 5;
            }

            Debug.Log("ComboCount:" + PlayerStatesScript.ComboCount);
            EnemyHP -= PlayerStatesScript.ComboCount;
            hpSlider_obj.SetActive(true);
            hpSlider.value = EnemyHP;
            damageFlag = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "JumpAttack")
        {
            Effect();//HitEffect����
            EnemyHP -= JumpAttack;
            hpSlider_obj.SetActive(true);
            hpSlider.value = EnemyHP;
            Debug.Log("�W�����v�q�b�g:" + EnemyHP);
        }
    }

    void Effect()
    {
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
        ParticleSystem newParticle = Instantiate(HitEffect);
        // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
        newParticle.transform.position = this.transform.position;
        // �p�[�e�B�N���𔭐�������B
        newParticle.Play();
    }
}
