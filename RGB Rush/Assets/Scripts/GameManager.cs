using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] float startSpawnDelay = 3;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameHeaderText;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    private float spawnX = 16;
    private float spawnY = 7f;

    private int score;
    private float spawnDelay;
    private int waveCounter;

    private float R = 0;
    private float G = 0;
    private float B = 0;

    void Start()
    {
        //InvokeRepeating("SpawnEnemy", startSpawnDelay, 0.7f);
        score = 0;
        spawnDelay = 1.0f;
        waveCounter = 1;
        restartButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        scoreText.text = "Score" + score;
        StartCoroutine("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;

        if(score / 25 >= waveCounter && spawnDelay > 0.3f)
        {
            spawnDelay -= 0.1f;
            Debug.Log(spawnDelay);
            waveCounter++;
        }

    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(startSpawnDelay);
        gameHeaderText.SetActive(false);

        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
        
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = RandomSpawnPosition(spawnX, spawnY);
        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }

    public void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            if(enemy.GetComponent<Enemy>().DestroyMe(R, G, B))
            {
                score++;
            }
        }
    }

    private void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void GameOver()
    {
        StopCoroutine("Spawner");
        this.DestroyAllEnemies();
        gameHeaderText.GetComponent<TextMeshProUGUI>().text = "Game over!";
        gameHeaderText.SetActive(true);
        restartButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private Vector3 RandomSpawnPosition(float boundaryX, float posY)
    {
        float x = Random.Range(-boundaryX, boundaryX);

        return new Vector3(x, posY, 0);
    }

    public void ChangeR()
    {
        this.R = this.R > 0 ? 0 : 1;
    }

    public void ChangeG()
    {
        this.G = this.G > 0 ? 0 : 1;
    }

    public void ChangeB()
    {
        this.B = this.B > 0 ? 0 : 1;
    }
}
