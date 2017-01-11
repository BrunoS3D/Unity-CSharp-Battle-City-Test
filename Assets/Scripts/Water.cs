using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	public SpriteRenderer component;
	public Sprite[] sprite;

	[ReadOnly]
	public float time;
	[Range(0.0f, 5.0f)]
	public float timeRate = 1.0f;

	void Update() {
		time += Time.deltaTime / timeRate;
		component.sprite = sprite[(int)(time % sprite.Length)];
	}
}
