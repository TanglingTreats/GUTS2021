using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    // Timer is in seconds
    public float timer;
    private float currTime;
    public float jumpVal;

    private int deadCount = 0;

    private bool isReleased = true;
    private bool isLanded = true;

    private bool jump = false;


    public bool isDead = false;

    // GameObjects
    private GameObject player;
    private Transform playerPos;
    private Rigidbody2D playerBody;

    private GameController gc;

    private List<GameObject> chatBoxes = default;

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

        if(!gc.GetPauseState() && !isDead)
        {
            // Initial jump
            if (Input.GetButton("Jump"))
            {
                if (this.currTime == this.timer && this.isReleased && this.isLanded)
                {
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

    public void FindChatBoxes()
    {
        chatBoxes = GameObject.FindGameObjectsWithTag("Player").ToList();
    }
    
    public void Jump(float val)
    {
        chatBoxes.ForEach(x =>
        {
            if(x.GetComponent<NetworkIdentity>().isLocalPlayer)
                x.GetComponent<ChatBehaviour>().SendJump(val);
        });
        //playerBody.velocity = new Vector3(0, val, 0);
        //DecreaseTimer(0.1f);
    }

    public void DecreaseTimer(float step)
    {
        this.currTime -= step;
    }

    public void Land()
    {
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

        Debug.Log("dead: " + deadCount);
        chatBoxes.ForEach(x =>
        {
            if(x.GetComponent<NetworkIdentity>().isLocalPlayer)
                x.GetComponent<ChatBehaviour>().SendDeath();
        });
    }
}
