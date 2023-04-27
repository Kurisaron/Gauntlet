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

    [SerializeField]
    private GameObject classChoices;

    // FUNCTIONS
    private void Update()
    {
        ShowClassChoices();

    }

    private void UpdateHealth()
    {
        for (int i = 0; i < playerContainers.Length; i++)
        {
            if (!playerContainers[i].frame.activeInHierarchy) continue;

        }
    }

    private void UpdateScore()
    {
        for (int i = 0; i < playerContainers.Length; i++)
        {
            if (!playerContainers[i].frame.activeInHierarchy) continue;

            playerContainers[i].ScoreText.text = GameManager.Instance.players[i].score.ToString();
        }
    }

    public void DisableAllContainers()
    {
        for(int i = 0; i < playerContainers.Length; i++)
        {
            playerContainers[i].frame.SetActive(false);
        }
    }

    public void EnableContainer(int index)
    {
        PlayerStats stats = playerContainers[index];
        stats.frame.SetActive(true);
        stats.ClassName.text = "Choose Class";
        stats.ClassName.fontSize = 13;
        stats.ScoreText.text = GameManager.Instance.players[index].score.ToString();
    }

    public void DisableContainer(int index)
    {
        if (playerContainers[index] != null) playerContainers[index].frame.SetActive(false);
    }

    public void SetClass(int index, ClassEnum classEnum)
    {
        PlayerStats stats = playerContainers[index];
        stats.ClassName.text = classEnum.ToString();
        stats.ClassName.fontSize = 18;

    }

    private void ShowClassChoices()
    {
        bool allCharactersHaveClass = true;
        for (int i = 0; i < playerContainers.Length; i++)
        {
            if (!playerContainers[i].frame.activeInHierarchy) continue;

            if (!GameManager.Instance.players[i].gameObject.GetComponent<PlayerInputEvents>().ClassSelected) allCharactersHaveClass = false;
        }

        if (allCharactersHaveClass == classChoices.activeInHierarchy) classChoices.SetActive(!allCharactersHaveClass);
    }

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

        public Text ClassName
        {
            get { return className; }
        }

        public Text ScoreText
        {
            get { return scoreText; }
        }

        public Text HealthText
        {
            get { return healthText; }
        }
    }
}
