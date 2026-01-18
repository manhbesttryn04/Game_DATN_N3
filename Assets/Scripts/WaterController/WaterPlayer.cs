using UnityEngine;

public class WaterPlayer : MonoBehaviour {

	public float timeSetTrigger = 0.25f;

	private bool arround = false;
	private Collider2D target;

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerEnter2D(Collider2D other){
		// Kiem tra va cham
		if (other.CompareTag ("Player")) {
			PlayerMoving.Move = false;
			PlayerMoving.LockJump = true;
			if (!arround) {
				arround = true;
				other.GetComponent <CapsuleCollider2D> ().isTrigger = true;
				target = other;
			}
		}
	}

	/// <summary>
	/// Raises the trigger stay2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerStay2D(Collider2D other){
		// Kiem tra va cham
		if (other.CompareTag ("Player")) {
			PlayerMoving.Move = false;
			PlayerMoving.LockJump = true;
			Invoke ("SetTrigger",timeSetTrigger);
		}
	}

	/// <summary>
	/// Sets the trigger.
	/// </summary>
	private void SetTrigger(){
		target.GetComponent <CapsuleCollider2D> ().isTrigger = false;
		target.GetComponent <PlayerHealth>().Dead ();
	}
}
