using UnityEngine;

public class EnemyDieShowTag : MonoBehaviour {

	public static EnemyDieShowTag dieShowTag;

	public GameObject[] ArrayTag;

	public GameObject Fllow;

	// Use this for initialization
	void Start () {
		// An cac doi tuong can thiet
		ChangeGameObjectWithTag (false);
	}

	public void ChangeGameObjectWithTag (bool value)
	{
		if (ArrayTag.Length > 0) {
			foreach (GameObject gameO in ArrayTag) {
				if (gameO != null) {
					gameO.SetActive (value);
				}
			}
		}
	}

	private void Update(){
		if (!Fllow.activeSelf) {
			this.ChangeGameObjectWithTag (true);
		}
	}
}
