using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tiles : NewBehaviour {

	public enum TileType {
		Empty, Chunk, Grass, Iron, Water, Glass
	}

	public int x = 8, y = 8;

	[Header("Chunk, Grass, Iron, Water")]
	public List<GameObject> tiles = new List<GameObject>();
	public List<TileType> elements = new List<TileType>();

	void Start() {
		elements = new List<TileType>();
		for (int _x = 0; _x < this.x; _x++) {
			for (int _y = 0; _y < this.y; _y++) {
				if (_x == 0 && _y == 0) {
					elements.Add(TileType.Empty);
					continue;
				}
				int tile = Random.Range(0, tiles.Count);
				GameObject obj = (GameObject)Instantiate(tiles[tile], transform.position + new Vector3(_x, _y, 0.0f), Quaternion.identity);
				obj.transform.SetParent(this.transform);
				elements.Add((TileType)(tile + 1));
			}
		}
	}
}
