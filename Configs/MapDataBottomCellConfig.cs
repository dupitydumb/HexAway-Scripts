using System;
using System.Collections.Generic;

[Serializable]
public class MapDataBottomCellConfig
{
	public int Row;

	public int Col;

	public List<EnumHexaColorType> Types;

	public int Cost;

	public int CoinCost;

	public int NeighbourSortPriority;

	public EnumHexaColorType RequiredType;

	public EnumStateOfBottomCell State;
}
