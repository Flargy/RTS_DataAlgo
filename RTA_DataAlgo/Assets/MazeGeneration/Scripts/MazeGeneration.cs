using UnityEngine;

public class MazeGeneration : MonoBehaviour
{
    [SerializeField] private Maze mazePrefab = null;
    [SerializeField] private Maze mazeInstance = null;
    
    void Start()
    {
        Begin();
    }

    void Update()
    {
        Restart();
       
    }

    private void Begin()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }

    private void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Space) == false)
        {
            return;
        }
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        
        Begin();
    }
}
