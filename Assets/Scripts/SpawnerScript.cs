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
            Vector3 randomSpawnPosition =  new Vector3(Random.Range(-2.0f,2.0f), Random.Range(-4.0f, 1.0f), 0);
            for (int i = 0; i <= difficulty; i ++)
            {
                Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
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
}
