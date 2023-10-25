using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();//ゲームプレイ終了
        }
    }

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnClickExitButton()
    {
        Application.Quit();//ゲームプレイ終了
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnClickRePlayButton()
    {
        if (WaveFlag.wave1 == true)
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (WaveFlag.wave2 == true)
        {
            SceneManager.LoadScene("Stage1Wave2Scene");
        }
        else if (WaveFlag.Boss_stage == true)
        {
            SceneManager.LoadScene("Stage1BossScene");
        }
    }
}
