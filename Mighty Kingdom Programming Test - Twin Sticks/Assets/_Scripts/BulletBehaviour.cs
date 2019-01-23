using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	public float speed;
	public float aliveTime;
	public float damage;

	Vector3 moveDir = Vector3.zero;
	Rigidbody rigidbody;
	float timer;


	// Use this for initialization
	void Awake () {
		rigidbody = GetComponent<Rigidbody>();
		timer = Time.time + aliveTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.gameObject.layer == 8) {
			rigidbody.MovePosition(transform.localPosition + transform.forward * Time.deltaTime * speed);
		}
		Destroy(transform.gameObject, aliveTime);
	}

	private void OnCollisionEnter(Collision collision) {
		//if it hits something before alive time is up, it despawns
		GetComponent<SphereCollider>().enabled = false;
		GetComponent<MeshRenderer>().enabled = false;
		Destroy(transform.gameObject, 0.1f);

		if(collision.transform.gameObject.layer == 10) {

		}
	}
}
