using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletMonkey : MonoBehaviour
{
    public static int MonkeyDeletCount = 0;
    void OnDestroy()
    {

        if (monkey.despone == true)
        {
            Debug.Log("���̉��͍폜�J�E���g����܂���");
            return;
        }

        if (PlayerStatesScript.HP > 0)
        {
            MonkeyDeletCount++;
            Debug.Log("���폜" + MonkeyDeletCount);
        }
        if (WaveFlag.wave1fin == true || WaveFlag.wave2fin == true)
        {
            if (PlayerStatesScript.HP < 0)
            {
                MonkeyDeletCount = 0;
                Debug.Log("�����Z�b�g" + MonkeyDeletCount);
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