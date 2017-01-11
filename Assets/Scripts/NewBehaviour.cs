using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class NewBehaviour : MonoBehaviour {

	public Vector3 position {
		get {
			return this.transform.position;
		}
		set {
			this.transform.position = value;
		}
	}

	public Quaternion rotation {
		get {
			return this.transform.rotation;
		}
		set {
			this.transform.rotation = value;
		}
	}
}
