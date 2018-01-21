using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour {
    
    public bool IsValidPosition
    {
        get {
            int blockerCount = blockingTiles.Count + blockingStructures.Count;
            return blockerCount == 0 && currentTiles.Count == area; 
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

    private int area;

    void Start () {
        StructureType structureType = GetComponent<Structure>().structureType;
        boxCollider = GetComponent<BoxCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        currentTiles = new List<Tile>();
        blockingTiles = new List<Tile>();
        blockingStructures = new List<Structure>();

        area = structureType.width * structureType.length;

        // Sub 0.25 on xz axis to not hit multiple tile borders
        boxCollider.size = new Vector3(structureType.width - 0.25f, 2f, structureType.length - 0.25f);

        UpdatePreviewColor();
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
