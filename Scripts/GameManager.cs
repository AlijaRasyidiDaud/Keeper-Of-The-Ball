using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

	public int score;
	public int lives;
	public int numberOfBricks;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI livesText;
	public TextMeshProUGUI endScoreText;
	public TextMeshProUGUI loadLevelText;
	public TextMeshProUGUI finScoreText;
	public bool gameOver;
	public GameObject gameOverPanel;
	public GameObject loadLevelPanel;
	public GameObject finishGamePanel;
	public Transform[] levels;
	public int currentLevelIndex = 0;

	// Use this for initialization
	void Start () {

		scoreText.text = "Score : " + score;

		livesText.text = "Lives : " + lives;

		numberOfBricks = GameObject.FindGameObjectsWithTag ("Brick").Length;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Fungsi update jumlah lives
	public void UpdateLives (int changeLives)
	{
		lives += changeLives;

		// Cek jumlah lives (jika habis maka game over)
		if (lives <= 0)
		{
			lives = 0;
			GameOver ();
		}

		livesText.text = "Lives : " + lives;
	}

	// Fungsi update score dari brick
	public void UpdateScore (int changeScore)
	{
		score += changeScore;
		scoreText.text = "Score : " + score;
	}

	// Fungsi jika brick hancur
	public void UpdateNumberOfBrick ()
	{
		numberOfBricks--;
		if (numberOfBricks <= 0)
		{
			// Cek apakah masih ada level selanjutnya
			if (currentLevelIndex >= levels.Length - 1)
			{
				// Tak ada level / finish game
				FinishGame ();
			} else // Kondisi masih ada level selanjutnya
			{
				// Proses load next level	
				loadLevelPanel.SetActive (true);
				loadLevelText.text = "Level " + (currentLevelIndex + 2);
				gameOver = true;
				Invoke ("LoadLevel", 3f);
			}
		}
	}

	// Fungsi meload level
	void LoadLevel ()
	{
		// Destroy level yang sudah dari scene
		GameObject refreshLevel = GameObject.FindWithTag ("Level");
		Destroy (refreshLevel);

		currentLevelIndex++;
		Instantiate (levels [currentLevelIndex], Vector2.zero, Quaternion.identity);
		numberOfBricks = GameObject.FindGameObjectsWithTag ("Brick").Length;
		gameOver = false;
		loadLevelPanel.SetActive (false);
	}

	// Fungsi game over
	void GameOver ()
	{
		gameOver = true;
		endScoreText.text = "Score : " + score; // Menampilkan text score akhir pada panel game over
		gameOverPanel.SetActive (true); // Set aktif panel game over
	}

	// Fungsi game selesai (semua level selesai)
	void FinishGame ()
	{
		gameOver = true;
		finScoreText.text = "Your score is " + score + " !!!" ; // Menampilkan text score akhir pada panel game over
		finishGamePanel.SetActive (true); // Set aktif panel game over
	}

	// Fungsi meload start menu pada kondisi game over
	public void BackMenu ()
	{
		SceneManager.LoadScene ("Main Menu");
	}

	// Fungsi replay level pada kondisi game over (reset score)
	public void RePlay ()
	{
		gameOverPanel.SetActive (false);

		// Readjust score, lives, dan number of bricks
		score = 0;
		lives = 5;
		scoreText.text = "Score : " + score;
		livesText.text = "Lives : " + lives;

		// Menetralisir brick
		GameObject[] refreshBricks = GameObject.FindGameObjectsWithTag ("Brick");
		for (int i = 0; i < refreshBricks.Length; i++)
		{
			Destroy (refreshBricks[i]);
		}

		// Load level
		currentLevelIndex--;
		loadLevelPanel.SetActive (true);
		loadLevelText.text = "Level " + (currentLevelIndex + 2);
		Invoke ("LoadLevel", 3f);
	}

}
