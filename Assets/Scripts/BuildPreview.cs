using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour {
    
    // do we still want to use this value publicly? even privately?
    public bool IsBlocked
    {
        get {
            return blockingTiles.Count + blockingStructures.Count != 0;
        }
    }

    public bool IsValidPosition
    {
        get {
            // FIXME Check equality against structure's area
            return !IsBlocked && currentTiles.Count >= 1; 
        }
    }

    public Tile[] CurrentTiles
    {
        get {
            return currentTiles.ToArray();
        }
    }

    private List<Tile> currentTiles;

    private List<Tile> blockingTiles;
    private List<Structure> blockingStructures;
    // private List<Unit> blockingUnits; 

    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    void Start () {
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        currentTiles = new List<Tile>();
        blockingTiles = new List<Tile>();
        blockingStructures = new List<Structure>();

        boxCollider.size = new Vector3(0.75f, 2f, 0.75f); // so we are on border of tiles
    
        meshRenderer.material.color = Color.green;
    }

    void UpdatePreviewColor()
    {
        if (IsValidPosition)
        {
            meshRenderer.material.color = Color.green;
        }
        else
        {
            meshRenderer.material.color = Color.red;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Tile t = other.GetComponent<Tile>();
        if(t != null)
        {
            if (t.tileType.buildable)
            {
                Debug.Log("OnTriggerEnter (Tile) - OPEN");
                currentTiles.Add(t);
            }
            else 
            {
                Debug.Log("OnTriggerEnter (Tile) - BLOCKING");
                blockingTiles.Add(t);
            }
        }
        
        Structure s = other.GetComponent<Structure>();
        if (s != null)
        {
            Debug.Log("OnTriggerEnter (Structure)");
            blockingStructures.Add(s);
        }

        UpdatePreviewColor();
    }

    void OnTriggerExit(Collider other)
    {
        Tile t = other.GetComponent<Tile>();
        if (t != null) {
            if (blockingTiles.Contains(t))
            {
                Debug.Log("OnTriggerExit (Tile) - BLOCKING");
                blockingTiles.Remove(t);
            }

            if(currentTiles.Contains(t))
            {
                Debug.Log("OnTriggerExit (Tile) - OPEN");
                currentTiles.Remove(t);
            }
        }

        Structure s = other.GetComponent<Structure>();
        if (s != null)
        {
            if(blockingStructures.Contains(s))
            {
                Debug.Log("OnTriggerExit (Structure)");
                blockingStructures.Remove(s);
            }
        }

        UpdatePreviewColor();
    }
}
