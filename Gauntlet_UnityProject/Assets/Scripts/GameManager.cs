using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
    public LevelManager levelManager;
    //[HideInInspector]
    public Player[] players = new Player[4];

    public CharacterClass[] classes;
    public Upgrade[] upgrades;
    public Material[] enemyMaterials;

    public override void Awake()
    {
        base.Awake();

        levelManager = gameObject.AddComponent<LevelManager>();

        GetComponent<GameUIManager>().DisableAllContainers();

    }

    private void OnEnable()
    {
        // Subscribe to events here
        GameEventBus.Subscribe(GameEvent.PlayerAdded, PlayerAddedAction);
        GameEventBus.Subscribe(GameEvent.PlayerLeft, PlayerLeftAction);
    }

    private void OnDisable()
    {
        // Unsubscribe from events here
        GameEventBus.Unsubscribe(GameEvent.PlayerAdded, PlayerAddedAction);
        GameEventBus.Unsubscribe(GameEvent.PlayerLeft, PlayerLeftAction);
    }

    // PLAYER INPUT MANAGER EVENTS
    public void NewPlayerJoined(PlayerInput playerInput)
    {
        
        // Open class selection key in UI
        // Put new player's input into class selection state

        // Put new player into the first empty spot of the local array
        if (SpotFree(out int index))
        {
            Debug.Log("Spot Free: " + index.ToString());
            players[index] = playerInput.gameObject.GetComponent<Player>();
            GameUIManager.Instance.EnableContainer(index);

            // Player has joined, game event must fire
            GameEventBus.Publish(GameEvent.PlayerAdded);
        }


    }

    public void PlayerLeft(PlayerInput playerInput)
    {
        // TO-DO: Remove player and anything related to it

        // Remove the player from the local array
        int index = GetPlayerIndex(playerInput.gameObject.GetComponent<Player>());
        players[index] = null;
        GameUIManager.Instance.DisableContainer(index);

        // Player has left, game event must fire
        GameEventBus.Publish(GameEvent.PlayerLeft);
    }

    // GAME EVENT BUS ACTIONS
    private void PlayerAddedAction()
    {
        Debug.Log("Player joined");
    }

    private void PlayerLeftAction()
    {
        Debug.Log("Player left");
    }

    // MISC PLAYERS FUNCTIONS
    private bool SpotFree(out int index)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null)
            {
                index = i;
                return true;
            }
        }

        index = 0;
        return false;
    }

    public int GetPlayerIndex(Player playerToFind)
    {
        return Array.FindIndex(players, player => player == playerToFind);
    }

    public bool AnyPlayers()
    {
        Player myPlayer = Array.Find(players, player => player != null);

        return myPlayer != null;
    }

    
 }
