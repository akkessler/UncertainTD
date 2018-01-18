using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour {
    
    public bool IsBlocked
    {
        get {
            return blockingTiles.Count + blockingStructures.Count != 0;
        }
    }
    
    private List<Tile> blockingTiles;
    private List<Structure> blockingStructures;
    // private List<Unit> blockingUnits; 

    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        blockingTiles = new List<Tile>();
        blockingStructures = new List<Structure>();

        boxCollider.size = new Vector3(0.75f, 2f, 0.75f); // so we are on border of tiles
    
        meshRenderer.material.color = Color.green;
    }

    void OnTriggerEnter(Collider other)
    {
        Tile t = other.GetComponent<Tile>();
        if(t != null && !t.tileType.buildable)
        {
            Debug.Log("OnTriggerEnter (Tile)");
            if (!IsBlocked)
            {
                meshRenderer.material.color = Color.red;
            }
            blockingTiles.Add(t);
            return;
        }
        
        Structure s = other.GetComponent<Structure>();
        if (s != null)
        {
            Debug.Log("OnTriggerEnter (Structure)");
            if (!IsBlocked)
            {
                meshRenderer.material.color = Color.red;
            }
            blockingStructures.Add(s);
            return;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Tile t = other.GetComponent<Tile>();
        if (t != null && blockingTiles.Contains(t))
        {
            Debug.Log("OnTriggerExit (Tile)");
            blockingTiles.Remove(t);
            if (!IsBlocked)
            {
                meshRenderer.material.color = Color.green;
            }
            return;
        }

        Structure s = other.GetComponent<Structure>();
        if (s != null)
        {
            Debug.Log("OnTriggerExit (Structure)");
            blockingStructures.Remove(s);
            if (!IsBlocked)
            {
                meshRenderer.material.color = Color.green;
            }
            return;
        }
    }
}
