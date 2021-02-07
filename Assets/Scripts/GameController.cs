using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{
    Camera firstCam;
    Camera secondCam;

    int colorChange = 0;
    float colorStep = 0.01f;
    float[] colorArray1 = new float[] { 0.4f, 1f, 0.4f };
    float[] colorArray2 = new float[] { 0.4f, 1f, 0.4f };

    private TheGap gap1;
    private TheGap gap2;
    public float speed = 0.2f;
    private int speedCounter = 0;
    private System.Random rand = new System.Random();
    private int level;
    private List<String[]> levels = new List<String[]>();

    [SerializeField] private uint chatLimit = 150;
    public uint charCount = 0;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        gap1 = GameObject.Find("TheGap1").GetComponent<TheGap>();
        gap2 = GameObject.Find("TheGap2").GetComponent<TheGap>();
        level = rand.Next(99);

        using (var reader = new StreamReader(Application.streamingAssetsPath + "/level.csv"))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                levels.Add(values);
            }
        }

        // Get Camera
        firstCam = GameObject.Find("Player1 Camera").GetComponent<Camera>();
        secondCam = GameObject.Find("Player2 Camera").GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            gap1.Move(speed);
            gap2.Move(speed);
            speedCounter += 1;
            if (speedCounter % 90 == 0)
                speed *= 1.001f;
            UpdateColor();
        }
        else
        {
            // Do death stuff, trigger chat etc
        }

    }

    void UpdateColor()
    {
        if ((colorArray1[colorChange] + colorStep) <= 1 && (colorArray1[colorChange] + colorStep) >= 0.4f)
        {
            colorArray1[colorChange] += colorStep;
        }
        else
        {
            colorChange = (colorChange + 1) % 3;
            colorStep = -colorStep;
        }

        if ((colorArray2[colorChange] + colorStep) <= 1 && (colorArray2[colorChange] + colorStep) >= 0.4f)
        {
            colorArray2[colorChange] += colorStep;
        }
        else
        {
            colorChange = (colorChange + 1) % 3;
            colorStep = -colorStep;
        }

        firstCam.backgroundColor = new Color(colorArray1[0], colorArray1[1], colorArray1[2], 0.8f);
        secondCam.backgroundColor = new Color(colorArray2[0], colorArray2[1], colorArray2[2], 0.8f);
    }

    public void Pulse()
    {
        colorArray1[0] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray1[1] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray1[2] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);

        colorArray2[0] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray2[1] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray2[2] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);

        firstCam.backgroundColor = new Color(colorArray1[0], colorArray1[1], colorArray1[2], 0.8f);
        secondCam.backgroundColor = new Color(colorArray2[0], colorArray2[1], colorArray2[2], 0.8f);

        firstCam.GetComponent<CameraShake>().Shake(0.3f);
        secondCam.GetComponent<CameraShake>().Shake(0.3f);
    }

    public void DeathSequence()
    {
        colorArray1[0] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray1[1] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray1[2] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);

        colorArray2[0] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray2[1] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);
        colorArray2[2] = (float)((rand.Next(100) / 100f * 0.6) + 0.4f);

        firstCam.backgroundColor = new Color(colorArray1[0], colorArray1[1], colorArray1[2], 0.8f);
        secondCam.backgroundColor = new Color(colorArray2[0], colorArray2[1], colorArray2[2], 0.8f);

        firstCam.GetComponent<CameraShake>().DeathShake();
        secondCam.GetComponent<CameraShake>().DeathShake();
    }


    public void DoChatLimitCheck(uint charLen)
    {
        charCount += charLen;
        if (charCount >= chatLimit)
        {
            Debug.Log("Game Over.");
        }
    }

    public void TriggerReset(int gapNumber)
    {
        if (gapNumber == 1)
            gap1.Reset(Int32.Parse(levels[level][0]), Int32.Parse(levels[level][1]), Int32.Parse(levels[level][2]), Int32.Parse(levels[level][3]), Int32.Parse(levels[level][4]));
        else
            gap2.Reset(Int32.Parse(levels[level][0]), Int32.Parse(levels[level][1]), Int32.Parse(levels[level][2]), Int32.Parse(levels[level][3]), Int32.Parse(levels[level][4]));

        level = (level + 1) % 100;
    }

    public void SetDeathState(bool flag)
    {
        this.isDead = flag;
    }
}
