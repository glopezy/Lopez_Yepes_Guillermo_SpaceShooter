using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class ShotScript : MonoBehaviour
{
    [SerializeField] private float shotSpeed;

    private ObjectPool<ShotScript> myPool;

    public ObjectPool<ShotScript> MyPool { get => myPool; set => myPool = value; }

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.right * shotSpeed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= 3)
        {
            timer = 0;
            myPool.Release(this);
        }
    }
}
