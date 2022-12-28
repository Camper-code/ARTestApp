using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhonePopup : Popup
{
	[SerializeField] Transform animationTransform;

	private void Update()
	{
		animationTransform.localPosition = Vector3.right * Mathf.Sin(Time.time / 3) * 100;
	}
}
