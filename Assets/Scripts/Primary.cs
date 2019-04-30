using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Primary : MonoBehaviour
{
    private int playerLives = 2;
    private int playerScore = 0;

    [SerializeField] float playerSpeed = 3.0f;
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float contactThreshold = 90f;

    public Text livesText;
    public Text scoreText;
    public Text timeText;
    public Text goalText;

    private float timeMonitoring;

    public Camera camera;

    private Animator animator;

    private float direction;
    private bool grounded = true;
    private bool isAttacking = false;
    private bool hasReversedWeaponCollider = false;
    private bool hasCorrectedWeaponCollider = true;
    //private bool isRunning = false;


    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeMonitoring = 300f - Time.time;
        direction = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(Vector2.left * playerSpeed * Time.deltaTime);
            animator.SetBool("Running", true);
        }
        else { animator.SetBool("Running", false); }
        if (direction < 0) { GetComponent<SpriteRenderer>().flipX = true; }
        else if (direction > 0) { GetComponent<SpriteRenderer>().flipX = false; }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && grounded == true)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)) && grounded == true && !animator.GetBool("Running"))
        {
            isAttacking = true;
            Invoke("TurnOffIsAttacking", 1.0f);
            animator.SetTrigger("Standing&Attacking");
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)) && grounded == true && animator.GetBool("Running")) {
            isAttacking = true;
            Invoke("TurnOffIsAttacking", 1.0f);
            animator.SetTrigger("Running&Attacking"); }

        if (playerLives > 0) { livesText.text = "Lives: " + playerLives + "/2"; }
        else if (playerLives <= 0)
        {
            livesText.text = "Lives: 0/2";
            ManageHighScores(playerScore);
            SceneManager.LoadScene("Menu");
        }

        livesText.text = "Lives: " + playerLives + "/2";
        timeText.text = "Time: " + Mathf.Round(timeMonitoring) + "s";
        scoreText.text = "Score: " + playerScore;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            // I couldn't get this vertex check to implement correctly
            //for (int i = 0; i < collision.contacts.Length; i++)
            //{
            //    if (Vector3.Angle(collision.contacts[i].normal, Vector3.up) <= contactThreshold)
            //    {
            //        Debug.Log("We reached the weird angle code");
            //    }
            //}

            grounded = true;
        }

        else if (collision.transform.tag == "Enemy" && isAttacking)
        {
            Destroy(collision.gameObject);
        }

        else if (collision.transform.tag == "Spike") { playerLives -= 1; }

        else if (collision.transform.tag == "Goal") { EndGame(); }

    }

    private void EndGame()
    {
        Invoke("LoadEndGame", 2f);
        //goalText.enabled = true;
        goalText.gameObject.SetActive(true);
        playerScore += (int)(Mathf.Round(timeMonitoring) * 10);
        ManageHighScores(playerScore);
        Destroy(FindObjectOfType<Camera>().GetComponent<CameraFollow>());
    }

    private void LoadEndGame() { SceneManager.LoadScene("Menu"); }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            grounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectible")
        {
            Destroy(collision.GetComponent<SpriteRenderer>());
            collision.GetComponent<ParticleSystem>().Play();
            Destroy(collision.gameObject, 1.5f);
            playerScore += 1000;
        }

        if (collision.tag == "Enemy")
        {
            Debug.Log("Enemy Hit!");
            playerLives -= 1;

            if (playerLives <= 0)
            {
                Debug.Log("Player is Dying");
                ManageHighScores(playerScore);
            }
        }
    }

    //public void CreateScorePref()
    //{
    //    if (PlayerPrefs.HasKey("Ultimate Score") == false) { PlayerPrefs.SetInt("Ultimate Score", 0); }
    //    else { PlayerPrefs.SetInt("Ultimate Score", 0); }
    //}

    public void ManageHighScores(int s) //this function manages the high scores, ordering them correctly after every new score
    {
        int scoreCount = 1;
        while (scoreCount < 11)
        {
            if (PlayerPrefs.HasKey("HighScore" + scoreCount))
            {
                if (s > PlayerPrefs.GetInt("HighScore" + scoreCount))
                {
                    int scoreCountFromBottom = 10;
                    while (scoreCountFromBottom > scoreCount)
                    {
                        if (PlayerPrefs.HasKey("HighScore" + scoreCountFromBottom))
                        {
                            PlayerPrefs.SetInt("HighScore" + scoreCountFromBottom, PlayerPrefs.GetInt("HighScore" + (scoreCountFromBottom - 1)));
                        }
                        scoreCountFromBottom -= 1;
                        continue;
                    }
                    PlayerPrefs.SetInt("HighScore" + scoreCount, s);
                    break;
                }

                else
                {
                    scoreCount += 1;
                    continue;
                }
            }
            else { PlayerPrefs.SetInt("HighScore" + scoreCount, s); }
        }
    }

    private void TurnOffIsAttacking() { isAttacking = false; }
}
