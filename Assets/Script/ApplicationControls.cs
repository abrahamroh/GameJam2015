using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ApplicationControls : MonoBehaviour {
	private bool pausedGame;
	private bool gameOver;

	public static ApplicationControls instance;

	public GameObject gameOverImage;
	public GameObject gameOverText;
	public GameObject pauseText;

	// Use this for initialization
	void Start () {
		UnityEngine.Cursor.visible = false;
		pausedGame = false;
		gameOver = false;
		Time.timeScale = 1;
	}

	void Awake() {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Exit")){
			Application.Quit();
			UnityEngine.Cursor.visible = true;
		}

		if(Input.GetButtonDown("Start")){
			if(pausedGame){
				ResumeGame();
			}
			else{
				PauseGame();
			}
		}

		/*if(Input.GetButtonDown("Restart")){
			UnityEngine.Cursor.visible = false;
			Application.LoadLevel(Application.loadedLevel);
		}*/
	}

	void PauseGame(){
		Time.timeScale = 0;
		pausedGame = true;
		UnityEngine.Cursor.visible = true;
		pauseText.SetActive(true);
	}

	void ResumeGame(){
		if(!gameOver){
			UnityEngine.Cursor.visible = false;
			pauseText.SetActive(false);
			Time.timeScale = 1;
			pausedGame = false;
		}
	}

	public static bool isGamePaused(){
		return instance.pausedGame;
	}

	public static void GameOver(int winner){
		instance.gameOver = true;
		instance.PauseGame();

		instance.gameOverImage.SetActive(true);
		instance.gameOverText.GetComponent<Text>().text = "Player " + winner + " Wins!";
		if(winner == 1){
			instance.gameOverText.GetComponent<Text>().color = new Color(1, 0, 0);
		}
		else{
			instance.gameOverText.GetComponent<Text>().color = new Color(0, 0, 1);
		}
	}
}
