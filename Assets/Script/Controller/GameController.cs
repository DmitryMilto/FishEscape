using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //public static GameObject playerG;
    public Transform player;

    [SerializeField]
    public static float speed;
    void Start()
    {
        //Instantiate(playerG, new Vector3(-0.07f, -0.15f, -5f), Quaternion.identity);
        //player = playerG.transform;
    }

    void OnMouseDrag()
    {
        //if (!TriggerPlayer.GameOver) {
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mousePos.y = mousePos.y > 3.26f ? 3.26f : mousePos.y;
        //    mousePos.y = mousePos.y < -4f ? -4f : mousePos.y;

        //    //mousePos.x = mousePos.x > 6.5f ? 6.5f : mousePos.x;
        //    //mousePos.x = mousePos.x < -6.5f ? -6.5f : mousePos.x;

        //    player.position = Vector2.MoveTowards(player.position, new Vector2(-2.5f, mousePos.y), Time.deltaTime * speed);
        //}
    }
}
