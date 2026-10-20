using System.Collections.Generic;
using UnityEngine;

// Define a class named Cell
public class Cell
{
    // Properties to store information about the cell
    public int Row { get; set; }
    public int Column { get; set; }
    public bool WallNorth { get; set; }
    public bool WallEast { get; set; }
    public bool WallSouth { get; set; }
    public bool WallWest { get; set; }
    public bool Visited { get; set; }
    public List<Cell> Neighbours { get; set; }
    public bool IsStartCell { get; set; }
    public bool IsEndCell { get; set; }
    public GameObject CellGameObject { get; private set; }
    public GameObject WallNObject { get; private set; }
    public GameObject WallEObject { get; private set; }
    public GameObject WallSObject { get; private set; }
    public GameObject WallWObject { get; private set; }

    // Constructor to initialise the cell with default values
    public Cell(int row, int column, GameObject cellGameObject)
    {
        Row = row;
        Column = column;
        WallNorth = true;
        WallEast = true;
        WallSouth = true;
        WallWest = true;
        Visited = false;
        IsStartCell = false;
        IsEndCell = false;
        CellGameObject = cellGameObject;
        Neighbours = new List<Cell>();
        WallNObject = cellGameObject.transform.Find("WallN").gameObject;
        WallEObject = cellGameObject.transform.Find("WallE").gameObject;
        WallSObject = cellGameObject.transform.Find("WallS").gameObject;
        WallWObject = cellGameObject.transform.Find("WallW").gameObject;
    }

    // Method to get unvisited neighbouring cells
    public List<Cell> GetUnvisitedNeighbouringCells(Cell[,] grid, int rows, int columns)
    {
        List<Cell> neighbouringCells = new List<Cell>();

        if (Row > 0 && !grid[Row - 1, Column].Visited)
        {
            neighbouringCells.Add(grid[Row - 1, Column]);
        }

        if (Column < columns - 1 && !grid[Row, Column + 1].Visited)
        {
            neighbouringCells.Add(grid[Row, Column + 1]);
        }

        if (Row < rows - 1 && !grid[Row + 1, Column].Visited)
        {
            neighbouringCells.Add(grid[Row + 1, Column]);
        }

        if (Column > 0 && !grid[Row, Column - 1].Visited)
        {
            neighbouringCells.Add(grid[Row, Column - 1]);
        }

        return neighbouringCells;
    }

    // Method to get visited neighbouring cells
    public List<Cell> GetVisitedNeighbouringCells(Cell[,] grid, int rows, int columns)
    {
        List<Cell> neighbouringCells = new List<Cell>();

        if (Row > 0 && grid[Row - 1, Column].Visited)
        {
            neighbouringCells.Add(grid[Row - 1, Column]);
        }

        if (Column < columns - 1 && grid[Row, Column + 1].Visited)
        {
            neighbouringCells.Add(grid[Row, Column + 1]);
        }

        if (Row < rows - 1 && grid[Row + 1, Column].Visited)
        {
            neighbouringCells.Add(grid[Row + 1, Column]);
        }

        if (Column > 0 && grid[Row, Column - 1].Visited)
        {
            neighbouringCells.Add(grid[Row, Column - 1]);
        }

        return neighbouringCells;
    }
}
