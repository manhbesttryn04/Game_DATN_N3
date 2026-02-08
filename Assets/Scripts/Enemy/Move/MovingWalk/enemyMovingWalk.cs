using UnityEngine;

public class enemyMovingWalk : MonoBehaviour {

	public bool Walk = true;
	public bool BlockFacing = false;
	// class enemyMoving duoc dung cho gameObject cha
	public bool facingRight = true; // Huong ve phia ben phai 
	public float minX, maxX; // Pham vi di chuyen cua quai
	public float Speed = 3f; // Van toc di chuyen
	//  
	public float speedAfterLook = 6f;

	public GameObject EnemyGraphics; 

	private int h = 1; // Tuong tu facingRight, Xac dinh "van toc" huong ve ben phai neu = 1 va nguoc lai 
	private Rigidbody2D enemyRB; // Lay rigidbody cua enemyMoving

	private float speedSub = 0f;

	private float nextTimeFacing = 0f;
	public float timeFacing = 3f;

	private void Awake(){
		nextTimeFacing = 0f;
		// Lay thong tin khi game moi duoc thuc thi
		enemyRB = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		// Set thoi gian gap
		//  speedAfterLook = 6f;
		//Speed = 3f;
		Speed = Mathf.Abs (Speed);
		speedSub = Speed;
		// Dinh nghia h lai 1 lan nua
		if (!facingRight) {
			h = -1;
		}
	}

	void EnemyMovingWalk ()
	{
		if (EnemyGraphics != null) {
			if (EnemyGraphics.activeInHierarchy) {
				if (Walk) { // Xac dinh pham vi di chuyen cua quai trong pham vi max, min
                    if (!facingRight && EnemyGraphics.transform.position.x < minX)
                    {
                        flip();
                    }
                    else if (facingRight && EnemyGraphics.transform.position.x > maxX)
                    {
                        flip();
                    }
					// Gan cho quai mot toc do
					enemyRB.AddForce (new Vector2 (h, 0) * Speed);
                }
			}
		}
	}

    public void SetVelocity(Vector2 force)
    {
        enemyRB.linearVelocity = force;
    }

    public void AddForceWalk(float Speed)
    {
        enemyRB.AddForce(new Vector2(h, 0) * Speed,ForceMode2D.Impulse);
    }

	// Update is called once per frame
	void Update () {
        if (!PauseUI.PauseGame)
        {
            EnemyMovingWalk();
        }
	}
		
	public void flip(){
        // Block facing when moving
        if (!BlockFacing)
        {
            facingRight = !facingRight; // Xac dinh doi huong
        }
        // Change 
		if (!facingRight) {
			h = -1;
		} else {
			h = 1;
		}
		Vector3 theScale = EnemyGraphics.transform.localScale;
		theScale.x *= -1; 
		EnemyGraphics.transform.localScale = theScale;
	}

	/// <summary>
	/// Raises the trigger enter2 d event.
	/// </summary>
	/// <param name="other">Other.</param>
	private void OnTriggerStay2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("Player") && !BlockFacing)
            {
                if (Vector2.Distance(gameObject.transform.position, other.gameObject.transform.position) <=30f && nextTimeFacing < Time.time)
                {
                    // Huong ve player 
                    if (EnemyGraphics != null)
                    {
                        Transform transEnemy = EnemyGraphics.transform;
                        if (!facingRight && other.transform.position.x > transEnemy.position.x)
                        {
                            flip();
                        }
                        else if (facingRight && other.transform.position.x < transEnemy.position.x)
                        {
                            flip();
                        }
                    }
                    Speed = speedAfterLook;
                    nextTimeFacing = timeFacing + Time.time;
                }
            }
        }
	}

	private void OnTriggerExit2D(Collider2D other){
        if (!ChangeHide.TanHinh)
        {
            if (other.CompareTag("Player"))
            {
                Speed = speedSub;
            }
        }
	}
}