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

        if (WaveFlag.wave1 == true && PlayerStatesScript.StartFlag == true && tutorialCheck == true) //Wave1���X�^�[�g�ʒu�̎��̂݃`���[�g���A�����\�������
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

        if (tutorialFlag == true)                                      // �`���[�g���A���J�n�I�I�I
        {
            tutorialMethod();
        }
    }

    public void OnClickYesButton()  //�͂� �������ƃ`���[�g���A���Ɉڍs
    {
        Time.timeScale = 1;
        TutorialCheckText.SetActive(false);
        TutorialText.SetActive(true);
        tutorialCheck = false;
        tutorialFlag = true;
        tut = 1;
    }

    public void OnClickNoButton()  //������ �������Ƃ��̂܂ܑ��s
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
                TextMethod("����ł�\n�`���[�g���A�����n�߂܂��B");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 2:
                TextMethod("�܂��͎�l�����^�b�`����\n�������Ă݂悤�I");

                TimeDelayMethod();

                if (Generater.CharaSlide == true)
                {
                    go = true;
                }
                break;

            case 3:
                TextMethod("���͓G���U�����Ă݂悤�I");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 4:
                TextMethod("���������ēG��|�����I");

                TimeDelayMethod();
                break;

            case 5:
                TextMethod("�ǂ��o���܂����I�I�I");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 6:
                TextMethod("�A�������܂��\n���̕��_���[�W����悹������");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 7:
                TextMethod("���ɂ������v���C���[�Ɏ����Ă�����\n������������Ɓu�K�E�Z�v���o���I");
                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }
                break;

            case 8:
                TextMethod("����Ń`���[�g���A���͏I��肾��");

                TimeDelayMethod();

                if (timeImage.fillAmount <= 0.5f && timeImage.fillAmount >= 0.2f)
                {
                    go = true;
                }

                PlayerStatesScript.HP = PlayerStatesScript.HPmax;
                Generater.sumi = Generater.sumiMax;
                break;

            case 9:
                TextMethod("����������ēG�������ς��|�����I�I");
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
