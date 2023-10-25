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
        // ラインの座標指定を、このラインオブジェクトのローカル座標系を基準にするよう設定を変更
        // この状態でラインオブジェクトを移動・回転させると、描かれたラインもワールド空間に取り残されることなく、一緒に移動・回転
        lineRenderer.useWorldSpace = false;
        positionCount = 0;
        mainCamera = Camera.main;
    }

    void Update()
    {
        // このラインオブジェクトを、位置はカメラ前方10m、回転はカメラと同じになるようキープさせる
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

            // 座標指定の設定をローカル座標系にしたため、与える座標にも手を加える
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

            // マウススクリーン座標をワールド座標に直す
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

            // さらにそれをローカル座標に直す。
            pos = transform.InverseTransformPoint(pos);

            // 得られたローカル座標をラインレンダラーに追加する
            positionCount++;
            lineRenderer.positionCount = positionCount;
            lineRenderer.SetPosition(positionCount - 1, pos);

        }

        if (Input.GetMouseButtonUp(0))
        {
            InstanceZ();

        }

        //リセットする
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
