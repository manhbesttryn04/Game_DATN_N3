using UnityEngine;

public class JumpController : MonoBehaviour
{
	public float timeCancelJump = 0.4f;
    private SpriteRenderer sprite;

    private void Awake(){
        sprite = GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start()
    {

    }

    private void SetColorForSpriteRenderer()
    {
        if (ChangeHide.TanHinh)
        {
            sprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            sprite.color = new Color(1f,1f,1f,1f);
        }
    }

    // Update is called once per frame
	void Update()
    {
        // Lam mo
        SetColorForSpriteRenderer();
        // Thuc hien hien an hieu ung nhao lon tren khong
        if (PlayerMoving.Jump && !PlayerHealth.control.GetHurt ()) {
            sprite.enabled = PlayerMoving.Jump;
            // Dung xoay trong 0.4s
            Invoke ("SetBoolJump", timeCancelJump);
           
		} else {
			SetBoolJump ();
		}
    }

    private void SetBoolJump()
    {
        sprite.enabled = false;
		PlayerMoving.Jump = false;
    }
    
}
