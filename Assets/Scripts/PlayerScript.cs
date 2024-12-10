using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private int lifes;
    [SerializeField] private int score;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private ShotScript shotPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int shots;

    [SerializeField] private int shootId;
    /* 1 normal
     * 2 double
     * 3 triple
     * 4 spiral
     */

    private ObjectPool<ShotScript> pool;


    [SerializeField] private float shotTimer;
    [SerializeField] private float shotRatio;
    private AudioSource shotSFX;

    private Rigidbody2D body;
    private BoxCollider2D collider;

    private float horizontal;
    private float vertical;
    private float moveLimiter = 0.7f;

    [SerializeField] private float moveSpeed = 5.0f;

    private void Awake()
    {
        pool  = new ObjectPool<ShotScript>(CreateShot, null, ReleaseShot, DestroyShot);
    }

    private ShotScript CreateShot()
    {
        ShotScript shotCopy = Instantiate(shotPrefab, transform.position, Quaternion.identity);
        shotCopy.MyPool = pool;
        return shotCopy;
    }

    /*
    private void GetShot(ShotScript shot)
    {
        shot.gameObject.SetActive(true);
        shot.transform.position = transform.position;
        
    }
    */

    private void ReleaseShot(ShotScript shot)
    {
        shot.gameObject.SetActive(false);
    }

    private void DestroyShot(ShotScript shot)
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
        if (lifes <= 0)
        {
            Time.timeScale = 0f;
            gameOverScreen.SetActive(true);
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Shoot();
        
    }

    void FixedUpdate()
    {


        if (horizontal != 0 && vertical != 0) 
        {
            
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    void Shoot()
    {
        shotTimer += 1 * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && shotTimer > shotRatio)
        {

            if (shootId == 1)
            {
                ShotScript shotCopy = pool.Get();
                shotCopy.gameObject.SetActive(true);
                shotCopy.transform.position = transform.position;
                shotSFX.Play();
            }

            else if (shootId == 2)
            {
                Instantiate(shotPrefab, spawnPoints[0].transform.position, Quaternion.identity);
                Instantiate(shotPrefab, spawnPoints[2].transform.position, Quaternion.identity);
                shotSFX.Play();
                shotSFX.Play();
            }

            else if (shootId == 3)
            {
                Instantiate(shotPrefab, spawnPoints[0].transform.position, Quaternion.identity);
                Instantiate(shotPrefab, spawnPoints[1].transform.position, Quaternion.identity);
                Instantiate(shotPrefab, spawnPoints[2].transform.position, Quaternion.identity);
                shotSFX.Play();
                shotSFX.Play();
                shotSFX.Play();
            }
            else if (shootId == 4)
            {
                StartCoroutine(Espiral());
            }

            
            shotTimer = 0;

        }
    }

    private IEnumerator Espiral()
    {
        float degrees = 360 / shots;


        for (float i = 0; i < 180; i += degrees)
        {
            //request shot
            //pool.Get();

            ShotScript shotCopy = pool.Get();
            shotCopy.gameObject.SetActive(true);
            shotCopy.transform.position = transform.position;
            shotCopy.transform.eulerAngles = new Vector3(0f, 0f, i);
            shotSFX.Play();

            //wait! they dont love you like a love you
            yield return new WaitForSeconds(0.001f);

        }
       
        
        
    }
}
