using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SerberManager : NetworkManager
{
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        StartCoroutine(lag(conn));
    }

    IEnumerator lag(NetworkConnection conn)
    {
        yield return new WaitForSeconds(.5f);
        ChangeNameAndReady.instance.p = conn.identity.GetComponent<O.Player>();

    }

}
