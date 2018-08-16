using UnityEngine;

public class EnemyController : MonoBehaviour {

	//敵の移動スピード
	float speed = 0.1f;

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
        transform.Translate(0, 0, 1 * Time.deltaTime * speed);

        if (this.gameObject.transform.localPosition.z < -10){
            Destroy(this.gameObject);
        }

		//弾を撃つメソッドを呼び出す
		interval += Time.deltaTime;
		if (interval >= 1f) {
			GenerateEnemyBullet();
		} 
	}

	//弾を撃つメソッド
	void GenerateEnemyBullet(){
        GameObject Enemy = GameObject.Find("Enemy(Clone)");

        //Quaternion q1 = Quaternion.Euler(0, 180, 0);
		//Quaternion q2 = Quaternion.Euler (0, 185, 0);
		//Quaternion q3 = Quaternion.Euler (0, 175, 0);
        //Quaternion q4 = Quaternion.Euler(5, 180, 0);
        //Quaternion q5 = Quaternion.Euler(-5, 180, 0);

        Quaternion BulletRotation = Enemy.transform.rotation;
       
		interval = 0;
        GameObject Obj = Instantiate (enemyBullet, new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z), BulletRotation);
        Obj.transform.parent = Enemy.transform;
        Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        //Obj = (GameObject)Instantiate (enemyBullet, new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z), q2);
        //Obj.transform.parent = Enemy.transform;
        //Obj = (GameObject)Instantiate(enemyBullet, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), q3);
        //Obj.transform.parent = Enemy.transform;
        //Obj = (GameObject)Instantiate(enemyBullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), q4);
        //Obj.transform.parent = Enemy.transform;
        //Obj = (GameObject)Instantiate(enemyBullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), q5);
        //Obj.transform.parent = Enemy.transform;
    } 

	//衝突判定・爆発
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "PlayerBullet") {
            GameObject Effect = (GameObject)Instantiate (explosion, transform.position, Quaternion.identity);
            Effect.transform.parent = Effects.transform;
            Effect.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(this.gameObject);
            Destroy(coll.gameObject);
			//スコアの加算
            ScoreController obj = GameObject.Find ("ARCamera").GetComponent<ScoreController>();
			obj.ScorePlus ();
		}
	}
}