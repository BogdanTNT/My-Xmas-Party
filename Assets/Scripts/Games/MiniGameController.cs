using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Games;

public class MiniGameController : NetworkBehaviour
{
    #region Singleton

    public static MiniGameController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log($"{this} is already in the scene");
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
    [SyncVar(hook = nameof(UpdateUI))] public States state;

    public class Battle 
    {
        public O.Player defence;
        public List<O.Player> attackers;

        public int index;

        public Battle(O.Player def, List<O.Player> att){
            defence = def;
            attackers = att;
            index = attackers[0].currentPlace;
        }
    }
    [SerializeField] private List<Battle> battles = new List<Battle>();

    public List<GameBase> games = new List<GameBase>();

    public void AddBattle(O.Player defence, List<O.Player> attack) {

        Battle b = new Battle(defence, attack);
        battles.Add(b);
    }

    [Server]
    public void StartBattle() {
        ChangeState(States.Starting);

    }

    private void UpdateUI(States old, States now) {
 
    }

    [Server]
    private void ChangeState(States now) => state = now;

}
