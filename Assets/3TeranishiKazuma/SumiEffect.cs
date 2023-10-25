using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumiEffect : MonoBehaviour
{
    public ParticleSystem particle;//�n�̃G�t�F�N�g
    public bool effect = false;

    // Start is called before the first frame update
    void Start()
    {
        effect = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (effect)
        {
            Invoke("Effect", 1.4f);
            Debug.Log("�G�t�F�N�g"+ effect);
            effect = false;
        }
    }

    void Effect()
    {
        // �p�[�e�B�N���V�X�e���̃C���X�^���X�𐶐�����B
        ParticleSystem newParticle = Instantiate(particle);
        // �p�[�e�B�N���̔����ꏊ�����̃X�N���v�g���A�^�b�`���Ă���GameObject�̏ꏊ�ɂ���B
        newParticle.transform.position = this.transform.position;
        // �p�[�e�B�N���𔭐�������B
        newParticle.Play();
    }
}
