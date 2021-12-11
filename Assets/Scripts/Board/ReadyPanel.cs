using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using LobbyState;
using Mirror;
using TMPro;
using UnityEngine.UI;

//namespace LobbyState
//{
//    public struct LobbyPlayerState : INetworkSerializable
//    {
//        public ulong clientId;
//        public string playerName;
//        public bool isReady;
//        public O.Player player;

//        public LobbyPlayerState(ulong id, string name, bool ready, O.Player pl)
//        {
//            clientId = id;
//            playerName = name;
//            isReady = ready;
//            player = pl;
//        }

//        public void NetworkSerialize(NetworkSerializer serializer)
//        {
//            serializer.Serialize(ref clientId);
//            serializer.Serialize(ref playerName);
//            serializer.Serialize(ref isReady);
//        }
//    }
//}

public class ReadyPanel : NetworkBehaviour
{
    [SerializeField] private ReadyComponent[] readyComp;
    [SerializeField] private TMP_InputField nameField;

    //NetworkList<LobbyPlayerState> lobbyPlayerStates = new NetworkList<LobbyPlayerState>(new NetworkVariableSettings { ReadPermission = NetworkVariablePermission.Everyone, WritePermission = NetworkVariablePermission.Everyone});

    //[ServerRpc(RequireOwnership = false)]
    //private void SetNameServerRpc(ServerRpcParams serverRpcParams = default)
    //{
    //    ulong senderId = serverRpcParams.Receive.SenderClientId;

    //    NetworkObject playerObject = NetworkSpawnManager.GetPlayerNetworkObject(senderId);

    //    Debug.Log(senderId);
    //    Debug.Log(playerObject);

    //    for (int i = 0; i < lobbyPlayerStates.Count; i++)
    //    {
    //        Debug.Log(lobbyPlayerStates[i].clientId);
    //        Debug.Log(lobbyPlayerStates[i].playerName);
    //        Debug.Log(lobbyPlayerStates[i].isReady);
    //        if(lobbyPlayerStates[i].clientId == senderId)
    //        {
    //            var po = playerObject.GetComponent<Player>();
    //            po.playerName.Value = nameField.text;
    //            po.ready.Value = !po.ready.Value;

    //            lobbyPlayerStates[i] = new LobbyPlayerState(
    //                    lobbyPlayerStates[i].clientId,
    //                    lobbyPlayerStates[i].playerName,
    //                    po.ready.Value, //!lobbyPlayerStates[i].isReady,
    //                    lobbyPlayerStates[i].player
    //                );

    //            Debug.Log("gasit");

    //            return;
    //        }
    //    }

    //    var pl = playerObject.GetComponent<Player>();
    //    pl.playerName.Value = nameField.text;

    //    lobbyPlayerStates.Add(new LobbyPlayerState(
    //            senderId,
    //            nameField.text,
    //            pl.ready.Value,
    //            pl
    //    ));

    //}

    //public override void NetworkStart()
    //{
    //    if(IsClient)
    //    {
    //        lobbyPlayerStates.OnListChanged += HandleLobbyPlayersStateChanged;
    //    }
    //}

    //private void OnDestroy()
    //{
    //    lobbyPlayerStates.OnListChanged -= HandleLobbyPlayersStateChanged;
    //}

    //private void HandleLobbyPlayersStateChanged(NetworkListEvent<LobbyPlayerState> lobbyState)
    //{
    //    var pl = Player.AllPlayers();
    //    Debug.Log(pl.Count);
    //    for (int i = 0; i < pl.Count; i++)
    //    {
    //        readyComp[i].UpdateDisplay();
    //    }
    //}

    //public void ReadyUp()
    //{
    //    SetNameServerRpc();
    //}
}
