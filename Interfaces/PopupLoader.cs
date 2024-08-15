using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PopupLoader : MonoBehaviour
{
	
	public string popupFileName;

	public UnityEvent actionHideCompleted;

	private void OnEnable()
	{
	}

	public void ForceLoadPopup()
	{
	}

	public GameObject LoadPopup()
	{
		return null;
	}


	public void DestroyPopup()
	{
	}

	private string GetPrefabName()
	{
		return null;
	}

	public static PopupLoader GetInstanceForPopup(string popupName)
	{
		return null;
	}
}
