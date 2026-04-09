using UnityEngine;

public class WitchS : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;

    public LogicS logic;
    public bool witchIsAlive = true;

    public int winScore = 20;
    public float winTime = 160;
    private float elapsedTime = 0;

    public GameManager mangerGame;

    private bool hasPlayedCollisionSound = false;
    private bool hasPlayedWinSound = false;
    public AudioClip collisionSound;
    public AudioClip winSound;

    private AudioSource audioSource;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicS>();
        mangerGame = GameObject.FindGameObjectWithTag("mangerGame").GetComponent<GameManager>();

        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        LoadCharacter(selectedCharacter);

        if (mangerGame.currentLevel == GameManager.DifficultyLevel.Easy)
        {
            winScore = mangerGame.winScoreEasy;
        }
        else if (mangerGame.currentLevel == GameManager.DifficultyLevel.Hard)
        {
            winScore = mangerGame.winScoreHard;
        }
    }

    private void LoadCharacter(int characterIndex)
    {
        Debug.Log("Loading character " + characterIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && witchIsAlive == true)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }

        if (witchIsAlive)
        {
            elapsedTime += Time.deltaTime;

            if (logic.playerScore >= winScore || elapsedTime >= winTime)
            {
                if (!audioSource.isPlaying && winSound != null && !hasPlayedWinSound)
                {
                    audioSource.PlayOneShot(winSound);
                    hasPlayedWinSound = true;
                }

                logic.WinGame();
            }
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionSound != null && !hasPlayedCollisionSound)
        {
            GetComponent<AudioSource>().PlayOneShot(collisionSound);
            hasPlayedCollisionSound = true;
        }

        witchIsAlive = false;

        float delay = collisionSound != null ? collisionSound.length : 0;
        StartCoroutine(WaitForCollisionSoundToEnd(delay));
    }

    private System.Collections.IEnumerator WaitForCollisionSoundToEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        logic.GameOver();
    }
}