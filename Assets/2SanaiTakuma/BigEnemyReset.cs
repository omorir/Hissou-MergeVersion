using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStatesScript.HP < 1 || WaveFlag.wave1fin == true || WaveFlag.wave2fin == true)
        {
            DeletBard.BardDeletCount = 0;
            DeletDog.DogDeletCount = 0;
            DeletMonkey.MonkeyDeletCount = 0;

            Debug.Log(DeletBard.BardDeletCount + "削除カウントリセットしています");
        }
    }
}
