using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HUD_Class : MonoBehaviour {

	public static bool pause = false;

	public GameObject hud;
	public Text scoreText, timeText;

	private float score, time = 0;
	private int multiplier = 10, minutes = 0, seconds = 0;


	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		score = 5000;
		scoreText.text = score.ToString();
		timeText.text = minutes + ":" + seconds;
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
		score -= newScore;
		score = (int)score;
		scoreText.text = score.ToString();
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

	public void PauseGame(){
		pause = !pause;

		if (pause) {
			Time.timeScale = 0;

		} else {
			Time.timeScale = 1;

		}


	}
}
