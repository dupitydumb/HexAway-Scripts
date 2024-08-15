using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupVideoLoading : MonoBehaviour
{
	private static PopupVideoLoading _instance;

	[HideInInspector]
	public UnityEvent actionShowVideo;

	[HideInInspector]
	public UnityEvent callbackVideoFail;

	private GameObject loadingPanel;

	private float showAdsTime;

	private float showAdsFailedTime;

	private string loadingText;

	private string notReadyText;

	public Text txtTitle;

	public static PopupVideoLoading instance => null;

	private void Start()
	{
	}

	public void DisplayVideoLoading(bool isShow)
	{
	}

	private void CheckVideo()
	{
	}

	private void CheckLoadingTimeout()
	{
	}
}
