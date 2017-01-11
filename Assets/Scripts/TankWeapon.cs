using UnityEngine;
using System.Collections;

public class TankWeapon : NewBehaviour {

	GameObject target;
	public TankMove player;
	public AudioClip shotSFX;

	public KeyCode fire = KeyCode.Space;

	void Update() {
		if (Input.GetKeyDown(fire)) {
			GameObject shot = new GameObject("Shot", typeof(AudioSource));
			shot.GetComponent<AudioSource>().PlayOneShot(shotSFX);
			Destroy(shot, 1.0f);
			if (target) {
				Destroy(target);
			}
		}
	}

	void FixedUpdate() {
		Vector2 direction = player.GetDirection();
		RaycastHit2D hit2d = Physics2D.Raycast(transform.position, direction);
		target = hit2d.collider && hit2d.transform.CompareTag("Tile - Destructible") ? hit2d.transform.gameObject : null;
	}
}

