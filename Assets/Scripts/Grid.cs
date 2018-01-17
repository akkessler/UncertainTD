using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int width;
    public int length;

    public Tile tilePrefab;
    public Structure structurePrefab;

    public TileType[] tileTypes;
    public StructureType[] structureTypes;

    Tile[,] tiles;

    // build mode stuff
    StructurePreview structurePreview;
    bool buildMode;

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


	// Update is called once per frame
	void Update () {
        // Toggle [B]uild mode
        if(Input.GetKeyUp(KeyCode.B))
        {
            ToggleBuildMode();
        }

        if(buildMode /*&& Input.GetMouseButton(0)*/)
        {
            // TODO Optimize as to not perform raycast every frame
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100f))
            {
                Tile t = hit.transform.GetComponent<Tile>();
                if(t != null)
                {
                    structurePreview.transform.position = t.transform.position + (Vector3.up * 1f); // TODO Scaling!?
                }
                else
                {
                    Debug.Log("No tile here!");
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                bool buildable = true;

                StructureType st = structurePreview.GetComponent<Structure>().structureType;
                Vector3 pos = structurePreview.transform.position;
                int x = (int)pos.x;
                int y = (int)pos.z;
                for(int i=x; i < x+st.width; i++)
                {
                    for(int j=y; j < y+st.length; j++)
                    {
                        Tile t = tiles[i, j];
                        if(!(t.tileType.buildable && t.structure == null))
                        {
                            Debug.Log(string.Format("Can't build at ({0},{1})",i,j));
                            buildable = false;
                            break; // convert to return eventually
                        }
                    }
                }

                if (!buildable)
                {
                    Debug.Log("Could not build there!");
                }
                else
                {
                    Debug.Log("Can build there! Hurray!");
                    // create something, assign to tiles, etc.
                }

            }
        }
	}

    void ToggleBuildMode()
    {
        if(!buildMode)
        {
            Structure structure = Instantiate(structurePrefab, Vector3.zero, Quaternion.identity);
            structure.name = "Structure Preview";
            structure.structureType = structureTypes[Mathf.FloorToInt(Random.value * structureTypes.Length)];
            structurePreview = structure.gameObject.AddComponent<StructurePreview>();
        } else
        {

            Destroy(structurePreview);
        }

        buildMode = !buildMode;
    }
}

