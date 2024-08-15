using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ColorConfigSO", menuName = "GameData/ColorConfigSO")]
public class ColorConfig : ScriptableObject
{
    public List<ColorItem> colorList;
}
[Serializable]
public class ColorItem
{
    public int colorID;

    public EnumHexaColorType colorType;

    public Color colorValue;

    public Material material;

    public Sprite surfaceSpr;
}