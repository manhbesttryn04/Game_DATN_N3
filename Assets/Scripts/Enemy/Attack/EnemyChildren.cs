using UnityEngine;

public class EnemyChildren : MonoBehaviour {
	public EnemyAnimation ThongTinBomb;
	public GameObject EnemyGraphics;
	private float nextThaBom = 0f;
	private bool Die = false;
	public float spaceTimeThaBomb = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (EnemyGraphics != null) {
			Die = EnemyGraphics.GetComponent <EnemyHealth> ().GetDead ();
		}
	}

	private void ThaBombKhiTrongPhamVi ()
	{
		// Sau moi thoi gian spaceTimeThaBomb tha 1 lan
		if (nextThaBom < Time.time) {
			Instantiate (ThongTinBomb.Graphicss, ThongTinBomb.GunTips.transform.position, Quaternion.identity);
			nextThaBom = Time.time + spaceTimeThaBomb;
		}
	}

	private void OnTriggerStay2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("Player") && !Die)
            {
                // Tha bomb khi nhan vat di vao pham vi hoat dong
                ThaBombKhiTrongPhamVi();
            }
        }
	}
}
