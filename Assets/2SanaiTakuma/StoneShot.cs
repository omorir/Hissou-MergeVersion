using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShot : MonoBehaviour
{
    [SerializeField] GameObject stone;
    Transform Target;
    GameObject Player;

    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject StoneCl = GameObject.Find("stone(Clone)");
        Player = GameObject.FindWithTag("Player");
        Target = Player.transform;

        Vector3 tarPos = Target.position;

        tarPos.y = transform.position.y;

        //ÉvÉåÉCÉÑÅ[ÇÃï˚å¸Çå¸Ç≠
        transform.LookAt(tarPos);


        GameObject Stone = Instantiate(stone, transform.position, Quaternion.identity);
        Rigidbody StoneRb = Stone.GetComponent<Rigidbody>();

        StoneRb.AddForce(transform.forward * speed);
        Destroy(StoneCl, 3.0f);
        Destroy(this, 3.0f);

        Debug.Log("ìäÇ∞ÇΩ");



    }

    // Update is called once per frame
    void Update()
    {



    }
}
