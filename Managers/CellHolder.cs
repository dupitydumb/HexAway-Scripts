using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

public class CellHolder : MonoBehaviour
{
    public List<HexaColumn> hexaColumnList;

    public HexaColumn hexaColumn;

    public int currentSlots;

    public enum DIFFICULT_LEVEL
    {
        EASY,
        MEDIUM,
        HARD
    };

    public DIFFICULT_LEVEL currentDifficult;

    private float probabilityValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitHolder()
    {
        currentSlots = 3;
        hexaColumnList = new List<HexaColumn>();
        CreateFirstPiece();

        if (GameManager.instance.boardGenerator.levelConfig.Difficulty == EnumConfigDiffTypeOfMap.Easy)
            currentDifficult = DIFFICULT_LEVEL.EASY;
        else if (GameManager.instance.boardGenerator.levelConfig.Difficulty == EnumConfigDiffTypeOfMap.Medium)
            currentDifficult = DIFFICULT_LEVEL.MEDIUM;
        else if (GameManager.instance.boardGenerator.levelConfig.Difficulty == EnumConfigDiffTypeOfMap.Hard)
            currentDifficult = DIFFICULT_LEVEL.HARD;

        switch (currentDifficult)
        {
            case DIFFICULT_LEVEL.EASY:
                probabilityValue = 0.75f;
                break;
            case DIFFICULT_LEVEL.MEDIUM:
                probabilityValue = 0.5f;
                break;
            case DIFFICULT_LEVEL.HARD:
                probabilityValue = 0.25f;
                break;
            default:
                probabilityValue = 0.25f;
                break;
        }

       // Debug.Log("PROB VALUE " + probabilityValue);
    }

    private void CreateFirstPiece()
    {
        for (int i = 0; i < currentSlots; i++)
        {
            HexaColumnData firstPiece = new HexaColumnData();
            firstPiece.columnDataList = new List<ColumnData>();

            ColumnData randomData1 = new ColumnData(Random.Range(1, 9), Random.Range(2, 4));
            ColumnData randomData2 = new ColumnData(Random.Range(1, 9), Random.Range(2, 5));

            firstPiece.columnDataList.Add(randomData1);
            firstPiece.columnDataList.Add(randomData2);

            HexaColumnData mergePiece = new HexaColumnData();
            mergePiece.columnDataList = new List<ColumnData>();

            for(int m = 0; m < firstPiece.columnDataList.Count; m++)
            {
                if(m == 0)
                {
                    mergePiece.columnDataList.Add(new ColumnData(firstPiece.columnDataList[0].colorID, firstPiece.columnDataList[0].columnValue));
                }
                else
                {
                    if(firstPiece.columnDataList[m].colorID == mergePiece.columnDataList[mergePiece.columnDataList.Count - 1].colorID)
                    {
                        mergePiece.columnDataList[mergePiece.columnDataList.Count - 1].columnValue += firstPiece.columnDataList[m].columnValue;
                    }
                    else
                    {
                        mergePiece.columnDataList.Add(new ColumnData(firstPiece.columnDataList[m].colorID, firstPiece.columnDataList[m].columnValue));
                    }
                }
            }

            HexaColumn column = GameManager.instance.poolManager.GetHexaColumn();
            column.InitColumn();
            column.transform.SetParent(transform);
            column.transform.localPosition = new Vector3((i - 1) * 3.0f, 0, 0);
            column.cellHoder = this;
            column.positionInHoler = column.transform.localPosition;
            column.CreateColumn(mergePiece);
            column.cellHoder = this;
            hexaColumnList.Add(column);
            column.gameObject.SetActive(false);
            StartCoroutine(ColumnAppear(column.gameObject, i));
        }
    }

    private IEnumerator ColumnAppear(GameObject column, int queueDelay)
    {
        yield return new WaitForSeconds(queueDelay * 0.15f);
        AudioManager.instance.columnSpawnSfx.Play();
        column.gameObject.SetActive(true);
        column.transform.localScale = 0.5f * Vector3.one;
        column.transform.DOScale(Vector3.one, 0.15f).SetEase(Ease.Linear);
    }

    private void CreateNextPiece()
    {
       
        for (int i = 0; i < currentSlots; i++)
        {
            HexaColumnData firstPiece = new HexaColumnData();
            firstPiece.columnDataList = new List<ColumnData>();

            if (GameManager.instance.boardController.hexaColumnsInMap.Count > i)
            {
                if(GameManager.instance.boardController.hexaColumnsInMap.Count <= 3)
                {
                    for (int j = 0; j < GameManager.instance.boardController.hexaColumnsInMap[i].currentHexaColumnData.columnDataList.Count; j++)
                    {
                        int clampValue = GameManager.instance.boardController.hexaColumnsInMap[i].currentHexaColumnData.columnDataList[j].columnValue;

                        float randomValue1 = Random.Range(0, 1.0f);
                        if(randomValue1 <= probabilityValue)
                        {
                            clampValue = GameManager.instance.boardController.hexaColumnsInMap[i].currentHexaColumnData.columnDataList[j].columnValue;
                        }
                        else
                        {
                            clampValue = Random.Range(1, 7);
                        }

                        clampValue = Mathf.Clamp(clampValue, 2, 4);

                        int colorID = GameManager.instance.boardController.hexaColumnsInMap[i].currentHexaColumnData.columnDataList[j].colorID;
                        float randomValue2 = Random.Range(0, 1.0f);
                        if (randomValue2 <= probabilityValue)
                        {
                            colorID = GameManager.instance.boardController.hexaColumnsInMap[i].currentHexaColumnData.columnDataList[j].colorID;
                        }
                        else
                        {
                            colorID = Random.Range(1, 9);
                        }

                        ColumnData columnData = new ColumnData(colorID, clampValue);
                        firstPiece.columnDataList.Add(columnData);

                       
                    }
                       
                }
                else
                {
                    int randomIndex = Random.Range(0, GameManager.instance.boardController.hexaColumnsInMap.Count);
                    for (int j = 0; j < GameManager.instance.boardController.hexaColumnsInMap[randomIndex].currentHexaColumnData.columnDataList.Count; j++)
                    {
                        int clampValue = GameManager.instance.boardController.hexaColumnsInMap[randomIndex].currentHexaColumnData.columnDataList[j].columnValue;

                        float randomValue1 = Random.Range(0, 1.0f);
                        if (randomValue1 <= probabilityValue)
                        {
                            clampValue = GameManager.instance.boardController.hexaColumnsInMap[randomIndex].currentHexaColumnData.columnDataList[j].columnValue;
                        }
                        else
                        {
                            clampValue = Random.Range(1, 7);
                        }

                        clampValue = Mathf.Clamp(clampValue, 2, 4);

                        int colorID = GameManager.instance.boardController.hexaColumnsInMap[randomIndex].currentHexaColumnData.columnDataList[j].colorID;
                        float randomValue2 = Random.Range(0, 1.0f);
                        if (randomValue2 <= probabilityValue)
                        {
                            colorID = GameManager.instance.boardController.hexaColumnsInMap[randomIndex].currentHexaColumnData.columnDataList[j].colorID;
                        }
                        else
                        {
                            colorID = Random.Range(1, 9);
                        }

                        ColumnData columnData = new ColumnData(colorID, clampValue);
                        firstPiece.columnDataList.Add(columnData);

                    }
                }
                       
            }
            else
            {
                ColumnData randomData1 = new ColumnData(Random.Range(1, 9), Random.Range(2, 4));
                ColumnData randomData2 = new ColumnData(Random.Range(1, 9), Random.Range(2, 5));

                if (randomData1.colorID == randomData2.colorID)
                {
                    ColumnData randomData3 = new ColumnData(randomData1.colorID, randomData1.columnValue + randomData2.colorID);
                    firstPiece.columnDataList.Add(randomData3);
                }
                else
                {
                    firstPiece.columnDataList.Add(randomData1);
                    firstPiece.columnDataList.Add(randomData2);
                }
            }

            HexaColumnData mergePiece = new HexaColumnData();
            mergePiece.columnDataList = new List<ColumnData>();

            for (int m = 0; m < firstPiece.columnDataList.Count; m++)
            {
                if (m == 0)
                {
                    mergePiece.columnDataList.Add(new ColumnData(firstPiece.columnDataList[0].colorID, firstPiece.columnDataList[0].columnValue));
                }
                else
                {
                    if (firstPiece.columnDataList[m].colorID == mergePiece.columnDataList[mergePiece.columnDataList.Count - 1].colorID)
                    {
                        mergePiece.columnDataList[mergePiece.columnDataList.Count - 1].columnValue += firstPiece.columnDataList[m].columnValue;
                    }
                    else
                    {
                        mergePiece.columnDataList.Add(new ColumnData(firstPiece.columnDataList[m].colorID, firstPiece.columnDataList[m].columnValue));
                    }
                }
            }

            HexaColumn column = GameManager.instance.poolManager.GetHexaColumn();
            column.InitColumn();
            column.transform.SetParent(transform);
            column.transform.localPosition = new Vector3((i - 1) * 3.0f, 0, 0);
            column.cellHoder = this;
            column.positionInHoler = column.transform.localPosition;
            column.CreateColumn(mergePiece);
            hexaColumnList.Add(column);
            column.gameObject.SetActive(false);
            StartCoroutine(ColumnAppear(column.gameObject, i));
        }
    }

    public void CheckPiecesInHolder()
    {
        currentSlots--;
        //Debug.Log("CURRENT SLOTS : " + currentSlots);
        if (currentSlots == 0)
        {
            currentSlots = 3;
            CreateNextPiece();
        }
    }

    public void ClearCellHolder()
    {
        for (int i = 0; i < hexaColumnList.Count; i++)
        {
            hexaColumnList[i].ClearAllElements();
            hexaColumnList[i].cellHoder = null;
            hexaColumnList[i].currentBottomCell = null;
            GameManager.instance.poolManager.RemoveHexaColumn(hexaColumnList[i]);
        }
           
    }

    public void ShuffleHolder()
    {
        ClearCellHolder();
        currentSlots = 3;
        CreateFirstPiece();
    }
}
