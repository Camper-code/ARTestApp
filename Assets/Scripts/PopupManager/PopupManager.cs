using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    const string canvasRootPath = "Popups/PopupRoot";

	readonly Color backgroundColor = new Color(0, 0, 0, 0.5f);

    public Popup Show(PopupType type, object data)
    {
		Canvas canvas = Instantiate(Resources.Load<GameObject>(canvasRootPath)).GetComponent<Canvas>();
        GameObject background = canvas.gameObject;
        RectTransform backgroundTransform = background.GetComponent<RectTransform>();
        background.AddComponent<Image>().color = backgroundColor;


		GameObject windowPrefab = Resources.Load<GameObject>($"Popups/{type}Popup");
        Popup popup = Instantiate(windowPrefab, backgroundTransform).GetComponent<Popup>();
        popup.Initialize(data);

        return popup;
	}
}
