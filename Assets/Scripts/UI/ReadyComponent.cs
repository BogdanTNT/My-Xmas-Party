using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class ReadyComponent : MonoBehaviour
{
    public O.Player pl;
    public TMPro.TMP_Text text;
    public Toggle readyToggle;
    public int index;

    //public void ReadyPlayer()
    //{
    //    //pl.Ready();
    //    ReadyPlayerServerRpc();
    //}

    //[ServerRpc(RequireOwnership = false)]
    //private void ReadyPlayerServerRpc(ServerRpcParams serverRpcParams = default)
    //{
    //    ulong senderId = serverRpcParams.Receive.SenderClientId;

    //    NetworkObject playerObject = NetworkSpawnManager.GetPlayerNetworkObject(senderId);

    //    if (playerObject == null) return;

    //    pl.Ready();
    //}

    public void UpdateDisplay()
    {
        var pl = O.Player.AllPlayers();
        //Debug.Log("Nu");

        if (index >= pl.Count) return;

        text.text = pl[index].playerName;
        readyToggle.isOn = pl[index].ready;
        //Debug.Log(index);
        //Debug.Log(pl[index].playerName.Value);
        //Debug.Log(pl[index].ready.Value);
    }

}
