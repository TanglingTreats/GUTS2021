using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGap : MonoBehaviour
{
    // Start is called before the first frame update
    private System.Random rand = new System.Random();

    private Transform P1;
    private Transform P2;

    void Start()
    {
        P1 = GameObject.Find("Player1 Tri").GetComponent<Transform>();
        P2 = GameObject.Find("Player2 Tri").GetComponent<Transform>();
    }


    void Move(float speed)
    {
        transform.Translate(-speed, 0, 0);
    }

    public void Reset(int yRange, int gapRange, int rotation)
    {
        // TODO: RNGesus y position
        double y = -0.24 + yRange / 100.0 * (0.24 * 2);

        double gap = gapRange / 100.0 * 0.1;


        transform.Rotate((float)rotation * 180, 0, 0);

        P1.localPosition = new Vector3(0, 0.8f + (float)gap, 0);
        P2.localPosition = new Vector3(0, -0.8f - (float)gap, 0);
        transform.position = new Vector3(2, (float)y, 0);
    }
    // Update is called once per frame
    void Update()
    {
        Move(0.02f);
    }

}
