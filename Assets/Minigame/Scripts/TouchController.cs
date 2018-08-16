﻿using UnityEngine;

public class TouchController : MonoBehaviour
{
    public GameObject TitleObjects;
    public GameObject PlayObjects;
    public GameObject PlayUI;
    GameObject Player;
    GameObject GamePlay;

    ObjectManager ObjectManager;

    Ray ray;

    //弾
    public GameObject Bullet;
    public float BulletInterval;

    //移動のスピード
    public float speedX;
    public float speedZ;

    void Start()
    {
        //弾のインターバル
        BulletInterval = 0;

        ObjectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManager>();
        GamePlay = GameObject.Find("GamePlay");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        BulletInterval += Time.deltaTime;

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        int distance = 100;

        #if UNITY_EDITOR
        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Input.GetMouseButton(0))
        {
            //Rayの作成
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
            Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);
            // 処理
            //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
            if (Physics.Raycast(ray, out hit, distance))
            {
                Debug.Log(hit.collider.tag);
                //Rayが当たったオブジェクトのtagがGameScreenだったら
                if (hit.collider.tag == "GameScreen")
                {
                    ObjectManager.GameStart();
                }

                BulletInterval += Time.deltaTime;

                //Rayが当たったオブジェクトのtagがShootだったら
                if (hit.collider.tag == "Shoot")
                {
                    if (BulletInterval >= 1.5f)
                    {
                        BulletShoot();
                    }
                }

                //Rayが当たったオブジェクトのtagがLeftだったら
                if (hit.collider.tag == "Left")
                {
                    MoveToLeft(horizontal);
                }

                //Rayが当たったオブジェクトのtagがRightだったら
                if (hit.collider.tag == "Right")
                {
                    MoveToRight(horizontal);
                }
            }
        }
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            Touch[] myTouches = Input.touches;
            for (int i = 0; i < myTouches.Length; i++)
            {
                //Rayの作成
                ray = Camera.main.ScreenPointToRay(myTouches[i].position);
                //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
                Debug.DrawLine(ray.origin, ray.direction * distance, Color.yellow);

                if (Physics.Raycast(ray, out hit, distance))
                {
                    Debug.Log(hit.collider.tag);
                    //Rayが当たったオブジェクトのtagがGameScreenだったら
                    if (hit.collider.tag == "GameScreen")
                    {
                        ObjectManager.GameStart();
                    }

                    BulletInterval += Time.deltaTime;

                    //Rayが当たったオブジェクトのtagがShootだったら
                    if (hit.collider.tag == "Shoot")
                    {
                        if (BulletInterval >= 1.5f)
                        {
                            BulletShoot();
                        }
                    }

                    //Rayが当たったオブジェクトのtagがLeftだったら
                    if (hit.collider.tag == "Left")
                    {
                        MoveToLeft(horizontal);
                    }

                    ////Rayが当たったオブジェクトのtagがRightだったら
                    if (hit.collider.tag == "Right")
                    {
                        MoveToRight(horizontal);
                    }
                }
            }
        }
        #endif
    }

   void BulletShoot(){
        if (BulletInterval >= 0.8f)
        {
            BulletInterval = 0.0f;
            Quaternion PlayerRotation = Player.transform.rotation;
            GameObject PlayerBullet = (GameObject)Instantiate(Bullet, Player.transform.position, PlayerRotation);
            PlayerBullet.transform.parent = GamePlay.transform;
            PlayerBullet.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

    //右移動するためのメソッド
    void MoveToRight(float horizontal)
    {
        if(Player.transform.localPosition.x > -35){
            Player.transform.Translate(1 * speedX / 100, 0, 0);            
        }
    }

    //左移動するためのメソッド
    void MoveToLeft(float horizontal)
    {
        if (Player.transform.localPosition.x < 35)
        {
            Player.transform.Translate(-1 * speedX / 100, 0, 0);
        }
    }
}
