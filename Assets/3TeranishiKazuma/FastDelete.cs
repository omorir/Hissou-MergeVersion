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

        kero = this.gameObject.transform.localScale; //�����݂̑傫������

        kero.x = Generater.scale;  //�A�ϐ�kero��x���W��1���₵�đ��
        kero.y = Generater.scale;
        kero.z = Generater.scale;

        this.gameObject.transform.localScale = kero; //�B�傫���ɕϐ�kero����
    }
}
