using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    ObjectManager ObjectManager;
    ScoreController ScoreController;

    GameObject GamePlay;
    GameObject Player;
    GameObject Enemies;
    GameObject Effects;

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
        BulletInterval = 0;
        //敵のインターバル
        enemyInterval = 0;
        //ハイスコア更新
        ScoreController = GameObject.Find("ARCamera").GetComponent<ScoreController>();
        //スライダーコンポーネントを取得
        slider = GameObject.Find("Slider").GetComponent<Slider> ();
        //スライダーの最大値をplayerLifeに合わせる
        //slider.maxValue = playerLife;

        GamePlay = GameObject.Find("GamePlay");
        Player = GameObject.Find("Player");
        Enemies = GameObject.Find("Enemies");
        Effects = GameObject.Find("Effects");
	}

    private void OnEnable()
    {
        //スライダーの最大値をplayerLifeに合わせる
        maxValue = 10;
        //体力の設定
        playerLife = maxValue;
        ////弾のインターバル
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
        GameObject Enemy = (GameObject)Instantiate(enemy, new Vector3(Random.Range(-100, 100), Enemies.transform.position.y, Enemies.transform.position.z + 200),q);
        Enemy.transform.parent = Enemies.transform;
        //自身の目の前に生成
        Enemy = (GameObject)Instantiate(enemy, new Vector3(Enemies.transform.position.x, Enemies.transform.position.y, Enemies.transform.position.z + 200),q);
        Enemy.transform.parent = Enemies.transform;
    }

	//爆発
	void OnTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "EnemyBullet") {
			//弾が当たれば体力を1減らす
			playerLife--;
			//sliderのvalueに、体力を代入する
			slider.value = playerLife;
            GameObject Effect = (GameObject)Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            Effect.transform.parent = Effects.transform;

			//体力が0以下になれば、戦闘機が消えるようにする
			if (playerLife <= 0) {
                Player.SetActive(false);
                ScoreController.SaveHighScore();
			}
		}
	}
}