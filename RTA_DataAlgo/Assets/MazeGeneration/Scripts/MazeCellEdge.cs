using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour
{
    public MazeCell Cell;
    public MazeCell edge;
    public MazeDirection Direction;

    public void Init(MazeCell cell, MazeCell other, MazeDirection direction)
    {
        Cell = cell;
        edge = other;
        Direction = direction;
        
        cell.SetEdge(this, direction);

        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}
