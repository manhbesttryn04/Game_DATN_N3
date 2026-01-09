using UnityEngine;

public class EnemyFlyAttack : EnemyDanhGan {
	
	private EnemyLaoVao laoVao;

    public override void Awake(){
		base.nextDamage = 0f;
		base.enemyHealth = EnemyGraphics.GetComponent <EnemyHealth> ();
		laoVao = GetComponentInParent <EnemyLaoVao>();
	}

	private void OnTriggerStay2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("Player") && !laoVao.Attack)
            {
                sound.Playsound("AttackEnemy");
                laoVao.Attack = true;
                base.CheckAttack(other);
            }
        }
	}
}
