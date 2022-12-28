using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetSizePopup : Popup
{
    public struct SetSizePopupData
    {
        public Action<Vector3> callback;
        public SetSizePopupData(Action<Vector3> callback)
        {
            this.callback = callback;
        }
    }

    [SerializeField] private Button confirmButton;
    [SerializeField] private TMP_InputField widthField;
    [SerializeField] private TMP_InputField heightField;
    [SerializeField] private TMP_InputField depthField;

    private SetSizePopupData data;

	public override void Initialize(object data)
	{
		base.Initialize(data);
        this.data = (SetSizePopupData)data;

        widthField.onValueChanged.AddListener(OnValueChanged);
		heightField.onValueChanged.AddListener(OnValueChanged);
		depthField.onValueChanged.AddListener(OnValueChanged);

        confirmButton.onClick.AddListener(Confirm);

        UpdateUI();
	}

    private void OnValueChanged(string value)
    {
        UpdateUI();
    }

	private void UpdateUI()
    {
        bool canConfirm = !(string.IsNullOrEmpty(widthField.text) ||
                            string.IsNullOrEmpty(heightField.text) ||
							string.IsNullOrEmpty(depthField.text));
        confirmButton.interactable = canConfirm;
    }

    private void Confirm()
    {
        Close();
        data.callback.Invoke(ParseSize());
    }

    private Vector3 ParseSize()
    {
        Vector3 result;
        result.x = float.Parse(widthField.text) / 100;
        result.y = float.Parse(heightField.text) / 100;
        result.z = float.Parse(depthField.text) / 100;
        return result;
    }
}
