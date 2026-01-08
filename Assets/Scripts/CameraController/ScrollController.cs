using UnityEngine;

public class ScrollController : MonoBehaviour {

	public bool ScrollRight = false;

	public float Speed = 0.1f;

	private Vector2 offset;

	private MeshRenderer mesh;

	private void Awake (){
		mesh = GetComponent <MeshRenderer> ();

	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		offset = mesh.material.mainTextureOffset;
		if (ScrollRight) {
			offset.x = offset.x + Speed;
		} else {
			offset.x = offset.x - Speed;
		}
		mesh.material.mainTextureOffset = offset;
	}
}
