using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
 
public class MovePlayer : NetworkBehaviour 
{
    [SerializeField]
    private float speed;
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
                gameObject.GetComponent<ChatBehaviour>().canvas.SetActive(true);
                gameObject.GetComponent<MovePlayer>().enabled = false;
            }
        }
    }
}