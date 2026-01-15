using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMuiTen : MonoBehaviour {

    public float speed = -0.5f;
    private Vector3 Move;
    public Animator anim;
    void Start()
    {
        Move = this.transform.position;
    }

    void Update()
    {
        if (!PauseUI.PauseGame)
        {
            Move.x += speed;
            this.transform.position = Move;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((!ChangeHide.TanHinh && col.CompareTag("Player")) || col.CompareTag("DanPlayer"))
        {
            if (anim != null)
            {
                anim.SetBool("No", true);
            }
            Destroy(gameObject, 0.1f);
        }
    }
}
