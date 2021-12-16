using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float _speed = 5;
    public float speed // ENCAPSULATION
    {
        get { return _speed; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("You can't have negative speed!");
            }
            else
            {
                _speed = value;
            }
        }
    }
    public float rotationSpeed;
    public float verticalInput;
    public float horizontalInput;
    public GameObject gameOverScreenPanel;
    public GameObject gameOverScreen;
    public GameObject gameOverRestartScreen;
    public GameObject particleEnemyDead;
    public TextMeshProUGUI finalScoreText;
    private Rigidbody playerRb;
    public bool playerHasFirefly = false;
    public bool playerIsMoving;
    public Animator playerAnim;
    public AudioSource playerAudio;
    private SpawnManager spawnManagerScript;
    public AudioClip fireflyPickUpSound;
    public AudioClip screamDeadSound;
    public AudioClip enemyVanishSound;
    public GameObject cameraObject;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManagerScript.gameIsActive)
        {
            MovePlayer(); //Player movement Script // ABSTRACTION
            CheckForPlayerMovement(); // ABSTRACTION
        }
        
    }

    void MovePlayer()
    {
        //Movement Inputs
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        //Rotation script
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

    }

    void CheckForPlayerMovement()
    {
        if (verticalInput != 0 || horizontalInput != 0)
        {
            playerAnim.SetBool("isPlayerMoving", true);
            playerIsMoving = true;
        }

        if (verticalInput == 0 && horizontalInput == 0)
        {
            playerIsMoving = false;
            playerAnim.SetBool("isPlayerMoving", false);
        }
    }

    private void OnTriggerEnter(Collider other) // Colision with powerup and enemy
    {
        if (other.CompareTag("Enemy") && playerHasFirefly)
        {
            Debug.Log("You collided with " + other.tag + " but u not ded u habe firefly!");
            playerHasFirefly = false;
            playerAudio.PlayOneShot(enemyVanishSound);
            spawnManagerScript.score += 1;
            Instantiate(particleEnemyDead, other.gameObject.transform.position, particleEnemyDead.transform.rotation);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy") && !playerHasFirefly)
        {
            gameOverScreen.gameObject.SetActive(true);
            Debug.Log("You collided with " + other.tag + " and you ded, lol");
            playerAudio.PlayOneShot(screamDeadSound);
            GameOver();

        }


        if (other.CompareTag("Firefly"))
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(fireflyPickUpSound);
            Debug.Log("You picked a firefly and it died, congrats...");
            playerHasFirefly = true;
        }
    }

    public void GameOver()
    {
        
        LeanTween.alpha(gameOverScreenPanel.GetComponent<RectTransform>(), 1, 0.2f);
        int leanID = LeanTween.alpha(gameOverScreenPanel.GetComponent<RectTransform>(), 1, 0.1f).id;
        spawnManagerScript.gameIsActive = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            GameObject.Destroy(enemy);
        }
        if (LeanTween.isTweening(leanID))
        {
            finalScoreText.text = "You survived " + spawnManagerScript.score + " enemys";
            gameOverRestartScreen.gameObject.SetActive(true);
        }

    }
}
