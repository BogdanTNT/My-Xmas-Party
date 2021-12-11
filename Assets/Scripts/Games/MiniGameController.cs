using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    #region Singleton

    public static MiniGameController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log(this + " is already in the scene");
    }

    #endregion

    // Closed nu e niciun minigame activ
    // In Start: nimic
    // IN update: asteapta sa inceapa un minigame
    //
    // Starting incepe jocul
    // In Start: Alege minigameu, reseteaza minigameul, reseteaza readyurile
    // In Update: Asteapta sa se termine animatile de minigame
    //
    // WaitForTutorial toti citesc tutorialu si zic cand sunt gata
    // In Start: 
    // In Update: Asteapta ca toti sa fie ready
    //
    // WaitForGame asteapta ca minigameu sa se termina si sa fie un castigar
    // In Start: Depinde de minigame
    // In Update: Depinde de minigame
    //
    // Finishing arata rezultatele si inchide minigameul
    // In Start: Transfera datele la TableController
    // In Update: Asteapta ca toate animatile sa se termine
    public enum States
    {
        Closed,
        Starting,
        WaitForTutorial,
        WaitForGame,
        Finishing
    }
    public States state;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(state == States.Closed)
        {
            //Debug.Log("No minigame is player at the moment");
        }
        else if(state == States.Starting)
        {
            //Debug.Log("The game is starting up");
        }
        else if(state == States.WaitForTutorial)
        {
            //Debug.Log("Waiting for everyone to read the tutorial");
        }
        else if(state == States.WaitForGame)
        {
            //Debug.Log("Waiting to see who wins the game");
        }
        else if(state == States.Finishing)
        {
            //Debug.Log("Waiting for game to close");
        }
    }
}
