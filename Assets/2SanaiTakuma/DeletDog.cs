using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletDog : MonoBehaviour
{
    public static int DogDeletCount = 0;
    void OnDestroy()
    {

        if (DogChase.despone == true)
        {
            return;
        }


        if (PlayerStatesScript.HP > 0)
        {
            DogDeletCount++;
            Debug.Log("犬削除" + DogDeletCount);
        }

        if (WaveFlag.wave1fin == true || WaveFlag.wave2fin == true)
        {
            if (PlayerStatesScript.HP < 0)
            {
                DogDeletCount = 0;
                Debug.Log("犬リセット" + DogDeletCount);
            }
        }


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}