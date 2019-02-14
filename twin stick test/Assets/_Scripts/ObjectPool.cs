using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
	public static ObjectPool poolInstance;
	public GameObject prefab;
	public List<GameObject> pooledObjects;
	public int numInPool = 20;

	private void Awake() {
		if(poolInstance == null) {
			poolInstance = this;
		}

		pooledObjects = new List<GameObject>();
		for(int i = 0; i < numInPool; i++) {
			GameObject obj = Instantiate(prefab);
			obj.SetActive(false);
			pooledObjects.Add(obj);
		}
	}

	public GameObject GetPooledObject() {
		for(int i = 0; i < pooledObjects.Count; i++) {
			if(!pooledObjects[i].activeInHierarchy) {
				return pooledObjects[i];
			}
		}
		return null;
	}

}
