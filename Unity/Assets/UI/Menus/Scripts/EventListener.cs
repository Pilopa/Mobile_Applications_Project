using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventListener : MonoBehaviour {

	public GameObject mainMenu, startMenu, optionsMenu;


	private AudioSource soundSource;
	private Sound sound;



	// Use this for initialization
	void Start () {
		soundSource = GameObject.Find ("Sound").GetComponent<AudioSource> ();
		sound = GameObject.Find ("Sound").GetComponent<Sound> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void playSound(AudioClip sound)
	{
		soundSource.PlayOneShot (sound);
	}


	public void StartLevel01()
	{		
		playSound (sound.button_click);
		SceneManager.LoadScene (1);
	}

    public void StartLevel02()
    {
        playSound(sound.button_click);
        SceneManager.LoadScene(2);
    }

    public void EnableMainMenu()
	{
		playSound (sound.button_click);
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
		playSound (sound.button_click);
		this.startMenu.SetActive (true);
		this.mainMenu.SetActive (false);
	}

	public void EnableOptionsMenu()
	{
		playSound (sound.button_click);
		this.optionsMenu.SetActive (true);
		this.mainMenu.SetActive (false);
	}
}
