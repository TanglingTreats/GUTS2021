using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
using UnityEngine.UI;

public class ChatBehaviour : NetworkBehaviour
{
    [SerializeField] private GameObject gameController = default;
    private static event Action<string> OnMessage;
    private static event Action OnDeath;
    private static event Action OnResume;

    private static event Action<float> OnJump;

    public void OnEnable()
    {
        gameController = GameObject.Find("GameController");
    }

    public override void OnStartAuthority()
    {
        OnDeath += HandleDeath;
        OnResume += HandleResume;
        OnJump += HandleJump;
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if(!hasAuthority)
            return;
        
        OnDeath -= HandleDeath;
        OnResume -= HandleResume;
        OnJump -= HandleJump;
    }

    private void HandleDeath()
    {
        GameObject player = GameObject.Find("Player");
        gameObject.GetComponent<MovePlayer>().enabled = false;

        player.GetComponent<Player>().isDead = true;
        gameController.GetComponent<GameController>().SetDeathState(true);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GameObject.Find("CubeSparks").GetComponent<ParticleSystem>().Stop();
        gameController.GetComponent<GameController>().DeathSequence();
    }

    private void HandleResume()
    {
        gameController.GetComponent<GameController>().SetPauseState(false);
        GameObject.Find("Player").GetComponent<Rigidbody2D>().WakeUp();
        gameObject.GetComponent<MovePlayer>().enabled = true;
    }

    private void HandleJump(float val)
    {
        GameObject player = GameObject.Find("Player");
        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0, val, 0);
        player.GetComponent<Player>().DecreaseTimer(0.1f);
    }

    [Client]
    public void SendResume()
    {
        CmdSendResume();
    }
    
    [Client]
    public void SendDeath()
    {
        CmdSendDeath();
    }

    [Client]
    public void SendJump(float val)
    {
        CmdSendJump(val);
    }

    [Command]
    public void CmdSendJump(float val)
    {
        RpcHandleJump(val);
    }
    [Command]
    public void CmdSendResume()
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

    [ClientRpc]
    private void RpcHandleJump(float val)
    {
        OnJump?.Invoke(val);
    }
    
}
