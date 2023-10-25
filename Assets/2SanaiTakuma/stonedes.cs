using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stonedes : MonoBehaviour
{
    [SerializeField] GameObject Stone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(Stone);
        }
    }
}
