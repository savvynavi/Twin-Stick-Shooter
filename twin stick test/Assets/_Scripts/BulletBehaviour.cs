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
	TrailRenderer trail;

	// Use this for initialization
	void Awake () {
		rigidbody = GetComponent<Rigidbody>();
		source = GetComponent<AudioSource>();
		trail = GetComponent<TrailRenderer>();
	}

	private void OnEnable() {
		GetComponent<SphereCollider>().enabled = true;
		trail.enabled = true;

		source.pitch = Random.Range(lowPitch, highPitch);
		source.PlayOneShot(source.clip);
		trail.Clear();

		//disables after a given time also
		StartCoroutine(disableBullet(aliveTime));
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(transform.gameObject.layer == 8) {
			rigidbody.MovePosition(transform.localPosition + transform.forward * Time.deltaTime * speed);
		}
	}

	IEnumerator disableBullet(float time) {
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}

	private void OnCollisionEnter(Collision collision) {
		//turns bullet off/resets them when it hits something
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		//turns off mesh/collider so that the sound can keep playing before the bullet is turned off
		GetComponent<SphereCollider>().enabled = false;
		trail.enabled = false;

		StartCoroutine(disableBullet(0.1f));

		//if it hits an enemy it ticks off 1 damage
		if(collision.transform.gameObject.layer == 10 && collision.gameObject.GetComponent<EnemyBehaviour>() != null) {
			collision.gameObject.GetComponent<Animator>().Play("Unarmed-GetHit-F2");
			collision.gameObject.GetComponent<EnemyBehaviour>().HP--;
		}
	}
}
