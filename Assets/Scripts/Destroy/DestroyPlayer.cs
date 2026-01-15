using UnityEngine;

public class DestroyPlayer : MonoBehaviour {

    public float time = 0;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            Invoke("DestroyFlat", time);
        }
    }


    void DestroyFlat()
    {
        Destroy(gameObject); // hủy đối tượng hiện tại
    }
}
