using UnityEngine;

public class DieCharacter : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D other){
		// Kiem tra va cham
		// Nhung doi tuong co tag = GaoChet khi palyer cham se phai chet
		if(other.gameObject.tag == "GaiChet"){
			// Thuc hien chet
			PlayerHealth.control.Dead ();
		}
	}

	private void OnTriggerStay2D(Collider2D other){
		// Kiem tra va cham
		// Nhung doi tuong co tag = GaoChet khi palyer cham se phai chet
		if(other.gameObject.tag == "Player"){
			// Thuc hien chet
			PlayerHealth.control.Dead ();
		}
	}
}
