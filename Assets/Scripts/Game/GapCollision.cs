﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GapCollision : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.Kill();
        }
    }
}
