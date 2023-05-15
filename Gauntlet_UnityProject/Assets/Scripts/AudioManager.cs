using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip enemyDefeatedAudio;
    [SerializeField] private AudioClip levelFinishedAudio;
    [SerializeField] private AudioClip playerJoinedAudio;
    [SerializeField] private AudioClip playerLeftAudio;
    [SerializeField] private AudioClip upgradedAudio;
    [SerializeField] private AudioClip addHealthAudio;
    
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEvent.PlayerAdded, PlayPlayerJoined);
        GameEventBus.Subscribe(GameEvent.PlayerLeft, PlayPlayerLeft);
        GameEventBus.Subscribe(GameEvent.EnemyDefeated, PlayEnemyDefeated);
        GameEventBus.Subscribe(GameEvent.LevelFinished, PlayLevelFinished);
        GameEventBus.Subscribe(GameEvent.Upgraded, PlayUpgraded);
        GameEventBus.Subscribe(GameEvent.AddHealth, PlayAddHealth);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameEvent.EnemyDefeated, PlayEnemyDefeated);
        GameEventBus.Unsubscribe(GameEvent.LevelFinished, PlayLevelFinished);
        GameEventBus.Unsubscribe(GameEvent.PlayerAdded, PlayPlayerJoined);
        GameEventBus.Unsubscribe(GameEvent.PlayerLeft, PlayPlayerLeft);
        GameEventBus.Unsubscribe(GameEvent.LevelFinished, PlayLevelFinished);
        GameEventBus.Unsubscribe(GameEvent.Upgraded, PlayUpgraded);
    }

    private void PlayEnemyDefeated() => GetComponent<AudioSource>().PlayOneShot(enemyDefeatedAudio);
    private void PlayLevelFinished() => GetComponent<AudioSource>().PlayOneShot(levelFinishedAudio);
    private void PlayPlayerJoined() => GetComponent<AudioSource>().PlayOneShot(playerJoinedAudio);
    private void PlayPlayerLeft() => GetComponent<AudioSource>().PlayOneShot(playerLeftAudio);
    private void PlayUpgraded() => GetComponent<AudioSource>().PlayOneShot(upgradedAudio);
    private void PlayAddHealth() => GetComponent<AudioSource>().PlayOneShot(addHealthAudio);
}
