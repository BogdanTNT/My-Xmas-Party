using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Special
{
    public class SpecialBase : NetworkBehaviour
    {
        public GameObject UI;

        [SyncVar(hook = nameof(OnChoiceChange))] public int choice;

        [SyncVar(hook = nameof(OnStartedChange))] public bool started;

        [SyncVar] public string player;

        // Canvasu o sa aiba un network transform ca toti playeri sa vada ce a ales playeru cu specialu 
        protected virtual void Awake()
        {
            //UI.SetActive(true);
            UI.GetComponent<CanvasGroup>().alpha = 0;
            Debug.Log("da");
        }

        // Reface canvasu sa fie vizibil si il dezactiveaza
        public override void OnStartClient()
        {
            base.OnStartClient();
            UI.GetComponent<CanvasGroup>().alpha = 1;
            UI.SetActive(false);
        }

        // Incepe specialu
        [Server]
        public virtual void Setup(O.Player p)
        {
            started = true;
            player = p.playerName;
            choice = 4;
        }

        public virtual void StartGame()
        {

        }

        private void Update()
        {
            if (!started) return;

            if(isServer)
                UpdateFunc();
        }

        [Server]
        protected virtual void UpdateFunc()
        {
            RpcUpdateFunc();

        }

        [ClientRpc]
        protected virtual void RpcUpdateFunc()
        {
            
        }

        public virtual void CmdFinish()
        {
            started = false;
            BuildManager.instance.NextPlayer();
        }

        [Server]
        public void OnChoiceChange(int old, int now)
        {
            switch (now)
            {
                case 0:
                    First();
                    break;
                case 1:
                    Second();
                    break;
                case 2:
                    Third();
                    break;
                case 3:
                    Forth();
                    break;

            }
        }

        public virtual void First() { Debug.Log("got one"); StartCoroutine(Lag()); }
        public virtual void Second() { Debug.Log("got two"); StartCoroutine(Lag()); }
        public virtual void Third() { Debug.Log("got three"); StartCoroutine(Lag()); }
        public virtual void Forth() { Debug.Log("got four"); StartCoroutine(Lag()); }

        private void OnStartedChange(bool old, bool now)
        {
            if(now == true)
            {
                UI.SetActive(true);
            }
            else
            {
                UI.SetActive(false);
            }
        }

        IEnumerator Lag()
        {
            yield return new WaitForSeconds(3f);

            CmdFinish();
        }
    }
}


