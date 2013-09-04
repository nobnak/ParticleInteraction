using UnityEngine;

[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class Trail : MonoBehaviour {
	public float decimation;
	public Material accumulator;

	private RenderTexture prevTex;
	
	void OnDisable() {
		DestroyImmediate(prevTex);
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		if (prevTex == null || prevTex.width != source.width || prevTex.height != source.height)
		{
			DestroyImmediate(prevTex);
			prevTex = new RenderTexture(source.width, source.height, 0);
			prevTex.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit( source, prevTex );
		}
		
		decimation = Mathf.Clamp(decimation, 0.0f, 1.0f );
		
		accumulator.SetTexture("_MainTex", prevTex);
		accumulator.SetFloat("_Decimation", decimation);
		
		Graphics.Blit(source, destination, accumulator);
	}
}