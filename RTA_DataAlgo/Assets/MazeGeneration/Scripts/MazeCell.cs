using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    public Vector2Int coord = Vector2Int.zero;
    
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count]; // - 1 i think is correct

    private int initializedEdgeCount;

    public bool IsFullyInitialized {
        get {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }
    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return edges[(int) direction];
    }

    public void SetEdge(MazeCellEdge edge, MazeDirection direction)
    {
        edges[(int) direction] = edge;
        initializedEdgeCount += 1;
    }

    
    public MazeDirection RandomUninitializedDirection {
        get {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++) {
                if (edges[i] == null) {
                    if (skips == 0) {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }
}
