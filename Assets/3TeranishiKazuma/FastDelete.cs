using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastDelete : MonoBehaviour
{

    Vector3 kero;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }

        kero = this.gameObject.transform.localScale; //ŸŒ»İ‚Ì‘å‚«‚³‚ğ‘ã“ü

        kero.x = Generater.scale;  //‡A•Ï”kero‚ÌxÀ•W‚ğ1‘‚â‚µ‚Ä‘ã“ü
        kero.y = Generater.scale;
        kero.z = Generater.scale;

        this.gameObject.transform.localScale = kero; //‡B‘å‚«‚³‚É•Ï”kero‚ğ‘ã“ü
    }
}
