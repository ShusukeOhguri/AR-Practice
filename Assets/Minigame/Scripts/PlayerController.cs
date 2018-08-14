using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    ObjectManager ObjectManager;
    ScoreController ScoreController;

    GameObject GamePlay;
    GameObject Player;

	//移動のスピード
	public float speedX;
	public float speedZ;

	////弾
	public GameObject Bullet;
	public float BulletInterval;

	//敵
	public GameObject enemy;
	float enemyInterval;

	//爆発
	public GameObject explosion;

	//Sliderと体力
	public Slider slider;
	public int playerLife;
    public int maxValue;

	// Use this for initialization
	void Start () {
        //スライダーの最大値をplayerLifeに合わせる
        maxValue = 10;
        //体力の設定
        playerLife = maxValue;
        ////弾のインターバル
        BulletInterval = 0;
        //敵のインターバル
        enemyInterval = 0;
        //ハイスコア更新
        ScoreController = GameObject.Find("ARCamera").GetComponent<ScoreController>();
        //スライダーコンポーネントを取得
        slider = GameObject.Find("Slider").GetComponent<Slider> ();
        //スライダーの最大値をplayerLifeに合わせる
        slider.maxValue = playerLife;

        GamePlay = GameObject.Find("GamePlay");
        Player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update () {
		//敵の生成
		enemyInterval += Time.deltaTime;
		if (enemyInterval >= 3f) {
			GenerateEnemy ();
		}
	}

	//敵を生成するためのメソッド
	void GenerateEnemy(){
		Quaternion q = Quaternion.Euler(0, 180, 0);
		enemyInterval = 0.0f;
		//ランダムな場所に生成
        GameObject Obj = (GameObject)Instantiate(enemy, new Vector3(Random.Range(-100, 100), GamePlay.transform.position.y, GamePlay.transform.position.z + 200),q);
        Obj.transform.parent = GamePlay.transform;
        //自身の目の前に生成
        Obj = (GameObject)Instantiate(enemy, new Vector3(GamePlay.transform.position.x, GamePlay.transform.position.y, GamePlay.transform.position.z + 200),q);
	    Obj.transform.parent = GamePlay.transform;
    }

	//爆発
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "EnemyBullet") {
			//弾が当たれば体力を1減らす
			playerLife--;
			//sliderのvalueに、体力を代入する
			slider.value = playerLife;
			Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
			
			//体力が0以下になれば、戦闘機が消えるようにする
			if (playerLife <= 0) {
                Player.SetActive(false);
                ScoreController.SaveHighScore();
			}
		}
	}
}