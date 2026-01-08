using UnityEngine;
public class DieController : MonoBehaviour {
	
	public float timeDestroy = 0.5f;

	//
	private void Awake(){
		Destroy (this.gameObject, timeDestroy);
	}
}
