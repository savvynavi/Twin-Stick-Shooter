
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
	public float HP;
	
	// Update is called once per frame
	void Update () {
		//if HP drops to zero, the enemy dies
		if(HP <= 0) {
			GetComponent<CapsuleCollider>().enabled = false;
			GetComponent<Animator>().Play("Unarmed-Death1");
		}
	}
}
