using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

// Itemu in canvas
// Trebuie facut un prefab cu asta
public class WeaponImage : NetworkBehaviour
{
    public Image image;
    public Sprite[] sprites;

    [SyncVar(hook = nameof(OnIndexChange))] public int index;

    // Alege un item random
    [Server]
    public void Change()
    {
        index = Random.Range(0, 4);
    }

    private void OnIndexChange(int old, int now)
    {
        image.sprite = sprites[now];

    }
}
