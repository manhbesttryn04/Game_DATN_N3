using System.Collections;
using UnityEngine;
using System;

public class FallingDown : MonoBehaviour {

	[Serializable]
	public class AutoFallingDown
	{
		public float maxY = 0f,minY = 0f;
		public float speed = 0.1f;
		public bool repeat = false;
	}
	[Header("Auto Down/Up...")]
	public AutoFallingDown Auto;
	[Space(10)]
	public bool DisableAutoDown = false;
	[Space(10)]
	// Thoi gian
	public float timeDown = 0f;
	public float timeShow = 10f;
	public float timeDestroy = 2f;

	private Rigidbody2D r2;
	private Vector3 position;

	Vector3 Move;
	// Use this for initialization
	void Start () {
		r2 = gameObject.GetComponent<Rigidbody2D>();
		Move = transform.position;
		position = Move;
	}

	void MovePlatformUpDown ()
	{
		//DisableAutoDown dung de chan di chuyen len xuong
		// Dung de thuc hien len xuong kieu khac
		if ((Auto.maxY != 0f || Auto.minY != 0f) && !DisableAutoDown) {
			if (this.transform.position.y < Auto.minY) {
				RepeatPlatform (true);
			} else if (this.transform.position.y > Auto.maxY) {
				RepeatPlatform (false);
			} 
			MovePlat ();
		}
	}

	private void RepeatPlatform(bool value){
		// Xac nhan lap lai
		if (Auto.repeat) {
			if (Auto.speed > 0 && true) {
				Move.y = Auto.minY;
			} else if (Auto.speed < 0) {
				Move.y = Auto.maxY;
			}
		} else {
			// Doi chieu khi buc ra khoi gioi han
			Auto.speed *= -1;
		}
	}

	//
	private void FixedUpdate(){
		MovePlatformUpDown ();
	}

	private void HiddenObject(){
		this.gameObject.SetActive (false); 
	}

	void AllowDown (Collision2D col)
	{
		// Neu min va max khac 0 thì cho no rot xuong 
		if (col.collider.CompareTag ("Player") && DisableAutoDown) {
			StartCoroutine (fall ());
			if (DisableAutoDown) {
                GameObject.FindGameObjectWithTag("PlayerManager").transform.SetParent(GameObject.FindGameObjectWithTag("SystemPlayer").transform);
				Invoke ("HiddenObject", timeDestroy);
			}
		}
	}

	// Update is called once per frame
	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag ("Player")) {
            GameObject.FindGameObjectWithTag("PlayerManager").transform.SetParent(transform);
		}
		AllowDown (col);
	}
	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag ("Player") && DisableAutoDown) {
			//r2.bodyType = RigidbodyType2D.Dynamic;
			StartCoroutine (fall ());
		}
	}
	private void OnTriggerExit2D(Collider2D collider){
		if (collider.CompareTag ("Player")) {
			Invoke ("DisplayTile", timeShow);
		}
	}
	IEnumerator fall()
	{
		yield return new WaitForSeconds(timeDown);
		r2.bodyType = RigidbodyType2D.Dynamic;
		yield return 0;
	}

	private void MovePlat(){
		Move.y += Auto.speed;
		this.transform.position = Move;
	}

	void AllowShowGameObject (Collision2D col)
	{
		if (col.collider.CompareTag ("Player") && DisableAutoDown) {
			Invoke ("DisplayTile", timeShow);
		}
	}

	private void OnCollisionExit2D(Collision2D col){
		if (col.collider.CompareTag ("Player")) {
            GameObject.FindGameObjectWithTag("PlayerManager").transform.SetParent(GameObject.FindGameObjectWithTag("SystemPlayer").transform);
		}
		AllowShowGameObject (col);
	}

	private void DisplayTile(){
		if (r2 != null) {
			r2.bodyType = RigidbodyType2D.Static;
		}
		transform.position = position;
		this.gameObject.SetActive (true);
	}
}
