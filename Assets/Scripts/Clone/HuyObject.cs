using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuyObject : MonoBehaviour {

    public float timeHuy=10f;
    public float speedX = 0.2f;
    public bool facingRight = true;

    private void Awake()
    {
        speedX = Random.Range(0, 0.2f); // Khoi tao ngau nhien huong bay
    }

    void Update () {
        if(!PauseUI.PauseGame)
        {        
            ThucHienBay();
        }
    }
    private void ThucHienBay()
    {
        Vector2 position = transform.position; // Lay vi tri hien tai
        // Kiem tra huong bay
        if (facingRight)
        {
            position.x = position.x + speedX; // Bay ve phia ben phai
        }
        else
        {
            position.x = position.x - speedX; // Bay ve phia ben trai
        }
        transform.position = position; // Thay doi vi tri
        Invoke("Huy", timeHuy);
    }

    private void Huy()
    {
        Destroy(gameObject);
    }
    /// <summary>
    /// 
    /// </summary>
   
}
