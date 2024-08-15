using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupSpin : MonoBehaviour
{
	public Text textAmountTicket;

	public Image imageAmountTicket;

	//public DataModelReward[] dataModelRewards;

	private System.Random rng;

	public Transform targetTranform;

	public Button btnStart;

	public Button btnHide;

	public const int spinTicketPrice = 5;

	private bool rotate;

	private bool rotationRoundEnd;

	private bool rotationEffectBegin;

	private float rotationAngle;

	private float rotationSpeed;

	private int rewardId;

	private int rotationRound;

	private int configCountId;

	private int configRoundStart;

	private int configRoundEnd;

	private float angleStop;

	private float[] percentRewards;

	private bool isFreeTurn;

	private void OnEnable()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnHideTapped()
	{
	}

	private void RefreshTicket()
	{
	}

	private void CheckStartButton()
	{
	}

	private void OnAdTapped()
	{
	}

	public void StartSpin()
	{
	}

	private bool StartedSpin()
	{
		return false;
	}

	private void OnSpinCompleted()
	{
	}

	public static int ChooseRandom(float[] probs)
	{
		return 0;
	}

	private void CheckHasReward()
	{
	}

	private void SetAngleStop()
	{
	}

	private void ShowReward()
	{
	}

	private bool LuckySpinRotation(int targetId)
	{
		return false;
	}
}
