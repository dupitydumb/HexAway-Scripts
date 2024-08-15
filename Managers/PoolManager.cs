using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public Transform poolParent = null;

    //for bottom
    [Range(1, 30)]
    public int initBottomCellAmount;
    [HideInInspector]
    public List<BottomCell> bottomCellPools = null;
    public BottomCell bottomCellPrefab;

    //for hexa cells
    [Range(1, 50)]
    public int initHexCellAmount;
    [HideInInspector]
    public List<HexaCell> hexaCellPools = null;
    public HexaCell hexaCellPrefab;

    //for hexa column
    [Range(1, 30)]
    public int initHexaColumnAmount;
    [HideInInspector]
    public List<HexaColumn> hexaColumnPools = null;
    public HexaColumn hexaColumnPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region INIT POOL
    public void InitPool()
    {
        //for bottom cell
        bottomCellPools = new List<BottomCell>();

        for(int i = 0; i < initBottomCellAmount; i++)
        {
            BottomCell bottomCell = Instantiate(bottomCellPrefab);
            bottomCell.transform.SetParent(poolParent);
            bottomCell.transform.localPosition = Vector3.zero;
            bottomCell.transform.localRotation = Quaternion.Euler(0, 0, 0);
            bottomCell.transform.localScale = Vector3.one;
            bottomCell.gameObject.SetActive(false);
            bottomCellPools.Add(bottomCell);
        }

        //for hexa cell
        hexaCellPools = new List<HexaCell>();

        for (int i = 0; i < initHexCellAmount; i++)
        {
            HexaCell hexaCell = Instantiate(hexaCellPrefab);
            hexaCell.transform.SetParent(poolParent);
            hexaCell.transform.localPosition = Vector3.zero;
            hexaCell.transform.localRotation = Quaternion.Euler(0, 0, 0);
            hexaCell.transform.localScale = Vector3.one;
            hexaCell.gameObject.SetActive(false);
            hexaCellPools.Add(hexaCell);
        }

        //for hexa column
        hexaColumnPools = new List<HexaColumn>();

        for (int i = 0; i < initHexaColumnAmount; i++)
        {
            HexaColumn hexaColumn = Instantiate(hexaColumnPrefab);
            hexaColumn.transform.SetParent(poolParent);
            hexaColumn.transform.localPosition = Vector3.zero;
            hexaColumn.transform.localRotation = Quaternion.Euler(0, 0, 0);
            hexaColumn.transform.localScale = Vector3.one;
            hexaColumn.gameObject.SetActive(false);
            hexaColumnPools.Add(hexaColumn);
        }
    }
    #endregion

    #region BOTTOM CELL POOL
    public BottomCell GetBottomCell()
    {
        BottomCell bottomCell = null;

        if(bottomCellPools.Count > 0)
        {
            bottomCell = bottomCellPools[0];
            bottomCellPools.RemoveAt(0);
            bottomCell.transform.SetParent(null);
            bottomCell.gameObject.SetActive(true);
        }
        else
        {
            bottomCell = Instantiate(bottomCellPrefab);
            bottomCell.transform.SetParent(null);
            bottomCell.gameObject.SetActive(true);
            bottomCell.transform.localPosition = Vector3.zero;
            bottomCell.transform.localRotation = Quaternion.Euler(0, 0, 0);
            bottomCell.transform.localScale = Vector3.one;
        }

        return bottomCell;
    }

    public void RemoveBottomCell(BottomCell bottomCell)
    {
        bottomCell.transform.SetParent(poolParent);
        bottomCell.transform.localPosition = Vector3.zero;
        bottomCell.transform.localRotation = Quaternion.Euler(0, 0, 0);
        bottomCell.transform.localScale = Vector3.one;
        bottomCell.gameObject.SetActive(false);
        bottomCellPools.Add(bottomCell);
    }
    #endregion

    #region HEXA CELL POOL
    public HexaCell GetHexaCell()
    {
        HexaCell  hexaCell = null;

        if (hexaCellPools.Count > 0)
        {
            hexaCell = hexaCellPools[0];
            hexaCellPools.RemoveAt(0);
            hexaCell.transform.SetParent(null);
            hexaCell.gameObject.SetActive(true);
        }
        else
        {
            hexaCell = Instantiate(hexaCellPrefab);
            hexaCell.transform.SetParent(null);
            hexaCell.gameObject.SetActive(true);
            hexaCell.transform.localPosition = Vector3.zero;
            hexaCell.transform.localRotation = Quaternion.Euler(0, 0, 0);
            hexaCell.transform.localScale = Vector3.one;
        }

        return hexaCell;
    }

    public void RemoveHexaCell(HexaCell hexaCell)
    {
        hexaCell.transform.SetParent(poolParent);
        hexaCell.transform.localPosition = Vector3.zero;
        hexaCell.transform.localRotation = Quaternion.Euler(0, 0, 0);
        hexaCell.transform.localScale = Vector3.one;
        hexaCell.gameObject.SetActive(false);
        hexaCellPools.Add(hexaCell);
    }
    #endregion

    #region HEXA COLUMN POOL
    public HexaColumn GetHexaColumn()
    {
        HexaColumn hexaColumn = null;

        if (hexaColumnPools.Count > 0)
        {
            hexaColumn = hexaColumnPools[0];
            hexaColumnPools.RemoveAt(0);
            hexaColumn.transform.SetParent(null);
            hexaColumn.gameObject.SetActive(true);
        }
        else
        {
            hexaColumn = Instantiate(hexaColumnPrefab);
            hexaColumn.transform.SetParent(null);
            hexaColumn.gameObject.SetActive(true);
            hexaColumn.transform.localPosition = Vector3.zero;
            hexaColumn.transform.localRotation = Quaternion.Euler(0, 0, 0);
            hexaColumn.transform.localScale = Vector3.one;
        }

        return hexaColumn;
    }

    public void RemoveHexaColumn(HexaColumn hexaColumn)
    {
        hexaColumn.transform.SetParent(poolParent);
        hexaColumn.transform.localPosition = Vector3.zero;
        hexaColumn.transform.localRotation = Quaternion.Euler(0, 0, 0);
        hexaColumn.transform.localScale = Vector3.one;
        hexaColumn.gameObject.SetActive(false);
        hexaColumnPools.Add(hexaColumn);
    }
    #endregion
}
