using UnityEngine;

public class EnemyItem : MonoBehaviour {
	public GameObject EnemyGraphics;
	public Transform GunTip;

	public GameObject[] ArrayItems; 
	private int lenItem = 0;

	private void Awake (){
		lenItem = 0;
	}

	// Use this for initialization
	void Update () {
		if (!EnemyGraphics.activeSelf) {
			FallItems ();
		} else {
			lenItem = 0;
		}
	}

	public void FallItems(){
		// Kiem tra so luong phan tu
		if (ArrayItems.Length > 0) {
			// Duyet mang
			if (lenItem < ArrayItems.Length) {
				foreach (GameObject item in ArrayItems) {
					// Kiem tra phan tu
					if (item != null) {
						lenItem++;
						Instantiate (item, GunTip.position, Quaternion.Euler (0, 0, 0));
					}
				}
			}
		}
	}
}
