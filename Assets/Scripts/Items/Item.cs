using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int ID;
    public bool Moving = true;
    private void Awake()
    {
        if (Moving)
        {
            Rigidbody2D myRig = GetComponent<Rigidbody2D>(); // Lay RidgidBody
            float speed = 4f;
            if (GameObject.FindGameObjectWithTag("PlayerManager").transform.localScale.x < 0)
            {
                speed *= -1;
            }
            myRig.AddForce(new Vector2(1f, 0.2f) * speed, ForceMode2D.Impulse);
        }
    }
}
