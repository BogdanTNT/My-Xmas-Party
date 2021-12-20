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

        // Creaza imaginile de iteme din csgo
        for (int i = 0; i < 5; i++)
        {
            // Alege pozitia
            Vector3 pos = start.position;
            float d = (end.position.x - start.position.x) / 5;
            pos.x += i * d;
            Debug.Log(pos.x);

            // Creaza imaginea
            GameObject n = Instantiate(uiPrefab, pos, Quaternion.identity);
            var g = n.GetComponent<WeaponImage>();
            weapons.Add(g);
            g.Change();
            n.transform.localScale = Vector3.one;

            // Adauga pe server 
            NetworkServer.Spawn(n);
            RpcSetupParent(weapons[i].transform, scroller);
        }
    }

    // Trebuie sa fie ClientRpc ca childu de la transform nu este transmis prin network
    [ClientRpc]
    private void RpcSetupParent(Transform to, Transform p)
    {
        // Pune itemele in canvas
        to.SetParent(p);
        to.localScale = Vector3.one;

        // Le aseaza in canvas cum trebuie
        Transform localPlayer = NetworkClient.localPlayer.gameObject.transform;

        // Creaza butonul de stop daca este playeru care face specialu
        if (localPlayer.GetComponent<O.Player>().playerName != player && started) return;
        endButton.SetActive(true);
    }

    [Server]
    protected override void UpdateFunc()
    {
        base.UpdateFunc();
        // Misca itemele in functie de viteza
        for (int i = 0; i < weapons.Count; i++)
        {
            //Debug.Log("DaA");
            // Tehnic nu e nevoie dar ma rog
            RpcSetupParent(weapons[i].transform, scroller);

            Vector3 n = weapons[i].transform.position;
            n.x += speed * Time.deltaTime;
            weapons[i].transform.position = n;

            // Muta inapoi in fata cand ajunge la sfarsit
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

    // Sfarseste specialu si alege itemu
    [Command(requiresAuthority = false)]
    public void CmdChoose()
    {
        if (ending != null) return;

        ending = Ending();
        StartCoroutine(ending);
        endButton.SetActive(false);
    }

    // Incepe sa tot scada viteza
    // Cand se opreste alege itemu care e cel mai aproape de mijloc (niddle)
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

    // Opreste specialu
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
