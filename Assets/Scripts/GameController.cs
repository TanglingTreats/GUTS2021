using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private uint chatLimit = 150;
    public uint charCount = 0;

    public void DoAddChatLen(uint charLen)
    {
        charCount += charLen;
    }

    public bool IsChatReachedLimit()
    {
        return charCount >= chatLimit;
    }

    
}
