using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class HexaColumnData
{
    public CELL_DIRECTION cellDirection;
    public Vector2Int axialCoordinates;
    public List<ColumnData> columnDataList;
}

[Serializable]
public class ColumnData
{
    public int colorID;

    public int columnValue;

    public ColumnData(int mColorID, int mColumnValue)
    {
        colorID = mColorID;
        columnValue = mColumnValue;
    }
}