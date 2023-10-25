using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    [SerializeField] Image timeImage;
    [SerializeField] GameObject TutorialCheckText;
    [SerializeField] GameObject TutorialText;
    [SerializeField] Text Text;
    [SerializeField] GameObject dog;
    [SerializeField] GameObject SousaSetumei_1;
    [SerializeField] GameObject SousaSetumei_2;


    private bool tutorialCheck = true;
    public static bool tutorialFlag = false;
    public static bool tutorialcount = false;
    public static bool go = false;
    private int tut = 0;

    public float tutorial_second = 5;
    public float tutorial_second_78 = 8;

    // Start is called before the first frame update
    void Start()
    {
        tutorialCheck = true;
        tutorialFlag = false;
        TutorialCheckText.SetActive(false);
        TutorialText.SetActive(false);
        timeImage.fillAmount = 1;

        SousaSetumei_1.SetActive(false);
        SousaSetumei_2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            go = true;
        }

        if (WaveFlag.wave1 == true && PlayerStatesScript.StartFlag == true && tutorialCheck == true) //Wave1かつスタート位置の時のみチュートリアルが表示される
        {
            Time.timeScale = 0;
            TutorialCheckText.SetActive(true);
        }

        if (tutorial.tutorialcount == true)
        {
            if (tut == 6 || tut == 7)
            {
                timeImage.fillAmount -= (Time.deltaTime / tutorial_second_78);
            }
            else
            {
                timeImage.fillAmount -= (Time.deltaTime / tutorial_second);
            }
        }

        if (tutorialFlag == true)                                      // チュートリアル開始！！！
        {
            tutorialMethod();
        }
    }

    public void OnClickYesButton()  //はい を押すとチュートリアルに移行
    {
        Time.timeScale = 1;
        TutorialCheckText.SetActive(false);
        TutorialText.SetActive(true);
        tutorialCheck = false;
        tutorialFlag = true;
        tut = 1;
    }

    public void OnClickNoButton()  //いいえ を押すとそのまま続行
    {
        gatescript.isOpen = true;
        Time.timeScale = 1;
        TutorialCheckText.SetActive(false);
        tutorialCheck = false;
    }

    private void tutorialMethod()
    {
        switch (tut)
        {
            case 1:
                TextMethod("それでは\nチュートリアルを始めます。");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 2:
                TextMethod("まずは主人公をタッチして\n動かしてみよう！");

                TimeDelayMethod();

                if (Generater.CharaSlide == true)
                {
                    go = true;
                }
                break;

            case 3:
                TextMethod("次は敵を攻撃してみよう！");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 4:
                TextMethod("線を引いて敵を倒そう！");

                TimeDelayMethod();
                break;

            case 5:
                TextMethod("良く出来ました！！！");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 6:
                TextMethod("連撃が決まると\nその分ダメージが上乗せされるよ");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 7:
                TextMethod("他にも線をプレイヤーに持ってきたり\n長押しをすると「必殺技」が出るよ！");
                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 8:
                TextMethod("これでチュートリアルは終わりだよ");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }

                PlayerStatesScript.HP = PlayerStatesScript.HPmax;
                Generater.sumi = Generater.sumiMax;
                break;

            case 9:
                TextMethod("門をくぐって敵をいっぱい倒そう！！");
                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

        }

        Debug.Log(go);
    }

    private void TextMethod(string str)
    {
        Text.text = str;
    }

    private void TimeDelayMethod()
    {
        tutorialcount = true;

        if (timeImage.fillAmount <= 0 && tutorialcount == true)
        {
            if (go == true)
            {
                
                tutorialcount = false;
                timeImage.fillAmount = 1;
                tut++;
                TutorialText.SetActive(true);
                go = false;
                StartCoroutine(DelateMethod());

                if(tut == 4)
                {
                    Instantiate(dog, new Vector3(-5.0f, 0.0f, -41.0f), Quaternion.Euler(0, 180, 0));
                    Instantiate(dog, new Vector3(0.0f, 0.0f, -41.0f), Quaternion.Euler(0, 180, 0));
                    Instantiate(dog, new Vector3(5.0f, 0.0f, -41.0f), Quaternion.Euler(0, 180, 0));
                }
                else if(tut == 6)
                {
                    SousaSetumei_1.SetActive(true);
                }
                else if(tut == 7)
                {
                    SousaSetumei_1.SetActive(false);
                    SousaSetumei_2.SetActive(true);
                }
                else if(tut == 8)
                {
                    SousaSetumei_2.SetActive(false);
                }
                else if(tut == 10)
                {
                    gatescript.isOpen = true;
                    TutorialText.SetActive(false);
                }
            }
            else
            {
                TutorialText.SetActive(false);
            }
        }
    }
    private IEnumerator DelateMethod()
    {
        yield return new WaitForSeconds(0.2f);
        go = false;
    }
}
