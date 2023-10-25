using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseScrean;
    [SerializeField] GameObject PauseButton;
    [SerializeField] GameObject Pause1;
    [SerializeField] GameObject Pause2;

    private int pausecount = 0;

    // Start is called before the first frame update
    void Start()
    {
        PauseScrean.SetActive(false);
        Pause2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))   //ポーズコマンド
        {
            pausecount++;
            if (pausecount % 2 == 1)
            {
                Time.timeScale = 0;
                PauseButton.SetActive(false);
                PauseScrean.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PauseScrean.SetActive(false);
                PauseButton.SetActive(true);
                Pause2.SetActive(false);
                Pause1.SetActive(true);
            }
        }
    }

    public void OnClickPauseButton()  //ポーズ
    {
        pausecount++;
        Time.timeScale = 0;
        PauseButton.SetActive(false);
        PauseScrean.SetActive(true);
    }
    public void OnClickBackToGameButton()  //ポーズからゲームに戻る
    {
        pausecount = 0;
        Time.timeScale = 1;
        PauseScrean.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void OnClickBackToTitleButton()  //ポーズからタイトルに戻る前の確認画面に推移
    {
        Pause1.SetActive(false);
        Pause2.SetActive(true);
    }

    public void OnClickNoButton()  //確認画面からポーズに推移
    {
        Pause2.SetActive(false);
        Pause1.SetActive(true);
    }
}
