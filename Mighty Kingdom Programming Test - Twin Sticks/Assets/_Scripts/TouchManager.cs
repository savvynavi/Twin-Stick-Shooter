using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour {

#if UNITY_ANDROID
	//storing each touuch in a dictionary with unique touch id
	Dictionary<int, TouchTarget> targets = new Dictionary<int, TouchTarget>();

	// Update is called once per frame
	void Update () {

		//get all touches
		Touch[] touches = Input.touches;
		foreach(Touch touch in touches) {
			switch(touch.phase) {
				case TouchPhase.Began:
					//began touch, test if target hit
					GameObject obj = GetObjectUnderPos(touch.position);
					TouchTarget target = obj.GetComponent<TouchTarget>();
					if(target != null) {
						target.OnDown(touch.position);
						targets[touch.fingerId] = target;
					}
					break;
				case TouchPhase.Ended:
					//if lifted off button, calls onUp for that fingerID
					if(targets.ContainsKey(touch.fingerId) && targets[touch.fingerId] != null) {
						targets[touch.fingerId].OnUp();
						targets.Remove(touch.fingerId);
					}

					break;
				case TouchPhase.Moved:
					//if moving finger around, calls onDrag for that fingerID
					if(targets.ContainsKey(touch.fingerId) && targets[touch.fingerId] != null) {
						targets[touch.fingerId].OnDrag(touch.position);
					}

					break;
				default:
					break;
			}
		}
	}

	//gets UI object at a given point
	List<RaycastResult> hitObjects = new List<RaycastResult>();
	GameObject GetObjectUnderPos(Vector3 pos) {

		PointerEventData pointer = new PointerEventData(EventSystem.current);
		pointer.position = pos;
		EventSystem.current.RaycastAll(pointer, hitObjects);
		return (hitObjects.Count <= 0) ? null : hitObjects[0].gameObject;
	}

#endif
}
