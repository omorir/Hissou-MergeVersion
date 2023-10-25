using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraButtonScript : MonoBehaviour
{
    int UpCount, FPSCount, SideCount;

    public static bool UPCameraWalkSwitch;
    public static bool FPSCameraWalkSwitch;
    public static bool SIDECameraWalkSwitch;

    [SerializeField] GameObject UpCamera;
    [SerializeField] GameObject FPSCamera;
    [SerializeField] GameObject SideCamera;
    [SerializeField] GameObject p12;

    private GameObject NCam;
    private GameObject TPSCam;
    public static int TPSSwitch;
    public static bool TPSFlag;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        UpCamera.SetActive(false);
        FPSCamera.SetActive(false); 
        SideCamera.SetActive(false);
        mainCamera = Camera.main;

        NCam  = GameObject.Find("NormalCamera");
        TPSCam = GameObject.Find("TPSCamera");
        TPSFlag = false;
        TPSSwitch = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            TPSSwitch++;
            if (TPSSwitch % 2 == 1)
            {
                TPSFlag = true;
                TPSCam.SetActive(true);
                NCam.SetActive(false);
            }
            else
            {
                TPSFlag = false;
                NCam.SetActive(true);
                TPSCam.SetActive(false);
            }
        }
    }

    public void OnClickUpCamera()
    {
        UpCount++;
        FPSCount = 0; FPSCamera.SetActive(false);
        SideCount = 0; SideCamera.SetActive(false);

        if (UpCount % 2 == 1)
        {
            UpCamera.SetActive(true);
            p12.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            UPCameraWalkSwitch = true;
            FPSCameraWalkSwitch = false;
            SIDECameraWalkSwitch = false;
        }
        else
        {
            UpCamera.SetActive(false);
            p12.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
            UPCameraWalkSwitch = false;
            FPSCameraWalkSwitch = false;
            SIDECameraWalkSwitch = false;
        }
    }
    public void OnClickFPSCamera()
    {
        FPSCount++;
        UpCount = 0; UpCamera.SetActive(false);
        SideCount = 0; SideCamera.SetActive(false);

        if (FPSCount % 2 == 1)
        {
            FPSCamera.SetActive(true);
            p12.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            FPSCameraWalkSwitch = true;
            UPCameraWalkSwitch = false;
            SIDECameraWalkSwitch = false;
        }
        else
        {
            FPSCamera.SetActive(false);
            p12.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
            FPSCameraWalkSwitch = false;
            UPCameraWalkSwitch = false;
            SIDECameraWalkSwitch = false;
        }
    }

    public void OnClickSideCamera()
    {
        SideCount++;
        UpCount = 0; UpCamera.SetActive(false);
        FPSCount = 0; FPSCamera.SetActive(false);

        if (SideCount % 2 == 1)
        {
            SideCamera.SetActive(true);
            p12.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            SIDECameraWalkSwitch = true;
            FPSCameraWalkSwitch = false;
            UPCameraWalkSwitch = false;
        }
        else
        {
            SideCamera.SetActive(false);
            p12.transform.rotation = Quaternion.Euler(40f, 0f, 0f);
            FPSCameraWalkSwitch = false;
            UPCameraWalkSwitch = false;
            SIDECameraWalkSwitch = false;
        }
    }
}
