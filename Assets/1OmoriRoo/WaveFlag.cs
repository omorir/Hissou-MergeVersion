using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaveFlag : MonoBehaviour
{
    public static bool wave1 = false;
    public static bool wave2 = false;
    public static bool Boss_former = false;
    public static bool Boss_stage = false;
    public static bool Boss_ED = false;
    public static bool wave1fin = false;
    public static bool wave2fin = false;
    private Vector3 SavePosition;

    public static float tutotime = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        wave1 = false;
        wave2 = false;
        wave1fin = false;
        wave2fin = false;
        Boss_former = false;
        Boss_stage = false;
        Boss_ED = false;

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            wave1 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Stage1Wave2Scene")
        {
            wave2 = true;
        }
        else if (SceneManager.GetActiveScene().name == "Stage1BossScene")
        {
            Boss_former = true;
            Boss_stage = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();//ゲームプレイ終了
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }


        if (Input.GetKey(KeyCode.K) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerStatesScript.killcount += 3;
        }
        else if (Input.GetKey(KeyCode.K) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerStatesScript.killcount += 10;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && wave1 == true) 
        {
            PlayerStatesScript.killcount = 100;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && wave2 == true)
        {
            PlayerStatesScript.killcount = 150;
        }

    }


}
