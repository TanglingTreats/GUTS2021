using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Mirror;
 
public class MovePlayer : NetworkBehaviour 
{
    [SerializeField]
    private float speed;

    void Start()
    {
        List<GameObject> playerGOs =  GameObject.FindGameObjectsWithTag("Player").ToList();
        if (playerGOs.Count < 2)
        {
            foreach (var playerGO in playerGOs)
            {
                playerGO.GetComponent<MovePlayer>().enabled = false;
            }
        }
        else
        {
            foreach (var playerGO in playerGOs)
            {
                playerGO.GetComponent<MovePlayer>().enabled = true;
            }
        }
    }
    void OnEnable()
    {
        //gameObject.GetComponent<ChatBehaviour>().canvas.SetActive(false);
    }
    void FixedUpdate () 
    {
        if(this.isLocalPlayer) 
        {
            float movement = Input.GetAxis("Horizontal");	
            GetComponent<Rigidbody2D>().velocity = new Vector2(movement * speed, 0.0f);
            if(Input.GetKeyDown(KeyCode.Q))
            {
                List<GameObject> playerGOs =  GameObject.FindGameObjectsWithTag("Player").ToList();
                foreach (var playerGO in playerGOs)
                {
                    gameObject.GetComponent<ChatBehaviour>().canvas.SetActive(true);
                    gameObject.GetComponent<MovePlayer>().enabled = false;
                }
            }
        }
    }
}