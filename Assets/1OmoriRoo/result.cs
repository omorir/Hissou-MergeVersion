using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class result : MonoBehaviour
{
    [SerializeField] GameObject result_parent;
    [SerializeField] Text TimeScoreText;
    [SerializeField] Text ScoreGradesText;
    [SerializeField] private CinemachineVirtualCamera vcam4_1;

    public static bool resultFlag = false; // Deadly2Ç≈trueÇ…Ç∑ÇÈ
    public static bool result_go = false;

    private bool Delay_text = false;

    private bool Grade_Delay = false;
    private float Grade_a = 0;

    // Start is called before the first frame update
    void Start()
    {
        resultFlag = false;
        result_go = false;
        result_parent.SetActive(false);
        Delay_text = false ;
        Grade_a = 0;
        ScoreGradesText.color = new Color(0, 0, 0, Grade_a);
        Grade_Delay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(vcam4_1.Priority == 13 && Delay_text == false)
        {
            StartCoroutine(vcamDelateMethod(result_parent));
            Delay_text = true;
        }

        if(Grade_Delay == true)
        {
            Grade_a += Time.deltaTime;
            ScoreGradesText.color = new Color(0, 0, 0, Grade_a);
        }
        if(Grade_a >= 1)
        {
            Grade_Delay = false;
        }
    }

    private IEnumerator vcamDelateMethod(GameObject res)
    {
        yield return new WaitForSeconds(2.0f);

        res.SetActive(true);
        TimeScoreText.text = "ì¢î∞éûä‘ÅF" + PlayerStatesScript.TimeCount.ToString("f2");

        yield return new WaitForSeconds(1.0f);
        ScoreMethod();
        Grade_Delay = true;
    }

    public void ScoreMethod()
    {
        if(WaveFlag.wave1fin == true)
        {
            if (PlayerStatesScript.TimeCount <= 50)
            {
                ScoreGradesText.text = "èG";
            }
            else if (50 < PlayerStatesScript.TimeCount && PlayerStatesScript.TimeCount <= 100)
            {
                ScoreGradesText.text = "óD";
            }
            else if (100 < PlayerStatesScript.TimeCount && PlayerStatesScript.TimeCount <= 150)
            {
                ScoreGradesText.text = "ó«";
            }
            else
            {
                ScoreGradesText.text = "â¬";
            }
        }
        else if(WaveFlag.wave2fin == true)
        {
            if (PlayerStatesScript.TimeCount <= 100)
            {
                ScoreGradesText.text = "èG";
            }
            else if (100 < PlayerStatesScript.TimeCount && PlayerStatesScript.TimeCount <= 150)
            {
                ScoreGradesText.text = "óD";
            }
            else if (150 < PlayerStatesScript.TimeCount && PlayerStatesScript.TimeCount <= 200)
            {
                ScoreGradesText.text = "ó«";
            }
            else
            {
                ScoreGradesText.text = "â¬";
            }
        }
        else if(WaveFlag.Boss_ED == true)
        {
            if (PlayerStatesScript.TimeCount <= 120)
            {
                ScoreGradesText.text = "èG";
            }
            else if (120 < PlayerStatesScript.TimeCount && PlayerStatesScript.TimeCount <= 240)
            {
                ScoreGradesText.text = "óD";
            }
            else if (240 < PlayerStatesScript.TimeCount && PlayerStatesScript.TimeCount <= 360)
            {
                ScoreGradesText.text = "ó«";
            }
            else
            {
                ScoreGradesText.text = "â¬";
            }
        }
    }

    public void OnClickContinueButton()
    {
        FixedCameraScript.winmotion_flag = false;
        resultFlag = false;
        result_go = true;
        result_parent.SetActive(false);
    }
    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
