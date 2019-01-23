using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	public float Speed = 6;
	public float rotationSpeed;
	public Thumbstick LeftStick;
	public Thumbstick RightStick;

	CharacterController charaController;
	Animator animator = null;
	Vector3 moveDir = Vector3.zero;
	Quaternion NewRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
		charaController = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animator.SetBool("isIdle", true);
	}
	
	// Update is called once per frame
	void Update () {
		//float vertical = Input.GetAxis("Vertical");
		//float horizontal = Input.GetAxis("Horizontal");

		//if(vertical != 0 || horizontal != 0) {
		//	animator.SetBool("isIdle", false);
		//} else {
		//	animator.SetBool("isIdle", true);
		//}


		//////ORIGINAL SOLUTION!
		//transform.Rotate(Vector3.up * horizontal * rotationSpeed);

		//moveDir = new Vector3(0.0f, 0.0f, vertical);
		//moveDir *= Speed;
		//moveDir = transform.TransformDirection(moveDir);


		//charaController.Move(moveDir * Time.deltaTime);

		//THUMBSTICK CODE
		float leftVertical = LeftStick.xAxis;
		float leftHorizontal = LeftStick.yAxis;

		float rightVertical = RightStick.xAxis;
		float rightHorizontal = RightStick.yAxis;

		if(leftVertical != 0 || leftHorizontal != 0) {
			animator.SetBool("isIdle", false);
		} else {
			animator.SetBool("isIdle", true);
		}

		//check if right stick is moving, if so rotate towards this
		Vector3 lookDir = new Vector3(rightVertical, 0.0f, rightHorizontal);
		moveDir = new Vector3(leftVertical, 0.0f, leftHorizontal);

		if(lookDir.x != 0 || lookDir.z != 0) {
			//transform.Rotate(Vector3.up * rightHorizontal * rotationSpeed);
			NewRotation = Quaternion.LookRotation(lookDir, Vector3.up);
			animator.SetBool("isShooting", true);
		} else if (moveDir.x != 0 || moveDir.z != 0){
			//transform.Rotate(Vector3.up * leftHorizontal * rotationSpeed);
			NewRotation = Quaternion.LookRotation(moveDir, Vector3.up);
			
		}

		if(lookDir.x == 0 || lookDir.z == 0) {
			animator.SetBool("isShooting", false);
		}

		transform.rotation = NewRotation;

		moveDir *= Speed;
		//moveDir = transform.TransformDirection(moveDir);

		if(Input.GetKeyDown("space")) {
			
		} else {
			
		}
		
	}

	//points player in direction of movement unless shooting, otherwise moves player around without rotating
	void LeftStickMovement() {

	}

	//always rotates player in direction of right stick when used, also shoots
	void RightStickMovement() {

	}

	void FixedUpdate() {
		charaController.Move(moveDir * Time.deltaTime);
	}
}
