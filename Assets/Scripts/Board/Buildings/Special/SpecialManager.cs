using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Special;

public class SpecialManager : NetworkBehaviour
{
    #region Singleton

    public static SpecialManager instance;

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
        // Closed: Inactiv
        Closed,
        // Starting: Se activeaza
        Starting,
        // Se asteapta interactiunea playerului
        WaitForSpecial,
        // Se dezactiveaza
        Finishing
    }
    [SyncVar] public States state;

    public List<SpecialBase> specials = new List<SpecialBase>();

    [SyncVar] public int spec;

    public override void OnStartServer()
    {
        base.OnStartServer();

        state = States.Closed;
    }

    // O sa inceapa un special
    public void Starting(int special, O.Player p)
    {
        // Alege un special in functie de punctu pe care e playeru
        spec = special;
        specials[spec].Setup(p);
        state = States.Starting;
    }

    [Server]
    private void ChangeState(States now) => state = now;

    private void OnStateChanged(States old, States now)
    {
        if(now == States.Starting)
        {

        }
    }

}
