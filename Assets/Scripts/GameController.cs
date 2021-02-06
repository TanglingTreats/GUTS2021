using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private uint chatLimit = 150;
    public uint charCount = 0;

    public void DoChatLimitCheck(uint charLen)
    {
        charCount += charLen;
        if (charCount >= chatLimit)
        {
            Debug.Log("Game Over.");
        }
    }
}
