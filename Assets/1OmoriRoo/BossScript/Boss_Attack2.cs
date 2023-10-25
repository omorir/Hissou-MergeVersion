using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack2 : MonoBehaviour
{
    [SerializeField] GameObject omen;
    [SerializeField] GameObject omen2;
    [SerializeField] GameObject omen3;
    [SerializeField] GameObject omen4;
    [SerializeField] GameObject Damage;
    [SerializeField] GameObject Damage2;
    [SerializeField] GameObject Damage3;
    [SerializeField] GameObject Damage4;

    public static bool Boss_Attack_2 = false;
    private Transform player;

    private float SaveTime;
    private int count = 0;
    private float stop_count;
    private Vector3 player_pos;
    private Vector3 now_pos;

    public float chuien_time = 2.0f;
    public float tyakuti_time = 0.5f;
    public float stop_time = 1.0f;
    public float chance_time = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        omen.SetActive(false);
        omen2.SetActive(false);
        omen3.SetActive(false);
        omen4.SetActive(false);
        Damage.SetActive(false);
        Damage2.SetActive(false);
        Damage3.SetActive(false);
        Damage4.SetActive(false);
        Boss_Attack_2 = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        count = 0;
        stop_count = stop_time;
        player_pos = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss_Attack_2 == false)
        {
            count = 0;

            omen.SetActive(false);
            omen2.SetActive(false);
            omen3.SetActive(false);
            omen4.SetActive(false);
            Damage.SetActive(false);
            Damage2.SetActive(false);
            Damage3.SetActive(false);
            Damage4.SetActive(false);
        }

        if (Boss_Attack_2 == true && count <= 2 && stop_count == stop_time) 
        {
            Vector3 pos = this.transform.position;

            if (count == 0)
            {
                if (pos.x >= 0.01f)
                {
                    //pos.x -= Time.deltaTime * 11.67f;
                    pos.x -= Time.deltaTime * 4f;
                }
                else if (pos.x <= -0.01f)
                {
                    //pos.x += Time.deltaTime * 11.67f;
                    pos.x += Time.deltaTime * 4f;
                }
                else
                {
                    pos.x = 0.0f;
                }

                if (pos.z >= 0.01f)
                {
                    //pos.z -= Time.deltaTime * 10;
                    pos.z -= Time.deltaTime * 4f;
                }
                else if (pos.z <= -0.01f)
                {
                    //pos.z += Time.deltaTime * 10;
                    pos.z += Time.deltaTime * 4f;
                }
                else
                {
                    pos.z = 0.0f;
                }
            }
          

            if (pos.y < 10.0f)
            {
                //pos.y += Time.deltaTime * 3.3f;
                pos.y += Time.deltaTime * 2.5f;
            }
            else
            {
                pos.y = 10.0f;

                TenguMotionScript.animator.SetInteger("isBossAttack", 0);

            }

            this.transform.position = pos;  // 座標を設定

            if (pos.x == 0f && pos.z == 0f && pos.y >= 10.0f && SaveTime == 0f)   //ここから攻撃開始(1回目)
            {
                if(this.gameObject.name == "BOSS4" || (BossStates.Boss_wave2 == false && this.gameObject.name == "BOSS"))
                {
                    StartCoroutine(Attack2Delay4Method(0));
                }
                else if(this.gameObject.name == "BOSS3")
                {
                    StartCoroutine(Attack2Delay4Method(1));
                }
                else if (this.gameObject.name == "BOSS2")
                {
                    StartCoroutine(Attack2Delay4Method(2));
                }
                else if (this.gameObject.name == "BOSS")
                {
                    StartCoroutine(Attack2Delay4Method(3));
                }

                SaveTime += Time.deltaTime;
            }
            else if (count >= 1 && pos.y >= 10.0f && SaveTime == 0f)   //ここから攻撃開始(2回目)　////y座標だけあげたい
            {
                if (this.gameObject.name == "BOSS4" || (BossStates.Boss_wave2 == false && this.gameObject.name == "BOSS"))
                {
                    StartCoroutine(Attack2Delay4Method(0));
                }
                else if (this.gameObject.name == "BOSS3")
                {
                    StartCoroutine(Attack2Delay4Method(1));
                }
                else if (this.gameObject.name == "BOSS2")
                {
                    StartCoroutine(Attack2Delay4Method(2));
                }
                else if (this.gameObject.name == "BOSS")
                {
                    StartCoroutine(Attack2Delay4Method(3));
                }

                SaveTime += Time.deltaTime;
            }

            if (0 < SaveTime && SaveTime < chuien_time)
            {
                SaveTime += Time.deltaTime;
                now_pos = this.transform.position;
            }
            else if(SaveTime >= chuien_time && player_pos != new Vector3(0,0,0))     //2秒空中で待機したら突進
            {
                TosinMethod();

                if (this.transform.position.y <= 1.25f)  //指定位置に到着したら(各1回だけ)
                {
                    player_pos.y = 1.25f;
                    this.transform.position = player_pos;
                    player_pos.y = 0;
                    if (this.gameObject.name == "BOSS4")
                    {
                        omen4.SetActive(false);
                        Damage4.SetActive(true);
                        Damage4.transform.position = player_pos;
                    }
                    else if (this.gameObject.name == "BOSS3")
                    {
                        omen3.SetActive(false);
                        Damage3.SetActive(true);
                        Damage3.transform.position = player_pos;
                    }
                    else if (this.gameObject.name == "BOSS2")
                    {
                        omen2.SetActive(false);
                        Damage2.SetActive(true);
                        Damage2.transform.position = player_pos;
                    }
                    else if (this.gameObject.name == "BOSS")
                    {
                        omen.SetActive(false);
                        Damage.SetActive(true);
                        Damage.transform.position = player_pos;
                    }
                    SaveTime = 0f;
                    count++;
                    stop_count -= Time.deltaTime;
                    StartCoroutine(DamageDelayMethod());

                }
            }
        }
        if (count == 3)
        {
            
            StartCoroutine(ThreeTimeMethod());
        }

        if (stop_count < stop_time)
        {
            player_pos.y = 1.25f;
            stop_count -= Time.deltaTime;
            this.transform.position = player_pos;

            if (stop_count <= 0f)
            {
                stop_count = stop_time;
            }
        }
    }

    private IEnumerator Attack2Delay4Method(int num)
    {
        chuien_time = 2.0F + num;

        yield return new WaitForSeconds(num);

        player_pos = player.transform.position;

        if(num == 0 && BossStates.Boss_wave2 == true)
        {
            omen4.SetActive(true);
            omen4.transform.position = player_pos;
        }
        else if(num == 1)
        {
            omen3.SetActive(true);
            omen3.transform.position = player_pos;
        }
        else if (num == 2)
        {
            omen2.SetActive(true);
            omen2.transform.position = player_pos;
        }
        else if (num == 3 || num == 0)
        {
            Debug.Log("ここ来てる？");
            omen.SetActive(true);
            omen.transform.position = player_pos;
        }
    }

    private void TosinMethod()
    {
        Vector3 Rush = this.transform.position;

        TenguMotionScript.animator.SetInteger("isBossAttack", 5);

        if (count <= 2)
        {
            if (player_pos.x >= 0)
            {
                Rush.x += (player_pos.x * (1f / tyakuti_time)) * Time.deltaTime;
            }
            else if (player_pos.x <= 0)
            {
                Rush.x += (player_pos.x * (1f / tyakuti_time)) * Time.deltaTime;
            }

            if (player_pos.z >= 0)
            {
                Rush.z += (player_pos.z * (1f / tyakuti_time)) * Time.deltaTime;
            }
            else if (player_pos.z <= 0)
            {
                Rush.z += (player_pos.z * (1f / tyakuti_time)) * Time.deltaTime;
            }
        }
        //else
        //{
        //    if(now_pos.x <= player_pos.x)
        //    {
        //        Rush.x += ((player_pos.x - now_pos.x) * (1f / tyakuti_time)) * Time.deltaTime;
        //    }
        //    else
        //    {
        //        Rush.x += (-(player_pos.x - now_pos.x) * (1f / tyakuti_time)) * Time.deltaTime;
        //    }

        //    if (now_pos.z <= player_pos.z)
        //    {
        //        Rush.z += ((player_pos.z - now_pos.z) * (1f / tyakuti_time)) * Time.deltaTime;
        //    }
        //    else
        //    {
        //        Rush.z += (-(player_pos.z - now_pos.z) * (1f / tyakuti_time)) * Time.deltaTime;
        //    }
        //}

        Rush.y -= (10f / tyakuti_time) * Time.deltaTime;

        this.transform.position = Rush;
    }

    private IEnumerator DamageDelayMethod()
    {
        yield return new WaitForSeconds(stop_time);
        if (this.gameObject.name == "BOSS4")
        {
            Damage4.SetActive(false);
        }
        else if (this.gameObject.name == "BOSS3")
        {
            Damage3.SetActive(false);
        }
        else if (this.gameObject.name == "BOSS2")
        {
            Damage2.SetActive(false);
        }
        else if (this.gameObject.name == "BOSS")
        {
            Damage.SetActive(false);
        }
        player_pos = new Vector3(0, 0, 0);
    }

    private IEnumerator ThreeTimeMethod()
    {
        

        Vector3 waitpos = this.transform.position;

        yield return new WaitForSeconds(chance_time);

        if (this.gameObject.name == "BOSS")
        {
            Boss_Attack_2 = false;

            TenguMotionScript.animator.SetInteger("isBossAttack", 0);
        }
        else
        {
            waitpos.y = 1.5f;
            this.transform.position = waitpos;
        }
    }
}
