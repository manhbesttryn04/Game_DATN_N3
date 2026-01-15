using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCloneRandomY : MonoBehaviour {

    public GameObject Roi;
    float randomY;
    Vector2 whereToSpawn;
    public float spawnRate = 0f;
    float nextSpawn = 0.0f;
    public float minY = 0f;
    public float maxY = 0f;
   
	void Update () {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randomY = Random.Range(minY, maxY);
            whereToSpawn = new Vector2(transform.position.x,randomY);
            Instantiate(Roi, whereToSpawn, Quaternion.identity);
        }
    }

   
}
