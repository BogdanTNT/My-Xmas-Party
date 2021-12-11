using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RollGames;
using Mirror;

public class RollController : NetworkBehaviour
{
    #region Singleton

    public static RollController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log(this + " is already in the scene");
    }

    #endregion

    // Closed inseamna ca nu e pe ecran si nu face nimic
    // In Start: nimic
    // In Update: Asteapta sa fie pornit. Poate scrie in chat ca nu face nimic.
    //
    // Starting inseamna ca acuma nu se da roll
    // In Start: reseteze ready, sa aleaga minigameu, sa reseteze interfata pentru a reprezenta minigameu
    // In Update: se fac animatile si se verifica cand s-au terminat
    //
    // WaitToReady inseamna ca asteapta ca toti playeri sa dea ready
    // In Start: 
    // In Update: verifica daca toti playeri au dat ready
    //
    // WaitForRoll inseamna ca aici playeri dau cu zaru cat merge fiecare
    // In Start: Depinde de tip de roll
    // In Update: Depinde de tip de roll
    //
    // Finishing asteapta sa se termine si sa se inchida animatile
    // In Start: Transmite informatile la TableController
    // In Update: se fac animatii de inchidere
    public enum States
    {
        Closed,
        Starting,
        WaitForRoll,
        Finishing
    }
    [SyncVar(hook = nameof(UpdateUI))]
    public States state;

    public List<RollGame> rollGames = new List<RollGame>();

    [SyncVar]
    public int roll;

    [SyncVar] public int minRolls;
    [SyncVar] public int maxRolls;

    public override void OnStartServer()
    {
        base.OnStartServer();

        state = States.Closed;
    }

    void Update()
    {
        if (state == States.Closed)
        {
            //Debug.Log("Roll is idleing and waiting to start");
        }
        else if (state == States.Starting)
        {
            //Debug.Log("Waiting for roll to start");
        }
        else if (state == States.WaitForRoll)
        {
            //Debug.Log("Waiting for everyone to roll");
        }
        else if (state == States.Finishing)
        {
            //Debug.Log("Waiting to end and close roll window");
        }
    }

    public void Roll()
    {
        ChangeStateServerRpc(States.Starting);
    }

    public void WaitForRoll()
    {
        ChangeStateServerRpc(States.WaitForRoll);
    }

    public void Finish()
    {
        state = States.Finishing;

    }

    public void Close()
    {
        state = States.Closed;
    }

    private void UpdateUI(States old, States now)
    {
        if (now == States.Starting)
        {
            rollGames[roll].SetupGame();
            O.Player.MakeAllUnfinishedRolls();
        }
        else if(now == States.Finishing)
        {
            TableController.instance.MovePlayers();
        }
    }

    [Server]
    private void RandomlyChosesTypeOfRollServerRpc()
    {
        roll = Random.Range(0, rollGames.Count);
    }

    [Server]
    public void ChangeStateServerRpc(States now)
    {
        if(now == States.Starting)
        {
            RandomlyChosesTypeOfRollServerRpc();
        }

        state = now;
    }

}
