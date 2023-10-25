using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameOver_Camera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam2;
    [SerializeField] private GameObject DestroyUI;

    private float change_time;

    // Start is called before the first frame update
    void Start()
    {
        vcam2.Priority = 9;
        DestroyUI.SetActive(false);

        change_time = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (1.0f <= change_time && change_time <= 6.0f)
        {
            change_time += Time.deltaTime;
        }
        else if (change_time > 6.0f)
        {
            vcam2.Priority = 13;
            StartCoroutine(UIDelateMethod());
            change_time = 0;
        }
        else
        {
            change_time = 0;
        }
    }

    private IEnumerator UIDelateMethod()
    {
        yield return new WaitForSeconds(2.3f);

        DestroyUI.SetActive(true);
    }
}
