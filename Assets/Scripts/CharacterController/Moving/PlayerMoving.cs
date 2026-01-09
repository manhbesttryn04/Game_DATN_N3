using UnityEngine;

public class PlayerMoving : MonoBehaviour
{ 
    //Am thanh
    public SoundManager sound;
 
    public float Speed = 15f; // Speed of Character
    public float maxVelocity = 10f; // Max velocity of Character
    public float jumpHeight = 250f;
    public float maxJump = 5f;
	//
    public GameObject wind;// Lay doi tuong animation gio
    //[SerializeField] // Use access Rigidbody outside

	private ChangePlayer changePlayer;
	//
	public static bool Jump = false; // Kiem tra nhay
	public static bool LockJump = false; // Khoa nhay
	public static bool Move = true; // Mo/Khoa di chuyen qua lai
	private bool grounded = false;
	public static Animator anim; // Lay animation
	public static Rigidbody2D myBody; //
	//
    public static float AvtiveMoving = 0; // Thuc hien di chuyen bang nut
    public static bool AvtiveJump = false; // Thuc hien nhay bang nut
    public static float h = 0;
    /// <summary>
    /// Awake this instance.
    /// </summary>
    private void Awake()
    {
		changePlayer = GetComponentInChildren <ChangePlayer>();
    }

    // Use this for initialization
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
        myBody = GetComponent<Rigidbody2D>(); // Get component Rigidbody2D
		Move = true;
        LockJump = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
	{	
		//
		if (!PlayerHealth.control.GetDie () && !PlayerHealth.control.GetHurt ()) {
			// Handling move character by keyboard
			PlayerMoveKeyboard ();
		} else {
            if (!ChangeHide.TanHinh)
            {
                SetSpriteRenderer(true);// Hien lai nhan vat
            }
			SetEnabledWind (false);
		}
    }

    // Use handling character move by keyboard
    private void PlayerMoveKeyboard()
    {
        float forxeX = 0f; // Position X
        float forxeY = 0f; // Position Y

        // Gets input keyboard to move left, right
        h = Input.GetAxisRaw("Horizontal");
        // Thuc hien kiem tra di chuyen
        if (h == 0 && AvtiveMoving != 0)
        {
            h = AvtiveMoving;
        }
		// Neu dang chuong thi dung chay
		if (Move) {
			// Move plaryer 
			MovePlayer (ref forxeX, h);
        }
        else
        {
            SetEnabledWind(false); // Huy gio
        }

        if (!LockJump) // Khoa khong cho nhan vat thuc hien nhay len
        {
            JumpPlayer(h, ref forxeY); // Thuc hien nhay
        }
        else {
            // Hien lai nhan vat
            PlayerMoving.Jump = false;
        }

         //Thuc hien an, hien nhan vat
        if (!Jump || LockJump)
        {
            SetSpriteRenderer(true); // Hien lai
        }
        else
        {
            SetSpriteRenderer(false); // An lai
        }

        // Thay doi thong so nhan vat
        myBody.AddForce(new Vector2(forxeX, forxeY), ForceMode2D.Force);    // Change position of character

    }

    // Thuc hien di chuyen
    private void MovePlayer(ref float forxeX, float h)
    {
        if (h > 0)
        {
          
          
            SetLocalScale(h);
            // Chuyen huong cua nhan vat
            forxeX = GetForxeX(h, forxeX);
            // Call
        }
        if (h < 0)
        {
         
            // Move right
            SetLocalScale(h);
            forxeX = GetForxeX(h, forxeX);
            // Call
		}
        if (h == 0)
        {
            anim.SetBool("Walk", false);
            SetEnabledWind(anim.GetBool("Walk"));
        }
    }

    /// <summary>
    /// Sets the local scale.
    /// </summary>
    /// <param name="h">The height.</param>
    private void SetLocalScale(float h)
    {
        // Set localScale for character
        Vector3 temp = transform.localScale;
        temp.x = h * Mathf.Abs(transform.localScale.x);
        transform.localScale = temp;

        // Set an/hien gio
		anim.SetBool("Walk", true);
        SetEnabledWind(anim.GetBool("Walk"));
    }

    /// <summary>
    /// Sets the local scale and animation.
    /// </summary>
    /// <returns>The local scale and animation.</returns>
    /// <param name="vel">Vel.</param>
    /// <param name="scaleX">Scale x.</param>
    /// <param name="h">The height.</param>
    private float GetForxeX(float h, float forxeX = 0f)
    {
        float vel = Mathf.Abs(myBody.linearVelocity.x); // Get velocity of character
        if (vel < maxVelocity)
        {
            // Truong hop binh thuong khong co vat can
            if (grounded)
            {
                forxeX = Speed * h;
            }
            else
            {
                // Truong hop khong co vat can
                forxeX = Speed * h * 1.5f;
            }
        }
        return forxeX;
    }

	private void SetSpriteRenderer (bool value)
	{
        if (changePlayer.KnightCharater != null && changePlayer.DragonCharater != null)
        {
            if (ChangePlayer.IsKnight)
            {
                changePlayer.KnightCharater.GetComponent<SpriteRenderer>().enabled = value;
            }
            else
            {
                changePlayer.DragonCharater.GetComponent<SpriteRenderer>().enabled = value;
            }
        }
	}

    // Hanh dong nhay
    private void JumpPlayer(float h, ref float forxeY)
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || AvtiveJump)
        {
            // Kiem tra neu dang dung tren nen dat
            if (grounded)
            {
             
                grounded = false;
                // Gioi han luc day nhan vat di len
				if (myBody.linearVelocity.y < maxJump)
                {
                    forxeY = jumpHeight;
                    sound.Playsound("Nhay");
                }
                // Hien nhao lon
				Jump = true;
                //
            }
        }
    }
    /// <summary>
    /// Hiddens the wind to run.
    /// </summary>
    public void SetEnabledWind(bool value)
    {
        if (grounded && value)
        {
            wind.gameObject.SetActive(value);
        }
        else
        {
            wind.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Raises the collision enter2 d event.
    /// </summary>
    /// <param name="target">Target.</param>
    private void OnCollisionEnter2D(Collision2D target)
    {
        // Xu ly va cham giua nhan vat va nen dat
        CollisionBetweenPlayerAndGround(target);
    }

    private void CollisionBetweenPlayerAndGround(Collision2D target)
    {
		if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Falling")
        {
            grounded = true;
        }
		if (target.gameObject.tag == "Thung") {
			grounded = true;
			maxJump = 10;
		} else {
			maxJump = 1;
		}
    }
}