using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.Networking.Match;
using System;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// Implementation of the IWebAPI interface.
/// Utilizes Json.Net to deserialize server data.
/// </summary>
/// <author>Konstantin Schaper</author>
/// <seealso cref="http://www.netonsoft.com/json" />
public class WebAPIManager : MonoBehaviour, IWebAPI {

	#region Inner Classes

	[Serializable]
	public class AuthTokenWrapper
	{
		public string authToken = null;
		public string bearer = null;
		public string token = null;
	}

	#endregion

	#region Inpsector Values

	[SerializeField]
	private string serverBaseURL = "http://localhost:81";

	#endregion

	private AuthTokenWrapper authData = null;

    /// <summary>
    /// Gets the singleton instance of this manager.
    /// </summary>
    /// <value>The singleton instance.</value>
    public static WebAPIManager Instance
    {
        get;
        private set;        
    }

    //private static WebAPIManager instance;
    //public static WebAPIManager GetInstance()
    //{
    //    if (instance == null)
    //        instance = new WebAPIManager();
    //    return instance;
    //}

    private WebAPIManager() { }

	/// <summary>
	/// Sets the instance property on game start.
	/// </summary>
	public void Awake() {
		if (Instance == null) {
            Instance = this;
		}

		// The web api instance is fine to be the same
		// in all scenes.
		DontDestroyOnLoad (Instance);
	}

	#region IWebAPI implementation

	public int Register(string username, string password) {
		try
		{
			// Create the data
			string postData = "{" +
				"\"username\" : \"" + username +
				"\", \"password\" : \"" + password +
				"\"}";
			byte[] byteArray = Encoding.UTF8.GetBytes (postData);

			// Create the request
			HttpWebRequest request = WebRequest.Create(String.Format("{0}/register", serverBaseURL)) as HttpWebRequest;

			// Define request parameters
			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;

			// Get the request stream.
			Stream dataStream = request.GetRequestStream ();

			// Write the data to the request stream.
			dataStream.Write (byteArray, 0, byteArray.Length);

			// Close the Stream object.
			dataStream.Close ();

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				return (int) response.StatusCode;
			}
		}
		catch (WebException e) {
			using (var stream = e.Response.GetResponseStream ())
			using (var reader = new StreamReader (stream)) {
				Debug.LogError (reader.ReadToEnd ());
			}
			return -1;
		}
		catch (Exception e)
		{
			Debug.LogError (e.Source  + ": " + e.Message);
			return -1;
		}
	}

	public bool Login (string username, string password)
	{
		if (authData != null)
			return true;

		try
		{
			// Create the data
			string postData = "{" +
				"\"username\" : \"" + username +
				"\", \"password\" : \"" + password +
			"\"}";
			byte[] byteArray = Encoding.UTF8.GetBytes (postData);

			// Create the request
			HttpWebRequest request = WebRequest.Create(String.Format("{0}/login", serverBaseURL)) as HttpWebRequest;

			// Define request parameters
			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;

			// Get the request stream.
			Stream dataStream = request.GetRequestStream ();

			// Write the data to the request stream.
			dataStream.Write (byteArray, 0, byteArray.Length);

			// Close the Stream object.
			dataStream.Close ();

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if ((int) response.StatusCode == 200) {
					using (var streamReader = new StreamReader(response.GetResponseStream())) {
						var responseBody = streamReader.ReadToEnd();
						authData = JsonUtility.FromJson<AuthTokenWrapper>(responseBody);

						// Extract bearer and token from auth data for later use
						string[] split = authData.authToken.Split(':');
						authData.bearer = split[0];
						authData.token = split[1];

						// Base64 encode auth token for easier use
						authData.authToken = Base64Encode(authData.authToken);
					}

					return true;
				} else return false;
			}
		}
		catch (WebException e) {
			using (var stream = e.Response.GetResponseStream ())
			using (var reader = new StreamReader (stream)) {
				Debug.LogError (reader.ReadToEnd ());
			}
			return false;
		}
		catch (Exception e)
		{
			Debug.LogError (e.Source  + ": " + e.Message);
			return false;
		}
	}

	public bool Logout ()
	{
		if (authData == null)
			return true;

		try
		{
			// Create the request
			HttpWebRequest request = WebRequest.Create(String.Format("{0}/logout", serverBaseURL)) as HttpWebRequest;

			// Define request parameters
			request.Method = "POST";
			request.ContentType = "application/json";
			request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authData.authToken);

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if (response.StatusCode == HttpStatusCode.OK) authData = null;
				return true;
			}
		}
		catch (WebException e) {
			using (var stream = e.Response.GetResponseStream ())
			using (var reader = new StreamReader (stream)) {
				Debug.LogError (reader.ReadToEnd ());
			}
			return false;
		}
		catch (Exception e)
		{
			Debug.LogError (e.Message);
			return false;
		}
	}

	public int GetHighscore (int levelIndex)
	{
		if (authData == null)
			return -401;
		
		try
		{
			// Create the request
			HttpWebRequest request = WebRequest.Create(String.Format("{0}/highscore/{1}/level/{2}", serverBaseURL, authData.bearer, levelIndex)) as HttpWebRequest;

			// Define request parameters
			request.Method = "GET";
			request.ContentType = "application/json";
			request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authData.authToken);

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if ((int) response.StatusCode == 200) {
					using (var streamReader = new StreamReader(response.GetResponseStream())) {
						var responseBody = streamReader.ReadToEnd();
						var highscoreData = JsonConvert.DeserializeObject<Dictionary<string, int>>(responseBody);
						return highscoreData[Convert.ToString(levelIndex)];
					}
				} else return -(int)response.StatusCode;
			}
		}
		catch (WebException e) {
			using (var stream = e.Response.GetResponseStream ())
			using (var reader = new StreamReader (stream)) {
				Debug.LogError (reader.ReadToEnd ());
			}
			return -1;
		}
		catch (Exception e)
		{
			Debug.LogError (e.Message);
			return -1;
		}
	}

	public int PostHighscore (int highscore, int levelIndex)
	{
		if (authData == null)
			return 401;

		try
		{
			// Create the data
			string postData = "{" +
				"\"value\" : " +  highscore +
				"}";
			byte[] byteArray = Encoding.UTF8.GetBytes (postData);

			// Create the request
			HttpWebRequest request = WebRequest.Create(String.Format("{0}/highscore/{1}/level/{2}", serverBaseURL, authData.bearer, levelIndex)) as HttpWebRequest;

			// Define request parameters
			request.Method = "POST";
			request.ContentType = "application/json";
			request.ContentLength = byteArray.Length;
			request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authData.authToken);

			// Get the request stream.
			Stream dataStream = request.GetRequestStream ();

			// Write the data to the request stream.
			dataStream.Write (byteArray, 0, byteArray.Length);

			// Close the Stream object.
			dataStream.Close ();

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				return (int) response.StatusCode;
			}
		}
		catch (WebException e) {
			using (var stream = e.Response.GetResponseStream ())
			using (var reader = new StreamReader (stream)) {
				Debug.LogError (reader.ReadToEnd ());
			}
			return -1;
		}
		catch (Exception e)
		{
			Debug.LogError (e.Message);
			return -1;
		}
	}

	public List<Dictionary<string, string>> GetRanking(int levelIndex, int maxCount) {
		try
		{
			// Create the request
			HttpWebRequest request = WebRequest.Create(String.Format("{0}/ranking/{1}" + (maxCount > 0 ? "?limit=" + maxCount : ""), serverBaseURL, levelIndex)) as HttpWebRequest;

			// Define request parameters
			request.Method = "GET";
			request.ContentType = "application/json";
			request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authData.authToken);

			// Get response
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				if ((int) response.StatusCode == 200) {
					using (var streamReader = new StreamReader(response.GetResponseStream())) {
						var responseBody = streamReader.ReadToEnd();
						return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(responseBody);
					}
				} else return null;
			}
		}
		catch (WebException e) {
			using (var stream = e.Response.GetResponseStream ())
			using (var reader = new StreamReader (stream)) {
				Debug.LogError (reader.ReadToEnd ());
			}
			return null;
		}
		catch (Exception e)
		{
			Debug.LogError (e.Message);
			return null;
		}
	}

	#endregion

	#region Methods

	public static string Base64Encode(string plainText) {
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
		return System.Convert.ToBase64String(plainTextBytes);
	}

	public static string Base64Decode(string base64EncodedData) {
		var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}

	#endregion

}
