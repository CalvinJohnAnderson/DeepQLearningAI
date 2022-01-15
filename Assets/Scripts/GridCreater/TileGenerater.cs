using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.WSA;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class TileGenerater : SerializedMonoBehaviour
{
    [SerializeField] private Transform parent;
    [SerializeField] private GridBuildingSystem gridBuildingSystem;
    [SerializeField] private GameObject[] tileList;
    [SerializeField] private PlaceTile placeTile;

    [SerializeField] private Dictionary<Vector2Int, int> TileData = new Dictionary<Vector2Int, int>();

    [SerializeField] private Dictionary<string, GameObject> placedTiles = new Dictionary<string, GameObject>();
    private int width;
    private int height;
    private float cellSize;
    private Vector3 tileOffset;

    private Vector2Int placePosition;


    private void Awake()
    {
        width = gridBuildingSystem.GetGridWidth();
        height = gridBuildingSystem.GetGridHeight();
        cellSize = gridBuildingSystem.GetCellSize();
        tileOffset = new Vector3(cellSize / 2, 0, cellSize / 2);
        Debug.Log("Width: " + width + "Height: " + height + "CellSize: " + cellSize);
    }

    private void Start()
    {
        //Debug.Log("Spawning tiles");
        //StartCoroutine(AnimateTilePlacement());
        //AnimatedTilePlacementWithoutDelay();
    }

    public void SetTilesPlaced(Dictionary<string, GameObject> savedTiles)
    {
        Debug.Log("Setting");
        placedTiles = savedTiles;
    }

    public Dictionary<string, GameObject> GetPlacedTiles()
    {
        return placedTiles;
    }
    
    public Dictionary<Vector2Int, int> GetTileData()
    {
        return TileData;
    }
    
    public void SetTileData(Dictionary<Vector2Int, int> savedTileData)
    {
        TileData = savedTileData;
    }
    
    public void BuildGrid(Dictionary<string, string> buildingData = null)
    {
        Debug.Log("Spawning tiles");
        if (TileData.Count == 0)
        {
            Debug.Log("Spawning new tiles");
            StartCoroutine(AnimateTilePlacement());
        }
        else
        {
            
        }
        
    }
    

    IEnumerator AnimateTilePlacement()
    {
        Vector2Int baseOrigin = gridBuildingSystem.GetBaseOrigin();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int rnd = Random.Range(0, 10);
                PlaceTile(rnd, x, y);
                // if 1 then is obstacle, -1 removes path tile
                //Vector3 targetPosition = new Vector3(x, 10, y) * cellSize + tileOffset;
                /*GameObject newTile = Instantiate(tileList[rnd], parent);
                newTile.transform.position = new Vector3(x, 0, y) * cellSize + tileOffset;
                //NavMeshSurface surface = newTile.GetComponent<NavMeshSurface>();
                
                //surface.BuildNavMesh();
                //placedTileSurfaces.Add(surface);
                PopulateDictionary(x + "," + y, rnd);
                placedTiles.Add(x + "," + y, newTile);*/

                yield return new WaitForSeconds(.005f);
            }
        }
        Debug.Log("Updating grid");
        placeTile.UpdateGrid(TileData);
    }

    private void PlaceTile(int rnd, int x, int y)
    {
        if (rnd < 9)
        {
            GameObject newTile = Instantiate(tileList[0], parent);
            newTile.transform.position = new Vector3(x, 0, y) * cellSize + tileOffset;
            PopulateDictionary(new Vector2Int(x,y), 0);
            placedTiles.Add(x + "," + y, newTile);
        }
        else
        {
            GameObject newTile = Instantiate(tileList[1], parent);
            newTile.transform.position = new Vector3(x, 0, y) * cellSize + tileOffset;
            PopulateDictionary(new Vector2Int(x,y), 1);//0 for tile, 1 for obs, 2 for player 1, 3 for player 2
            placedTiles.Add(x + "," + y, newTile);
        }
    }
    

    private void PopulateDictionary(Vector2Int position, int tile)
    {
        TileData.Add(position, tile);
    }

    public void UpdateDictionary(Vector2Int position, int player)
    {
        Debug.Log("Updating tiles");
        TileData[position] = player;
        TileController tileController = placedTiles[position.x + "," + position.y].GetComponent<TileController>();
        GameObject newTile = tileController.ReplaceSelf(tileList[player], parent);
        tileController.DestroySelf();
        placedTiles[position.x + "," + position.y] = newTile;
        /*if (isObs)
        {
            //TileData[position] = false;
            TileController tileController = placedTiles[position].GetComponent<TileController>();
            GameObject newTile = tileController.ReplaceSelf(tileList[0], parent);
            tileController.DestroySelf();
            placedTiles[position] = newTile;
        }
        else
        {
            //TileData[position] = true;
            TileController tileController = placedTiles[position].GetComponent<TileController>();
            GameObject newTile = tileController.ReplaceSelf(tileList[1], parent);
            tileController.DestroySelf();
            placedTiles[position] = newTile;
        }*/
    }

    private void UpdateTile(Vector2Int coords, int playerTurn)
    {
        TileData[coords] = playerTurn;
        placedTiles[coords.x + "," + coords.y] = tileList[playerTurn];
    }

    public Vector2Int ObsTileChecker(string xy, int value, bool playerTurn)
    {
        switch(xy)
        {
            case "x" :
                for (int i = 9; i > 0; i--)
                {
                    if (TileData[new Vector2Int(value, i)] == 0)
                    {
                        placePosition = new Vector2Int(value, i);
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            case "y" :
                for (int i = 9; i > 0; i--)
                {
                    if (TileData[new Vector2Int(i, value)] == 0)
                    {
                        placePosition = new Vector2Int(i, value);
                    }
                    else
                    {
                        break;
                    }
                }
                break;
        }
        
        Debug.Log("Checking dictionary");
        Debug.Log("placement = x:" + placePosition.x + " y:" + placePosition.y);
        //UpdateDictionary(placePosition, );
        return placePosition;
        //return TileData[position];
    }
    
}
