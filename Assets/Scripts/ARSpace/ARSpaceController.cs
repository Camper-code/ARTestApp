using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Product;

public class ARSpaceController : MonoBehaviour
{
    const float maxDistanceToFloor = 5;

    [SerializeField] private Camera camera;
    [SerializeField] private GameCore gameCore;
    [SerializeField] private ProductInstantiator instantiator;
    [SerializeField] private PopupManager popupManager;

    private GameObject productObject;

	public void StartPreview(Product3DData product)
    {
        StartCoroutine(IStartPreview(product));
    }

    private IEnumerator IStartPreview(Product3DData product)
    {
        Popup waiting = popupManager.Show(PopupType.MovePhone,null);


        productObject = instantiator.InitializeInstance(product);
        AddTransformer(productObject);
        productObject.SetActive(false);

        RaycastHit hit;

        while (!Physics.Raycast(camera.transform.position, Vector3.down,out  hit, maxDistanceToFloor))
            yield return null;

        waiting.Close();

        productObject.transform.position = hit.point;
        productObject.gameObject.SetActive(true);
    }

    public void AddTransformer(GameObject instance)
    {
        instance.AddComponent<ProductTransformer>().Initialize(camera);
    }

	public void CleanAndGoToCatalog()
	{
		Destroy(productObject);
        gameCore.ToCatalog();
	}
}
