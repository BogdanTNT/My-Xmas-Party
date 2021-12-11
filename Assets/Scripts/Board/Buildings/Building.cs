using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Building : NetworkBehaviour
{
    [SyncVar] public Color color;

    public void Rise(Color newColor)
    {
        color = newColor;
        StartCoroutine(StartRise());

    }

    public void Lower()
    {
        StartCoroutine(Lowerin());
    }

    [Command(requiresAuthority = false)]
    private void CmdNext() => BuildManager.instance.NextPlayer();

    IEnumerator StartRise()
    {
        yield return new WaitForSeconds(1f);

        CmdNext();
    }

    IEnumerator Lowerin()
    {
        yield return new WaitForSeconds(1f);

        CmdNext();
    }
}
