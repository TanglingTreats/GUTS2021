using System.Collections;
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
    private static event Action OnDeath;
    private static event Action OnResume;

    public void OnEnable()
    {
        gameController = GameObject.Find("GameController");
        canvas.SetActive(false);
    }

    public override void OnStartAuthority()
    {
        //canvas.SetActive(true);
        OnMessage += HandleNewMessage;
        OnDeath += HandleDeath;
        OnResume += HandleResume;
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if(!hasAuthority)
            return;
        OnMessage -= HandleNewMessage;
        OnDeath -= HandleDeath;
        OnResume -= HandleResume;
    }

    private void HandleDeath()
    {
        chatText.text = String.Empty;;
        canvas.gameObject.SetActive(true);
        gameObject.GetComponent<MovePlayer>().enabled = false;
    }

    private void HandleResume()
    {
        canvas.SetActive(false);
        gameController.GetComponent<GameController>().charCount = 0;      
        gameObject.GetComponent<MovePlayer>().enabled = true;
    }
    private void HandleNewMessage(string message)
    {
        gameController.GetComponent<GameController>().DoAddChatLen((uint) message.Length);
        chatText.text += message;
        if(gameController.GetComponent<GameController>().IsChatReachedLimit())
        {
            CmdSendResume();
        }
    }

    [Client]
    public void Send()
    {
        if(!Input.GetKeyDown(KeyCode.Return) || string.IsNullOrWhiteSpace(inputField.text) )
            return;
        CmdSendMessage(inputField.text);
        inputField.text = string.Empty;
    }

    [Command]
    private void CmdSendResume()
    {
        RpcHandleResume();
    }

    [ClientRpc]
    private void RpcHandleResume()
    {
        OnResume?.Invoke();
    }
    
    [Command]
    public void CmdSendDeath()
    {
        RpcHandleDeath();
    }

    [ClientRpc]
    private void RpcHandleDeath()
    {
        OnDeath?.Invoke();
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
