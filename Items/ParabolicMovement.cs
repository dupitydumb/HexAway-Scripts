using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;

public class ParabolicMovement : MonoBehaviour
{
    public List<Transform> hexaList;

    public Transform hexa2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for(int i = 0; i < hexaList.Count; i++)
            {
                Fly(hexaList[i], i);
            }
        }
    }
    /*
     * thread : https://forum.unity.com/threads/generating-dynamic-parabola.211681/
     */
    Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        if (Mathf.Abs(start.y - end.y) < 0.1f)
        {
            //start and end are roughly level, pretend they are - simpler solution with less steps
            Vector3 travelDirection = end - start;
            Vector3 result = start + t * travelDirection;
            result.y += Mathf.Sin(t * Mathf.PI) * height;
            return result;
        }
        else
        {
            //start and end are not level, gets more complicated
            Vector3 travelDirection = end - start;
            Vector3 levelDirection = end - new Vector3(start.x, end.y, start.z);
            Vector3 right = Vector3.Cross(travelDirection, levelDirection);
            //Vector3 up = Vector3.Cross(right, travelDirection);
            Vector3 up = Vector3.Cross(right, levelDirection);
            if (end.y > start.y) up = -up;
            Vector3 result = start + t * travelDirection;
            result += (Mathf.Sin(t * Mathf.PI) * height) * up.normalized;
            return result;
        }
    }

    public void Fly(Transform hexaTrs, int queueTimer)
    {
        List<Vector3>  arcPoint = new List<Vector3>();

        for (int i = 0; i < 10; i++)
        {
            arcPoint.Add(SampleParabola(hexaTrs.localPosition, hexa2.localPosition, 2.5f, (float)i / 9.0f));
        }

        hexaTrs.DOLocalPath(arcPoint.ToArray(), 1.0f, PathType.Linear).SetDelay(queueTimer * 0.15f).SetLoops(1).SetEase(Ease.Linear).OnComplete(() =>
        {

        });

        hexaTrs.DOLocalRotate(new Vector3(0, 0, 180), 1.0f).SetRelative().SetDelay(queueTimer * 0.15f).SetEase(Ease.Linear).OnComplete(() =>
        {


        });

        /*
          rotateTw = nutModel.DOLocalRotate(new Vector3(0, 360, 0), 0.25f * Common.movingToTopNutSpeed).SetRelative().SetLoops(2 * tileDistance, LoopType.Incremental).SetEase(Ease.Linear).OnComplete(() =>
        {


        });

        */
}
    
}
