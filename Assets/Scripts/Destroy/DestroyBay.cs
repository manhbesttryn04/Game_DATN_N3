using UnityEngine;

public class DestroyBay : MonoBehaviour {

    public float time = -1;
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Invoke("DestroyFlat", time);
        }
    }
    void DestroyFlat()
    {
        Destroy(gameObject); // hủy đối tượng hiện tại
    }
}
