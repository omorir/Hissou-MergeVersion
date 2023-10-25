using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawnScript : MonoBehaviour
{
    [SerializeField] GameObject Dog;
    [SerializeField] GameObject Bard;
    [SerializeField] GameObject Monkey;
    [SerializeField] GameObject BigDog;
    [SerializeField] GameObject BigBard;
    [SerializeField] GameObject BigMonkey;
    [SerializeField] Transform rangeA;
    [SerializeField] Transform rangeB;
    [SerializeField] Transform rangeC;
    [SerializeField] Transform rangeD;

    public float spawn = 0.5f;
    public float Respawn = 5f;
    public float Bigencount = 5f;

    private GameObject Enemy;
    private float time;
    private float count;
    private float Dogcount;
    private float Bardcount;
    private float Monkeycount;

    public float wave1_Bigencount = 50f;
    public float wave2_Bigencount = 5f;

    [SerializeField] GameObject SpawnEffect;


    // Start is called before the first frame update
    void Start()
    {
        if (WaveFlag.wave1 == true)
        {
            Bigencount = wave1_Bigencount;
        }
        if (WaveFlag.wave2 == true)
        {
            Bigencount = wave2_Bigencount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStatesScript.StartFlag == true || WaveFlag.Boss_former == true || WaveFlag.Boss_stage == true || WaveFlag.wave1fin == true || WaveFlag.wave2fin == true || WaveFlag.Boss_ED == true)
        {
            return;
        }

        Dogcount = DeletDog.DogDeletCount / Bigencount;
        Bardcount = DeletBard.BardDeletCount / Bigencount;
        Monkeycount = DeletMonkey.MonkeyDeletCount / Bigencount;

        time = time + Time.deltaTime;

        count = count + Time.deltaTime;


        if (time < spawn)
        {
            return;
        }
        int range = Random.Range(0, 2);
        int mob = Random.Range(0, 3);

        float x, y, z;

        switch (range)
        {
            case 0:
                x = Random.Range(rangeA.position.x, rangeB.position.x);

                y = Random.Range(rangeA.position.y, rangeB.position.y);

                z = Random.Range(rangeA.position.z, rangeB.position.z);
                break;

            default:
                x = Random.Range(rangeC.position.x, rangeD.position.x);

                y = Random.Range(rangeC.position.y, rangeD.position.y);

                z = Random.Range(rangeC.position.z, rangeD.position.z);
                break;
        }
        switch (mob)
        {
            case 0:
                DogSpawn();
                break;
            case 1:
                BardSpawn();
                break;
            default:
                MonkeySpawn();
                break;
        }

        Instantiate(Enemy, new Vector3(x, y, z), Enemy.transform.rotation);
        Instantiate(SpawnEffect, new Vector3(x, y, z), Enemy.transform.rotation);

        time = 0f;
        spawn = Respawn;

    }

    void DogSpawn()
    {
        int Big = Random.Range(1, 11);

        if (Big < Dogcount)
        {
            Debug.Log("大きい犬出現");
            Enemy = BigDog;
        }
        else
        {
            Enemy = Dog;
        }
    }
    void BardSpawn()
    {
        int Big = Random.Range(1, 11);

        if (Big < Bardcount)
        {

            Debug.Log("大きい鳥出現");
            Enemy = BigBard;
        }
        else
        {
            Enemy = Bard;
        }
    }
    void MonkeySpawn()
    {
        int Big = Random.Range(1, 11);

        if (Big < Monkeycount)
        {
            Debug.Log("大きいサル出現");
            Enemy = BigMonkey;
        }
        else
        {
            Enemy = Monkey;
        }
    }

}