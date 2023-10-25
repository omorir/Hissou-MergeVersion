using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack4pointmove : MonoBehaviour
{
    Transform Target;
    GameObject Player;


    public float speed = 3f;

    float speedcopy;

    // Start is called before the first frame update
    void Start()
    {
        //プレイヤータグの取得
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;

        speedcopy = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;


        transform.LookAt(tarPos);

        float distance = Vector3.Distance(transform.position, Target.position);

        if (transform.position.z > 10 || transform.position.z < -10 || transform.position.x > 10 || transform.position.x < -10)
        {
            speed = speedcopy;
            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
            return;
        }

       

        if (distance < 0.01f)
        {
            speed = 0;
            Debug.Log("うごいてない");
        }
        else
        {
            speed = speedcopy;
            transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        }

        


    }
}
