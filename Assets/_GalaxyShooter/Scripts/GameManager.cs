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

    private void Start()
    {
        winpanel.SetActive(false);
        score.SetActive(false);

        startButton.onClick.AddListener(() => OnGameStart());
        replayButton.onClick.AddListener(() => Replay());
        exitButton.onClick.AddListener(() => ExitGame());
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
            Invoke(nameof(OpenPanel), 1f);
        }
    }

    public void UpdateScoretext()
    {
        scoreText.text = _scoreString + _score.ToString();
    }

    public void OpenPanel()
    {
        AudioManager.Instance.Shoot(winSound);
        winpanel.gameObject.SetActive(true);
        winRoot.localScale = Vector3.zero;
        winRoot.DOScale(1, 0.3f);
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
    }
}
