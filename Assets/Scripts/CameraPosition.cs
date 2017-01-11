using UnityEngine;
using System.Collections;

public class CameraPosition : NewBehaviour {

	public Tiles terrain;

	void Start() {
		position = new Vector3((terrain.x / 2.0f) - 1.0f, (terrain.y / 2.0f) - 1.0f, -10.0f);
	}
}
