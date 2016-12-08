using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD_Class : MonoBehaviour {

	public static bool pause;

	public GameObject hud, pauseMenu, failMenu, finishMenu;
	public Text hudScoreText, hudHighScoreText, hudTimeText, pauseScoreText, pauseTimeText, failScoreText, failTimeText, scoreText, timeText;
    public float Score { get; set; }


	private float time = 0;
	private float multiplier = 10;
	private int minutes = 0, seconds = 0;
	private Ad_Manager ads;

    private AudioSource soundSource;
    private Sound sound;

    [SerializeField]
    private WebAPIManager webMan;


	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
        Score = 5000;
		hudScoreText.text = Score.ToString();
		hudTimeText.text = minutes + ":" + seconds;
		pause = false;
		ads = GameObject.Find ("Ad_Manager").GetComponent<Ad_Manager> ();
        webMan = WebAPIManager.Instance;
        if (!webMan.Login("test", "1337")) {
            Application.Quit();
        }
		//soundSource = GameObject.Find ("Sound").GetComponent<AudioSource> ();
		//sound = GameObject.Find ("Sound").GetComponent<Sound> ();
    }

    void OnApplicationQuit()
    {
        webMan.Logout();
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
		if (Score >= 0) {
            Score -= newScore;
            Score = (int)Score;
			hudScoreText.text = Score.ToString ();
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
				hudTimeText.text = "0" + minutes + ":0" + seconds;
			}else
				hudTimeText.text = "0" + minutes + ":" + seconds;
		} else {
			if (seconds < 10) {
				hudTimeText.text = minutes + ":0" + seconds;
			}else
				hudTimeText.text = minutes + ":" + seconds;
		}
	}

	public void PauseAndResumeGame(){
		pause = !pause;
		//playSound (sound.button_click);

		if (pause) {
			Time.timeScale = 0;
			pauseScoreText.text = "Score: " + hudScoreText.text;
			pauseTimeText.text = "Time: " + hudTimeText.text;
            //webMan.
			pauseMenu.SetActive (true);
			hud.SetActive (false);
		} else {
			Time.timeScale = 1;
			pauseMenu.SetActive (false);
			hud.SetActive (true);
		}
	}

	public void RestartGame(){
		//playSound (sound.button_click);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		Time.timeScale = 1;	
		pause = false;
	}

	public void QuitGame(){
		//playSound (sound.button_click);
		ads.ShowAd ();
		SceneManager.LoadScene (0);
	}

	public void Fail(){
		pause = true;
		Time.timeScale = 0;
		failScoreText.text = "Score: " + hudScoreText.text;
		failTimeText.text = "Time: " + hudTimeText.text;
		failMenu.SetActive (true);
		hud.SetActive (false);
	}

	public void Finish(){
		pause = true;
		Time.timeScale = 0;
		scoreText.text = "Score: " + hudScoreText.text;
		timeText.text = "Time: " + hudTimeText.text;
		finishMenu.SetActive (true);
		hud.SetActive (false);
        int score = Int32.Parse(hudScoreText.text);
        webMan.PostHighscore(score, SceneManager.GetActiveScene().buildIndex - 1); //SceneManager.GetActiveScene().buildIndex - 1
        score = webMan.GetHighscore(SceneManager.GetActiveScene().buildIndex - 1); //SceneManager.GetActiveScene().buildIndex - 1
        hudHighScoreText.text = score.ToString();
	}

    void playSound(AudioClip sound)
    {
        soundSource.PlayOneShot(sound);
    }
}
