using UnityEngine;

public class DanNghien : MonoBehaviour {
    public float Speed = 40f;
    protected Animator anim;
    protected Rigidbody2D bd;
    public float TimeLive;
    public bool blAnim = true;
    private EnemyLaoVao laoVao;

    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }

    protected void Awake()
    {
        bd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // Dieu chinh huong bay
        if (this.gameObject.transform.localRotation.y > 0)
        {
            bd.AddForce(new Vector2(-1, -1) * Speed, ForceMode2D.Impulse);
        }
        if (this.gameObject.transform.localRotation.y <= 0)
        {
            bd.AddForce(new Vector2(1, -1) * Speed, ForceMode2D.Impulse);
        }
        Destroy(gameObject, TimeLive);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("Ground") )
        {
            if (anim != null && blAnim)
            {
                sound.Playsound("BombVaCham");
                anim.SetBool("No", true);
                bd.linearVelocity = Vector3.zero;
               
            }
            Destroy(gameObject, 0.5f);
        }
    }
}
