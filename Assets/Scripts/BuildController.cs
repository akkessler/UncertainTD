using UnityEngine;

public class BuildController : MonoBehaviour {

    public Grid grid;

    public Structure structurePrefab;
    public StructureType[] structureTypes;

    BuildPreview buildPreview;

    // Use this for initialization
    void Start () {
        grid = FindObjectOfType<Grid>(); // FIXME inefficient
    }
	
	// Update is called once per frame
	void Update () {
        // Toggle [B]uild mode
        if (Input.GetKeyUp(KeyCode.B))
        {
            ToggleBuildMode();
        }

        if (buildPreview != null)
        {
            // TODO Optimize to not run every frame
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            buildPreview.transform.position = new Vector3(
                Mathf.FloorToInt(ray.origin.x + 0.5f), 0.5f, Mathf.FloorToInt(ray.origin.z + 0.5f));

            if (Input.GetMouseButtonUp(0))
            {
                if (buildPreview.IsBlocked)
                {
                    Debug.Log("ERROR - CAN'T BUILD HERE.");
                }
                else
                {
                    // FIXME Bug where you can get here when out of bounds.
                    Debug.Log("SUCCESS - YOU CAN BUILD HERE!");
                    ToggleBuildMode();
                }
            }

        }
    }

    void ToggleBuildMode()
    {
        if (buildPreview == null)
        {
            Structure structure = Instantiate(structurePrefab, Vector3.zero, Quaternion.identity);
            structure.structureType = structureTypes[Mathf.FloorToInt(Random.value * structureTypes.Length)];
            structure.enabled = false; // TODO Future things to turn off / Destroy later?
            Destroy(structure.GetComponent<Rigidbody>());
            structure.GetComponent<Collider>().isTrigger = true;
            structure.name = "Build Preview";
            buildPreview = structure.gameObject.AddComponent<BuildPreview>();
        }
        else
        {
            Destroy(buildPreview.gameObject);
            buildPreview = null;
        }
    }
}
