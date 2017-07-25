using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    private Player player;

    void Awake()
    {
        player = GameObject.Find("player").GetComponent<Player>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (gameObject.name == "LeftButton")
        {
            player.SetMoveLeft(true);
        }
        else
        {
            player.SetMoveLeft(false);
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        player.StopMoving();
    }


} // Joystick
