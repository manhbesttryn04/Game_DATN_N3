using UnityEngine;

public class DestroyTime : MonoBehaviour {

	public float Time = 2f;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject,Time);
	}
}
