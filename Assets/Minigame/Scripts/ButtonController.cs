using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 

public class ButtonController : MonoBehaviour {

	public void GameStart() {
		SceneManager.LoadScene ("main");
	}

}