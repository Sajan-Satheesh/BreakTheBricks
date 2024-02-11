using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour 
{
	//Called when the play button is pressed
	public void PlayButton ()
	{
		SceneManager.LoadScene(1);	//Loads the 'Game' scene to begin the game
	}

	//Called when the quit button is pressed
	public void QuitButton ()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
#endif
		Application.Quit();			//Quits the game
	}
}
