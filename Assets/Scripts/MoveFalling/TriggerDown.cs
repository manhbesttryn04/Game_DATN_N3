using System.Collections;
using UnityEngine;

public class TriggerDown : MonoBehaviour {
	public float timeDown = 0.5f;
	public float timeShow = 10f;
	private Rigidbody2D downRB;
	private Vector2 possition;
	private float nextTimeShow = 0f;
	//
	private void Awake(){
		nextTimeShow = 0f;
		downRB = GetComponent <Rigidbody2D>();
	}
	// Use this for initialization
	void Start () {
		possition = this.gameObject.transform.position;
	}

	private void Update(){
		if (nextTimeShow < Time.time) {
			DisplayTile ();
			nextTimeShow = timeShow + Time.time;
		}
	}

	void AllowDown (Collider2D col)
	{
		// Neu min va max khac 0 thì cho no rot xuong 
		if (col	.CompareTag ("Player")) {
			StartCoroutine (Downfall ());
		}
	}

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D col)
	{
		AllowDown (col);
	}

	IEnumerator Downfall()
	{
		yield return new WaitForSeconds(timeDown);
		downRB.bodyType = RigidbodyType2D.Dynamic;
		yield return 0;
	}

	private void DisplayTile(){
		if (downRB != null) {
			downRB.bodyType = RigidbodyType2D.Static;
		}
		transform.position = possition;
	}
}
