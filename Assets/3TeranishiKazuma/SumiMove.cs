using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumiMove : MonoBehaviour
{
    public List<Vector3> SumiPoint = new List<Vector3>();//Rayの座標管理用リスト
    float speed=50f;
    bool AttackFlag = false;
   
    // Start is called before the first frame update
    void Start()
    {
        if (Generater.keep == false)
        {
            Movetest.SumiAttack = true;
        }
        AttackFlag = true;
        SumiPoint.Clear();

        for (int i = 0; i < RayPoint.Point.Count; i++)
        {
            SumiPoint.Add(RayPoint.Point[i]);
            //Debug.Log("ムーヴ"+SumiPoint[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
      if(AttackFlag)
        {
            Movetest.SumiAttack = false;
            AttackFlag = false;

        }
        StartCoroutine(Move());
        Invoke("Kill",1.6f);
    }
    void Kill()
    {
        Destroy(this.gameObject);
    }

    IEnumerator Move()
    {
      
        for (int i= 1; i<SumiPoint.Count; i++)
        {
            transform.position = Vector3.MoveTowards(transform.position, SumiPoint[i] + new Vector3(0f, 0.2f, 0f), speed);
            yield return new WaitForSeconds(0.001f);
        }
    }
}
