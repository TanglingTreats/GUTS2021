using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Timer is in seconds
    public float timer;
    private float currTime;
    public float jumpVal;

    private int deadCount = 0;

    private bool isReleased = false;
    private bool isDead = false;

    // GameObjects
    private GameObject player;
    private Transform playerPos;
    private Rigidbody2D playerBody;

    private GameController gc;


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.playerPos = player.GetComponent<Transform>();
        this.playerBody = player.GetComponent<Rigidbody2D>();

        this.gc = GameObject.Find("GameController").GetComponent<GameController>();

        this.currTime = this.timer;
    }

    // Update is called once per frame
    void Update()
    {

        if(!isDead)
        {
            if (Input.GetButton("Jump") && this.currTime > 0 && !GetIsReleased())
            {
                Jump(this.jumpVal);
                DecreaseTimer(0.1f);
            }
            else if (Input.GetButtonUp("Jump"))
            {
                SetIsReleased(true);

            }
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

    public void SetIsReleased(bool flag)
    {
        this.isReleased = flag;
    }

    public bool GetIsReleased()
    {
        return this.isReleased;
    }

    public void ResetJump()
    {
        SetIsReleased(false);
        this.currTime = this.timer;
    }

    public void Kill()
    {
        ++deadCount;
        this.isDead = true;
        this.gc.SetDeathState(true);

        Debug.Log("dead: " + deadCount);
    }
}
