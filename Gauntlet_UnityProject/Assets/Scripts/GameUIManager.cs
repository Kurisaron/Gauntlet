using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    // VARIABLES
    [SerializeField]
    private GameObject gameUICanvas;

    [SerializeField]
    private PlayerStats[] playerContainers;

    [SerializeField]
    private Text levelUIText;

    // FUNCTIONS
    public void DisableAllContainers()
    {
        for(int i = 0; i < playerContainers.Length; i++)
        {
            playerContainers[i].frame.SetActive(false);
        }
    }

    public void EnableContainer(int index) => playerContainers[index].frame.SetActive(true);

    public void DisableContainer(int index) => playerContainers[index].frame.SetActive(false);

    // CLASSES
    [Serializable]
    public class PlayerStats
    {
        public GameObject frame;
        [SerializeField]
        private Text className;
        [SerializeField]
        private Text scoreText;
        [SerializeField]
        private Text healthText;
        [SerializeField]
        private Image[] upgradeIndicators;
        [SerializeField]
        private Image[] inventoryIndicators;
    }
}
