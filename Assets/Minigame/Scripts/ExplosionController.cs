using UnityEngine;

public class ExplosionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject,3);
	}
	
	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject,3);
	}
}
