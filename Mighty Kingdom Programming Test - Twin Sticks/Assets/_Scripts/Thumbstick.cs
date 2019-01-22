using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Thumbstick : TouchTarget {

	public Vector3 mouseDownPos;
	public float extents = 32;
	public float xAxis;
	public float yAxis;

	Vector3 pos;

#if UNITY_ANDROID
	//if it's an android build, it will use the touchmanager class for movement


#else

	//pc movement based on keys/mouse
	public void onMouseDown(){
		OnDown(Input.mousePosition);
	}

	public void onMouseDrag(){
		OnDrag(Input.mousePosition);
	}

	public void onEndDrag(){
		OnUp();
	}

#endif

	//when first clicked
	public override void OnDown(Vector3 mousePos) {
		mouseDownPos = mousePos;
	}

	//while dragging, will move stick to point
	public override void OnDrag(Vector3 mousePos) {

		pos = mousePos - mouseDownPos;
		pos.x = Mathf.Clamp(pos.x, -extents, extents);
		pos.y = Mathf.Clamp(pos.y, -extents, extents);
		transform.localPosition = pos;

		xAxis = pos.x / extents;
		yAxis = pos.y / extents;
	}

	//when stick let go, recentres
	public override void OnUp() {
		pos = transform.localPosition = Vector3.zero;
		xAxis = yAxis = 0;
	}

}
