using UnityEngine;
using System.Collections.Generic;

public class ChestBreaker : MonoBehaviour
{
    [Header("Kéo tất cả đồng vàng vào đây")]
    public List<GameObject> allCoins = new List<GameObject>(); 

    [Header("Cài đặt lớp hiển thị")]
    public string layerName = "Foreground";
    public int orderInLayer = 10;

    private bool isBroken = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isBroken)
        {
            BreakChest();
        }
    }

    public void BreakChest()
    {
        isBroken = true;
        
        // Duyệt qua TỪNG đồng vàng bạn đã kéo vào danh sách
        foreach (GameObject coin in allCoins)
        {
            if (coin != null)
            {
                // 1. Hiện đồng vàng đó lên
                coin.SetActive(true);

                // 2. Ép lớp hiển thị lên trên cùng để không bị che
                SpriteRenderer sr = coin.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sortingLayerName = layerName;
                    sr.sortingOrder = orderInLayer;
                }
            }
        }

        // Xóa cái rương (HopVang)
        Destroy(gameObject);
    }
}