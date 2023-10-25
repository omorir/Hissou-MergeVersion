using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Manager : MonoBehaviour
{
    [Header("�X���C�_�[")]
    [SerializeField]
    public Slider slider;
    AudioSource audioSource;

    public static float SoundVal= 0.4f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //Slider��audioSource.volume�̏�����
        SoundVal = getVal();
        slider.value = SoundVal;
        this.audioSource.volume = SoundVal;

        slider.onValueChanged.AddListener(value => this.audioSource.volume = value);
       

    }
    // Update is called once per frame
    void Update()
    {
        SoundVal = this.audioSource.volume;
    }

    public static float getVal()
    {
        return SoundVal;
    }
}
