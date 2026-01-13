using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Su dung cho interface IPointerUpHandler,IPointerDownHandler
using UnityEngine.UI;

public class ActiveMovingController : MonoBehaviour, IPointerUpHandler,IPointerDownHandler {

    private Button buttonMoving;

    private void Awake()
    {
        buttonMoving = GetComponent<Button>();
    }

    private void Update()
    {
        if (!PlayerMoving.Move || PlayerHealth.control.GetDie())
        {
            buttonMoving.enabled = false;
        }
        else
        {
            buttonMoving.enabled = true;
        }
    }

    // Thuc hien xu ly nhan nut
    public void OnPointerDown(PointerEventData data)
    {
        if (PlayerMoving.Move)
        {
            if (gameObject.name == "movingLeft")
            {
                PlayerMoving.AvtiveMoving = -1;
            }
            if (gameObject.name == "movingRight")
            {
                PlayerMoving.AvtiveMoving = 1;
            }
        }
        if (!PlayerMoving.LockJump)
        {
            if (gameObject.name == "movingJump")
            {
                PlayerMoving.AvtiveJump = true;
            }
        }
            if (gameObject.name == "movingDown")
            {
                // Do thing
                PlatJump.Active = true;
            }
    }

    // Thuc hien xu ly nha nut
    public void OnPointerUp (PointerEventData data)
    {
        if (gameObject.name == "movingLeft")
        {
            PlayerMoving.AvtiveMoving = 0;
        }
        if (gameObject.name == "movingRight")
        {
            PlayerMoving.AvtiveMoving = 0;
        }
        if (gameObject.name == "movingJump")
        {
            PlayerMoving.AvtiveJump = false;
        }
        if (gameObject.name == "movingDown")
        {
            // Do thing
            PlatJump.Active = false;
        }
    }
}
