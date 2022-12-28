using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Product;

[RequireComponent(typeof(Button))]
public class ProductView : MonoBehaviour
{

	public Action<ProductData> OnClicked;

    [SerializeField] private Image previewView;
    [SerializeField] private TMP_Text nameView;

	private ProductData product;

	public void Initialize(ProductData product)
	{
		this.product = product;
		previewView.sprite = product.previewSprite;
		nameView.text = product.name;

		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	private void OnClick()
	{
		OnClicked(product);
	}
}
