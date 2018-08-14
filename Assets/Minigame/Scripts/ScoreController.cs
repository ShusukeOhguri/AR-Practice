using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {
    
	public Text scoreText;
	public Text HighScore;
	int score;

    GameObject GamePlay;
    GameObject Title;
    GameObject PlayUI;

    ObjectManager ObjectManager;

	void Start() {
		score = 0;
        ObjectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        GamePlay = GetComponent<TouchController>().PlayObjects;
        Title = GetComponent<TouchController>().TitleObjects;
        PlayUI = GetComponent<TouchController>().PlayUI;
	}

	// Update is called once per frame
	void Update () {
        if (GamePlay.activeInHierarchy == true){
			scoreText.text = "Score: " + score;
		}

        if (Title.activeInHierarchy == true){
            HighScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        }
	}

	public void ScorePlus() {
		score += 10;
	}

	//ハイスコアを更新する
	public void SaveHighScore() {
		if (PlayerPrefs.GetInt ("HighScore") < score) {
			PlayerPrefs.SetInt ("HighScore", score);
		}

		//2秒後にタイトルへ
		Invoke ("ReturnTitle", 2.0f);
	}

    //タイトルに戻るメソッド
    void ReturnTitle()
    {
        score = 0;
        GamePlay.SetActive(false);
        PlayUI.SetActive(false);
        Title.SetActive(true);
        ObjectManager.ResetGame();
    }
}