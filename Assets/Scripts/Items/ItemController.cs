using UnityEngine;

public class ItemController : MonoBehaviour {

	public GameObject ItemObject;
	public float scopeHorizontal = 10f; // Khoang xa
	public float scopeHight = 15f; //Khoang cao
	private float nextBlink = 0f;
	public float timeBlink = 0.2f;
	public float timeDestroy = 10f;
    public bool Isdestroy = true;
	private Rigidbody2D itemRB;
	private bool Blink = false;
	private float nextHiden = 0f;
	private bool continued = true;
    public bool boolForce = true;
	private void Awake(){
        if (boolForce)
        {
            SetForceAwake(ref itemRB);
        }
	}

	public void SetForceAwake (ref Rigidbody2D _bombRG)
	{
		_bombRG = GetComponent<Rigidbody2D> ();
		_bombRG.AddForce (new Vector2 (Random.Range (-scopeHorizontal, scopeHorizontal), Random.Range (0, scopeHight)), ForceMode2D.Impulse);
	}

	private void Update(){
        if (Isdestroy)
        {
            XuLyChopNhay();
        }
	}

	/// <summary>
	/// Settings the blink.
	/// </summary>
	public void SettingBlink ()
	{
		// Chop nhay khi va cham duoc bat, tuong ung voi viec buc da hien
		// Chop nhay sau thoi gian timeHideShow, nghi mot khoang
		if (nextBlink < Time.time) {
			// Chop nhay
			ItemObject.SetActive (!Blink);
			Blink = !Blink;
			nextBlink = timeBlink + Time.time;
		}
        // Thuc hien an hoan toan sau 2f
        if (gameObject.tag != "Item")
        {
            Destroy(this.gameObject, 2f);
        }
	}

	public void XuLyChopNhay ()
	{
		if (continued) {
			// Thay doi thoi gian tiep theo se an buc
			nextHiden = timeDestroy + Time.time;
			continued = false;
		}
		// Kiem tra thoi gian de an buc
		if (nextHiden < Time.time) {
			SettingBlink ();
		}
	}
}
