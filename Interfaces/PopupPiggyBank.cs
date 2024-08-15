using UnityEngine;
using UnityEngine.UI;

public class PopupPiggyBank : BasePopup
{
	[SerializeField]
	private Text textProgress;

	[SerializeField]
	private Text textPrice;

	[SerializeField]
	private Image imageValue;

	[SerializeField]
	private Button buttonBuy;

	public const int maxGoldAmount = 300;

	private const int iapPackId = 8;

    public override void Start()
    {
       
    }

    public override void Update()
    {
       
    }

    public override void InitView()
    {
       
    }
}
