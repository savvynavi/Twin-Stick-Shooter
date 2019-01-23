using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public float Speed = 6;
	public float rotationSpeed;
	public Transform bullet;
	public float bulletTimer;
	public Thumbstick LeftStick;
	public Thumbstick RightStick;

	CharacterController charaController;
	Animator animator = null;
	Vector3 moveDir = Vector3.zero;
	Quaternion NewRotation = Quaternion.identity;
	float lastShot = 0;

	// Use this for initialization
	void Start () {
		charaController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animator.SetBool("isIdle", true);
	}
	
	// Update is called once per frame
	void Update () {
		//THUMBSTICK CODE
		float leftVertical = LeftStick.xAxis;
		float leftHorizontal = LeftStick.yAxis;

		float rightVertical = RightStick.xAxis;
		float rightHorizontal = RightStick.yAxis;

		//if any left stick movement, the player is no longer idle and will do walk anim
		if(leftVertical != 0 || leftHorizontal != 0) {
			animator.SetBool("isIdle", false);
		} else {
			animator.SetBool("isIdle", true);
		}

		//check if right stick is moving, if so rotate towards this, if not rotate towards left stick
		Vector3 lookDir = new Vector3(rightVertical, 0.0f, rightHorizontal);
		moveDir = new Vector3(leftVertical, 0.0f, leftHorizontal);

		if(lookDir.x != 0 || lookDir.z != 0) {
			NewRotation = Quaternion.LookRotation(lookDir, Vector3.up);
			animator.SetBool("isShooting", true);
			Shooting();
		} else if (moveDir.x != 0 || moveDir.z != 0){
			NewRotation = Quaternion.LookRotation(moveDir, Vector3.up);
			
		}

		//if no right stick movement, shooting anim stops
		if(lookDir.x == 0 || lookDir.z == 0) {
			animator.SetBool("isShooting", false);
		}

		transform.rotation = NewRotation;

		moveDir *= Speed;
	}

	void FixedUpdate() {
		charaController.Move(moveDir * Time.deltaTime);
	}

	public void Shooting() {
		if(Time.time - lastShot > bulletTimer) {
			lastShot = Time.time;
			Instantiate(bullet, transform.position + (transform.up * ((charaController.height / 4) * 3)) + (transform.forward * (charaController.radius + 1)), transform.rotation);
		}
	}
}
