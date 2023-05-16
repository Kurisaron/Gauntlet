using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public int levelIndex;

    void Awake()
    {
        levels.Add((GameObject)Resources.Load("Prefabs/Levels/Level1"));
        levels.Add((GameObject)Resources.Load("Prefabs/Levels/Level2"));
        levels.Add((GameObject)Resources.Load("Prefabs/Levels/Level3"));
        
        
    }

    private void Start()
    {
        //levels[0].SetActive(true);
        Instantiate(levels[0], transform.position, transform.rotation);
    }

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEvent.LevelFinished, TransitionLevel);
    }
    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.LevelFinished, TransitionLevel);
    }

    public void TransitionLevel()
    {
        levels[levelIndex].SetActive(false);
        levelIndex++;
        Instantiate(levels[levelIndex], transform.position, transform.rotation);
        //levels[levelIndex].SetActive(true);
    }
}
