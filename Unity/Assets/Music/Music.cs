using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	void Awake() {		
		DontDestroyOnLoad (this.gameObject);
		if (FindObjectsOfType(GetType()).Length > 1)
		{
			Destroy(gameObject);
		}
	}
}
