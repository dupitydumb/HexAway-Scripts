using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using static AdsControl;
using UnityEngine.Advertisements;

public class BottomCell : MonoBehaviour
{
    public int row;
    public int column;

    //[HideInInspector]
    public HexaColumn hexaColumn;

    public MeshRenderer meshRenderer;

    public Material cellMaterial;

    public Material cellSelectedMaterial;

    public Material lockMaterial;

    public GameObject lockObj;

    public bool isVoid;
    public bool isLock;

    // Start is called before the first frame update
    void Start()
    {
        GetNearCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateColumn()
    {
        hexaColumn = GameManager.instance.poolManager.GetHexaColumn();
        hexaColumn.InitColumn();
        hexaColumn.transform.SetParent(transform);
        hexaColumn.transform.localPosition = Vector3.zero;
        hexaColumn.currentBottomCell = this;
    }

    public void InitBottomCell(bool isLockCell)
    {
        nearCellList = new List<BottomCell>();
        UnSelectCell();
        isLock = isLockCell;
        SetAxialCoordinates(row, column);
        if (isLock)
            LockCell();
        else
            OpenCell();
    }

    public void ClearBottomCell()
    {
        hexaColumn = null;
    }    

    public void SetAxialCoordinates(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public void SelectCell()
    {
        meshRenderer.material = cellSelectedMaterial;
        meshRenderer.transform.localPosition = new Vector3(0, 0.25f, 0);
    }

    public void UnSelectCell()
    {
        meshRenderer.material = cellMaterial;
        meshRenderer.transform.localPosition = new Vector3(0, 0.0f, 0);
    }

    private void LockCell()
    {
        meshRenderer.material = lockMaterial;
        lockObj.SetActive(true);
    }

    private void OpenCell()
    {
        isLock = false;
        meshRenderer.material = cellMaterial;
        lockObj.SetActive(false);
    }

    public void UnLockCell()
    {
        if (GameManager.instance.currentGameState != GameManager.GAME_STATE.PLAYING)
            return;

        WatchAds();
    }

    public LayerMask bottomMask;

    public List<BottomCell> nearCellList;

    public void GetNearCells()
    {
        nearCellList.Clear();

        for(int i = 0; i < 6; i++)
        {
            Ray ray = new Ray(transform.position, Quaternion.Euler(0, 60.0f * i, 0) * transform.forward);
            RaycastHit hitData;

            if (Physics.Raycast(ray, out hitData, 1.5f, bottomMask))
            {
                //Debug.Log("HIT " + hitData.transform.name);
                BottomCell nearCell = hitData.transform.GetComponent<BottomCell>();
                if(nearCell.hexaColumn.hexaCellList.Count > 0 && nearCell.hexaColumn.topColorID == hexaColumn.topColorID && nearCell.hexaColumn.topColorID != -1)
                {
                    nearCellList.Add(hitData.transform.GetComponent<BottomCell>());
                    //hitData.transform.GetComponent<BottomCell>().SelectCell();
                }
                
            }
        }

    }

    public void WatchAds()
    {
        AudioManager.instance.clickSound.Play();
        if (AdsControl.Instance.currentAdsType == ADS_TYPE.ADMOB)
        {
            if (AdsControl.Instance.rewardedAd != null)
            {
                if (AdsControl.Instance.rewardedAd.CanShowAd())
                {
                    AdsControl.Instance.ShowRewardAd(EarnReward);
                }
            }
        }
        else if (AdsControl.Instance.currentAdsType == ADS_TYPE.UNITY)
        {
            ShowRWUnityAds();
        }
        else if (AdsControl.Instance.currentAdsType == ADS_TYPE.MEDIATION)
        {
            if (AdsControl.Instance.rewardedAd.CanShowAd())

                AdsControl.Instance.ShowRewardAd(EarnReward);

            else
                ShowRWUnityAds();
        }
    }

    public void EarnReward(Reward reward)
    {
        AudioManager.instance.rewardDone.Play();
        isLock = false;
        meshRenderer.material = cellMaterial;
        lockObj.SetActive(false);
    }

    public void ShowRWUnityAds()
    {
        AdsControl.Instance.PlayUnityVideoAd((string ID, UnityAdsShowCompletionState callBackState) =>
        {

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                isLock = false;
                meshRenderer.material = cellMaterial;
                lockObj.SetActive(false);
            }

            if (ID.Equals(AdsControl.Instance.adUnityRWUnitId) && callBackState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                AdsControl.Instance.LoadUnityAd();
            }

        });
    }

}
