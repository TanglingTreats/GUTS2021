﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
using UnityEngine.UI;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private Text chatText = default;
    [SerializeField] private InputField inputField = default;
    [SerializeField] public GameObject canvas = default;
    [SerializeField] private GameObject gameController = default;
    private static event Action<string> OnMessage;

    public void OnEnable()
    {
        gameController = GameObject.Find("GameController");
        canvas.SetActive(false);
    }

    public override void OnStartAuthority()
    {
        //canvas.SetActive(true);
        OnMessage += HandleNewMessage;
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if(!hasAuthority)
            return;
        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send()
    {
        if(!Input.GetKeyDown(KeyCode.Return) || string.IsNullOrWhiteSpace(inputField.text) )
            return;

        gameController.GetComponent<GameController>().DoAddChatLen((uint) inputField.text.Length);
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
        if(gameController.GetComponent<GameController>().IsChatReachedLimit())
        {
            canvas.SetActive(false);  
            gameController.GetComponent<GameController>().charCount = 0;      
            gameObject.GetComponent<MovePlayer>().enabled = true;
        }
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        OnMessage?.Invoke($"\n{message}");
    }
}
