using UnityEngine;

// Xu ly bi tan cong
public class EnemyNo : EnemyHealth
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ChangeHide.TanHinh)
        {
            if (collision.tag == "Player")
            {
                base.Dead();
                this.gameObject.SetActive(false);
            }
        }
    }
   
}
