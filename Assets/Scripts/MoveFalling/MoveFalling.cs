using UnityEngine;

public class MoveFalling : MonoBehaviour {

    public float speed = 0.08f, changeDirection = -1;
	public float minX,maxX;
    private Vector3 Move;
	// Awake
	private void Awake(){
	}

    // Use this for initialization
    void Start () {
        Move = this.transform.position;
    }

	private void CheckScopeMoving(Vector2 position,float minX,float maxX){
		if (position.x < minX || position.x > maxX) {
			speed *= changeDirection;
		}
	}
	// Update is called once per frame
	void Update () {
        if (!PauseUI.PauseGame)
        {
            CheckScopeMoving(this.transform.position, minX, maxX);
            Move.x += speed;
            this.transform.position = Move;
        }
    }

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.CompareTag ("Player")) {
			// Thay doi vi tri theo diem
			// Diem o day la platform
            GameObject.FindGameObjectWithTag("PlayerManager").transform.SetParent(transform);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.CompareTag ("Player")) {
			// Khi thoat dong nghia voi viec huy theo doi vi tri
            GameObject.FindGameObjectWithTag("PlayerManager").transform.SetParent(GameObject.FindGameObjectWithTag("SystemPlayer").transform);
		}
	}
}
