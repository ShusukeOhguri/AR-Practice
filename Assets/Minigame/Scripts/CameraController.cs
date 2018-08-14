using UnityEngine;

public class CameraController : MonoBehaviour {

	Vector3 diff;
	public GameObject player;
	public float speed;

	// Use this for initialization
	void Start () {
		diff = player.transform.position - transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (player) {
			transform.position = Vector3.Lerp (
				transform.position,
				player.transform.position - diff,
				Time.deltaTime * speed
			);
		}
	}
}