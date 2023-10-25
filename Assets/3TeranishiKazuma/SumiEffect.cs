using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumiEffect : MonoBehaviour
{
    public ParticleSystem particle;//墨のエフェクト
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
            Debug.Log("エフェクト"+ effect);
            effect = false;
        }
    }

    void Effect()
    {
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(particle);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
    }
}
