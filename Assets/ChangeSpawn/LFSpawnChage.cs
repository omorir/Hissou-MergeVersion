using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LFSpawnChage : MonoBehaviour
{

    [SerializeField] GameObject SpawnLF;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //青ゾーンにはいったときの当たり判定

    void OnTriggerEnter(Collider other)
    {
        //プレイヤーかどうか判定
        if (other.gameObject.tag == "Player")
        {
            GameObject RF = GameObject.Find("RFSpawnPoint(Clone)");
            //GameObject LF = GameObject.Find("LFSpawnPoint(Clone)");
            GameObject RB = GameObject.Find("RBSpawnPoint(Clone)");
            GameObject LB = GameObject.Find("LBSpawnPoint(Clone)");
            GameObject C = GameObject.Find("CSpawnPoint(Clone)");
            GameObject CMF = GameObject.Find("CMFSpawnPoint(Clone)");
            GameObject CMB = GameObject.Find("CMBSpawnPoint(Clone)");
            GameObject CMR = GameObject.Find("CMRSpawnPoint(Clone)");
            GameObject CML = GameObject.Find("CMLSpawnPoint(Clone)");
            GameObject RFO = GameObject.Find("RFOSpawnPoint(Clone)");
            GameObject LFO = GameObject.Find("LFOSpawnPoint(Clone)");
            GameObject RBO = GameObject.Find("RBOSpawnPoint(Clone)");
            GameObject LBO = GameObject.Find("LBOSpawnPoint(Clone)");
            Destroy(RF);
            //Destroy(LF);
            Destroy(RB);
            Destroy(LB);
            Destroy(C);
            Destroy(CMF);
            Destroy(CMB);
            Destroy(CMR);
            Destroy(CML);
            Destroy(RFO);
            Destroy(LFO);
            Destroy(RBO);
            Destroy(LBO);

            //スポーンさせるオブジェクトの追加
            Instantiate(SpawnLF, new Vector3(0, 0, 0), SpawnLF.transform.rotation);
        }
        Debug.Log("青ゾーンはいった");
    }


   


}
