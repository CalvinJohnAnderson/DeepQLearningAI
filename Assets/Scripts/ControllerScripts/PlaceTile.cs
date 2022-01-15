using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;

public class PlaceTile : SerializedMonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TileGenerater tileGenerater;
    [SerializeField] private QLearning qLearning;
    
    [SerializeField] private Dictionary<Vector2Int, int> grid = new Dictionary<Vector2Int, int>();
    private bool movingTiles = false;
    private Vector2Int placeable = new Vector2Int(10,10);
    private int placedCounter = 0;

    public void UpdateGrid(Dictionary<Vector2Int, int> newGrid)
    {
        grid = newGrid;
    }

    public Dictionary<Vector2Int, int> DuplicateGrid()
    {
        Dictionary<Vector2Int, int> duplicateGrid = grid;
        return duplicateGrid;
    }
    
    
    public Dictionary<Vector2Int, int> TileChecker(int coords, string axis, int playerTurn, Dictionary<Vector2Int, int> tileDictionary = null) //0 for tile, 1 for obs, 2 for player 1, 3 for player 2
    {
        Debug.Log("Starting tile check");
        if (tileDictionary == null)
        {
            tileDictionary = grid;
        }
        GetPlaceableTile(tileDictionary, coords, axis);
        switch (axis)
        {
            case "x":
                for (int y = placeable.y; y <= 9; y++)
                {
                    if (tileDictionary[new Vector2Int(coords, y)] > 1)
                    {
                        int tileNum = tileDictionary[new Vector2Int(coords, y)];
                        
                        tileDictionary[new Vector2Int(placeable.x, placeable.y)] = //removed placeable counter
                            tileDictionary[new Vector2Int(coords, y)];
                        
                        tileGenerater.UpdateDictionary(new Vector2Int(placeable.x, placeable.y), tileNum);
                        
                        tileDictionary[new Vector2Int(coords, y)] = 0;
                        tileGenerater.UpdateDictionary(new Vector2Int(coords, y), 0);
                        placedCounter++;
                        GetPlaceableTile(tileDictionary, coords, axis);
                    }
                }
                break;
            
            case "y":
                for (int x = placeable.x; x <= 9; x++)
                {
                    if (tileDictionary[new Vector2Int(x, coords)] > 1)
                    {
                        int tileNum = tileDictionary[new Vector2Int(x, coords)];
                        
                        tileDictionary[new Vector2Int(placeable.x, placeable.y)] =
                            tileDictionary[new Vector2Int(x, coords)];
                        
                        tileGenerater.UpdateDictionary(new Vector2Int(placeable.x, placeable.y), tileNum); //removed placedcounter
                        
                        tileDictionary[new Vector2Int(x, coords)] = 0;
                        tileGenerater.UpdateDictionary(new Vector2Int(x, coords), 0);
                        placedCounter++;
                        GetPlaceableTile(tileDictionary, coords, axis);
                        
                    }
                }
                break;
        }
        PlacePlayerTile(coords, axis, playerTurn, tileDictionary);
        return tileDictionary;
    }

    private void GetPlaceableTile(Dictionary<Vector2Int, int> tileDictionary, int coords, string axis)
    {
        Debug.Log("Getting placeable tile");
        movingTiles = false;
        switch (axis)
        {
            case "x":
                for (int y = 0; y <= 9; y++)//getting first placeable tile
                {
                    if (tileDictionary[new Vector2Int(coords, y)] == 1)
                    {
                        placeable = new Vector2Int(10, 10); //resets placeable
                        movingTiles = false;
                    }
                    else if (tileDictionary[new Vector2Int(coords, y)] == 0 && !movingTiles)
                    {
                        movingTiles = true;
                        placeable = new Vector2Int(coords, y);
                    }
                }
                break;
            case "y":
                for (int x = 0; x <= 9; x++)//getting first placeable tile
                {
                    if (tileDictionary[new Vector2Int(x, coords)] == 1)
                    {
                        placeable = new Vector2Int(10, 10); //resets placeable
                        movingTiles = false;
                    }
                    else if (tileDictionary[new Vector2Int(x, coords)] == 0 && !movingTiles)
                    {
                        movingTiles = true;
                        placeable = new Vector2Int(x, coords);
                    }
                }
                break;
        }
        Debug.Log("Placeable tile is : x = " + placeable.x + " : y = " + placeable.y);
        //return placeable;
    }

    private void PlacePlayerTile(int coords, string axis, int playerTurn, Dictionary<Vector2Int, int> tileDictionary)
    {
        Debug.Log("Placing final tile");
        GetPlaceableTile(tileDictionary, coords, axis);
        tileDictionary[placeable] = playerTurn;
        tileGenerater.UpdateDictionary(placeable, playerTurn);
        UpdateGrid(tileDictionary);
        int gameState = Evaluator.EvaluateGrid(tileDictionary);
        if (gameState > 0)
        {
            if (playerTurn == 3)
            {
                STATE currentState = STATE.WIN;
                qLearning.CalculateQValue(currentState, placeable.x, placeable.y, tileDictionary);
            }
            gameManager.EndGame(gameState);
            Debug.Log("Game is over");
            Debug.LogWarning("GameOver: Cant move");
        }
        else
        {
            if (playerTurn == 3)
            {
                STATE currentState = Evaluator.RewardState(placeable.x, placeable.y, tileDictionary);
                qLearning.CalculateQValue(currentState, placeable.x, placeable.y, tileDictionary);
            }
            gameManager.ChangeTurn();
        }
    }
}
