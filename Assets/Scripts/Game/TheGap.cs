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
        P1 = this.transform.GetChild(0).GetComponent<Transform>();
        P2 = this.transform.GetChild(1).GetComponent<Transform>();
    }


    public void Move(float speed)
    {
        Debug.Log(speed);
        transform.Translate(-speed, 0, 0);
    }

    public void Reset(int yRange, int gapRange, int rotation, int offset1Range, int offset2Range)
    {
        float y = (float)(-0.24 + yRange / 100.0 * (0.24 * 2));

        float gap = (float)(gapRange / 100.0 * 0.1);


        float offset1 = (float)(offset1Range / 100.0);
        float offset2 = (float)(offset2Range / 100.0);

        transform.Rotate((float)rotation * 180, 0, 0);

        P1.localPosition = new Vector3(offset1, 0.8f + gap, 0);
        P2.localPosition = new Vector3(offset2, -0.8f - gap, 0);
        transform.position = new Vector3(3, y, 0);
    }

}
