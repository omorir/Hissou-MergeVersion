using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayPoint : MonoBehaviour
{
    public static List<Vector3> Point = new List<Vector3>();//Ray�̍��W�Ǘ��p���X�g
    public GameObject sumi;//�O�Ձ@
    Quaternion rot = Quaternion.Euler(0f, 0f, 0f);//�n�G�t�F�N�g�̌���

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

            Point.Clear();//���X�g�̏���

            for (int i = 0; i < Generater.Ray.Count; i++)
            {
                Point.Add(Generater.Ray[i]);
                //Debug.Log(Point[i]);
            }

            Instantiate(sumi, Point[0] + new Vector3(0f, 0.4f, 0f), rot);

        }
    }
}
