using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loader;
using Product;

public class GameCore : MonoBehaviour
{
    [SerializeField] private ProductLoader productLoader;
	[SerializeField] private CatalogView view;
	[SerializeField] private ARSpaceController spaceController;
	[SerializeField] private PopupManager popupManager;

	private ProductData currentProduct;

	private void Start()
	{
		view.OnProductSelected = OnProductSelected;

		productLoader.OnOneProductLoaded = view.AddProduct;
		productLoader.StartLoadProducts();

	}

	private void OnProductSelected(ProductData product)
	{
		
		currentProduct = product;

		popupManager.Show(PopupType.SetSize, new SetSizePopup.SetSizePopupData(Load3DProduct));
	}

	private void Load3DProduct(Vector3 productSize)
	{
		StartCoroutine(ILoad3DProduct(productSize));
	}

	private IEnumerator ILoad3DProduct(Vector3 productSize)
	{
		Product3DData product3D;
		product3D.size = productSize;
		product3D.textureSize = currentProduct.textureSize; 

		TextureLoader textureLoader = new TextureLoader(currentProduct.textureImageURL);
		yield return textureLoader.Load();
		product3D.texture = textureLoader.result;

		view.gameObject.SetActive(false);
		spaceController.gameObject.SetActive(true);

		spaceController.StartPreview(product3D) ;
	}

	public void ToCatalog()
	{
		view.gameObject.SetActive(true);
		spaceController.gameObject.SetActive(false);
	}
}
