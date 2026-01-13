using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFly : MonoBehaviour {

    public float Speed = 2f;
    public float TimeLive = 1f;
    private Rigidbody2D bd;
    protected void Awake()
    {
        bd = GetComponent<Rigidbody2D>();
        // Dieu chinh huong bay
        bd.AddForce(new Vector2(0, 1) * Speed, ForceMode2D.Impulse);

        Destroy(gameObject, TimeLive);
    }
}
