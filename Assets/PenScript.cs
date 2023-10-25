using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenScript : MonoBehaviour
{
    public float FPSZpos;
    public float UPZpos;
    public float SIDEZpos;
    private LineRenderer lineRenderer;
    private int positionCount;
    private Camera mainCamera;
    [SerializeField] Camera UpCamera;
    [SerializeField] Camera FPSCamera;
    [SerializeField] Camera SideCamera;
    [SerializeField] Camera NormalCamera;

    public GameObject prefab;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        // ���C���̍��W�w����A���̃��C���I�u�W�F�N�g�̃��[�J�����W�n����ɂ���悤�ݒ��ύX
        // ���̏�ԂŃ��C���I�u�W�F�N�g���ړ��E��]������ƁA�`���ꂽ���C�������[���h��ԂɎ��c����邱�ƂȂ��A�ꏏ�Ɉړ��E��]
        lineRenderer.useWorldSpace = false;
        positionCount = 0;
        mainCamera = Camera.main;
    }

    void Update()
    {
        // ���̃��C���I�u�W�F�N�g���A�ʒu�̓J�����O��10m�A��]�̓J�����Ɠ����ɂȂ�悤�L�[�v������
        if (CameraButtonScript.FPSCameraWalkSwitch == true)
        {
            transform.position = FPSCamera.transform.position + FPSCamera.transform.forward * 10f;
            transform.rotation = FPSCamera.transform.rotation;
        }
        else if (CameraButtonScript.UPCameraWalkSwitch == true)
        {
            transform.position = UpCamera.transform.position + UpCamera.transform.forward * 10f;
            transform.rotation = UpCamera.transform.rotation;
        }
        else if (CameraButtonScript.SIDECameraWalkSwitch == true)
        {
            transform.position = SideCamera.transform.position + SideCamera.transform.forward * 10f;
            transform.rotation = SideCamera.transform.rotation;
        }
        else
        {
            transform.position = mainCamera.transform.position + mainCamera.transform.forward * 10f;
            transform.rotation = mainCamera.transform.rotation;
        }

        if (Input.GetMouseButton(0))
        {

            // ���W�w��̐ݒ�����[�J�����W�n�ɂ������߁A�^������W�ɂ����������
            Vector3 pos = Input.mousePosition;
            if (CameraButtonScript.FPSCameraWalkSwitch == true)
            {
                //FPSZpos = 1.2f;
                pos.z = FPSZpos;
            }
            else if (CameraButtonScript.UPCameraWalkSwitch == true)
            {
                //UPZpos = 2.5f;
                pos.z = UPZpos;
            }
            else if (CameraButtonScript.SIDECameraWalkSwitch == true)
            {
                //UPZpos = 1.2f;
                pos.z = SIDEZpos;
            }
            else
            {
                pos.z = 1.2f;
            }

            // �}�E�X�X�N���[�����W�����[���h���W�ɒ���
            if (CameraButtonScript.FPSCameraWalkSwitch == true)
            {
                pos = FPSCamera.ScreenToWorldPoint(pos);
            }
            else if (CameraButtonScript.UPCameraWalkSwitch == true)
            {
                pos = UpCamera.ScreenToWorldPoint(pos);
            }
            else if (CameraButtonScript.SIDECameraWalkSwitch == true)
            {
                pos = SideCamera.ScreenToWorldPoint(pos);
            }
            else
            {
                pos = mainCamera.ScreenToWorldPoint(pos);
            }

            // ����ɂ�������[�J�����W�ɒ����B
            pos = transform.InverseTransformPoint(pos);

            // ����ꂽ���[�J�����W�����C�������_���[�ɒǉ�����
            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, pos);

        }

        if (Input.GetMouseButtonUp(0))
        {
            InstanceZ();

        }

        //���Z�b�g����
        if (!(Input.GetMouseButton(0)))
        {
            positionCount = 0;
        }
        
    }

    void InstanceZ()
    {
        var RendererPos = new Vector3[lineRenderer.positionCount];
        int cnt = lineRenderer.GetPositions(RendererPos);

        int i = 0;

        while (i < RendererPos.Length)
        {
            //RendererPos[i] = transform.InverseTransformPoint(RendererPos[i]);

            //Instantiate(prefab, new Vector3(RendererPos[i].x, RendererPos[i].y, 10.0f), Quaternion.Euler(90, 90, 0));
            i++;
        }

        var parent = new GameObject("Parent").transform;

        //RendererPos.SetParent(parent);
    }
}
