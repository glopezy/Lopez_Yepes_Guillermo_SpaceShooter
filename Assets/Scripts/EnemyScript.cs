using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveCicle;
    [SerializeField] private float shotCicle;
    [SerializeField] private EnemyShotScript shotPrefab;
    private ObjectPool<EnemyShotScript> pool;

    [SerializeField] private AudioSource shotSFX;
    [SerializeField] private AudioSource explodeSFX;

    private Rigidbody2D body;
    private BoxCollider2D collider;


    private float timer;
    private float timer2;
    private Vector3 direction = new Vector3(0,1,0);



    private void Awake()
    {
        pool = new ObjectPool<EnemyShotScript>(CreateShot, null, ReleaseShot, DestroyShot);
    }

    private EnemyShotScript CreateShot()
    {
        EnemyShotScript shotCopy = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        shotCopy.MyPool = pool;
        return shotCopy;
    }

    private void ReleaseShot(EnemyShotScript shot)
    {
        shot.gameObject.SetActive(false);
    }

    private void DestroyShot(EnemyShotScript shot)
    {
        Destroy(shot.gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        shotSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        timer += 1 * Time.deltaTime;
        timer2 += 1 * Time.deltaTime;
        if (timer > moveCicle)
        {
            direction *= -1;
            Debug.Log("cambio");
            timer = 0;
        }

        if (timer2 > shotCicle)
        {
            EnemyShotScript shotCopy = pool.Get();
            shotCopy.gameObject.SetActive(true);
            shotCopy.transform.position = transform.position;
            shotSFX.Play();
            Debug.Log("disparo");
            timer2 = 0;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Shot(Clone)" || collision.gameObject.name == "PlayerShip")
        {
            StartCoroutine(EnemyDeath());
        }
        
    }

    private IEnumerator EnemyDeath()
    {
        explodeSFX.Play();
        yield return new WaitForSeconds(0.5f); 
        this.gameObject.SetActive(false);
    }
    
}
