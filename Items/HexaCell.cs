using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public SpriteRenderer icon1, icon2;
  
    public void InitCell(int colorID)
    {
        meshRenderer.material = GameManager.instance.colorConfig.colorList[colorID].material;
        icon1.sprite = GameManager.instance.colorConfig.colorList[colorID].surfaceSpr;
        icon2.sprite = GameManager.instance.colorConfig.colorList[colorID].surfaceSpr;
    }
}
