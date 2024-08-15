using UnityEngine;

public class PopupShop : BaseView
{
	
	public PopupQuestGiftReward giftReward;


    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }

    public override void InitView()
    {
        
    }

    public void BuyIAPPackage(Config.IAPPackageID packageID)
    {
        AudioManager.instance.clickSound.Play();

        IAPManager.instance.BuyConsumable(packageID, (string iapID, IAPManager.IAP_CALLBACK_STATE state) =>
        {
            if (state == IAPManager.IAP_CALLBACK_STATE.SUCCESS)
            {

                Debug.Log("SUCCESSSUCCESS " + iapID);

                if (iapID.Equals(Config.IAPPackageID.NoAds.ToString()))
                {
                    Debug.Log("REMOVE ADS");
                    AdsControl.Instance.RemoveAds();
                }
                else
                {
                    BuySuccesss(packageID);
                }
            }
            else
            {
                Debug.Log("Buy Fail!");

            }
        });



    }

    public void BuySuccesss(Config.IAPPackageID packageID)
    {
        switch (packageID)
        {
            case Config.IAPPackageID.start_pack:
                GameManager.instance.AddCoin(500);
                GameManager.instance.AddHammerBooster(2);
                GameManager.instance.AddMoveBooster(2);
                GameManager.instance.AddShuffleBooster(2);
                break;

            case Config.IAPPackageID.NoAds:
                RemoveAds();
                break;

            case Config.IAPPackageID.coin_150:
                GameManager.instance.AddCoin(150);
                break;

            case Config.IAPPackageID.coin_700:
                GameManager.instance.AddCoin(700);
                break;

            case Config.IAPPackageID.coin_1800:
                GameManager.instance.AddCoin(1800);
                break;

            case Config.IAPPackageID.coin_4000:
                GameManager.instance.AddCoin(4000);
                break;

            case Config.IAPPackageID.coin_7000:
                GameManager.instance.AddCoin(7000);
                break;

            case Config.IAPPackageID.coin_15000:
                GameManager.instance.AddCoin(15000);
                break;

            case Config.IAPPackageID.live_30:
                GameManager.instance.livesManager.GiveInifinite(30);
                break;

            case Config.IAPPackageID.live_1day:
                GameManager.instance.livesManager.GiveInifinite(24*60);
                break;

            case Config.IAPPackageID.live_7day:
                GameManager.instance.livesManager.GiveInifinite(7 * 24 * 60);
                break;

            case Config.IAPPackageID.hammer_2:
                GameManager.instance.AddHammerBooster(2);
                break;

            case Config.IAPPackageID.hammer_30:
                GameManager.instance.AddHammerBooster(30);
                break;

            case Config.IAPPackageID.move_4:
                GameManager.instance.AddMoveBooster(4);
                break;

            case Config.IAPPackageID.move_40:
                GameManager.instance.AddMoveBooster(40);
                break;

            case Config.IAPPackageID.shuffle_10:
                GameManager.instance.AddShuffleBooster(10);
                break;

            case Config.IAPPackageID.shuffle_60:
                GameManager.instance.AddShuffleBooster(60);
                break;
            case Config.IAPPackageID.special_offer:
                GameManager.instance.AddCoin(500);
                GameManager.instance.AddHammerBooster(2);
                GameManager.instance.AddMoveBooster(2);
                GameManager.instance.AddShuffleBooster(2);
                break;
        }
    }

    public void RemoveAds()
    {
        AudioManager.instance.clickSound.Play();
        BuyIAPPackage(Config.IAPPackageID.NoAds);
        Debug.Log("Remove Ads");
    }

    public void Restore()
    {
        AudioManager.instance.clickSound.Play();
        IAPManager.instance.RestorePurchases();
        Debug.Log("Restore");
    }

    public void PurchaseItem(int index)
    {
        BuyIAPPackage((Config.IAPPackageID)index);
      
    }

}
