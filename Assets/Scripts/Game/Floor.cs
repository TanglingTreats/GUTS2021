using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private Player player;
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player").GetComponent<Player>();
        this.gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            this.player.ResetJump();
            this.gc.Pulse();
        }
    }
}
