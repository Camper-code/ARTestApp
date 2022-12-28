using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTransformer : MonoBehaviour
{
	const float rotationSensitivity = 1;

	private Camera camera;
	private Transform cameraTransform;
	private Transform objectTransform;

	private Vector3 offsetPosition;

	public void Initialize(Camera camera)
	{
		this.camera = camera;
		objectTransform = transform;
		cameraTransform = camera.transform;
	}

	private Vector3 GetPosOfTouch(Vector2 touch)
	{
		Vector3 direction = camera.ScreenPointToRay(touch).direction;
		Vector3 objectPos = objectTransform.position;
		Vector3 camPos = cameraTransform.position;

		Vector2 directionInPlace = new Vector2(direction.x, direction.z);
		Vector2 camPosInPlace = new Vector2(camPos.x, camPos.z);

		Vector2 final = (directionInPlace / direction.y) * (objectPos.y - camPos.y) + camPosInPlace;

		return new Vector3(final.x, objectPos.y, final.y);
	}

	private void OnMouseDown()
	{
		if (Input.touchCount == 1)
		{
			Vector3 touchInPlace = GetPosOfTouch(Input.GetTouch(0).position);
			offsetPosition = objectTransform.position - touchInPlace;
		}
	}

	private void OnMouseDrag()
	{
		if (Input.touchCount == 1)
		{
			objectTransform.position = GetPosOfTouch(Input.GetTouch(0).position) + offsetPosition;
		}
		else
		{
			Touch upperTouch = Input.GetTouch(0);
			for(int i =0; i < Input.touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				if(touch.position.y > upperTouch.position.y)
					upperTouch= touch;
			}

			objectTransform.Rotate(Vector3.up,upperTouch.deltaPosition.x * rotationSensitivity);
		}
	}
}
