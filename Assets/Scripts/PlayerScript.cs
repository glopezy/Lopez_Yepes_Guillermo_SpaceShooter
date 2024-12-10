using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private ShotScript shotPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int shots;

    private ObjectPool<ShotScript> pool;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            StartCoroutine(Espiral());
            
            
        }
    }
    private IEnumerator Espiral()
    {
        float degrees = 360 / shots;

        for (float i = 0; i < 360; i += degrees)
        {
            //request shot
            //pool.Get();

            ShotScript shotCopy = pool.Get();
            shotCopy.gameObject.SetActive(true);
            shotCopy.transform.position = transform.position;
            shotCopy.transform.eulerAngles = new Vector3(0f, 0f, i);

            //wait! they dont love you like a love you
            yield return new WaitForSeconds(0.001f);

        }
        
    }
}
