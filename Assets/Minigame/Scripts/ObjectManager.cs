using UnityEngine;

public class ObjectManager : MonoBehaviour {
    
    public PlayerController mPlayerController;

    public GameObject TitleObjects;
    public GameObject PlayObjects;
    public GameObject PlayUI;

    //プレイヤー
    public GameObject Player;

	// Use this for initialization
	void Start () {
        mPlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        TitleObjects = GameObject.Find("Title");
        PlayObjects = GameObject.Find("GamePlay");
        Player = GameObject.Find("Player");
        PlayUI = GameObject.Find("UIPlane");
        PlayObjects.SetActive(false);
        PlayUI.SetActive(false);
	}

    public void ResetGame(){
        //体力の設定
        mPlayerController.playerLife = 10;
        mPlayerController.slider.value = mPlayerController.playerLife;

        Destroy(GameObject.Find("Enemy(clone)"));
        Destroy(GameObject.Find("EnemyBullet(clone)"));
        Destroy(GameObject.Find("PlayerBullet"));
        Destroy(GameObject.Find("Explosion01b(Clone)"));

        //GameObject Obj = (GameObject)Instantiate(Player, new Vector3(PlayObjects.transform.position.x, PlayObjects.transform.position.y - (1 / 100), PlayObjects.transform.position.z), Quaternion.identity);
        //Obj.transform.parent = PlayObjects.transform;
    }

    public void GameStart()
    { 
        TitleObjects.SetActive(false);
        PlayObjects.SetActive(true);
        PlayUI.SetActive(true);
        Player.SetActive(true);
    }
}
