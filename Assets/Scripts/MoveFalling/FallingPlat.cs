using UnityEngine;

public class FallingPlat : MonoBehaviour {

    public float timeDestroy = 0 ;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
			Invoke ("DestroyFlat",timeDestroy);
		}
    }
		
    void DestroyFlat()
    {
        Destroy(gameObject); // hủy đối tượng hiện tại
    }
}