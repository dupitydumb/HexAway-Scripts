using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaCell : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public SpriteRenderer icon1, icon2;
  
    private HexaColumn parentHexaColumn;
    public void InitCell(int colorID)
    {
        meshRenderer.material = GameManager.instance.colorConfig.colorList[colorID].material;
        // icon1.sprite = GameManager.instance.colorConfig.colorList[colorID].surfaceSpr;
        // icon2.sprite = GameManager.instance.colorConfig.colorList[colorID].surfaceSpr;
        //Get the parent column

        SetArrow();
        Debug.Log("InitCell");
    }

    public void SetArrow()
    {
        parentHexaColumn = GetComponentInParent<HexaColumn>();

        if (parentHexaColumn == null)
        {
            Debug.LogError("parentHexaColumn is null");
            return;
        }

        Debug.Log("parentHexaColumn.cellDirection: " + parentHexaColumn.cellDirection);

        //Rotate the spirte based on the column's cellDirection
        switch (parentHexaColumn.cellDirection)
        {
            case CELL_DIRECTION.UP:
                icon1.transform.rotation = Quaternion.Euler(90, 0, 0);
                icon2.transform.rotation = Quaternion.Euler(90, 0, 0);
                break;
            case CELL_DIRECTION.UP_RIGHT:
                icon1.transform.rotation = Quaternion.Euler(90, 60, 0);
                icon2.transform.rotation = Quaternion.Euler(90, 60, 0);
                break;
            case CELL_DIRECTION.DOWN_RIGHT:
                icon1.transform.rotation = Quaternion.Euler(90, 130, 0);
                icon2.transform.rotation = Quaternion.Euler(90, 130, 0);
                break;
            case CELL_DIRECTION.DOWN:
                icon1.transform.rotation = Quaternion.Euler(90, 180, 0);
                icon2.transform.rotation = Quaternion.Euler(90, 180, 0);
                break;
            case CELL_DIRECTION.DOWN_LEFT:
                icon1.transform.rotation = Quaternion.Euler(90, -130, 0);
                icon2.transform.rotation = Quaternion.Euler(90, -130, 0);
                break;
            case CELL_DIRECTION.UP_LEFT:
                icon1.transform.rotation = Quaternion.Euler(90, -60, 0);
                icon2.transform.rotation = Quaternion.Euler(90, -60, 0);
                break;

            default:
                Debug.LogError("Unknown cellDirection: " + parentHexaColumn.cellDirection);
                break;
        }
    }
}
