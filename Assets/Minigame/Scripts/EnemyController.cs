using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	//敵の移動スピード
	float speed = 25.0f;

	//弾を撃つ間隔をあける
	float interval; 

	//弾
	public GameObject enemyBullet;

	//爆発
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		//intervalの初期値の設定
		interval = 0;
	}

	// Update is called once per frame
	void Update () {
        GameObject GamePlay = GameObject.Find("GamePlay");

		//敵の移動
        //transform.Translate (-1 * GamePlay.transform.up * Time.deltaTime * speed);
        transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime * speed));

		//弾を撃つメソッドを呼び出す
		interval += Time.deltaTime;
		if (interval >= 2f) {
			GenerateEnemyBullet();
		} 
	}

	//弾を撃つメソッド
	void GenerateEnemyBullet(){
        GameObject Enemy = GameObject.Find("Enemy(Clone)");
		Quaternion q1 = Quaternion.Euler (0, 185, 0);
		Quaternion q2 = Quaternion.Euler (0, 175, 0);
		interval = 0;
        GameObject Obj = (GameObject)Instantiate (enemyBullet, new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z), q1);
        Obj.transform.parent = Enemy.transform;
        Obj = (GameObject)Instantiate (enemyBullet, new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z), q2);
        Obj.transform.parent = Enemy.transform;
    } 

	//衝突判定・爆発
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "PlayerBullet") {
			Instantiate (explosion, transform.position, Quaternion.identity);
			Destroy (this.gameObject);
			Destroy (coll.gameObject);
			//スコアの加算
			ScoreController obj = GameObject.Find ("Main Camera").GetComponent<ScoreController>();
			obj.ScorePlus ();
		}
	}
}