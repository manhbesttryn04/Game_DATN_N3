using UnityEngine;
public class PlatJump : MonoBehaviour {

    public static bool Active = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        // khi nhân vật vừa va chạm đứng lại trên bục thì được gọi là OnCollisionStay2D
        if (collision.collider.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Active) // khi nhấn nút xuống
            {
                gameObject.GetComponent<Collider2D>().enabled = false; // tắt collider2D để cho nhân vật đi xuống
                Invoke("Restore", 1f); // huy va cham
            }
            Active = false;
        }
    }

    void Restore()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        CancelInvoke("Restore"); // Huy hang cho
    }
}
