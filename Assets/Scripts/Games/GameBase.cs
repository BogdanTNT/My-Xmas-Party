using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Games
{
    public class GameBase : NetworkBehaviour
    {
        // Imi trebuie o zona de spawn care sa ii puna in formatie.

        public bool started;



        public virtual void Setup()
        {

        }

        public virtual void StartGame()
        {

        }

        protected virtual void UpdateFunc()
        {

        }

        protected virtual void RpcUpdateFunc()
        {

        }


    }
}
