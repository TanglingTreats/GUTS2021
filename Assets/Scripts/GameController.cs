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
    float colorStep = 0.035f;
    float[] colorArray1 = new float[] { 0.4f, 1f, 0.4f };
    float[] colorArray2 = new float[] { 0.4f, 1f, 0.4f };

    TheGap gap;
    float speed = 0.02f;
    int speedCounter = 0;
    System.Random rand = new System.Random();
    int level;
    List<String[]> levels = new List<String[]>();
    // Start is called before the first frame update

    [SerializeField] private uint chatLimit = 150;
    public uint charCount = 0;

    void Start()
    {
        gap = GameObject.Find("TheGap").GetComponent<TheGap>();
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

    void Update()
    {
        gap.Move(speed);
        speedCounter += 1;
        if (speedCounter % 60 == 0)
            speed *= 1.0001f;
        UpdateColor();
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
        colorArray1[0] = rand.Next(100) / 100f;
        colorArray1[1] = rand.Next(100) / 100f;
        colorArray1[2] = rand.Next(100) / 100f;

        colorArray2[0] = rand.Next(100) / 100f;
        colorArray2[1] = rand.Next(100) / 100f;
        colorArray2[2] = rand.Next(100) / 100f;

        firstCam.backgroundColor = new Color(colorArray1[0], colorArray1[1], colorArray1[2], 0.8f);
        secondCam.backgroundColor = new Color(colorArray2[0], colorArray2[1], colorArray2[2], 0.8f);
    }


    public void DoChatLimitCheck(uint charLen)
    {
        charCount += charLen;
        if (charCount >= chatLimit)
        {
            Debug.Log("Game Over.");
        }
    }

    public void TriggerReset()
    {
        gap.Reset(Int32.Parse(levels[level][0]), Int32.Parse(levels[level][1]), Int32.Parse(levels[level][2]));
        level = (level + 1) % 100;
    }
}
