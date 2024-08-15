using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataLevelConfigSO", menuName = "GameData/MapDataLevelConfigSO")]
public class MapDataLevelConfigSO : ScriptableObject
{
	public EnumModeGameplayOfMap LevelMode;

	public int Time;

	public int Moves;

	public int PreCreatedHex;

	public float FlipTime;

	public int TutorialSteps;

	public List<DataConfigMaxGoalOfMap> Goals;

	public Vector2Int[] CellsForTutorial;

	public EnumConfigDiffTypeOfMap Difficulty;

	public DataConfigDiffOfMapLevel[] Thresholds;

	public ConfigMapDataDO LevelData;
}
