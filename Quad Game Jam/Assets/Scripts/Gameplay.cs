using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public GameObject playerExemple;
    public GameObject actualPlayer;
    public Player actualPlayerScript;
    public float timeLeft = 60f;
    public bool gameRunning = true;
    public float score = 0f;

    // UI
    public Slider timeLeftUi;
    public Image timeLeftUiColor;
    public Text playerLifeUi;
    public GameObject End;
    public Text scoreUi;
    public Text scoreUiFinal;
    public bool isMale = true;

    // SOUND
    public AudioSource soundBox;
    public AudioClip deathSound;

    void Start()
    {
        actualPlayer = null;
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        isMale = !isMale;
        actualPlayer = Instantiate(playerExemple);
        actualPlayerScript = actualPlayer.GetComponent<Player>();
        actualPlayerScript.SetSex(isMale);
    }

    void Update()
    {
        if (timeLeft < 0)
            gameRunning = false;
        if (gameRunning)
        {
            timeLeft -= Time.deltaTime;
            timeLeftUi.value = timeLeft;
            if (timeLeft < 15f)
                timeLeftUiColor.color = Color.red;
            if (actualPlayer == null)
            {
                soundBox.clip = deathSound;
                soundBox.Play();
                SpawnPlayer();
            }
            playerLifeUi.text = Mathf.RoundToInt(actualPlayerScript.life).ToString();
            if (actualPlayerScript.alive == false) // Death
                actualPlayer = null;
        }
        else
        {
            End.SetActive(true);
            scoreUiFinal.text = score.ToString() + " points";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void WinPoints(int scoreIncoming)
    {
        score += scoreIncoming;
        scoreUi.text = score.ToString();
    }
}
