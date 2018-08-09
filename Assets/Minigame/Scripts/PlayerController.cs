using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	//移動のスピード
	public float speedX;
	public float speedZ;

	//弾
	public GameObject Bullet;
	float BulletInterval;

	//敵
	public GameObject enemy;
	float enemyInterval;

	//爆発
	public GameObject explosion;

	//Sliderと体力
	Slider slider;
	int playerLife;

	// Use this for initialization
	void Start () {
		//弾のインターバル
		BulletInterval = 0;
		//敵のインターバル
		enemyInterval = 0;
		//体力の設定
		playerLife = 30;
		//スライダーコンポーネントを取得
		slider = GameObject.Find ("Slider").GetComponent<Slider> ();
	}

	// Update is called once per frame
	void Update () {

		//移動
		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		if (Input.GetKey ("up")) {
			MoveToUp (vertical);
		}
		if (Input.GetKey ("right")) {
			MoveToLeft (horizontal);
		}
		if (Input.GetKey ("left")) {
			MoveToLeft (horizontal);
		}
		if (Input.GetKey ("down")) {
			MoveToBack (vertical); 
		}

		//弾の生成
		BulletInterval += Time.deltaTime;
		if (Input.GetKey ("space")) {
			if (BulletInterval >= 0.8f) {
				GenerateBullet ();
			}
		}

		//敵の生成
		enemyInterval += Time.deltaTime;
		if (enemyInterval >= 5.0f) {
			GenerateEnemy ();
		}
       
	}

	//移動するためのメソッド
	void MoveToUp(float vertical){
        GameObject GamePlay = GameObject.Find("GamePlay");
        GamePlay.transform.Translate(0, 0, vertical * speedZ);
	}

	void MoveToRight(float horizontal){
        GameObject GamePlay = GameObject.Find("GamePlay");
        GamePlay.transform.Translate(horizontal * speedX, 0, 0);
	}

	void MoveToLeft(float horizontal){
        GameObject GamePlay = GameObject.Find("GamePlay");
        GamePlay.transform.Translate(horizontal * speedX, 0, 0);
	}

	void MoveToBack(float vertical){
        GameObject GamePlay = GameObject.Find("GamePlay");
        GamePlay.transform.Translate(0, 0, vertical * speedZ);
	} 

	//弾を生成するためのメソッド
	void GenerateBullet(){
        GameObject GamePlay = GameObject.Find("GamePlay");
		BulletInterval = 0.0f;
        Instantiate (Bullet, GamePlay.transform.position, Quaternion.identity);
	}

	//敵を生成するためのメソッド
	void GenerateEnemy(){
        GameObject GamePlay = GameObject.Find("GamePlay");
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
			Destroy (coll.gameObject);
			//体力が0以下になれば、戦闘機が爆発するようにする
			if (playerLife <= 0) {
				Destroy (this.gameObject);

				//ハイスコア更新
				ScoreController obj = GameObject.Find ("Main Camera").GetComponent<ScoreController> ();
				obj.SaveHighScore ();

			}
		}
	}
}