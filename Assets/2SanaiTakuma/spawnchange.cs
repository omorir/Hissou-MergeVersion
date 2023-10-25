using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnchange : MonoBehaviour
{

    [SerializeField] GameObject SpawnPoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }


    void OnTriggerEnter(Collider other)
    {
        //プレイヤーかどうか判定
        if (other.gameObject.tag == "Player")
        {
            GameObject delete_spawnpoint = GameObject.FindWithTag("spawnpoint");

            Destroy(delete_spawnpoint.gameObject);

            //スポーンさせるオブジェクトの追加
            Instantiate(SpawnPoint, new Vector3(0, 0, 0), SpawnPoint.transform.rotation);
        }
    }


}
