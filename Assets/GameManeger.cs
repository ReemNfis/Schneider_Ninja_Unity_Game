using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public WitchS witchScript;
    public PipeSpawner pipeSpawnerScript;
    public GameObject startGame;
    public GameObject level1Button;
    public GameObject level2Button;

    public enum DifficultyLevel { Easy, Hard }
    public DifficultyLevel currentLevel;

    public ShopSystem shopSystem;
    public GameObject ShowShop;
    public GameObject shopMenu;

    public LogicS logic;

    public int winScoreEasy = 20;
    public int winScoreHard = 40;

    void Start()
    {
        witchScript.enabled = false;
        pipeSpawnerScript.enabled = false;
        Time.timeScale = 0;

        level1Button.GetComponent<Button>().onClick.AddListener(() => SetDifficulty(DifficultyLevel.Easy));
        level2Button.GetComponent<Button>().onClick.AddListener(() => SetDifficulty(DifficultyLevel.Hard));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        witchScript.enabled = true;
        pipeSpawnerScript.enabled = true;
        shopMenu.SetActive(false);
        ShowShop.SetActive(false);
        Time.timeScale = 1;
    }

    public void PassPipe()
    {
        logic.AddPipeScore(1);
    }

    public void CollectCoin()
    {
        logic.AddCoinScore(1);
    }

    public void SetDifficulty(DifficultyLevel level)
    {
        currentLevel = level;

        if (currentLevel == DifficultyLevel.Easy)
        {
            pipeSpawnerScript.spawnRate = 3.5f;
            pipeSpawnerScript.heightOffset = 10f;
            witchScript.flapStrength = 8f;
        }
        else if (currentLevel == DifficultyLevel.Hard)
        {
            pipeSpawnerScript.spawnRate = 2.5f;
            pipeSpawnerScript.heightOffset = 5f;
            witchScript.flapStrength = 6f;
        }

        startMenu.SetActive(false);
    }

    public void ShopMenu()
    {
        shopMenu.SetActive(true);
        ShowShop.SetActive(true);
        startMenu.SetActive(false);
    }
}