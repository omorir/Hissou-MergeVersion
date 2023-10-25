using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastDelete : MonoBehaviour
{

    Vector3 kero;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }

        kero = this.gameObject.transform.localScale; //◆現在の大きさを代入

        kero.x = Generater.scale;  //�A変数keroのx座標を1増やして代入
        kero.y = Generater.scale;
        kero.z = Generater.scale;

        this.gameObject.transform.localScale = kero; //�B大きさに変数keroを代入
    }
}
