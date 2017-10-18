using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_mgrScene : MonoBehaviour 
{

	public void loadNextScene()
	{
		int curScnIndex = SceneManager.GetActiveScene().buildIndex;

		SceneManager.LoadScene(curScnIndex + 1);
	}

	public void loadSpecificScene( int sceneIndex )
	{
			SceneManager.LoadScene( sceneIndex );
	}
}
