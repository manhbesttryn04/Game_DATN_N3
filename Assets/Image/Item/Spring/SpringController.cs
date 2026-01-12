using UnityEngine;
using System;
using System.Collections;

public class SpringController : MonoBehaviour {

	public Transform rayCastStart;
	public Transform rayCastEnd;
	public float springForce = 200f;

	private Animator animator;
	// Use this for initialization
	void Start () {
        // Lay component
		animator = GetComponent<Animator>();
	}

    /// <summary>
    /// Thuc hien thay doi hieu ung
    /// </summary>
    /// <param name="value">gia tri nhan/nha</param>
    private void SetChangeAnimation(bool value)
    {
        animator.SetBool("Pressing", value);
        animator.SetBool("Releasing", !value);
    }

	// Update is called once per frame
	void Update () {
		Debug.DrawLine (rayCastStart.position, rayCastEnd.position, Color.green);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Kiem tra doi tuong dang dung o tren mat cua lo xo
        if (other.gameObject.tag == "PlayerManager" && CheckPositionPlayer(other))
        {
            // Thay doi hieu ung
            SetChangeAnimation(true);
            // Them luc
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, springForce));
        }
    }

    private bool CheckPositionPlayer(Collision2D other)
    {
        Vector2 position = other.gameObject.transform.position; // Lay vi tri cua nhan vat
        // Kiem tra vi tri
        if (position.x <= rayCastEnd.position.x && position.x >= rayCastStart.position.x)
        {
            return true;
        }
        return false;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // Kiem tra doi tuong dang o tren
        if (other.gameObject.tag == "PlayerManager")
        {
            // Thay doi hieu ung
            SetChangeAnimation(false);
        }
    }
}