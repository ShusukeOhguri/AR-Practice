using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    GameObject TitleObjects;
    GameObject PlayObjects;
    GameObject PlayUI;

    PlayerController mPlayerController = new PlayerController();

    //弾
    public GameObject Bullet;
    public float BulletInterval;

    //移動のスピード
    public float speedX;
    public float speedZ;

    void Start()
    {
        TitleObjects = GameObject.Find("Title");
        PlayObjects = GameObject.Find("GamePlay");
        PlayUI = GameObject.Find("UIPlane");
        PlayObjects.SetActive(false);
        PlayUI.SetActive(false);
        //弾のインターバル
        BulletInterval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //移動
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey("up"))
        {
            MoveToUp(vertical);
        }

        if (Input.GetKey("down"))
        {
            MoveToBack(vertical);
        }

        BulletInterval += Time.deltaTime;

        //Rayの作成
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //Rayの飛ばせる距離
        int distance = 100;

        //Rayの可視化    ↓Rayの原点　　　　↓Rayの方向　　　　　　　　　↓Rayの色
        Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Input.GetMouseButtonDown(0))
        {
            // 処理
            //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
            if (Physics.Raycast(ray, out hit, distance))
            {

                Debug.Log(hit.collider.tag);

                //Rayが当たったオブジェクトのtagがGameScreenだったら
                if (hit.collider.tag == "GameScreen")
                {
                    TitleObjects.SetActive(false);
                    PlayObjects.SetActive(true);
                    PlayUI.SetActive(true);
                }

                BulletInterval += Time.deltaTime;
                if (hit.collider.tag == "Shoot")
                {
                    if (BulletInterval >= 0.8f)
                    {
                        GameObject GamePlay = GameObject.Find("GamePlay");
                        BulletInterval = 0.0f;
                        Instantiate(Bullet, GamePlay.transform.position, Quaternion.identity);
                        GameObject PlayerBullet = (GameObject)Instantiate(Bullet, GamePlay.transform.position, Quaternion.identity);
                        PlayerBullet.transform.parent = GamePlay.transform;
                    }
                }

                if (hit.collider.tag == "Left"){
                    MoveToLeft(horizontal);
                }

                if (hit.collider.tag == "Right"){
                    MoveToRight(horizontal);
                }
            }
        }
    }

    //移動するためのメソッド
    void MoveToUp(float vertical)
    {
        GameObject GamePlay = GameObject.Find("GamePlay");
        GamePlay.transform.Translate(0, 0, vertical * speedZ);
    }

    void MoveToRight(float horizontal)
    {
        GameObject Player = GameObject.Find("Player");
        if(Player.transform.localPosition.x > -15){
            Player.transform.Translate(1 * speedX / 100, 0, 0);            
        }
    }

    void MoveToLeft(float horizontal)
    {
        GameObject Player = GameObject.Find("Player");
        if (Player.transform.localPosition.x < 15)
        {
            Player.transform.Translate(-1 * speedX / 100, 0, 0);
        }
    }

    void MoveToBack(float vertical)
    {
        GameObject GamePlay = GameObject.Find("GamePlay");
        GamePlay.transform.Translate(0, 0, vertical * speedZ);
    }
}
