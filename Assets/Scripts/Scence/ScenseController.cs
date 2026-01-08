using UnityEngine;

public class ScenseController : MonoBehaviour {

	public int fameRate = 25;
	private void Awake(){
		Application.targetFrameRate = fameRate;
	}
}
