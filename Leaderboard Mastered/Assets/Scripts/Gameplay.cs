using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Gameplay : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private RectTransform objectTransform;
    [SerializeField] private LeaderboardManager leaderboardManager;

    public float time;
    public bool gameIsStopped;

    private void Start()
    {
        time = 30f;
        MoveAround();
    }

    private void Update()
    {
        timerText.text = time.ToString("0.00");
        scoreText.text = $"Score:{leaderboardManager.score}";
        if (time <= 0 && !gameIsStopped)
        {
            gameIsStopped = true;
            time = 0;
            Time.timeScale = 0;
            objectTransform.GetComponent<Button>().interactable = false;
            leaderboardManager.inGamePanel.SetActive(false);
            leaderboardManager.highScorePanel.SetActive(true);
        }
        else
        {
            time -= Time.deltaTime;
        }
    }

    public void AddScore()
    {
        leaderboardManager.score++;
        MoveAround();
    }

    private void MoveAround()
    {
        float minX = -345;
        float maxX = 345;
        float minY = -175;
        float maxY = 130;

        objectTransform.localPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    public void RestartGameValues()
    {
        time = 30f;
        Time.timeScale = 1;
        gameIsStopped = false;
        leaderboardManager.score = 0;
        objectTransform.GetComponent<Button>().interactable = true;
    }
}
