using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicS : MonoBehaviour
{
    public static LogicS instance;

    public int playerScore = 0;
    public int coinScore = 0;

    public Text scoreText;
    public Text coinText;
    public Text timerText;

    public GameObject gameOverScreen;
    public GameObject winScreen;

    private float gameTime = 100;
    private bool isGameOver = false;

    public AudioClip backgroundSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;

    public PipeSpawner pipeSpawnerScript;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (backgroundSound != null)
        {
            audioSource.loop = true;
            audioSource.clip = backgroundSound;
            audioSource.Play();
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (!isGameOver)
        {
            gameTime -= Time.deltaTime;
            timerText.text = $"Time: {Mathf.Ceil(gameTime)}s";

            if (gameTime <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddCoinScore(int value)
    {
        coinScore += value;
        UpdateScoreText();
        CheckRewardSystem();
    }

    public void AddPipeScore(int value)
    {
        playerScore += value;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = $"Pipes: {playerScore}";
        coinText.text = $"Coins: {coinScore}";
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;

        // **أولاً: أوقف صوت الخلفية.**
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        // **ثانياً: شغل صوت النهاية.**
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    public void WinGame()
    {
        isGameOver = true;
        winScreen.SetActive(true);
        Time.timeScale = 0;

        // **أوقف صوت الخلفية عند الفوز أيضاً.**
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void CheckRewardSystem()
    {
        if (coinScore % 2 == 0 && coinScore > 0)
        {
            if (pipeSpawnerScript != null)
            {
                pipeSpawnerScript.spawnRate -= 1f;
                pipeSpawnerScript.heightOffset -= 1f;

                Debug.Log($"Reward Activated! New Spawn Rate: {pipeSpawnerScript.spawnRate}, New Height Offset: {pipeSpawnerScript.heightOffset}");
            }
        }
    }
}