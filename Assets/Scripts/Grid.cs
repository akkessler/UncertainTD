using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int width;
    public int length;

    public Tile tilePrefab;
    public TileType[] tileTypes;

    Tile[,] tiles;    

    // Use this for initialization
    void Start () {
        tiles = new Tile[width, length];
        for(int x=0; x < width; x++)
        {
            for(int y=0; y < length; y++) {
                Tile t = Instantiate(tilePrefab, transform);
                t.name = string.Format("Tile({0},{1})", x, y);
                t.transform.position = new Vector3(x, 0f, y);
                t.tileType = tileTypes[Mathf.FloorToInt(Random.value * tileTypes.Length)];
                tiles[x, y] = t;
            }
        }
	}
    
}
