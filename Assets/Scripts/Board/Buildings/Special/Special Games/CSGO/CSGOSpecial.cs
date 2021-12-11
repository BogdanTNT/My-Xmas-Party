using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Special;
using System.Linq;
using Mirror;

public class CSGOSpecial : SpecialBase
{
    public Transform start;
    public Transform end;
    public List<WeaponImage> weapons;
    public Sprite[] quality;

    public GameObject endButton;
    public Transform niddle;

    public float initSpeed;
    [SerializeField] [SyncVar] private float speed;

    public GameObject uiPrefab;
    public Transform scroller;

    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    [Server]
    public override void Setup(O.Player p)
    {
        base.Setup(p);
        speed = initSpeed;

        for (int i = 0; i < 5; i++)
        {
            Vector3 pos = start.position;
            float d = (end.position.x - start.position.x) / 5;
            pos.x += i * d;
            Debug.Log(pos.x);
            GameObject n = Instantiate(uiPrefab, pos, Quaternion.identity);

            var g = n.GetComponent<WeaponImage>();
            weapons.Add(g);
            g.Change();

            n.transform.localScale = Vector3.one;

            //Debug.Log(i);
            NetworkServer.Spawn(n);

            RpcSetupParent(weapons[i].transform, scroller);

        }
    }

    [ClientRpc]
    private void RpcSetupParent(Transform to, Transform p)
    {
        to.SetParent(p);
        to.localScale = Vector3.one;

        Transform localPlayer = NetworkClient.localPlayer.gameObject.transform;
        //Debug.Log(localPlayer.GetComponent<O.Player>().playerName);
        if (localPlayer.GetComponent<O.Player>().playerName != player && started) return;
        endButton.SetActive(true);
    }

    [Server]
    protected override void UpdateFunc()
    {
        base.UpdateFunc();
        // return;
        for (int i = 0; i < weapons.Count; i++)
        {
            //Debug.Log("DaA");
            RpcSetupParent(weapons[i].transform, scroller);

            Vector3 n = weapons[i].transform.position;
            n.x += speed * Time.deltaTime;
            weapons[i].transform.position = n;

            if (n.x > end.position.x)
            {
                weapons[i].transform.position = start.position;
                Debug.Log($"{weapons[i].transform.name} reached the end");
                weapons[i].Change();
            }
        }
    }

    [ClientRpc]
    protected override void RpcUpdateFunc()
    {
        //for (int i = 0; i < weapons.Length; i++)
        //{
        //    weapons[i].transform.position = pos[i];
        //}
    }

    [Command(requiresAuthority = false)]
    public void CmdChoose()
    {
        if (ending != null) return;

        ending = Ending();
        StartCoroutine(ending);
        endButton.SetActive(false);
    }

    IEnumerator ending;
    IEnumerator Ending()
    {
        while(speed > 0)
        {
            speed -= Time.deltaTime * initSpeed / 2;
            yield return null;
        }

        float smallest = 99999;
        int value = 0;
        for(int i = 0; i < weapons.Count; i++)
        {
            float d = Vector3.Distance(weapons[i].transform.position, niddle.position);

            if (d < smallest)
            {
                value = i;
                smallest = d;
            }
        }

        choice = weapons[value].index;
        
        ending = null;
    }

    public override void CmdFinish()
    {
        while (weapons.Count != 0)
        {
            NetworkServer.Destroy(weapons[0].gameObject);
            weapons.RemoveAt(0);
        }
        base.CmdFinish();

    }
    

}
