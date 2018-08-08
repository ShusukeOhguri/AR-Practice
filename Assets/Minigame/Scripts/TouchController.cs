using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
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
                //Rayが当たったオブジェクトのtagがGameScreenだったら
                if (hit.collider.tag == "GameScreen")
                {
                    Debug.Log("Click GameScreen");
                    Debug.Log(hit.collider.gameObject);

                    GameObject Marker = hit.collider.gameObject;

                    var CanvasComponents = Marker.GetComponentsInChildren<Canvas>(true);
                    foreach (var component in CanvasComponents)
                    {
                        if (component.enabled == false)
                        {
                            component.enabled = true;
                            Debug.Log("true");
                        }else if (component.enabled == true)
                        {
                            component.enabled = false;
                            Debug.Log("false");
                        }
                    }
                }
            }
        }
    }
}
