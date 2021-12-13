using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BuildingState : NetworkBehaviour
{
    public enum States
    {
        Special, 
        Empty,
        Owned
    }
    [SyncVar(hook = nameof(UpdateVisual))] public States state;

    public int specIndex;

    public int index;

    public GameObject building;

    [Server]
    public void ChangeState(States now) => state = now;

    [Server]
    public void NewPlayer(O.Player pl)
    {
        if(state == States.Special)
        {
            // Alege din cevauri

            SpecialManager.instance.Starting(specIndex, pl);
            return;
        }

        O.Player? p = O.Player.WhoOwnsThis(index);

        if(p == pl)
        {
            pl.Money(100);
        }
        else if(p == null)
        {
            // Construieste ceva pentru nou player si da-i assign lui
            pl.buildIndex.Add(index);

            GameObject b = Instantiate(BuildManager.instance.prefabBuilding, transform.position, transform.rotation);

            //b.transform.SetParent(BuildManager.instance.transform);

            b.GetComponent<Building>().Rise(O.Player.WhoOwnsThis(index).color);
            building = b;

            NetworkServer.Spawn(b);

            ChangeState(States.Owned);
            Debug.Log($"Build For {pl.playerName}.");
        }
        else
        {
            MiniGameController.instance.AddBattle(O.Player.WhoOwnsThis(index), WhoAttack());
            Debug.Log($"{pl.playerName} is fighting for {p.playerName} castle.");

        }
    }

    private void UpdateVisual(States old, States now)
    {
        if(now == States.Owned)
        {
            // ridica cladire
            //GameObject b = Instantiate(BuildManager.instance.prefabBuilding, transform);

            //b.GetComponent<Building>().Rise(O.Player.WhoOwnsThis(index).color);
            //building = b;

            //NetworkServer.Spawn(b);
        }
        else if(now == States.Empty)
        {
            // coboara cladirea
        }
    }

    private List<O.Player> WhoAttack()
    {
        List<O.Player> attackers = new List<O.Player>();
        attackers = O.Player.AllPlayers();
        for(int i = 0; i < attackers.Count; i++){
            if(!attackers[i].buildIndex.Contains(index))
            {
                attackers.RemoveAt(i);
                i--;
            }
        }
        return attackers;
    }
}
