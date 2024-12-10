using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    [SerializeField] private float timer;
    [SerializeField] private float cicle;
    [SerializeField] private EnemyScript enemyPrefab;

    

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;
        if (timer > cicle)
        {
            Vector3 randomSpawnPosition =  new Vector3(Random.Range(-10,11), Random.Range(-10, 11), 0);
            Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
            timer = 0;
        }
    }
}
