using System.Collections.Generic;
using UnityEngine;

public class enemyMovingFly : MonoBehaviour {
    //

    public bool facingRight = true;
    public bool BlockFacing = false;
	public float Speed = 0.1f;
	public GameObject Enemy; 
	// Danh sach diem bay
	protected List<Transform> DanhSachDiem;

	protected int i = 1;
	protected Vector2 positionEnemy;
	protected Vector2 positionPoint;
	protected float n2Speed;

    // Use this for initialization
	private void Awake()
    {
		n2Speed = Speed / 2;
    }
    void Start () {
	}

	protected void AddTransform(Transform trans){
		DanhSachDiem.Add (trans);
	}

	/// <summary>
	/// Checks the position enemy with point.
	/// </summary>
	/// <returns><c>true</c>, if position enemy with point was checked, <c>false</c> otherwise.</returns>
	/// <param name="enemy">Enemy.</param>
	/// <param name="point">Point.</param>
	protected bool checkPositionEnemyWithPoint (Vector2 enemy,Vector2 point){
		// Kiem tra da bang vi tri enemy hay chua
		// Vi tri o day chi xet o diem tuong doi chu khong the nao tuyet doi
		if (enemy.x - point.x > -n2Speed && enemy.x - point.x < n2Speed) {
			if( enemy.y - point.y > -n2Speed && enemy.y - point.y < n2Speed){
				return false; // Da bang diem
			}
		}
		return true; // Chua bang diem

	}

	/// <summary>
	/// Movings the fly ponit.
	/// </summary>
	public void MovingFlyPonit ()
	{
		n2Speed = Speed / 2;
		// Gan vi tri
		positionEnemy = transform.position;
		positionPoint = DanhSachDiem [i].position;

		// Kiem tra vi tri enemy va diem 
		if (checkPositionEnemyWithPoint(positionEnemy, positionPoint)) {
			FllowingPointOther (positionEnemy, positionPoint,Speed); // Di chuyen toi diem
		}else{
			// Neu da bang diem, thi di chuyen toi diem tiep theo
			i++;
			// Neu la diem cuoi thi quay lai diem ban dau
			if (DanhSachDiem.Count == i) {
				i = 0;
			}
            // Chan quay dau
            if (!BlockFacing)
            {
                // Kiem tra diem tiep theo nam o dau de quay dau enemy
                if (facingRight && DanhSachDiem[i].position.x < positionEnemy.x)
                {
                    flip();
                }
                else if (!facingRight && DanhSachDiem[i].position.x > positionEnemy.x)
                {
                    flip();
                }
            }
		}
	}

	protected void SetLocalScale (bool value)
	{
        if (!BlockFacing)
        {
            facingRight = value;
            Vector3 theScale = Enemy.transform.localScale;
            // Hieu chinh mac dinh
            // Neu facingRight = true, huong nhan vat se chuyen sang phai
            if (!facingRight && theScale.x > 0)
            {
                theScale.x *= -1;
            }
            if (facingRight && theScale.x < 0)
            {
                theScale.x *= -1;
            }
            Enemy.transform.localScale = theScale;
        }
	}

	/// <summary>
	/// Flip this instance.
	/// </summary>
	protected void flip(){
		SetLocalScale (!facingRight);
	}

	/// <summary>
	/// Fllowings the point other.
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	/// <param name="point">Point.</param>
	protected void FllowingPointOther(Vector2 enemy,Vector2 point, float _speed) {
		// s2Speed la 1/2 cua Speed
		// Muc dich: Thu nho pham vi va cham tuyet doi voi diem (point) 
		// Kiem tra truc x
		if (enemy.x - point.x > n2Speed || enemy.x - point.x < -n2Speed) {
			// Description: neu vi tri enemy > vi tri cua point thi giam truc x, nguoc lai
			if (enemy.x > point.x) {
				enemy.x -= _speed;
				SetLocalScale (false); // Chuyen huong di chuyen
			} else if (enemy.x < point.x) {
				enemy.x += _speed;
				SetLocalScale (true);
			}
		} else {
			
		}
		// Kiem tra truc y
		if (enemy.y - point.y > n2Speed || enemy.y - point.y < -n2Speed) {
			if (enemy.y > point.y) {
				enemy.y -= _speed;
			} else if (enemy.y < point.y) {
				enemy.y += _speed;
			}
		}
		transform.position = Vector2.Lerp(transform.position,enemy,1f);
	}
}
