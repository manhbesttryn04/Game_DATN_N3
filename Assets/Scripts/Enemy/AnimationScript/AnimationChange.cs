using UnityEngine;

public class AnimationChange : MonoBehaviour {

	public GameObject EnemyGraphics;
	private Animator anim; 

	private void Awake(){
		anim = EnemyGraphics.GetComponent <Animator> ();
	}

	public Animator GetAnimation(){
		return anim;
	}

	public void SetAnimation(string paras,bool value){
		anim.SetBool (paras,value);
	}
}
