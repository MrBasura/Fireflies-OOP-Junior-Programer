using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject firefly;
    public PlayerController playerScript;
    public GameObject titleScreen;
    public TextMeshProUGUI scoreText;
    [SerializeField] int wave = 1;
    public int score = 0;
    private float boundryMin = 20f;
    private float boundryMax = 20f;
    public float timeToWait = 4f;
    private int firefliesCounter;
    private bool isSpawningEnemy = false;
    private bool isSpawningFirefly = false;
    public bool gameIsActive = false;
    

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckScore();
        firefliesCounter = FindObjectsOfType<Firefly>().Length;

        if (!isSpawningEnemy && gameIsActive)
        {
            isSpawningEnemy = true;
            StartCoroutine(SpawnNextWave());
        }

        if (!isSpawningFirefly && firefliesCounter == 0 && playerScript.playerHasFirefly == false && gameIsActive)
        {
            isSpawningFirefly = true;
            SpawnNextFirefly();
        }


    }
    IEnumerator SpawnNextWave()
    {

        yield return new WaitForSeconds(timeToWait);
        if (gameIsActive)
        {
            Vector3 spawnPosition = transform.position;
            float distance = Random.Range(boundryMin, boundryMax);
            float angle = Random.Range(-Mathf.PI, Mathf.PI);
            spawnPosition += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
            Quaternion lookRotation = Quaternion.LookRotation(player.transform.position, Vector3.up);
            Instantiate(enemy, spawnPosition, enemy.transform.rotation);
            if (timeToWait < 1)
            {
                timeToWait = 1;

            }
            else
            {
                timeToWait -= 0.2f;
            }

            isSpawningEnemy = false;
        }
       

    }
    void SpawnNextFirefly()
    {
        Vector3 spawnPosition = transform.position;
        float distance = Random.Range(boundryMin, boundryMax);
        float angle = Random.Range(-Mathf.PI, Mathf.PI);
        spawnPosition += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;
        Instantiate(firefly, spawnPosition, firefly.transform.rotation);
        isSpawningFirefly = false;
    }

    public void StartGame()
    {
        gameIsActive = true;
        titleScreen.gameObject.SetActive(false);
    }
    public void CheckScore()
    {
        scoreText.text = "Enemys Survived: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

