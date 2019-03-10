using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
	public Text restartText;
	public Text gameOverText;
	private Text scoreText;
	private bool gameOver;
	private bool restart;
	private int score;

	void Start(){
		gameOver = false;
		restart = false;

		GameObject scoreTextObject = GameObject.FindWithTag("ScoreText");
		if (scoreTextObject != null){
			scoreText = scoreTextObject.GetComponent<Text>();
		}
		if (scoreText == null){
			Debug.Log("Cannot Find 'scoreText' Script");
		}

		GameObject restartTextObject = GameObject.FindWithTag("RestartText");
		if (restartTextObject != null){
			restartText.text = "";
		}
		if (restartText == null){
			Debug.Log("Cannot Find 'restartText' Script");
		}

		GameObject gameOverTextObject = GameObject.FindWithTag("GameOverText");
		if (gameOverTextObject != null){
			gameOverText.text = "";
		}
		if (gameOverText == null){
			Debug.Log("Cannot Find 'gameOverText' Script");
		}

        score = 0;
        UpdateScore ();
		StartCoroutine (SpawnWaves ());
    }
	
	void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }
	
	public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore ()
    {
        scoreText.text = "Score: " + score;
    }
	
	public void GameOver()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
