using UnityEngine;
using System.Collections;

public class WebAPIManagerTest : MonoBehaviour {

	[SerializeField]
	private WebAPIManager manager;

	// Use this for initialization
	void Start () {

		// Login + Logout
		bool loginResult = manager.Login ("test", "1337");
		Debug.Log ("Login: " + loginResult);

		bool logoutResult = manager.Logout ();
		Debug.Log ("Logout with previous Login: " + logoutResult);

		// Logout without login
		logoutResult = manager.Logout ();
		Debug.Log ("Logout without previous Login: " + logoutResult);

		// Post Highscore without Login
		bool postHighscoreResult = manager.PostHighscore (100, 0) == 200;
		Debug.Log ("PostHighscore(100, 0) without Login: " + postHighscoreResult);

		// Login + PostHighscore + GetHighscore
		loginResult = manager.Login ("test", "1337");
		Debug.Log ("Login: " + loginResult);

		postHighscoreResult = manager.PostHighscore (100, 0) == 200;
		Debug.Log ("PostHighscore(100, 0): " + postHighscoreResult);

		int getHighScoreResult = manager.GetHighscore (0);
		Debug.Log ("GetHighscore(0): " + getHighScoreResult);

		// Register
		int registerResult = manager.Register("test", "1337");
		Debug.Log ("Register: " + registerResult);
	}
}
