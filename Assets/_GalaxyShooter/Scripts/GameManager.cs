using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonDestroy<GameManager>
{
    [Header("Spawn")]
    public GameObject playerPrefabs;
    public GameObject spawnFX;
    public Transform spawnPos;
    public SpawnManager spawnManager;

    [Space]
    [Header("Score")]
    private int _score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject score;
    private string _scoreString = "Score: ";

    [Space]
    [Header("Button Control")]
    public Button startButton;
    public AudioClip buttonSound;
    public AudioClip winSound;
    public GameObject winpanel;
    public Transform winRoot;
    public Button replayButton;
    public Button exitButton;

    [Space]
    [Header("Pause")]
    public GameObject pausePanel;
    public Transform pauseRoot;
    public Button pauseReplayButton;
    public Button pauseButton;
    public Button continueButton;

    private void Start()
    {
        Application.targetFrameRate = 60;

        winpanel.SetActive(false);
        score.SetActive(false);
        pausePanel.SetActive(false);

        startButton.onClick.AddListener(() => OnGameStart());
        replayButton.onClick.AddListener(() => Replay());
        exitButton.onClick.AddListener(() => ExitGame());

        pauseButton.onClick.AddListener(() => OpenPausePanel());
        pauseReplayButton.onClick.AddListener(() => Replay());
        continueButton.onClick.AddListener(() => Continue());
    }

    public void SpawnPlayer()
    {
        GameObject fx = Instantiate(spawnFX, spawnPos.position, spawnPos.rotation);

        fx.transform.localScale = Vector3.zero;
        fx.transform.DORotate(new Vector3(0, 0, 180), 1f);
        fx.transform.DOScale(1f, 1.25f).OnComplete(() => { Instantiate(playerPrefabs, spawnPos.position, spawnPos.rotation); Destroy(fx); });
    }

    public void OnGameStart()
    {
        score.SetActive(true);
        UpdateScoretext();

        startButton.gameObject.SetActive(false);
        SpawnPlayer();
        spawnManager.StartGame();
        AudioManager.Instance.Shoot(buttonSound);
    }

    public void AddScore()
    {
        _score++;
        UpdateScoretext();

        if (_score >= 16)
        {
            Invoke(nameof(OpenWinPanel), 1f);
        }
    }

    public void UpdateScoretext()
    {
        scoreText.text = _scoreString + _score.ToString();
    }

    public void OpenWinPanel()
    {
        AudioManager.Instance.Shoot(winSound);
        winpanel.gameObject.SetActive(true);
        winRoot.localScale = Vector3.zero;
        winRoot.DOScale(1, 0.3f);
    }

    public void Continue()
    {
        pausePanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.Instance.Shoot(buttonSound);
    }

    public void OpenPausePanel()
    {
        AudioManager.Instance.Shoot(buttonSound);
        pausePanel.gameObject.SetActive(true);
        pauseRoot.localScale = Vector3.zero;
        pauseRoot.DOScale(1, 0.2f).OnComplete(() => Time.timeScale = 0f);
    }

    public void ExitGame()
    {
        AudioManager.Instance.Shoot(buttonSound);
        Application.Quit();
    }

    public void Replay()
    {
        AudioManager.Instance.Shoot(buttonSound);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
