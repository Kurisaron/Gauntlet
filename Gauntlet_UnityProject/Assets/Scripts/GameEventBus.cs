using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameEvent
{
    EnemySpawned,
    EnemyDefeated,
    ShotFood,
    ShotPotion,
    LevelFinished,
    GameOver,
    AddHealth,
    ScoreAdded,
    PlayerAdded,
    PlayerLeft
}

public class GameEventBus
{
    private static readonly IDictionary<GameEvent, UnityEvent> gameEvents = new Dictionary<GameEvent, UnityEvent>();

    public static void Subscribe(GameEvent eventType, UnityAction action)
    {
        UnityEvent thisEvent;

        if (gameEvents.TryGetValue(eventType, out thisEvent))
        {
            thisEvent.AddListener(action);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(action);
            gameEvents.Add(eventType, thisEvent);
        }
    }

    public static void Unsubscribe(GameEvent gameEvent, UnityAction action)
    {
        UnityEvent thisEvent;

        if(gameEvents.TryGetValue(gameEvent, out thisEvent))
        {
            thisEvent.RemoveListener(action);
        }
    }

    public static void Publish(GameEvent gameEvent)
    {
        UnityEvent thisEvent;

        if (gameEvents.TryGetValue(gameEvent, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}
