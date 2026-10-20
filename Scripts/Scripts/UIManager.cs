using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Define a class named UIManager
public class UIManager : MonoBehaviour
{
    // Declare variables
    System.Random random = new System.Random();
    public CameraController cameraController;
    public PlayerMovement playerMovement;
    public Canvas canvas;
    public Button playButton;
    private int selectedValue = -1;
    public GameObject cellGameObject;
    private float numberOfRows = 0.0f;
    private float numberOfColumns = 0.0f;
    public Cell[,] grid;
    private int numRows;
    private int numColumns;
    private Cell startCell;
    private Cell endCell;
    public GameObject wallEntrance;
    public GameObject player;
    public int randRowOne;
    public int randRowTwo;
    public Transform playerTransform;
    public int randomInt;
    public TimerScript timerScript;
    public Canvas inGameUI;
    public bool playerMoved = false;
    public bool canMoveToStart = false;
    public Canvas pauseCanvas;
    public bool spawnLock = true;

    private void Start()
    {
        playButton.onClick.AddListener(HandlePlayButtonClick);
        inGameUI.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(false);
        canMoveToStart = false;
        playerMoved = false;
    }

    private void Update()
    {
        if (spawnLock)
        {
            player.transform.position = new Vector3(0, 5, 0);
        }
        if (canMoveToStart)
        {
            if (playerMoved == false)
            {
                playerToStart();
                playerMoved = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
            if (pauseCanvas.gameObject.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {     
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

    }

    public int returnRows()
    {
        return numRows;
    }
    public int returnColumns()
    {
        return numColumns;
    }

    public void TogglePauseMenu()
    {
        pauseCanvas.gameObject.SetActive(!pauseCanvas.gameObject.activeSelf);
        cameraController.cameraGameToggle();
        
        playerMovement.playerGameToggle();
        timerScript.ToggleTimer();

    }

    public void SetCanMoveToStart(bool value)
    {
        canMoveToStart = value;
    }

    public void SetPlayerMoved(bool value)
    {
        playerMoved = value;
    }

    private void HandlePlayButtonClick()
    {
        spawnLock = false;
        grid = gridGenerator();

        if (selectedValue == -1)
        {
            Debug.Log("Random maze type selected: ");
            randomInt = random.Next(1,4);
            if (randomInt == 1) 
            {
                recursiveAlgorithmGenerator(startCell);
                SetExitWall(endCell, wallEntrance);
            }
            else if (randomInt == 2)
            {
                primsAlgorithmGenerator(startCell);
                SetExitWall(endCell, wallEntrance);
            }
            else if (randomInt == 3)
            {
                kruskalsAlgorithmGenerator();
                SetExitWall(endCell, wallEntrance);
            }
        }
        else if (selectedValue == 1)
        {
            recursiveAlgorithmGenerator(startCell);
            SetExitWall(endCell, wallEntrance);
        }
        else if (selectedValue == 2)
        {
            primsAlgorithmGenerator(startCell);
            SetExitWall(endCell, wallEntrance);
        }
        else if (selectedValue == 3)
        {
            kruskalsAlgorithmGenerator();
            SetExitWall(endCell, wallEntrance);
        }


        cameraController.cameraGameActive();
        playerMovement.playerGameActive();
        canvas.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        timerScript.ResetTimer();
        timerScript.StartTimer();
        canMoveToStart = true;
        
    }


    public void SetRowSliderValue(float value)
    {
        numberOfRows = value;
    }


    public void SetColumnSliderValue(float value)
    {
        numberOfColumns = value;
    }



    // Recursive Backtracking Maze Generation Algorithm
    public void recursiveAlgorithmGenerator(Cell currentCell)
    {
        Debug.Log("Recursive Backtracking Algorithm Selected");
        Stack<Cell> cellStack = new Stack<Cell>();
        currentCell.Visited = true;

        while (true)
        {
            List<Cell> unvisitedNeighbours = currentCell.GetUnvisitedNeighbouringCells(grid, numRows, numColumns);

            if (unvisitedNeighbours.Count > 0)
            {
                cellStack.Push(currentCell);

                int randomIndex = random.Next(unvisitedNeighbours.Count);
                Cell nextCell = unvisitedNeighbours[randomIndex];

                removeWall(currentCell, nextCell);

                currentCell = nextCell;
                currentCell.Visited = true;
            }
            else if (cellStack.Count > 0)
            {
                currentCell = cellStack.Pop();
            }
            else
            {
                break;
            }
        }
    }

    // Prim's Maze Generation Algorithm
    public void primsAlgorithmGenerator(Cell startCell)
    {
        Debug.Log("Prim's Algorithm Selected");
        HashSet<Cell> inMaze = new HashSet<Cell>();
        HashSet<Cell> frontier = new HashSet<Cell>();

        inMaze.Add(startCell);
        startCell.Visited = true;

        foreach (var neighbour in startCell.Neighbours)
        {
            frontier.Add(neighbour);
        }

        while (frontier.Count > 0)
        {
            List<Cell> frontierList = new List<Cell>(frontier);
            int randomIndex = random.Next(frontierList.Count);
            Cell currentCell = frontierList[randomIndex];

            List<Cell> mazeNeighbours = currentCell.GetVisitedNeighbouringCells(grid, numRows, numColumns);
            if (mazeNeighbours.Count > 0)
            {
                int randomMazeNeighbourIndex = random.Next(mazeNeighbours.Count);
                Cell mazeNeighbour = mazeNeighbours[randomMazeNeighbourIndex];

                removeWall(currentCell, mazeNeighbour);

                inMaze.Add(currentCell);
                currentCell.Visited = true;

                foreach (var neighbour in currentCell.Neighbours)
                {
                    if (!neighbour.Visited)
                    {
                        frontier.Add(neighbour);
                    }
                }
            }

            frontier.Remove(currentCell);
        }
    }

    // Kruskal's Maze Generation Algorithm
    public void kruskalsAlgorithmGenerator()
    {
        Debug.Log("Kruskal's Algorithm Selected");

        List<Wall> walls = new List<Wall>();
        for (int x = 0; x < numRows; x++)
        {
            for (int z = 0; z < numColumns; z++)
            {
                if (x > 0)
                    walls.Add(new Wall(grid[x, z], grid[x - 1, z]));
                if (z > 0)
                    walls.Add(new Wall(grid[x, z], grid[x, z - 1]));
            }
        }

        walls.Shuffle();

        Dictionary<Cell, int> cellId = new Dictionary<Cell, int>();

        foreach (var wall in walls)
        {
            int setId1 = GetSetId(cellId, wall.Cell1);
            int setId2 = GetSetId(cellId, wall.Cell2);

            if (setId1 != setId2)
            {
                removeWall(wall.Cell1, wall.Cell2);
                UpdateSetIds(cellId, setId1, setId2);
            }
        }
    }

    public class Wall
    {
        public Cell Cell1 { get; }
        public Cell Cell2 { get; }

        public Wall(Cell cell1, Cell cell2)
        {
            Cell1 = cell1;
            Cell2 = cell2;
        }
    }

    public void SetExitWall(Cell cellToEndOn, GameObject wallEntrance)
    {
        Destroy(cellToEndOn.WallEObject);

        GameObject gateObject = Instantiate(wallEntrance);
        gateObject.transform.SetParent(cellToEndOn.CellGameObject.transform);
        gateObject.transform.position = cellToEndOn.WallEObject.transform.position;
    }

    private int GetSetId(Dictionary<Cell, int> cellId, Cell cell)
    {
        if (!cellId.ContainsKey(cell))
        {
            cellId[cell] = cell.Row * numColumns + cell.Column;
        }
        return cellId[cell];
    }

    private void UpdateSetIds(Dictionary<Cell, int> cellId, int setId1, int setId2)
    {
        List<Cell> keysToUpdate = new List<Cell>();

        foreach (var pair in cellId)
        {
            if (pair.Value == setId2)
            {
                keysToUpdate.Add(pair.Key);
            }
        }

        foreach (var key in keysToUpdate)
        {
            cellId[key] = setId1;
        }
    }

    public void SetSelectedValue(int val)
    {
        selectedValue = val;
    }

    public void removeWall(Cell currentCellToRemove, Cell neighbourToRemove)
    {
        int rowDifference = neighbourToRemove.Row - currentCellToRemove.Row;
        int colDifference = neighbourToRemove.Column - currentCellToRemove.Column;

        if (rowDifference == -1)
        {
            // Neighbour to the north
            Destroy(currentCellToRemove.WallNObject);
            Destroy(neighbourToRemove.WallSObject);
        }
        else if (rowDifference == 1)
        {
            // Neighbour to the south
            Destroy(currentCellToRemove.WallSObject);
            Destroy(neighbourToRemove.WallNObject);
        }
        else if (colDifference == 1)
        {
            // Neighbour to the east
            Destroy(currentCellToRemove.WallEObject);
            Destroy(neighbourToRemove.WallWObject);
        }
        else if (colDifference == -1)
        {
            // Neighbour to the west
            Destroy(currentCellToRemove.WallWObject);
            Destroy(neighbourToRemove.WallEObject);
        }
    }

    // Generate the grid of cells
    public Cell[,] gridGenerator()
    {
        Debug.Log("Grid generated");
        numRows = Mathf.Max(1, Mathf.RoundToInt(numberOfRows));
        numColumns = Mathf.Max(1, Mathf.RoundToInt(numberOfColumns));
        Cell[,] grid = new Cell[numRows, numColumns];

        for (int x = 0; x < numRows; x++)
        {
            for (int z = 0; z < numColumns; z++)
            {
                Vector3 pos = new Vector3(x * 4, 0, z * 4);
                GameObject cellGO = Instantiate(cellGameObject, pos, Quaternion.identity) as GameObject;
                cellGO.transform.SetParent(this.transform);
                cellGO.name = $"Cell_{x}_{z}";
                Cell cell = new Cell(x, z, cellGO);
                grid[x, z] = cell;
            }
        }

        randRowOne = random.Next(numRows);
        startCell = grid[randRowOne, 0];
        startCell.CellGameObject.name = "StartCell";


        randRowTwo = random.Next(numRows);
        endCell = grid[randRowTwo, numColumns - 1];
        endCell.CellGameObject.name = "EndCell";

        Debug.Log("Starting cell is at Row: " + startCell.Row + " Column: " + startCell.Column);
        Debug.Log("End cell is at Row: " + endCell.Row + " Column: " + endCell.Column);

        for (int x = 0; x < numRows; x++)
        {
            for (int z = 0; z < numColumns; z++)
            {
                grid[x, z].Visited = false;
                grid[x, z].Neighbours = grid[x, z].GetUnvisitedNeighbouringCells(grid, numRows, numColumns);
            }
        }

        return grid;
    }


    public void playerToStart()
    {
        GameObject startCellObject = GameObject.Find("ButtonController/StartCell");

        if (startCellObject != null)
        {
            GameObject playerObject = GameObject.Find("Player");
            if (playerObject != null)
            {
                Vector3 startPosition = startCellObject.transform.position;
                playerObject.transform.position = new Vector3(startPosition.x, startPosition.y +3f, startPosition.z);
            }
            else
            {
                Debug.LogError("Player game object not found!");
            }
        }
        else
        {
            Debug.LogError("StartCell object not found under ButtonController!");
        }
    }


}
