using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Sound : MonoBehaviour {
	
	public AudioClip button_click;

	void Awake() {		
		DontDestroyOnLoad (this.gameObject);
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}

}
