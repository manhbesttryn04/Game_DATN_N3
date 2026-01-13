using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClone : MonoBehaviour {


    public GameObject Roi;
    float randomX;
    Vector2 whereToSpawn;
    public float spawnRate=2f; // thoi gian xuat hien vat lan 2
    float nextSpawn=0.0f; // dung de kiem tra voi thoi gian hien tai
    public float minX=0f;
    public float maxX=0f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update () {
        if (!PauseUI.PauseGame)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                randomX = Random.Range(minX, maxX);
                whereToSpawn = new Vector2(randomX, transform.position.y);
                Instantiate(Roi, whereToSpawn, Quaternion.identity);
            }
        }
    }
    
}
