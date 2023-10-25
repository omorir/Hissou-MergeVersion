using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Stop", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Stop()
    {
        
        LineRenderer line = gameObject.GetComponent<LineRenderer>();//lineコンポーネント取得

        line.enabled = false;
    }
}
