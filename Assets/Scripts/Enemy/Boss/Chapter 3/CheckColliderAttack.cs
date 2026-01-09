using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColliderAttack : MonoBehaviour {

    public static bool IsScope = false; // Xac nhan dang trong pham vi tan cong gan cua quai
    private void OnTriggerStay2D(Collider2D other)
    {
        // Kiem tra nhan vat
        if (other.CompareTag("Player"))
        {
            IsScope = true; // Nhan vat da di vao pham vi tan cong
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Kiem tra nhan vat
        if (other.CompareTag("Player"))
        {
            IsScope = false; // Huy pham vi tan cong
        }
    }
}
