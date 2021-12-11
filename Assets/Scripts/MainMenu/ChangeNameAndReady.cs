using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class ChangeNameAndReady : MonoBehaviour
{
    public static ChangeNameAndReady instance;

    public O.Player p;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log(this + " is already in the scene");

        for (int i = 0; i < readyComp.Count; i++)
            readyComp[i].index = i;
    }

    public TMP_InputField nameField;
    public List<ReadyComponent> readyComp;

    private void Update()
    {
        //UpdateReady();
    }

    public void Change()
    {
        // TO DO
        //ulong id = NetworkManager.singleton.LocalClientId;

        //if (!NetworkManager.singleton.ConnectedClients.TryGetValue(id, out NetworkClient client)) return;

        //if (!client.PlayerObject.TryGetComponent<O.Player>(out var p)) return;

        p.CmdChangeName(nameField.text, true);
    }

    public void UpdateReady()
    {
        foreach (ReadyComponent c in readyComp)
            c.UpdateDisplay();
    }

    public void ChangeName(string text)
    {
        p.CmdChangeName(text, false);
    }
}
