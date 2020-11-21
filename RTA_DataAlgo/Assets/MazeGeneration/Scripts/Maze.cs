using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private Vector2Int size = Vector2Int.one * 20;
    [SerializeField] private MazeCell cellPrefab = null;
    [SerializeField] private float generationStepDelay = 0.05f;
    [SerializeField] private MazePassage passagePrefab = null;
    [SerializeField] private MazeWall wallPrefab = null;
        
        
    private MazeCell[,] cells = new MazeCell[0,0];


    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        
        cells = new MazeCell[size.x, size.y];
        Vector2Int coord = RandomCoordinates();
        
        List<MazeCell> activeCells = new List<MazeCell>();
        FirstGenerationStep(activeCells);
        
        while (activeCells.Count > 0)
        {
            yield return delay;
            /*CreateCell(coord);
            coord += MazeDirections.RandomValue.ToVector2Int();*/
            
            NextGenerationStep(activeCells);
        }
        
    }

    private void FirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates()));
    }

    private Vector2Int RandomCoordinates()
    {
        return new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
    }

    private void NextGenerationStep(List<MazeCell> activeCells)
    {
       
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized) {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        Vector2Int coordinates = currentCell.coord + direction.ToVector2Int();
        if (ContainsCordinates(coordinates)) {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null) {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else {
                CreateWall(currentCell, neighbor, direction);
                // No longer remove the cell here.
            }
        }
        else {
            CreateWall(currentCell, null, direction);
            // No longer remove the cell here.
        }
        
        
    }

    private bool ContainsCordinates(Vector2Int coord)
    {
        return coord.x >= 0f && coord.x < size.x &&
               coord.y >= 0f && coord.y < size.y;
    }

    private MazeCell CreateCell(Vector2Int coord)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        newCell.name = "Maze Cell" + coord.x + ", " + coord.y;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coord.x - size.x * 0.5f + 0.5f, coord.y - size.y * 0.5f + 0.5f, 0f);

        cells[coord.x, coord.y] = newCell;
        newCell.coord = coord;
        return newCell;
    }

    public MazeCell GetCell(Vector2Int coord)
    {
        return cells[coord.x, coord.y];
    }
    
    private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Init(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Init(otherCell, cell, direction.GetOpposite());
    }

    private void CreateWall (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Init(cell, otherCell, direction);
        if (otherCell != null) {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Init(otherCell, cell, direction.GetOpposite());
        }
    }
}
