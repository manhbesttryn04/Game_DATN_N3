using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyController : MonoBehaviour {

    private static DontDestroyController _playerManager;
	// Use this for initialization
    void Awake()
    {
        if (_playerManager == null)
        {
            // Khong uy khi chuyen qua scence moi
            DontDestroyOnLoad(this.gameObject);
            _playerManager = this; // Gan player bang chinh no
        }
        else if (_playerManager != this)
        {
            // Neu no chi la noi xac dinh vi tri thi destroy luon
            Destroy(this.gameObject);
        }
    }
}
