using UnityEngine;

public class ObjectManager : MonoBehaviour {
    
    public PlayerController mPlayerController;

    public GameObject TitleObjects;
    public GameObject PlayObjects;
    public GameObject PlayUI;

    //プレイヤー
    public GameObject Player;

    GameObject Enemies;
    GameObject Effects;

	// Use this for initialization
	void Start () {
        mPlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        TitleObjects = GameObject.Find("Title");
        PlayObjects = GameObject.Find("GamePlay");
        Player = GameObject.Find("Player");
        Enemies = GameObject.Find("Enemies");
        Effects = GameObject.Find("Effects");
        PlayUI = GameObject.Find("UIPlane");
        PlayObjects.SetActive(false);
        PlayUI.SetActive(false);
	}

    public void ResetGame(){
        //体力の再設定
        mPlayerController.playerLife = 10;
        mPlayerController.slider.value = mPlayerController.playerLife;
       
        foreach (Transform child in Enemies.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in Effects.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void GameStart()
    { 
        TitleObjects.SetActive(false);
        PlayObjects.SetActive(true);
        PlayUI.SetActive(true);
        Player.SetActive(true);
    }
}
