using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public TileType tileType;

    public Structure structure;

    // Use this for initialization
    void Start () {
        GetComponent<MeshRenderer>().material.color = tileType.color;
        Debug.Log(tileType.name);
	}

}
