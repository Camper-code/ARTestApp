using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Product;

public class CatalogView : MonoBehaviour
{
    public Action<ProductData> OnProductSelected;
	[SerializeField] private ProductView productViewPrefab;
    [SerializeField] private RectTransform content;

    public void AddProduct(ProductData product)
    {
		ProductView productView = Instantiate(productViewPrefab.gameObject, content).GetComponent<ProductView>();
        productView.Initialize(product);
        productView.OnClicked = OnProductSelected;

	}
}
