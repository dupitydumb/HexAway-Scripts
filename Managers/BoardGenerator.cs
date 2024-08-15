using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //Debug.Log("HEIGH OF MAP " + heighOfMap);
        SetCam();
        goalNumber = levelConfig.Goals[0].Target;
        currentMapSlots = levelConfig.LevelData.Cells.Count;
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
