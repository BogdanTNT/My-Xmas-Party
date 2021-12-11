using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace RollGames
{
    public abstract class RollGame : MonoBehaviour
    {
        public GameObject UI;

        public bool started;
        public bool ended;

        public float timer;

        //[Server]
        public virtual void SetupGame()
        {
            UI.SetActive(true);
            ended = false;
            started = false;
            //Debug.Log("Nu");
        }

        //[Server]
        public virtual void StartGame()
        {
            timer = 10;

            O.Player.MakeAllUnfinishedRolls();
            //RollController.instance.WaitForRoll();

            started = true;
        }

        private void Update()
        {
            //if (!isServer) return;

            UpdateFunc();
        }

        //[Server]
        protected virtual void UpdateFunc()
        {
            if (O.Player.TestForAllReady() && !ended && !started)
                StartGame();

            if (timer > 0)
                timer -= Time.deltaTime;
            //if (timer < 0 && RollController.instance.state == RollController.States.WaitForRoll)
            //{
            //    ForceEnd();
            //}
            if (O.Player.TestForAllFinishedRolls() && !ended)
                Finish();
        }

        //[Server]
        public virtual void ForceEnd()
        {

        }

        //[Server]
        protected virtual void Finish()
        {
            UI.SetActive(false);
            RollController.instance.Finish();
            ended = true;
            started = false;
        }

        
    }

}
