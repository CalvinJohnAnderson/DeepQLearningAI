using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEngine;
using CodeMonkey.Utils;
//using Unity.Mathematics;
using Functions;
using UnityEngine.UIElements;

public class GridBuildingSystem : MonoBehaviour
{
    //[SerializeField] private PlayerDataManager playerDataManager;
    [SerializeField] private TileGenerater tileGenerater;
    [SerializeField] private Transform parent;
    private Vector2Int BaseOrigin = new Vector2Int(0, 0);
    private bool _playerTurn; //false = player1

    public Vector2Int GetBaseOrigin()
    {
        return BaseOrigin;
    }
    
    public static GridBuildingSystem Instance { get; private set; }

    private int gridWidth = 10;
    private int gridHeight = 10;
    private float cellSize = 10f;

    

    //[SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    //[SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOListP2;
    //private List<PlacedObjectTypeSO> _currentPlacedObjectTypeSoList = new List<PlacedObjectTypeSO>();
    
    //create new list that is equal to the list of buildings that the player has
    // this list will be the list that is used during that turn and is swapped at the end of the turn


    /*public List<string> BuildingList()
    {
        List<string> buildingNames = new List<string>();
        foreach (var building in placedObjectTypeSOList)
        {
            if (building != null)
            {
                buildingNames.Add(building.nameString);
            }
        }

        return buildingNames;
    }
    
    private PlacedObjectTypeSO placedObjectTypeSO;*/
    
    private GridXZ<GridObject> grid;
    //private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;
    
    public event EventHandler OnSelectedChanged;
    public event EventHandler OnObjectPlaced;
    private void Awake()
    {
        Instance = this;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero,
            (GridXZ<GridObject> g, int x, int z) => new GridObject(g,x, z));
        //defaults the current building to the first on the list;
        //_currentPlacedObjectTypeSoList = placedObjectTypeSOList;
        //placedObjectTypeSO = _currentPlacedObjectTypeSoList[0];
        _playerTurn = false;
    }

    private void Start()
    {
        //GameManager.OnPlayerTurnChange += TurnChange;
    }
    public void BuildGrid()
    {
        tileGenerater.BuildGrid();
    }

    public class GridObject
    {
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        //private PlacedObject placedObject;

        public GridObject(GridXZ<GridObject> grid, int x, int z)
        {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        /*public void SetPlacedObject(PlacedObject placedObject)
        {
            Debug.Log("Placing building");
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, z);
        }

        

        public PlacedObject GetPlacedObject()
        {
            return placedObject;
        }

        public void ClearPlacedObject()
        {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, z);
        }

        public bool CanBuild()
        {
            return placedObject == null;
        }*/

        public override string ToString()
        {
            return x + ", " + z + "\n";
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))//BuildGrid
        {
            Debug.Log("Building grid");
            BuildGrid();
        }
        /*if (Input.GetMouseButtonDown(0))
        {
            grid.GetXZ(GetMouseWorldPosition(), out int x, out int z);

            if (placedObjectTypeSO != null)//checks if building
            {
                List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, z), dir);

                //Test for building placement
                bool canBuild = true;
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                    {
                        canBuild = false;
                        break;
                    }
                    if (tileGenerater.ObsTileChecker(gridPosition.x + "," + gridPosition.y))
                    {
                        Debug.Log("Calling tile check");
                        canBuild = false;
                        break;
                    }
                }

                
                if (canBuild)
                {
                    Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                    Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();


                    PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, z), dir, placedObjectTypeSO, placedObjectTypeSO.BaseHealth);
                    playerDataManager.AddBuilding(placedObject);

                    foreach (Vector2Int gridPosition in gridPositionList)
                    {
                        grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                    }
                    placedObjectTypeSO = placedObjectTypeSOList[0]; RefreshSelectedObjectType();
                }
                else
                {
                    UtilsClass.CreateWorldTextPopup("Cannot build here!", GetMouseWorldPosition());
                }
            }
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            GridObject gridObject = grid.GetGridObject(GetMouseWorldPosition());
            PlacedObject placedObject = gridObject.GetPlacedObject();
            if (placedObject != null)
            {
                placedObject.DestroySelf();
                
                List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
                
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
                }
            }
        }
        
        //rotates the buildings
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            UtilsClass.CreateWorldTextPopup("" + dir, GetMouseWorldPosition());
        }

        //Changes the building being placed
        if (Input.GetKeyDown(KeyCode.Alpha1)) { placedObjectTypeSO = _currentPlacedObjectTypeSoList[0]; RefreshSelectedObjectType(); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { placedObjectTypeSO = _currentPlacedObjectTypeSoList[1]; RefreshSelectedObjectType(); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { placedObjectTypeSO = _currentPlacedObjectTypeSoList[2]; RefreshSelectedObjectType(); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { placedObjectTypeSO = _currentPlacedObjectTypeSoList[3]; RefreshSelectedObjectType(); }*/
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 99999f))
        {
            //Debug.Log(raycastHit.point);
            return raycastHit.point;
        }
        else
        {
            //Debug.Log("Not hitting anything");
            return Vector3.zero;
        }
    }
    
    /*private void DeselectObjectType() {
        placedObjectTypeSO = null; RefreshSelectedObjectType();
    }

    private void RefreshSelectedObjectType() {
        OnSelectedChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (placedObjectTypeSO != null) {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        } else {
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation() {
        if (placedObjectTypeSO != null) {
            return Quaternion.Euler(0, placedObjectTypeSO.GetRotationAngle(dir), 0);
        } else {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
        return placedObjectTypeSO;
    }*/
    
    public int GetGridWidth()
    {
        return gridWidth;
    }

    public int GetGridHeight()
    {
        return gridHeight;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    /*public void SetPlacedObjectTypeSO(int buildingNumber)
    {
        placedObjectTypeSO = placedObjectTypeSOList[buildingNumber]; RefreshSelectedObjectType();
    }

    public void PlacedLoadedBuildings(Dictionary<string, string> buildings)//change to accomodate for both players
    {
        playerDataManager.placedBuildings = new List<PlacedObject>();
        Debug.Log("Placing buildings in grid system");
        Debug.Log("Amount of buildings = " + buildings.Count);
        foreach (var building in buildings)
        {
            string[] buildingInfo = building.Key.Split(':');
            string[] buildingPosition = buildingInfo[0].Split(',', '(', ')');

            /*foreach (var info in buildingInfo)
            {
                Debug.Log("BuildingInfo = " + info);
            }
            
            switch (building.Value)
            {
                case "ArcherTower":
                    placedObjectTypeSO = placedObjectTypeSOList[1];
                    break;
                case "LargeCamp":
                    placedObjectTypeSO = placedObjectTypeSOList[2];
                    break;
                case "LongCamp":
                    placedObjectTypeSO = placedObjectTypeSOList[3];
                    break;
                case "SmallCamp":
                    placedObjectTypeSO = placedObjectTypeSOList[4];
                    break;
                case "Base":
                    placedObjectTypeSO = placedObjectTypeSOList[5];
                    break;
                
            }
            
            PlacedObjectTypeSO.Dir newDir = (PlacedObjectTypeSO.Dir)Enum.Parse(typeof(PlacedObjectTypeSO.Dir), buildingInfo[1]);
            
            //Debug.Log("Direction = " + newDir.ToString());

            if (placedObjectTypeSO == null)
            {
                Debug.Log("PlacedObjectTypeSo = null");
            }
            
            
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(newDir);
            
            //Debug.Log("RotationOffset = " + rotationOffset.x + "," + rotationOffset.y);
            
            Vector3 placedObjectWorldPosition = new Vector3(Convert.ToInt32(buildingPosition[1]),0, Convert.ToInt32(buildingPosition[2])) * cellSize + new Vector3(rotationOffset.x, 0, rotationOffset.y) * cellSize;
            
            PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, 
                new Vector2Int(Convert.ToInt32(buildingPosition[1]), 
                    Convert.ToInt32(buildingPosition[2])), newDir, placedObjectTypeSO, 
                Convert.ToSingle(buildingInfo[3]), Convert.ToInt32(buildingInfo[2]));
            playerDataManager.AddBuilding(placedObject);
            
            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(Convert.ToInt32(buildingPosition[1]), Convert.ToInt32(buildingPosition[2])), newDir);

            if (grid == null)
            {
                Debug.Log("Grid not found");
            }
            
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
            }
        }
        placedObjectTypeSO = placedObjectTypeSOList[0];
    }

    /*public void BuildBase()
    {
        placedObjectTypeSO = placedObjectTypeSOList[5];
        List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(BaseOrigin.x, BaseOrigin.y), dir);
        Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
        Vector3 placedObjectWorldPosition = grid.GetWorldPosition(BaseOrigin.x, BaseOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();


        PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(BaseOrigin.x, BaseOrigin.y), dir, placedObjectTypeSO, placedObjectTypeSO.BaseHealth);
        playerDataManager.AddBuilding(placedObject);

        foreach (Vector2Int gridPosition in gridPositionList)
        {
            grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
        }
        placedObjectTypeSO = placedObjectTypeSOList[0]; RefreshSelectedObjectType();
    }

    private void TurnChange()
    {
        Debug.Log("Changing player turns");
        if (_playerTurn)
        {
            _playerTurn = false;
            _currentPlacedObjectTypeSoList = placedObjectTypeSOList;
        }
        else
        {
            _playerTurn = true;
            _currentPlacedObjectTypeSoList = placedObjectTypeSOListP2;
        }
    }*/
}
