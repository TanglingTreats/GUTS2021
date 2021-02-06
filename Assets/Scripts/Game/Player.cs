using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Timer is in seconds
    public float timer;
    private float currTime;
    public float jumpVal;

    // GameObjects
    private GameObject player;
    private Transform playerPos;
    private Rigidbody2D playerBody;



    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.playerPos = player.GetComponent<Transform>();
        this.playerBody = player.GetComponent<Rigidbody2D>();

        this.currTime = this.timer;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Jump") && this.currTime > 0)
        {
            Jump(this.jumpVal);
            DecreaseTimer(0.1f);
        }
    }

    public void Jump(float val)
    {
        playerBody.velocity = new Vector3(0, val, 0);
    }

    public void DecreaseTimer(float step)
    {
        this.currTime -= step;
    }

    public void ResetTimer()
    {
        this.currTime = this.timer;
    }

    public void Kill()
    {
        // TODO: Implement death
    }
}
