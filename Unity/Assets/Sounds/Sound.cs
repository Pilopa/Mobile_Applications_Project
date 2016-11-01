using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	public UnityEngine.UI.Toggle soundToggle;
	public UnityEngine.UI.Slider soundSlider;

	public AudioClip button_click;

	private AudioSource soundSource;

	private float volume;

	void Start()
	{
		soundSource = this.GetComponent<AudioSource> ();
		volume = soundSource.volume;
	}


	public void setSFX()
	{		
		if (soundToggle.isOn)
			soundSource.enabled = true;
		else
			soundSource.enabled = false;
	}

	public void setSFXVolume()
	{
		float newVolume = soundSlider.value;
		volume = newVolume;
	}


}
