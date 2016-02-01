using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

	public GameObject loadingImage;

	public void LoadGame()
	{
		loadingImage.SetActive(true);
		Application.LoadLevel(1);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
