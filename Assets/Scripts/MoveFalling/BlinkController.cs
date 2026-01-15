using UnityEngine;

public class BlinkController : MonoBehaviour {

	public float timeHide = 6f;
	public float timeShow = 10f;
	public float timeHideShow = 0.1f;

	public GameObject ObjectGraphics; // Doi tuong con
	public Collider2D col;

	private float nextHiden = 0f;
	private float nextBlink = 0f;

	private bool Blink = false;
	private bool Hidden = false;
	private bool continued = true;

	public GameObject PhuThuoc;

	// Use this for initialization
	void Start () {
		nextHiden = timeHide;
		nextBlink = 0f;
	}

	/// <summary>
	/// Settings the blink.
	/// </summary>
	public void SettingBlink ()
	{
		// Chop nhay khi va cham duoc bat, tuong ung voi viec buc da hien
		if (col.enabled) {
			// Chop nhay sau thoi gian timeHideShow, nghi mot khoang
			if (nextBlink < Time.time) {
			// Chop nhay
				ObjectGraphics.SetActive (!Blink);
				Blink = !Blink;

				nextBlink = timeHideShow + Time.time;
			}
			// Thuc hien an hoan toan sau 2f
			Invoke ("SettingHidden",2f);
		}
	}

	public void XuLyChopNhay (float timeHide, ref bool contin)
	{
		if (contin) {
			// Thay doi thoi gian tiep theo se an buc
			nextHiden = timeHide + Time.time;
			contin = false;
		}
		// Kiem tra thoi gian de an buc
		if (nextHiden < Time.time) {
			SettingBlink ();
			// Thuc hien chop nhay, sau do an hoan toan
		}
	}

	private void ChangeTime ()
	{
		// Kiem tra da an chua
		if (Hidden) {
			// Thuc hien hien lai buc
			Invoke ("SettingShow", timeShow);
			continued = true;
		}
		else
			if (!Hidden) {
				// Thay doi thoi gian tiep theo an buc
				// Chi thuc hien lan dau tien sau khi no da hien
				XuLyChopNhay (timeHide,ref continued);
			}
	}

	// Update is called once per frame
	void Update () {
		if (PhuThuoc == null) {
			ChangeTime ();
		} else {
			if (PhuThuoc.activeSelf) {
				ChangeTime ();
			} else {
				SetObject (true);
			}
		}
	}

	private void SettingHidden(){
		SetObject (false);
		CancelInvoke(); // Huy tat ca cac hang cho
	}

	private void SettingShow(){
		SetObject (true);
		CancelInvoke(); // Huy tat ca cac hang cho
	}

	private void SetObject(bool value){
		col.enabled = value;
		ObjectGraphics.SetActive (value);
		Hidden = !value;
	}
}
