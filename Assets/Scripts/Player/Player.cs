using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using Mirror;

namespace O
{
    public class Player : NetworkBehaviour
    {
        [SyncVar(hook = nameof(UpdateReadyComp))] public bool ready;

        [SyncVar(hook = nameof(OnNameChange))] public string playerName;

        [SyncVar] public int rolls;

        [SyncVar] public bool finishRoll;

        [SyncVar] public int currentPlace;

        [SyncVar] public Color color;

        [SyncVar] public int money;

        readonly public SyncList<int> buildIndex = new SyncList<int>();

        public NavMeshAgent agent;
        public int degreesPerSecond;

        public override void OnStartClient()
        {
            base.OnStartClient();

            transform.position = MoveController.instance.points[0].transform.position;
            ChangeNameAndReady.instance.UpdateReady();

        }

        [Command]
        public void CmdChangeName(string s, bool change)
        {
            playerName = s;
            if (change)
                ready = !ready;
            ChangeNameAndReady.instance.UpdateReady();
        }

        [Command]
        public void CmdRolls(int roll)
        {
            if (finishRoll) return;

            rolls = roll;
            finishRoll = true;
        }

        public void Move(Vector3 direction)
        {
            float dist = Vector3.Distance(direction, transform.position);

            if (dist > .1f)
            {
                LookAt(direction);

                Vector3 move = transform.forward * Time.deltaTime * agent.speed;

                agent.Move(move);
            }
        }

        public void Money(int amount)
        {
            if(amount > 0)
            {
                money += amount;
            }
            else
            {
                money -= amount;
            }
        }
        
        public void LookAt(Vector3 Target)
        {
            Quaternion rot = transform.rotation;
            transform.LookAt(Target);
            transform.rotation = Quaternion.Lerp(rot, transform.rotation, Time.deltaTime * (degreesPerSecond / 360f));
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        }

        private void UpdateReadyComp(bool s, bool af)
        {
            ChangeNameAndReady.instance.UpdateReady();
        }

        private void OnNameChange(string old, string now)
        {
            ChangeNameAndReady.instance.UpdateReady();
        }

        #region Static
        // ----------- Static -----------

        public static List<Player> AllPlayers()
        {
            var g = GameObject.FindGameObjectsWithTag("Player");
            List<Player> pl = new List<Player>();
            foreach (GameObject i in g)
            {
                pl.Add(i.GetComponent<Player>());
            }
            return pl;
        }

        public static bool TestForAllReady()
        {
            var g = GameObject.FindGameObjectsWithTag("Player");
            List<Player> pl = new List<Player>();
            foreach (GameObject i in g)
            {
                pl.Add(i.GetComponent<Player>());
            }
            return pl.All(n => n.ready == true);
        }

        public static bool TestForAllFinishedRolls()
        {
            var g = GameObject.FindGameObjectsWithTag("Player");
            List<Player> pl = new List<Player>();
            foreach (GameObject i in g)
            {
                pl.Add(i.GetComponent<Player>());
            }
            return pl.All(n => n.finishRoll == true);
        }

        public static void MakeAllUnfinishedRolls()
        {
            var g = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject i in g)
            {
                i.GetComponent<Player>().finishRoll = false;
            }
        }

        public static O.Player? WhoOwnsThis(int b)
        {
            List<Player> p = AllPlayers();
            foreach(Player i in p)
            {
                if (i.buildIndex.Contains(b))
                    return i;
            }

            return null;
        }

        #endregion

    }

}
