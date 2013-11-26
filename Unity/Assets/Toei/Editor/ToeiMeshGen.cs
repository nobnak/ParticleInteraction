using UnityEngine;
using UnityEditor;
using System.Collections;

public static class ToeiMeshGen {
	[MenuItem("Custom/GenToeiMesh")]
	public static void GenToeiMesh() {
		var vertices = new Vector3[]{ new Vector3(0f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, 1f, 0f) };
		var uv = new Vector2[]{ new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f) };
		var triangles = new int[]{ 0, 3, 1, 0, 2, 3 };
		var normals = new Vector3[]{ Vector3.one, Vector3.one, Vector3.one, Vector3.one };
		
		var mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.RecalculateBounds();
		
		AssetDatabase.CreateAsset(mesh, "Assets/Toei/ToeiMesh.asset");
	}
}
