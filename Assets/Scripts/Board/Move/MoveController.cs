using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using PathCreation;
using System.Linq;

public class MoveController : NetworkBehaviour
{
    #region Singleton

    public static MoveController instance;

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
        MovingPlayer,
        Building
    }
    [SyncVar(hook = nameof(UpdateUI))] public States state;

    [SyncVar] public int currentPlayer;
    public List<O.Player> players;

    public List<Transform> points;
    //public List<Transform> buildingPoints;
    [SyncVar] public int whereGo;

    [SerializeField] private int pricePerBuilding;

    public override void OnStartServer()
    {
        base.OnStartServer();


    }

    private void Update()
    {
        if (isServer) UpdateFunc();
    }

    public void MovePlayers() => ChangeStateServerRpc(States.MovingPlayer);

    private void BuildingPhase() => ChangeStateServerRpc(States.Building);

    [Server]
    private void ChangeStateServerRpc(States now)
    {
        state = now;
        if(now == States.MovingPlayer)
        {
            players = O.Player.AllPlayers();
            currentPlayer = 0;
            
            while(currentPlayer < players.Count)
            {
                if(players[currentPlayer].rolls != 0)
                    break;
                currentPlayer += 1;
            }

            if(currentPlayer < players.Count)
                whereGo = players[currentPlayer].currentPlace + 1;
        }
        else if(now == States.Building)
        {
            BuildManager.instance.StartBuild();
        }
    }

    [Server]
    private void UpdateFunc()
    {
        if (state == States.MovingPlayer)
        {
            PlayerMove();
        }
    }

    [Server]
    private void PlayerMove()
    {
        if (currentPlayer < O.Player.AllPlayers().Count)
        {
            O.Player p = players[currentPlayer];

            if (Vector3.Distance(players[currentPlayer].transform.position, points[whereGo].transform.position) > 1f)
            {
                p.Move(points[whereGo].transform.position);
            }
            else
            {
                // Move a place forward
                p.rolls -= 1;
                p.currentPlace += 1;
                if (p.currentPlace >= points.Count)
                    p.currentPlace = 0;

                // Player can still go forward
                if (p.rolls > 0)
                {
                    whereGo += 1;
                    if (whereGo >= points.Count)
                        whereGo = 0;
                }
                else // Next Player should move
                {
                    currentPlayer += 1;

                    while (currentPlayer < players.Count)
                    {
                        // Check if player rolled a 0
                        if (players[currentPlayer].rolls != 0)
                            break;
                        currentPlayer += 1;
                    }

                    // No player left
                    if (currentPlayer >= players.Count)
                    {
                        BuildingPhase();
                    }
                    else // Next player, reset where to go
                    {
                        p = players[currentPlayer];
                        whereGo = p.currentPlace + 1;
                        if (whereGo >= points.Count)
                            whereGo = 0;
                    }
                }
            }
        }
    }

    private void UpdateUI(States old, States now)
    {

    }

    //// 0 no building: Start Building
    //// 1 his building: Give Money
    //// 2 someone elses building: Start Fight
    //// 3 upgrade building: Combine Building
    //[Server]
    //private int StateOfBuildin()
    //{
    //    O.Player p = players[currentPlayer];
    //    for(int i = 0; i < castles.Count; i++)
    //    {
    //        // Ca sa faca cladiri care ocupa mai multe spatii:
    //        // Mai trebuie sa fie facut indexu o lista
    //        //if (castles[i].index.Contains(p.currentPlace))
    //        //{
    //        //    if (castles[i].color == p.color)
    //        //        return 1;
    //        //    else
    //        //        return 2;
    //        //}
    //        //else if(castles[i].index.Contains(p.currentPlace + 1) || castles[i].index.Contains(p.currentPlace - 1))
    //        //{
    //        //    return 3;
    //        //}

    //        if(castles[i].index == p.currentPlace)
    //        {
    //            currentCastle = i;
    //            if(castles[i].color == p.color)
    //            {
    //                return 1;
    //            }
    //            else
    //            {
    //                return 2;
    //            }
    //        }
    //    }

    //    return 0;
    //}

}
