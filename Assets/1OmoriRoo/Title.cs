using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public static bool wave2_flag = false;
    public static bool Boss_flag = false;

    [SerializeField] CinemachineVirtualCamera vcam1;
    [SerializeField] CinemachineVirtualCamera vcam2;
    [SerializeField] CinemachineVirtualCamera vcam3;
    [SerializeField] CinemachineVirtualCamera vcam4;
    [SerializeField] CinemachineVirtualCamera vcam4_2;
    [SerializeField] CinemachineVirtualCamera vcam4_3;
    [SerializeField] CinemachineVirtualCamera vcam4_4;
    [SerializeField] CinemachineVirtualCamera vcam5;

    [SerializeField] GameObject wave2_obj;
    [SerializeField] GameObject waveBoss_obj;

    private bool ZoomOut = false;

    private int SousaNum = 0;
    private float SousaTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        vcam1.m_Lens.FieldOfView = 60;

        vcam1.Priority = 10;
        vcam2.Priority = 9;
        vcam3.Priority = 9;
        vcam4.Priority = 9;
        vcam5.Priority = 9;

        ZoomOut = false;

        wave2_obj.SetActive(false);
        waveBoss_obj.SetActive(false);

        Time.timeScale = 1;

        SousaNum = 0;
        SousaTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SousaTime);

        if(SousaTime <= 0)
        {
            SousaTime = 1;
        }
        else if(SousaTime < 1)
        {
            SousaTime -= Time.deltaTime;
        }

        if(vcam5.Priority == 13)
        {
            if(Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.Alpha2))
            {
                wave2_obj.SetActive(true);
            }
            else if(Input.GetKey(KeyCode.T) && Input.GetKeyDown(KeyCode.Alpha3))
            {
                waveBoss_obj.SetActive(true);
            }
        }

        if(ZoomOut == true)
        {
            vcam1.m_Lens.FieldOfView += Time.deltaTime * 6.25f;

            if (vcam1.m_Lens.FieldOfView >= 70)
            {
                vcam2.Priority = 11;
                ZoomOut = false;
                StartCoroutine(ZoomDelayMethod());
            }
        }

        if(wave2_flag == true)
        {
            wave2_obj.SetActive(true);
        }
        if(Boss_flag == true)
        {
            waveBoss_obj.SetActive(true);
        }
    }

    public void OnClickStartButton()
    {
        ZoomOut = true;
    }
    
    public void OnClickWave1Button()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OnClickWave2Button()
    {
        SceneManager.LoadScene("Stage1Wave2Scene");
    }
    public void OnClickBossButton()
    {
        SceneManager.LoadScene("Stage1BossScene");
    }

    public void OnClickSettingButton()
    {
        vcam3.Priority = 13;
    }

    public void OnClickSousaButton()
    {
        vcam4.Priority = 13;
    }

    public void OnClickBattleButton()
    {
        vcam5.Priority = 13;

        SousaNum = 0;
        vcam4.Priority = 9;
        vcam4_2.Priority = 9;
        vcam4_3.Priority = 9;
        vcam4_4.Priority = 9;
    }

    public void OnClickSousaBuckButton()
    {
        if (SousaTime == 1)
        {
            SousaNum--;
            SousaTime -= Time.deltaTime;
        }

        Sousa_SwitchMethod();
    }

    public void OnClickSousaNextButton()
    {
        if(SousaTime == 1)
        {
            SousaNum++;
            SousaTime -= Time.deltaTime;
        }

        Sousa_SwitchMethod();
    }

    public void Sousa_SwitchMethod()
    {
        switch (SousaNum)
        {
            case 0:
                vcam4.Priority = 13;
                vcam4_2.Priority = 9;
                vcam4_3.Priority = 9;
                vcam4_4.Priority = 9;
                break;
            case 1:
                vcam4.Priority = 9;
                vcam4_2.Priority = 13;
                vcam4_3.Priority = 9;
                vcam4_4.Priority = 9;
                break;
            case 2:
                vcam4.Priority = 9;
                vcam4_2.Priority = 9;
                vcam4_3.Priority = 13;
                vcam4_4.Priority = 9;
                break;
            case 3:
                vcam4.Priority = 9;
                vcam4_2.Priority = 9;
                vcam4_3.Priority = 9;
                vcam4_4.Priority = 13;
                break;
            case 4:
                vcam4.Priority = 9;
                vcam4_2.Priority = 9;
                vcam4_3.Priority = 9;
                vcam4_4.Priority = 9;
                break;
        }
    }

    public void OnClickBackButton()
    {
        if(vcam3.Priority == 13)
        {
            vcam3.Priority = 9;
        }
        else if (vcam4.Priority == 13)
        {
            vcam4.Priority = 9;
        }
        else if (vcam5.Priority == 13)
        {
            vcam5.Priority = 9;
        }
        else if (vcam2.Priority == 11)
        {
            vcam2.Priority = 9;
        }
    }

    public void OnClickExitButton()
    {
        Application.Quit();//ゲームプレイ終了
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private void Back(CinemachineVirtualCamera cam)
    {
        cam.Priority = 9;
    }

    private IEnumerator ZoomDelayMethod()
    {
        yield return new WaitForSeconds(1.0f);
        vcam1.m_Lens.FieldOfView = 60;
    }
}
