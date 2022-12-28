using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Product;
using Unity.VisualScripting;

public class ProductInstantiator : MonoBehaviour
{
	[SerializeField] private Material defaultMaterial;

	public GameObject InitializeInstance(Product3DData product)
	{
		GameObject instance = new GameObject("[Instance]");

		AddMeshAndCollider(instance, product.size);
		AddMaterial(instance, product.texture, product.textureSize);

		return instance;
	}

	private void AddMeshAndCollider(GameObject instance, Vector3 size)
	{
		Mesh mesh = GenerateMesh(size);
		instance.AddComponent<MeshRenderer>();
		MeshFilter filter = instance.AddComponent<MeshFilter>();
		filter.sharedMesh = mesh;
		MeshCollider collider = instance.AddComponent<MeshCollider>();
		collider.sharedMesh = mesh;
	}
	

	private void AddMaterial(GameObject instance, Texture2D texture, float textureSize)
	{
		Material material = new Material(defaultMaterial);
		material.SetTexture("_MainTex", texture);
		material.SetFloat("_TexSize", textureSize);
		MeshRenderer renderer = instance.GetComponent<MeshRenderer>();
		renderer.sharedMaterial = material;
	}

	private Mesh GenerateMesh(Vector3 size)
	{
		Mesh result = new Mesh();
		Vector3 halfSize = size / 2;

		Vector3[] vertices = {
		new Vector3(-halfSize.x, size.y, -halfSize.z),
		new Vector3(halfSize.x, size.y, - halfSize.z),
		new Vector3(halfSize.x, 0, -halfSize.z),
		new Vector3(-halfSize.x, 0, -halfSize.z),

		new Vector3(-halfSize.x, 0, halfSize.z),
		new Vector3(halfSize.x, 0, halfSize.z),
		new Vector3(halfSize.x, size.y, halfSize.z),
		new Vector3(-halfSize.x, size.y, halfSize.z),

		new Vector3(halfSize.x, size.y, - halfSize.z),
		new Vector3(-halfSize.x, size.y, -halfSize.z),
		new Vector3(-halfSize.x, size.y, halfSize.z),
		new Vector3(halfSize.x, size.y, halfSize.z),

		new Vector3(-halfSize.x, 0, -halfSize.z),
		new Vector3(halfSize.x, 0, -halfSize.z),
		new Vector3(halfSize.x, 0, halfSize.z),
		new Vector3(-halfSize.x, 0, halfSize.z),

		new Vector3(halfSize.x, 0, -halfSize.z),
		new Vector3(halfSize.x, size.y, - halfSize.z),
		new Vector3(halfSize.x, size.y, halfSize.z),
		new Vector3(halfSize.x, 0, halfSize.z),

		new Vector3(-halfSize.x, size.y, -halfSize.z),
		new Vector3(-halfSize.x, 0, -halfSize.z),
		new Vector3(-halfSize.x, 0, halfSize.z),
		new Vector3(-halfSize.x, size.y, halfSize.z)
		};

		int[] indices = new int[24];
		for(int i = 0; i < 24; i++)
			indices[i] = i;

		result.SetVertices(vertices);
		result.SetIndices(indices, MeshTopology.Quads, 0);
		result.RecalculateTangents();
		result.RecalculateNormals();
		result.RecalculateBounds();
		


		return result;
	}
}
