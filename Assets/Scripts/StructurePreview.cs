using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class StructurePreview : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEnter");
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("TriggerStay");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("TriggerExit");
    }

}
