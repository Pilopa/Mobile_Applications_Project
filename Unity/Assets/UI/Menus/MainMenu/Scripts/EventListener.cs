using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EventListener : MonoBehaviour {

	public GameObject mainMenu, startMenu, optionsMenu;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartGame()
	{		
		SceneManager.LoadScene ("prototyp");
	}

	public void EnableMainMenu()
	{
		this.mainMenu.SetActive (true);

		if (this.optionsMenu.activeSelf)
		{			
			this.optionsMenu.SetActive (false);
		}
		if (this.startMenu.activeSelf)
		{
			this.startMenu.SetActive (false);
		}
	}

	public void EnableStartMenu()
	{
		this.startMenu.SetActive (true);
		this.mainMenu.SetActive (false);
	}

	public void EnableOptionsMenu()
	{
		this.optionsMenu.SetActive (true);
		this.mainMenu.SetActive (false);
	}



}
