using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
   
    public GameObject cube;//当たり判定のオブジェクト

    public int pointTime;
    public float delateTime = 5;

    bool on = true;
   

    Vector3 point = new Vector3(999, 999, 999);
    Vector3 delete = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        pointTime = Generater.penTime;
        if (Generater.nowHit != point)
        {
            if (Generater.nowHit != delete)
            {
                point = Generater.nowHit;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0) && on)
        {
            StartCoroutine("Spawn");

            Destroy(this.gameObject, delateTime);
            on = false;
        }

        
    }

    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(pointTime / 400f);

        Generater.AttackCount++;
        if (Generater.AttackCount <= Generater.PointCount)
        {
            PlayerStatesScript.ComboFlag = true;
        }

        if (Generater.Blink == false)
        {
            Instantiate(cube, point, Quaternion.identity);

        }
    }

}