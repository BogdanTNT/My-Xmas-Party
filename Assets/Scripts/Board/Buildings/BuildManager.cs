using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BuildManager : NetworkBehaviour
{
    #region Singleton

    public static BuildManager instance;

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
        Closed,
        Checking,
        Battle,
        PostBattle
    }
    [SyncVar(hook = nameof(UpdateVisual))] public States state;


    //[SerializeField] private GameObject castlesPrefabs;
    //public BuildingPoint[] castles;
    //[SyncVar] public int cc;

    public BuildingState[] build;
    public GameObject prefabBuilding;

    [SyncVar] public int cp;
    public List<O.Player> p;

    public override void OnStartServer()
    {
        base.OnStartServer();
        for (int i = 0; i < build.Length; i++)
            build[i].index = i;
    }

    [Server]
    public void StartBuild()
    {
        cp = 0;
        p = O.Player.AllPlayers();
        ActOnPlayer();
    }

    [Server]
    public void NextPlayer()
    {
        cp++;

        ActOnPlayer();
    }

    private void UpdateVisual(States old, States now)
    {
        if (now == States.Checking)
        {
            //cp = 0;
            //p = O.Player.AllPlayers();
            //ActOnPlayer();
        }
    }

    [Server]
    private void ActOnPlayer()
    {
        if (cp >= p.Count)
        {
            // Urmatoru stagiu
            ChangeState(States.Battle);
            return;
        }
        Debug.Log($"Acting on {p[cp].playerName} with index {cp}.");

        //castles[p[cp].currentPlace].NewPlayer(p[cp]);
        build[p[cp].currentPlace].NewPlayer(p[cp]);
    }

    [Server]
    private void ChangeState(States now) => state = now;
}
