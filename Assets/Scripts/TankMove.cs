using UnityEngine;
using System.Collections;

public class TankMove : NewBehaviour {

	public Tiles terrain;
	public AudioSource audioSource;
	public new SpriteRenderer renderer;
	[Header("Front, back, left, right")]
	public Sprite[] sprite;

	public float speed = 5.0f;
	public int direction;

	public KeyCode front = KeyCode.W;
	public KeyCode back = KeyCode.S;
	public KeyCode left = KeyCode.A;
	public KeyCode right = KeyCode.D;

	void FixedUpdate() {
		bool isMoving = GetKeyMove();
		if (isMoving) {
			int newDirection = GetKeyDirection();
			if (direction != newDirection) {
				SnapPositionToGrid();
				renderer.sprite = sprite[newDirection];
				direction = newDirection;
			}

			float x = 0.0f;
			float y = 0.0f;
			if (newDirection <= 1) {
				y = (speed * Input.GetAxis("Vertical")) * Time.deltaTime;
			}
			else {
				x = (speed * Input.GetAxis("Horizontal")) * Time.deltaTime;
			}
			if (true) {
				transform.Translate(x, y, 0.0f);
			}
			SnapToMap();
		}
		audioSource.volume = isMoving ? 1.0f : 0.0f;
	}

	public void SnapToMap() {
		position = new Vector3(Mathf.Clamp(position.x, terrain.position.x, terrain.x - 1),
			Mathf.Clamp(position.y, terrain.position.y, terrain.y - 1),
			0.0f);
	}

	public void SnapPositionToGrid() {
		position = new Vector3(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));
	}

	public bool GetKeyMove() {
		return Input.GetKey(front) || Input.GetKey(back) || Input.GetKey(left) || Input.GetKey(right);
	}

	public int GetKeyDirection() {
		if (Input.GetKey(front)) {
			return 0;
		}
		else if (Input.GetKey(back)) {
			return 1;
		}
		else if (Input.GetKey(left)) {
			return 2;
		}
		else if (Input.GetKey(right)) {
			return 3;
		}
		return 0;
	}

	public Vector2 GetDirection() {
		switch (direction) {
			case 0:
			return Vector2.up;
			case 1:
			return Vector2.down;
			case 2:
			return Vector2.left;
			case 3:
			return Vector2.right;
		}
		return Vector2.up;
	}
}
