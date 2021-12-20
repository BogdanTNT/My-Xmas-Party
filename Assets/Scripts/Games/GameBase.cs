using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

namespace Games
{
    public class GameBase : NetworkBehaviour
    {
        public Formation formation;

        public bool started;

        private List<Vector3> _points = new List<Vector3>();

        public List<O.Player> players = new List<O.Player>();

        [Server]
        public virtual void Setup()
        {
            // Teleporteaza playeri la zona in care e jocu
            _points = formation.EvaluatePoints().ToList();
            players = O.Player.AllPlayers();

            for(int i = 0; i < players.Count; i++)
            {
                players[i].transform.position = _points[i];
            }
        }

        [Server]
        public virtual void StartGame()
        {

        }

        [Server]
        protected virtual void UpdateFunc()
        {

        }
        
        [ClientRpc]
        protected virtual void RpcUpdateFunc()
        {

        }


    }

}
