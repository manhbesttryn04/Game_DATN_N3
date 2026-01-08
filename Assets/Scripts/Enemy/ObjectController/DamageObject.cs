using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour {
    public float Damage = 25f;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) {
            PlayerHealth.control.addDamage(Damage);
        }
    }
}
