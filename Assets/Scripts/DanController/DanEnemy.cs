using UnityEngine;

public class DanEnemy : DanCharacter {

    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    public string NameParameter = "No";

	private void OnTriggerEnter2D(Collider2D col){
		if((!ChangeHide.TanHinh && col.CompareTag ("Player") )|| col.CompareTag ("DanPlayer")){
			if (anim != null) {
				anim.SetBool ("No", true);
                sound.Playsound("BombVaCham");
			}
			bd.linearVelocity = Vector3.zero;
			Destroy (gameObject,0.5f);
		}
	}
}
