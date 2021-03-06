﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public Transform player;

	Quaternion cameraAngle;
	Vector3 position;

	// Use this for initialization
	void Start () {
		position = transform.position;
		cameraAngle = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		//will set the camera up to follow the player around at the distance/angle set in the scene
		if(player != null) {
			transform.position = new Vector3(player.position.x + position.x, player.position.y + position.y, player.position.z + position.z);
		}
	}
}
