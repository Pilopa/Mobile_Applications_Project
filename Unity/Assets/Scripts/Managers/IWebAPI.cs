using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The main interface to marblemaze's backend database server. 
/// This is purely the design and implementation may vary.
/// </summary>
/// <author>Konstantin Schaper</author>
public interface IWebAPI {

	/// <summary>
	/// Attempt to register with the given username and password.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="password">Password.</param>
	/// <returns>The HTTP response code or -1 if an internal error occurred.</returns>
	int Register(string username, string password);

	/// <summary>
	/// Attempts to authenticate the specified username and password with
	/// the server associated with this WebAPI instance.
	/// </summary>
	/// <param name="username">Username.</param>
	/// <param name="password">Password.</param>
	/// <returns>The whether the login was successful.</returns>
	bool Login(string username, string password);

	/// <summary>
	/// Attempts to log off the server associated with this WebAPI instance.
	/// Only works when logged in.
	/// </summary>
	/// <returns>The whether the logout was successful.</returns>
	bool Logout();

	/// <summary>
	/// Attempts to retrieve the current highscore value for a given level from the server associated
	/// with this WebAPI instance. Only works when logged in.
	/// </summary>
	/// <returns>The positive highscore value or a negative value representing the HTTP error code.</returns>
	int GetHighscore(int levelIndex);

	/// <summary>
	/// Gets a list of highscores ordered by value for the given level.
	/// </summary>
	/// <returns>The ordered list of highscores for that level or null, if an error occurred.</returns>
	/// <param name="levelIndex">Level index.</param>
	/// <param name="maxCount">The maximum number of records to retrieve.</param>
	List<Dictionary<string, string>> GetRanking(int levelIndex, int maxCount);

	/// <summary>
	/// Attempts to post the given highscore for the given level to the server associated with this
	/// WebAPI instance. Only works when logged in.
	/// </summary>
	/// <returns>The HTTP response code or -1 if an internal error occurred.</returns>
	int PostHighscore(int highscore, int levelIndex);
}