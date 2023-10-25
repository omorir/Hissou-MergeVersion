using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStop : MonoBehaviour
{
    [SerializeField] GameObject[] EnemyPrefab;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] not_attack_enemy = GameObject.FindGameObjectsWithTag("Not_Attack_Enemy");
        Debug.Log(enemy.Length.ToString()+"enemy‚Ì”");
        Debug.Log(enemy.Length.ToString()+"not_attack_enemy‚Ì”");

        if (enemy.Length+ not_attack_enemy.Length >= 50)
        {
            GameObject spawnpoint = GameObject.FindWithTag("spawnpoint");

            Destroy(spawnpoint.gameObject);
            
        }

       
    }
}
