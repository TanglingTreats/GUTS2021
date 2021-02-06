﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{

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
    }

    void Update()
    {
        gap.Move(speed);
        speedCounter += 1;
        if (speedCounter % 90 == 0)
            speed *= 1.001f;
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
