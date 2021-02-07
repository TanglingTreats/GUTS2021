using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        this.gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "TheGap")
        {
            this.gc.TriggerReset();
        }
    }
}
