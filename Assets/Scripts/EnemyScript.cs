using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float moveCicle;
    [SerializeField] private float shotCicle;
    [SerializeField] private EnemyShotScript shotPrefab;
    private ObjectPool<EnemyShotScript> pool;

    [SerializeField] private PowerupScript powerupPrefab;

    [SerializeField] private AudioSource shotSFX;
    [SerializeField] private AudioSource explodeSFX;
    [SerializeField] private AudioSource powerupSFX;

    private Rigidbody2D body;
    private BoxCollider2D collider;


    private float timer;
    private float timer2;
    private Vector3 direction = new Vector3(0,1,0);

    // to change score
    [SerializeField] PlayerScript player;

    [SerializeField] private int chance;
    [SerializeField] private int powerup;

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
        moveCicle = UnityEngine.Random.Range(0.0f, 2.0f);
        moveCicle = UnityEngine.Random.Range(0.70f, 1.00f);
        powerup = UnityEngine.Random.Range(1, 101);
        // powerup = 30;

        
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

        /* 
         * When an enemy dies there is a chance they drop a powerup. This probability decreases every time. 
         * Powerups scale and change the shoot id from the player
         * Chances:
         * 35% - 25% - 10% 
         * 
         */

        if (player.shootId == 1)
        {
            chance = 35;

        }
        else if (player.shootId == 2)
        {
            chance = 25;
        }
        else if (player.shootId == 3)
        {
            chance = 10;
        }
        else if (player.shootId == 4)
        {
            chance = 0;
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
        player.score += 100;


        if (powerup <= chance)
        {
            powerupSFX.Play();
            Instantiate(powerupPrefab, transform.position, Quaternion.identity);
            

        }

        
        yield return new WaitForSeconds(0.5f);    

        this.gameObject.SetActive(false);
        
    }
    
}
