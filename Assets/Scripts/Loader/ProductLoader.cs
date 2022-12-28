using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net;
using Newtonsoft.Json;
using Product;

namespace Loader
{
	public class ProductLoader : MonoBehaviour
	{
		const string blank = "https://sheets.googleapis.com/v4/spreadsheets/1Wj75QfY2F8PkNCTMYvOsL-FxYia2mdGvQITVti1xHMk/values:batchGet?key=AIzaSyC7yFu0urX0ichrzaSMK8ELaoS_7e3sQUs";
		const int startRow = 2;
		const int rowsPerDownload = 20;

		public Action<ProductData> OnOneProductLoaded;

		public void StartLoadProducts()
		{
			List<List<string>> sheet = LoadProductsSheet();
			StartCoroutine(ILoadProducts(sheet));
		}


		private List<List<string>> LoadProductsSheet()
		{
			List<List<string>> productsSheet = new List<List<string>>();
			WebClient client = new WebClient();

			for (int i = startRow;; i += rowsPerDownload+1)
			{
				string range = $"A{i}:D{i+rowsPerDownload}";
				string result = client.DownloadString(blank + "&ranges=" + range);
				SheetData data = JsonConvert.DeserializeObject<SheetData>(result);
				if (data.valueRanges[0].values == null)
					break;
				productsSheet.AddRange(data.valueRanges[0].values);

			}

			return productsSheet;
		}

		private IEnumerator ILoadProducts(List<List<string>> sheet)
		{
			foreach (List<string> row in sheet)
			{
				ProductData productData = new ProductData();
				productData.name = row[0];
				productData.textureImageURL = row[2];
				productData.textureSize = float.Parse(row[3].Replace(".", ","));

				TextureLoader textureLoader = new TextureLoader(row[1]);
				yield return textureLoader.Load();

				if (textureLoader.result == null) continue;

				Texture2D previewTexture = textureLoader.result;

				productData.previewSprite = Sprite.Create(previewTexture, new Rect(0, 0, previewTexture.width, previewTexture.height), Vector2.one * 0.5f);

				OnOneProductLoaded(productData);
			}
		}

		
	}
}
