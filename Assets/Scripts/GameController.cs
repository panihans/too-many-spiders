using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SpiderController SpiderPrefab;
    // Start is called before the first frame update
    float spawnDelay = 4;
    float currentSpawnDelay = 0;

    public Text ScoreText;
    float score;
    public float Score
    {
        get => score;
        set
        {
            score = value;
            ScoreText.text = $"Score: {score}";
        }
    }

    public Canvas Menu;
    public Button StartButton;
    public Button ExitButton;
    public Canvas Menu1;
    public Button RestartButton;
    public Button ReexitButton;
    public bool running = false;
    void Start()
    {
        Menu.enabled = true;
        Menu1.enabled = false;
        Time.timeScale = 0;
        currentSpawnDelay = 1;
        Score = 0;

        StartButton.onClick.AddListener(() =>
        {
            Menu.enabled = false;
            Menu1.enabled = false;
            Time.timeScale = 1;
            running = true;
        });
        ExitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        ReexitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            return;
        }
        if (currentSpawnDelay < 0)
        {
            spawnDelay = Mathf.Max(spawnDelay * 0.90f, 1f);
            currentSpawnDelay = spawnDelay;
            Instantiate(SpiderPrefab, new Vector3(Random.Range(-8f, 8f), Random.Range(-6f, 3.5f)), Quaternion.identity, null);
        }
        else
        {
            currentSpawnDelay -= Time.deltaTime;
        }
    }

    public void RestartGame()
    {
        Menu.enabled = false;
        Menu1.enabled = true;
        Time.timeScale = 0;
    }
}
