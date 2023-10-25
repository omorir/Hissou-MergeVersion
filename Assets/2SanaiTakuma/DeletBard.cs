using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletBard : MonoBehaviour
{
    public static int BardDeletCount = 0;
    void OnDestroy()
    {
        if (BardChase.despone == true)
        {
            return;
        }


        if (PlayerStatesScript.HP > 0)
        {
            BardDeletCount++;
            Debug.Log("���폜" + BardDeletCount);
        }

        if (WaveFlag.wave1fin == true || WaveFlag.wave2fin == true)
        {
            if (PlayerStatesScript.HP < 0)
            {
                BardDeletCount = 0;
                Debug.Log("�����Z�b�g" + BardDeletCount);
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