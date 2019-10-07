using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockData
{
    public string name;
    public Vector3 pos;
    public GameObject reference;
    public BlockData (string newName, Vector3 newPos, GameObject newReference) {
        name = newName;
        pos = newPos;
        reference = newReference;
    }
}
