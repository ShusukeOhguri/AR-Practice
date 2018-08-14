using UnityEngine;

public class EnemyController : MonoBehaviour {

	//敵の移動スピード
	float speed = 25.0f;

	//弾を撃つ間隔をあける
	float interval; 

	//弾
	public GameObject enemyBullet;

	//爆発
	public GameObject explosion;
    GameObject Effects;

	// Use this for initialization
	void Start () {
		//intervalの初期値の設定
		interval = 0;
        //敵を30秒後に削除する
        Destroy(this.gameObject,30);
        Effects = GameObject.Find("Effects");
	}

	// Update is called once per frame
	void Update () {
        GameObject GamePlay = GameObject.Find("GamePlay");

		//敵の移動
        //transform.Translate (-1 * GamePlay.transform.up * Time.deltaTime * speed);
        transform.Translate(new Vector3(0, 0, 1 * Time.deltaTime * speed));

		//弾を撃つメソッドを呼び出す
		interval += Time.deltaTime;
		if (interval >= 1f) {
			GenerateEnemyBullet();
		} 
	}

	//弾を撃つメソッド
	void GenerateEnemyBullet(){
        GameObject Enemy = GameObject.Find("Enemy(Clone)");
        //GameObject Player = GameObject.Find("Player");

        Quaternion q1 = Quaternion.Euler(0, 180, 0);
		//Quaternion q2 = Quaternion.Euler (0, 185, 0);
		//Quaternion q3 = Quaternion.Euler (0, 175, 0);
        //Quaternion q4 = Quaternion.Euler(5, 180, 0);
        //Quaternion q5 = Quaternion.Euler(-5, 180, 0);
       
		interval = 0;
        GameObject Obj = (GameObject)Instantiate (enemyBullet, new Vector3 (transform.position.x, transform.position.y, transform.position.z), q1);
        Obj.transform.parent = Enemy.transform;
        //Obj = (GameObject)Instantiate (enemyBullet, new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z), q2);
        //Obj.transform.parent = Enemy.transform;
        //Obj = (GameObject)Instantiate(enemyBullet, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), q3);
        //Obj.transform.parent = Enemy.transform;
        ////Obj = (GameObject)Instantiate(enemyBullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), q4);
        ////Obj.transform.parent = Enemy.transform;
        //Obj = (GameObject)Instantiate(enemyBullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), q5);
        //Obj.transform.parent = Enemy.transform;
    } 

	//衝突判定・爆発
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "PlayerBullet") {
            GameObject Effect = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
            Effect.transform.parent = Effects.transform;
            Destroy (this.gameObject);
            Destroy (coll.gameObject);
			//スコアの加算
            ScoreController obj = GameObject.Find ("ARCamera").GetComponent<ScoreController>();
			obj.ScorePlus ();
		}
	}
}