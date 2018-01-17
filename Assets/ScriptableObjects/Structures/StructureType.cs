using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class StructureType : ScriptableObject
{

    public int width;
    public int length;

    // TODO Need to figure out what buildable means for Structures
    //// Building properties
    //public bool buildable;

    // Pathing properties
    public bool walkable;
    public bool flyable;
    
    // Visual properties
    public Color color;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        
    }

    private void OnDestroy()
    {
        
    }

}
