using System;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ScoreEntry
{
    public string name;
    public int score;
}

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private List<ScoreEntry> scoreEntryList = new List<ScoreEntry>(10);
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private GameObject entryTemplate;
    [SerializeField] private Transform entryContainer;
    [SerializeField] private GameObject leaderboardPanel;
    [SerializeField] private Gameplay gameplayScript;
    
    public GameObject inGamePanel;
    public GameObject highScorePanel;
    
    public string playerInputName;
    public int score; 
    
    private void Update()
    {
        scoreText.text = score.ToString();
        playerInputName = nameInputField.text;
    }

    private void OrganizeListByScore()
    {
        for (var i = 0; i < scoreEntryList.Count; i++)
        {
            for (var j = 0; j < scoreEntryList.Count; j++)
            {
                if (scoreEntryList[j].score < scoreEntryList[i].score)
                {
                    var tempScoreEntry = scoreEntryList[i];
                    scoreEntryList[i] = scoreEntryList[j];
                    scoreEntryList[j] = tempScoreEntry;
                }
            }
        }    
    }
    
    public void AddScoreEntry()
    {
        var scoreEntry = new ScoreEntry() {name = playerInputName, score = score};

        scoreEntryList.Add(scoreEntry);
        OrganizeListByScore();
    }

    public void ConstructLeaderboard()
    {
        highScorePanel.SetActive(false);
        leaderboardPanel.SetActive(true);
        
        AddScoreEntry();
        
        float entryHeight = entryTemplate.GetComponent<RectTransform>().rect.height;
        float spaceBetweenEntries = entryHeight + 3;
            
        for (int i = 0; i < 10; i++)
        {
            if (scoreEntryList[i] != null)
            {
                GameObject entryGameObject = Instantiate(entryTemplate, entryContainer);
                entryGameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = scoreEntryList[i].name;
                entryGameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = scoreEntryList[i].score.ToString();
                RectTransform entryRectTransform = entryGameObject.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0,-spaceBetweenEntries * i);
            }
        }
    }

    public void RestartGame()
    {
        leaderboardPanel.SetActive(false);
        nameInputField.text = "";
        gameplayScript.RestartGameValues();
        inGamePanel.SetActive(true);
        
        foreach (Transform child in entryContainer.transform) {
            Destroy(child.gameObject);
        }
    }
}


