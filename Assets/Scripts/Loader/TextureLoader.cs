using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Loader
{
    public class TextureLoader
    {
        private string url;
        public Texture2D result;

        public TextureLoader(string url)
        {
            this.url = url;
        }

        public IEnumerator Load()
        {
			UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
			yield return request.SendWebRequest();
			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.LogError(request.error);
				result = null; ;
			}
			else
			{
				result = DownloadHandlerTexture.GetContent(request);
			}
		}
    }
}