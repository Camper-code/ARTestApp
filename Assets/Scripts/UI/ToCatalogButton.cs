using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToCatalogButton : MonoBehaviour
{
    [SerializeField] ARSpaceController spaceController;

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(spaceController.CleanAndGoToCatalog);
	}
}
