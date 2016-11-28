using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventListener : MonoBehaviour {

	public GameObject mainMenu, startMenu, optionsMenu;
	public Toggle soundToggle, musicToggle;
	public Slider soundSlider, musicSlider;

	private AudioSource soundSource, musicSource;
	private Sound sound;


	// Use this for initialization
	void Start () {
		soundSource = GameObject.Find ("Sound").GetComponent<AudioSource> ();
		musicSource = GameObject.Find ("Music").GetComponent<AudioSource> ();
		sound = GameObject.Find ("Sound").GetComponent<Sound> ();

		if (PlayerPrefs.HasKey ("MusicVolume"))
			musicSource.volume = PlayerPrefs.GetFloat ("MusicVolume");
		if (PlayerPrefs.HasKey ("MusicMute"))
			musicSource.mute = GetBool ("MusicMute");
		if (PlayerPrefs.HasKey ("SoundVolume"))
			soundSource.volume = PlayerPrefs.GetFloat ("SoundVolume");
		if (PlayerPrefs.HasKey ("SoundMute"))
			soundSource.mute = GetBool ("SoundMute");
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
		this.startMenu.SetActive (false);
	}

	public void EnableStartMenu()
	{
		playSound (sound.button_click);
		this.startMenu.SetActive (true);
		this.mainMenu.SetActive (false);
	}


	public void SaveSettings()
	{
		playSound (sound.button_click);
		this.mainMenu.SetActive (true);
		this.optionsMenu.SetActive (false);
		PlayerPrefs.SetFloat ("MusicVolume", musicSource.volume);
		SetBool ("MusicMute", musicSource.mute);
		PlayerPrefs.SetFloat ("SoundVolume", soundSource.volume);
		SetBool ("SoundMute", soundSource.mute);

	}

	public void EnableOptionsMenu()
	{
		playSound (sound.button_click);
		this.optionsMenu.SetActive (true);
		this.mainMenu.SetActive (false);
		soundToggle = GameObject.Find ("SoundToggle").GetComponent<Toggle> () as Toggle;
		soundSlider = GameObject.Find ("SoundSlider").GetComponent<Slider> () as Slider;
		musicToggle =GameObject.Find ("MusicToggle").GetComponent<Toggle> () as Toggle;
		musicSlider = GameObject.Find ("MusicSlider").GetComponent<Slider> () as Slider;

		soundToggle.isOn = !soundSource.mute;
		soundSlider.value = soundSource.volume;
		musicToggle.isOn = !musicSource.mute;
		musicSlider.value = musicSource.volume;


		soundToggle.onValueChanged.AddListener (delegate {
			SoundValueChangeCheck();
		});
		soundSlider.onValueChanged.AddListener (delegate {
			SoundVolumeValueChangeCheck();
		});

		musicToggle.onValueChanged.AddListener (delegate {
			MusicValueChangeCheck();
		});
		musicSlider.onValueChanged.AddListener (delegate {
			MusicVolumeValueChangeCheck();
		});

	}

	public void SoundValueChangeCheck(){
		soundSource.mute = !soundToggle.isOn;
	}	
	public void SoundVolumeValueChangeCheck(){
		soundSource.volume = soundSlider.value;
	}

	public void MusicValueChangeCheck(){
		musicSource.mute = !musicToggle.isOn;
	}
	public void MusicVolumeValueChangeCheck(){
		musicSource.volume = musicSlider.value;
	}

	public static void SetBool(string name, bool value){
		PlayerPrefs.SetInt (name, value ? 1 : 0);
	}

	public static bool GetBool(string name){		
		return PlayerPrefs.GetInt (name) == 1 ? true : false;
	}


}
