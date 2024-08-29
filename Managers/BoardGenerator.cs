using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class BoardGenerator : MonoBehaviour
{
    public float cellSpacing;

    public float XtileDistance;

    public float ZtileDistance;

    public MapDataLevelConfigSO levelConfig;

    public GameObject prefab_winStreakPower;

    public Transform cellHolder;

    public const int maxCountOfMapFile = 174;

    private int maxRow;

    public int goalNumber;

    public int currentGoalNumber;

    public int widthOfMap;

    public int heighOfMap;

    public int currentMapSlots;

    public List<BottomCell> bottomCellList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitBoardGenerator()
    {
        currentGoalNumber = 0;
        goalNumber = 0;
        widthOfMap = 0;
        maxRow = 0;
        GenMap();
    }

    public void GenMap()
    {
        levelConfig = Resources.Load("levels/map_" + GameManager.instance.levelIndex.ToString()) as MapDataLevelConfigSO;

        bottomCellList = new List<BottomCell>();

        for (int i = 0; i < levelConfig.LevelData.Cells.Count; i++)
        {
            BottomCell bottomCell = GameManager.instance.poolManager.GetBottomCell();
            bottomCell.transform.SetParent(transform);
            bottomCell.row = levelConfig.LevelData.Cells[i].Row;
            bottomCell.column = levelConfig.LevelData.Cells[i].Col;
            int row = levelConfig.LevelData.Cells[i].Row;
            int column = levelConfig.LevelData.Cells[i].Col;    
            int oddColumn = levelConfig.LevelData.Cells[i].Col % 2;
            if (oddColumn == 0)
            {
                bottomCell.transform.localPosition = new Vector3(levelConfig.LevelData.Cells[i].Col * XtileDistance, 0.0f,
                    levelConfig.LevelData.Cells[i].Row * ZtileDistance);
            }
            else
            {
                bottomCell.transform.localPosition = new Vector3(levelConfig.LevelData.Cells[i].Col * XtileDistance, 0.0f,
                        levelConfig.LevelData.Cells[i].Row * ZtileDistance + ZtileDistance * 0.5f);
            }

            if (levelConfig.LevelData.Cells[i].State == EnumStateOfBottomCell.RV)
                bottomCell.InitBottomCell(true);
            else
                bottomCell.InitBottomCell(false);

            bottomCell.CreateColumn();
            bottomCell.gameObject.GetComponentInChildren<TMP_Text>().text = row.ToString() + " / " + column.ToString();
            bottomCellList.Add(bottomCell);
            
            if (Mathf.Abs(bottomCell.column) > widthOfMap)
            {
                widthOfMap = Mathf.Abs(bottomCell.column);
            }

            if (Mathf.Abs(bottomCell.row) > heighOfMap)
            {
                heighOfMap = Mathf.Abs(bottomCell.row);
            }
        }

        SetVoidCells();
        GenerateHexaColumn();
        //Debug.Log("HEIGH OF MAP " + heighOfMap);
        SetCam();
        goalNumber = levelConfig.Goals[0].Target;
        currentMapSlots = levelConfig.LevelData.Cells.Count;
    }


    private void SetVoidCells()
    {
        int minRow = bottomCellList.Min(cell => cell.row);
        int maxRow = bottomCellList.Max(cell => cell.row);
        int minCol = bottomCellList.Min(cell => cell.column);
        int maxCol = bottomCellList.Max(cell => cell.column);

        foreach (var bottomCell in bottomCellList)
        {
            bool isOddColumn = bottomCell.column % 2 != 0;

            if (isOddColumn)
            {
                if (bottomCell.row == minRow || bottomCell.row == maxRow - 1 || bottomCell.column == minCol || bottomCell.column == maxCol)
                {
                    // Lower the cell y position to make it look like a void cell
                    bottomCell.transform.position = new Vector3(bottomCell.transform.position.x, -5f, bottomCell.transform.position.z);
                    bottomCell.isVoid = true;
                }
            }
            if (bottomCell.row == minRow || bottomCell.row == maxRow || bottomCell.column == minCol || bottomCell.column == maxCol)
            {
                // Lower the cell y position to make it look like a void cell
                bottomCell.transform.position = new Vector3(bottomCell.transform.position.x, -5f, bottomCell.transform.position.z);
                bottomCell.isVoid = true;
            }
        }
    }

    public void GenerateHexaColumn()
    {
        Debug.Log("GenerateHexaColumn");
        levelConfig = Resources.Load("levels/map_" + GameManager.instance.levelIndex.ToString()) as MapDataLevelConfigSO;

        for (int i = 0; i < levelConfig.HexagonConfig.hexaColumnDataList.Count; i++)
        {
            HexaColumnData hexaColumnData = levelConfig.HexagonConfig.hexaColumnDataList[i];
            HexaColumnData hexaDataTemp = new HexaColumnData();
            hexaDataTemp.axialCoordinates = hexaColumnData.axialCoordinates;
            hexaDataTemp.cellDirection = hexaColumnData.cellDirection;
            hexaDataTemp.columnDataList = new List<ColumnData>();
            for (int j = 0; j < hexaColumnData.columnDataList.Count; j++)
            {
                ColumnData columnData = new ColumnData(hexaColumnData.columnDataList[j].colorID, hexaColumnData.columnDataList[j].columnValue);
                hexaDataTemp.columnDataList.Add(columnData);
            }
            //Generate preset hexa column from the level config to the board
            //Search bottom cell with the same coordinates as the hexa column
            BottomCell bottomCell = bottomCellList.Find(cell => cell.row == levelConfig.HexagonConfig.hexaColumnDataList[i].axialCoordinates.x && cell.column == levelConfig.HexagonConfig.hexaColumnDataList[i].axialCoordinates.y);
            HexaColumn hexaColumn = bottomCell.hexaColumn;
            hexaColumn.CreateColumn(hexaDataTemp);
            hexaColumn.cellDirection = hexaColumnData.cellDirection;
            //rename the hexa column gameobject
            hexaColumn.gameObject.name = "HexaColumnTEST " + i;
        }
    }
    public void ClearMap()
    {
        for (int i = 0; i < bottomCellList.Count; i++)
        {
            bottomCellList[i].hexaColumn.ClearAllElements();
            bottomCellList[i].hexaColumn.cellHoder = null;
            bottomCellList[i].hexaColumn.currentBottomCell = null;
            GameManager.instance.poolManager.RemoveHexaColumn(bottomCellList[i].hexaColumn);

            bottomCellList[i].hexaColumn = null;
            GameManager.instance.poolManager.RemoveBottomCell(bottomCellList[i]);
        }
        bottomCellList.Clear();
    }

    private void SetCam()
    {
        if (widthOfMap == 2)
        {
            Camera.main.orthographicSize = 11;
            cellHolder.transform.localPosition = new Vector3(0.0f, 0.0f, -7.5f);
        }
        else if (widthOfMap == 3)
        {
            Camera.main.orthographicSize = 14;
            cellHolder.transform.localPosition = new Vector3(0.0f, 0.0f, -8.5f);
        }
        else
        {
            Camera.main.orthographicSize = 11;
            cellHolder.transform.localPosition = new Vector3(0.0f, 0.0f, -7.5f);
        }


        if (heighOfMap == 2)
        {
            Camera.main.orthographicSize = 11;
            cellHolder.transform.localPosition = new Vector3(0.0f, 0.0f, -7.5f);
        }
        else if (heighOfMap == 3)
        {
            Camera.main.orthographicSize = 13;
            cellHolder.transform.localPosition = new Vector3(0.0f, 0.0f, -9.25f);
        }
        else
        {
            Camera.main.orthographicSize = 11;
            cellHolder.transform.localPosition = new Vector3(0.0f, 0.0f, -7.5f);
        }
    }
}
