using UnityEngine;

public enum Direction {None,Top,Right,Bottom,Left};

/// <summary>
/// Door character.
/// </summary>
public class DoorCharacter : MonoBehaviour {

	// Thuc hien lay thong tin
	// Khai bao static de co the truy xuat huoong doi tuong ra ben ngoai
	public static DoorCharacter _player;
	// Xac nhan dieu huong
	public Direction direction;

	/// <summary>
	/// Contructors the control.
	/// </summary>
	private void SettingGameObject ()
	{
        if (_player == null && this.gameObject.CompareTag("PlayerManager"))
        {
            direction = GetDriection();

			_player = this; // Gan player bang chinh no
        }
        else if (!this.gameObject.CompareTag("PlayerManager"))
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<DoorCharacter>();
                if (GetDriection() != Direction.None)
                {
                    _player.direction = GetDriection();
                }
            }
			// Kiem tra nhan vat moi bat dau
            if (_player != null && _player.direction != Direction.None)
            {
                if (direction == _player.direction)
                {
                    // Kiem tra khi chuyen man
                    // Gan vi tri moi
                    _player.gameObject.transform.position = this.gameObject.transform.position;
                    PlayerPrefs.SetString("Direction", Direction.None.ToString());
                }
			}
			// Neu no chi la noi xac dinh vi tri thi destroy luon
            this.gameObject.SetActive(false);
		}
	}

    private Direction GetDriection()
    {
        if (PlayerPrefs.GetString("Direction") != null)
        {
            string dir = PlayerPrefs.GetString("Direction");
            if (dir == "Left")
            {
                return Direction.Left;
            }
            else if (dir == "Right")
            {
                return Direction.Right;
            }
            else if (dir == "Top")
            {
                return Direction.Top;
            }
            else if (dir == "Bottom")
            {
                return Direction.Bottom;
            }
            else
            {
                return Direction.None;
            }
        }
        else
        {
            return Direction.None;
        }
    }

	//
	private void Awake(){
		SettingGameObject ();
	}
}
