using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class Ad_Manager : MonoBehaviour {


	public void ShowAd()
	{
		if (Advertisement.IsReady())
			Advertisement.Show();
	}
}
