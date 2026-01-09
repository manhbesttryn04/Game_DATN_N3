using UnityEngine;

public class BloodController : MonoBehaviour {

	public float timeDestroy = 0.5f;

	//
	private void Awake(){
		Destroy (this.gameObject, timeDestroy);
	}
}
