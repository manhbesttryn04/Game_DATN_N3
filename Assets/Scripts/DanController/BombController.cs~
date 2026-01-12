using UnityEngine;

public class BombController : MonoBehaviour {

	public float scopeHight; //Khoang cao
	public float scopeLow; // Khoang thap
	public float scopeHorizontal; // Khoang xa
	[SerializeField]
	private float timeDestroy = 3f;

	private Rigidbody2D bombRG;

	public void SetForceAwake (ref Rigidbody2D _bombRG)
	{
		_bombRG = GetComponent<Rigidbody2D> ();
		_bombRG.AddForce (new Vector2 (Random.Range (-scopeHorizontal, scopeHorizontal), Random.Range (scopeLow, scopeHight)), ForceMode2D.Impulse);
	}

	//
	private void Awake(){
		SetForceAwake (ref bombRG);
		Destroy (this.gameObject,timeDestroy);
	}
}
