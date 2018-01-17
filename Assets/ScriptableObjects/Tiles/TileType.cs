using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class TileType : ScriptableObject
{
    // Building properties
    public bool buildable;

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
