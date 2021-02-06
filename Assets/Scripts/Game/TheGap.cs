using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGap : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject topTri;

    void Start()
    {
    }


    void Move()
    {
        transform.Translate(-0.01f, 0, 0);
    }

    public void Reset()
    {
        // TODO: RNGesus y position

        transform.position = new Vector3(2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

}
