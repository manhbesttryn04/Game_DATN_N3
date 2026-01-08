using UnityEngine;

public class Destroy : MonoBehaviour {

    private float time = -1;

    private void Start()
    {
        if (gameObject.tag == "Money")
        {
            //GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
		if (col.CompareTag("Player"))
        {
            if (gameObject.tag != "Money")
            {
                Invoke("DestroyFlat", time);
            }
        }
    }
    
    void DestroyFlat()
    {
        Destroy(gameObject); // hủy đối tượng hiện tại
    }
}
