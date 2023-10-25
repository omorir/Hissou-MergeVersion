using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    [SerializeField] GameObject PrefabA;
    [SerializeField] GameObject PrefabB;
    [SerializeField] GameObject PrefabC;
    [SerializeField] Transform rangeA;
    [SerializeField] Transform rangeB;
    [SerializeField] Transform rangeC;
    [SerializeField] Transform rangeD;



    private float time;
    private float count;
    private float spawn = 1f;
    private GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        time = time + Time.deltaTime;

        count = count + Time.deltaTime;


        if (time > spawn)
        {
            int n = Random.Range(0, 2);
            int m = Random.Range(0, 3);

            if (n == 0)
            {
                if (m == 0) { Enemy = PrefabA; }
                else if (m == 1) { Enemy = PrefabB; }
                else { Enemy = PrefabC; }
                float x = Random.Range(rangeA.position.x, rangeB.position.x);

                float y = Random.Range(rangeA.position.y, rangeB.position.y);

                float z = Random.Range(rangeA.position.z, rangeB.position.z);

                Instantiate(Enemy, new Vector3(x, y, z), PrefabA.transform.rotation);

                time = 0f;
                spawn = 5.0f;
            }

            else if (n == 1)
            {
                if (m == 0) { Enemy = PrefabA; }
                else if (m == 1) { Enemy = PrefabB; }
                else { Enemy = PrefabC; }
                float x = Random.Range(rangeC.position.x, rangeD.position.x);

                float y = Random.Range(rangeC.position.y, rangeD.position.y);

                float z = Random.Range(rangeC.position.z, rangeD.position.z);

                Instantiate(PrefabB, new Vector3(x, y, z), PrefabB.transform.rotation);

                time = 0f;
                spawn = 5.0f;
            }
        }
    }



}