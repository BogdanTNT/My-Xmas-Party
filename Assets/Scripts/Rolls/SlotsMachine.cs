using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Mirror;

namespace RollGames
{
    public class SlotsMachine : RollGame
    {
        public TMP_Text text;
        public int roll;

        public override void SetupGame()
        {
            base.SetupGame();

        }

        public override void StartGame()
        {
            base.StartGame();

            slots = Slots();
            StartCoroutine(slots);

        }

        protected override void Finish()
        {
            base.Finish();

        }

        IEnumerator slots;
        IEnumerator Slots()
        {
            while (timer > 0)
            {
                roll = Random.Range(RollController.instance.minRolls, RollController.instance.maxRolls);
                text.text = roll.ToString();

                yield return new WaitForSeconds(.4f);
            }

            ForceEnd();
            slots = null;
        }

        public void Choose()
        {
            O.Player localPlayer = NetworkClient.localPlayer.gameObject.GetComponent<O.Player>();

            localPlayer.CmdRolls(roll);

            //timer = 0;
            StopCoroutine(slots);
        }

        public override void ForceEnd()
        {
            roll = 1;
            Choose();
            base.ForceEnd();
        }
    }

}
