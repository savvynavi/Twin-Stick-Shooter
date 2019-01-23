using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	public float speed;
	public float aliveTime;
	public float damage;

	AudioSource source;
	float lowPitch = 0.75f;
	float highPitch = 1.5f;
	Vector3 moveDir = Vector3.zero;
	Rigidbody rigidbody;


	// Use this for initialization
	void Awake () {
		rigidbody = GetComponent<Rigidbody>();
		source = GetComponent<AudioSource>();
		source.pitch = Random.Range(lowPitch, highPitch);
		source.PlayOneShot(source.clip);

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
		Debug.Log("Hit Wall");
		Destroy(transform.gameObject, 0.1f);
		if(collision.transform.gameObject.layer == 10 && collision.gameObject.GetComponent<EnemyBehaviour>() != null) {
			collision.gameObject.GetComponent<Animator>().Play("Unarmed-GetHit-F2");
			collision.gameObject.GetComponent<EnemyBehaviour>().HP--;
		}
	}
}
