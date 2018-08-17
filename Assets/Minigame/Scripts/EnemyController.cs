using UnityEngine;

public class EnemyController : MonoBehaviour {
    GameObject Enemy;
    GameObject GamePlay;

	//敵の移動スピード
	float speed = 0.1f;

	//弾を撃つ間隔をあける
	float interval; 

	//弾
	public GameObject enemyBullet;
    Quaternion Bulletdirection;

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
        Enemy = GameObject.Find("Enemy(Clone)");
        GamePlay = GameObject.Find("GamePlay");
	}
    
	// Update is called once per frame
	void Update () {
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
        Enemy = GameObject.Find("Enemy(Clone)");
        interval = 0;
        Quaternion q1 = Quaternion.Euler(0, 0, 0);
        Quaternion q2 = Quaternion.Euler (0, 20, 0);
        Quaternion q3 = Quaternion.Euler (0, -20, 0);
        //Quaternion q4 = Quaternion.Euler(5, 180, 0);
        //Quaternion q5 = Quaternion.Euler(-5, 180, 0);

        int ShootingPatarn = Random.Range(0, 3);
        Debug.Log(ShootingPatarn);
        if (ShootingPatarn == 0)
        {
            Bulletdirection = q1;
            BulletShoot(Bulletdirection);
        }
        else if (ShootingPatarn == 1)
        {
            Bulletdirection = q2;
            BulletShoot(Bulletdirection);
        }else if(ShootingPatarn == 2)
        {
            Bulletdirection = q3;
            BulletShoot(Bulletdirection);
        }
        //Quaternion BulletRotation = Enemy.transform.rotation;

    } 

    void BulletShoot(Quaternion direction){
        GameObject Obj = Instantiate(enemyBullet, transform.position, Enemy.transform.rotation);
        Obj.transform.parent = Enemy.transform;
        Obj.transform.localRotation = direction * Obj.transform.localRotation;
        Obj.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
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