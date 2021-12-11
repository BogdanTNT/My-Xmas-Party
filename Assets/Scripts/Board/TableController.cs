using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;
public class TableController : NetworkBehaviour
{
    #region Singleton

    public static TableController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log(this + " is already in the scene");
    }

    #endregion

    public enum States
    {
        WaitToStart,
        WaitToReady,
        WaitForRoll,
        WaitForMove,
        WaitForMiniGame
    }
    [SyncVar(hook = nameof(UpdateUI))]
    public States state;

    [HideInInspector] public List<O.Player> pList;

    public GameObject tableUI;

    public override void OnStartServer()
    {
        base.OnStartServer();

        state = States.WaitToReady;
        //state.OnValueChanged += UpdateUI;
        tableUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == States.WaitToStart) 
        {
            //Debug.Log("Waiting To Start Game");
        }
        else if (state == States.WaitToReady) 
        {
            //Debug.Log("Waiting for every player to be ready");
        }
        else if (state == States.WaitForRoll)
        {
            //Debug.Log("Waiting to see who rolls what");
        }
        else if (state == States.WaitForMove) 
        {
            //Debug.Log("Waiting for players to move");
        }
        else if (state == States.WaitForMiniGame)
        {
            //Debug.Log("Waiting to see who won mini game");
        }
    }

    // -------------- PUBLIC

    public void NextTurn()
    {
        if (O.Player.TestForAllReady())
        {
            ChangeStateServerRpc(States.WaitForRoll);
        }
        else
        {
            Debug.Log("Not everyone is ready!");
        }
    }

    //[ServerRpc(RequireOwnership = false)]
    public void NextTurnServerRpc() => NextTurn();

    public void MovePlayers()
    {
        ChangeStateServerRpc(States.WaitForMove);
    }

    // -------------- PRIVATE


    private void UpdateUI(States old, States now)
    {
        if(now == States.WaitForRoll)
        {
            tableUI.SetActive(false);
            //RollController.instance.ChangeStateServerRpc(RollController.States.Starting);
            RollController.instance.Roll();
        }
        else if(now == States.WaitToReady)
        {
            tableUI.SetActive(true);
        }
        else if(now == States.WaitForMove)
        {
            //if(IsServer)
                MoveController.instance.MovePlayers();
        }
    }

    //[ServerRpc(RequireOwnership = false)]
    private void ChangeStateServerRpc(States now)
    {
        state = now;

        if(now == States.WaitForRoll)
        {

        }
    }
}
