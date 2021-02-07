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

    private bool isReleased = true;
    private bool isLanded = true;

    private bool jump = false;


    private bool isDead = false;

    // GameObjects
    private GameObject player;
    private Transform playerPos;
    private Rigidbody2D playerBody;
    private ParticleSystem buttSpark;

    private GameController gc;


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.playerPos = player.GetComponent<Transform>();
        this.playerBody = player.GetComponent<Rigidbody2D>();

        this.buttSpark = GameObject.Find("CubeSparks").GetComponent<ParticleSystem>();
        
        this.gc = GameObject.Find("GameController").GetComponent<GameController>();

        this.currTime = this.timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // Initial jump
            if (Input.GetButton("Jump"))
            {
                if (this.currTime == this.timer && this.isReleased && this.isLanded)
                {
                    this.buttSpark.Stop();
                    this.isLanded = false;
                    this.jump = true;
                }
                this.isReleased = false;
            }

            if (Input.GetButtonUp("Jump"))
            {
                this.isReleased = true;
            }

            if (this.currTime <= 0 || this.isReleased)
            {
                this.jump = false;
            }

            if (this.jump)
            {
                Jump(this.jumpVal);
            }

        }
    }

    public void Jump(float val)
    {
        playerBody.velocity = new Vector3(0, val, 0);
        DecreaseTimer(0.1f);
    }

    public void DecreaseTimer(float step)
    {
        this.currTime -= step;
    }

    public void Land()
    {
        if (!this.isDead)
            this.buttSpark.Play();
        this.isLanded = true;
        this.ResetJump();
    }

    public void ResetJump()
    {
        this.currTime = this.timer;
    }

    public void Kill()
    {
        ++deadCount;
        this.isDead = true;
        this.buttSpark.Stop();
        this.gc.SetDeathState(true);
        playerBody.constraints = RigidbodyConstraints2D.None;
        this.gc.DeathSequence();

        Debug.Log("dead: " + deadCount);
    }
}
