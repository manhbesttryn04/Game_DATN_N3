using UnityEngine;

public class WaterControllder : MonoBehaviour {
	// Lay component cua nuoc
	public BuoyancyEffector2D effector;
	// Luc chay cua nuoc
	public float Speed = 8f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Tac dong vao luc chay cua nuoc
		effector.flowMagnitude = Speed;
	}

	private void OnCollisionEnter2D(Collision2D other){
		if (other.collider.CompareTag ("Ground")) {
			Speed *= -1; 
		}
	}
   
}
