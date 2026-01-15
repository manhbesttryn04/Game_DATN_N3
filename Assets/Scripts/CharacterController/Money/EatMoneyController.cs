using UnityEngine;

public class EatMoneyController : MonoBehaviour {

    private Rigidbody2D myRB = new Rigidbody2D();
    public SoundManager sound;
    private void Start()
    {
        sound = GameObject.FindGameObjectWithTag("sound").GetComponent<SoundManager>();
    }
    private void ThucHienChoPhepVatPhamBay(GameObject Obj)
    {
        if (Obj != null)
        {
            if (Obj.GetComponent<Rigidbody2D>() == null)
            {
                myRB = Obj.AddComponent<Rigidbody2D>() as Rigidbody2D;
            }
           
            // Cho trong luc = 0;
            myRB.gravityScale = 0;
            // + y
            myRB.AddForce(new Vector2(0, 1) * 2, ForceMode2D.Impulse);
            Destroy(Obj, 2);
        }
        //
    }

	private void OnTriggerEnter2D(Collider2D money){
		if (money.CompareTag ("Money")) 
            
            if (money.GetComponent<Rigidbody2D>() == null)
            {
                money.GetComponent<Collider2D>().enabled = false;

                PlayerHealth.control._Player.Gold += 100;
                ThucHienChoPhepVatPhamBay(money.gameObject);
                sound.Playsound("Vang");
            }            
		}
}

