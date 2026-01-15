using UnityEngine;

public class LazeController : MonoBehaviour {

	public float timeDestroy = 3f;

	// Use this for initialization
	private void Awake () {
		Destroy (this.gameObject, timeDestroy);	
	}
}
