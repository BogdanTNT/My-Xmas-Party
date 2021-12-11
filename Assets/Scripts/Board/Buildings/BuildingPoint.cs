using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BuildingPoint : NetworkBehaviour
{
    public O.Player owner = null;

    public enum States
    {
        Closed,
        Working
    }
    //[SyncVar] public States state;

    [SyncVar] public bool special;
    [SyncVar] public int sIndex;

    public Building child;

    [Server]
    public void NewPlayer(O.Player pl)
    {
        if (special)
        {
            // Alege ceva
            return;
        }

        if(pl == owner)
        {
            pl.Money(100);
        }
        else if(pl == null)
        {
            GiveCastle(pl);
        }
        else
        {
            // Battle
        }
    }

    [Server]
    private void GiveCastle(O.Player newPlayer)
    {
        owner = newPlayer;
        child.Rise(owner.color);
        StartCoroutine(Build());
    }

    //private void UpdateVisual(States old, States now)
    //{
    //    if(now == States.Lowering)
    //    {
    //        // Darama cladirea
    //        child.Lower();
    //    }
    //    else if(now == States.Rising)
    //    {
    //        // Ridica cladirea
    //        child.Rise(owner.color);
    //    }
    //    else if(now == States.Owned)
    //    {
    //        MoveController.instance.
    //    }
    //}

    IEnumerator Build()
    {
        yield return new WaitForSeconds(2f);

        BuildManager.instance.NextPlayer();
    }
}


/* 1. Road
 * 2. City Center
 * 3. wait
 * 4. door-to-door
 * 5. reserve a seat
 * 6. 
 * 7. 
 * 8. acidfk 
 * 9. Thomson
 * 10. 3303idfk
 * 11. b 
 * 12. a
 * 13. a
 * 14. b
 * 15. c
 * 16. b
 * 17. b
 * 18. a
 * 19. c
 * 20. b
 * 31. rock art
 * 32. children
 * 33. repeated
 * 34. human
 * 35. magic
 * 36. distance
 * 37. culture
 * 38. fire
 * 39. touching
 * 40. intact
 */