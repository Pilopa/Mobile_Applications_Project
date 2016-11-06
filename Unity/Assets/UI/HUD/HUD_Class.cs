using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD_Class : MonoBehaviour {

	public static bool pause;

	public GameObject hud, pauseMenu;
	public Text scoreText, timeText, pauseScoreText, pauseTimeText;

	private float score, time = 0;
	private int multiplier = 10, minutes = 0, seconds = 0;


	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		score = 5000;
		scoreText.text = score.ToString();
		timeText.text = minutes + ":" + seconds;
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pause) {
			SetScore (Time.deltaTime * multiplier);
			SetTime ();
		}
	}


	private void SetScore(float newScore)
	{
		if (score >= 0) {
			score -= newScore;
			score = (int)score;
			scoreText.text = score.ToString ();
		}
	}

	private void SetTime()
	{
		time += Time.deltaTime;
		seconds = (int)time;
		if (seconds == 60) {
			time = 0;
			minutes += 1;
		}
		if (minutes < 10) {
			if (seconds < 10) {
				timeText.text = "0" + minutes + ":0" + seconds;
			}else
				timeText.text = "0" + minutes + ":" + seconds;
		} else {
			if (seconds < 10) {
				timeText.text = minutes + ":0" + seconds;
			}else
				timeText.text = minutes + ":" + seconds;
		}
	}

	public void PauseAndResumeGame(){
		pause = !pause;

		if (pause) {
			Time.timeScale = 0;
			pauseScoreText.text = "Score: " + scoreText.text;
			pauseTimeText.text = "Time: " + timeText.text;
			pauseMenu.SetActive (true);
			hud.SetActive (false);
		} else {
			Time.timeScale = 1;
			pauseMenu.SetActive (false);
			hud.SetActive (true);
		}
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		Time.timeScale = 1;	
	}

	public void QuitGame(){
		SceneManager.LoadScene (0);
	}
}
