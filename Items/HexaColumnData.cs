using System.Collections.Generic;
using System;

[Serializable]
public class HexaColumnData
{
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