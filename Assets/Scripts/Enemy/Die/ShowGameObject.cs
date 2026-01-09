using UnityEngine;

public class ShowGameObject : MonoBehaviour {

	public GameObject ObjectGame;

	public bool Hide = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!ObjectGame.activeSelf && !Hide) {
			this.gameObject.SetActive (true);	
		} else if (!ObjectGame.activeSelf && Hide) {
			this.gameObject.SetActive (false);	
		}
	}
}
