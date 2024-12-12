using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    [SerializeField] private float timer;
    [SerializeField] private float timer2;
    [SerializeField] private float cicle;
    [SerializeField] private float difficulty;
    [SerializeField] private EnemyScript enemyPrefab;
   
    

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        timer2 += 1 * Time.deltaTime;



        if (timer > cicle)
        {
            
            for (int i = 0; i <= difficulty; i ++)
            {
                StartCoroutine(Spawn());
            }
            
            timer = 0;
        }

        if (timer2 / 10 > difficulty)
        {
            difficulty++;
            if (cicle > 0.5f)
            {
                
                cicle -= 0.5f;

            }
            

        }



    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 randomSpawnPosition = new Vector3(Random.Range(0f, 9.0f), Random.Range(-4.0f, 5.0f), 0);
        Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.Euler(0, 0, 90));
        
    }


}
