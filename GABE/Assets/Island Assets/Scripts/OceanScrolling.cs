using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OceanScrolling : MonoBehaviour {

//============================================================================

	public Vector2 scrollSpeed = new Vector2(-0.01f, -0.01f);	//Wave speed property - Keep between -1 & +1.
	public string textureProperty = "_DetailAlbedoMap";			//Which shader property to modify - Use "_MainTex" or "_DetailAlbedoMap" for normals.
	public Renderer rend;										//Which GameObject to modify.
	public Texture scrollingTexture;							//Assigns which texture to animate/scroll.
	public Vector2 scrollTile = new Vector2( 50f, 50f);			//Tiles (multiplies) the UV's of the "scrollingTexture".
	public float WaveSpeed = 2.25f;								//Controls the velocity/speed of the waves.
	public float WaveAmplitude = 0.1f;							//Controls how high/low the waves be.
	public float TickFrequency = 0.01f;							//Controls the refresh rate of the wave - lower is better.
	public float CustomScale = 1f;								//Scales the overall effect of the wave algorithm on the mesh.
	[Range(0,1)]
	public float Noise = 1;										//Noise effect value to distort the water.


	private Vector2 uvVector = Vector2.zero;
	private Mesh _mesh;
	private Vector3[] _baseHeight;
	private Vector3[] _newVerts; 

//============================================================================




	// Use this for initialization.
	void Start () 
	{
		rend = GetComponent<Renderer> ();
		_mesh = this.GetComponent<MeshFilter> ().mesh;
		_baseHeight = _mesh.vertices;
		_newVerts = new Vector3[_baseHeight.Length];
				
		StartCoroutine(CustomUpdate());
	}


	// Update is called once per frame.
	void Update () 
	{
		//Scrolls the texture(s) - Scroll (PropertyName, Speed).
		uvVector += (Time.deltaTime * (scrollSpeed * 0.5f));
		rend.material.SetTextureOffset (textureProperty, uvVector);	
	}


	//WIP.
	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts [0];
		rend.material.mainTexture = scrollingTexture;
		rend.material.mainTextureScale = scrollTile;
	}


	//Wave generation algorithm.
	System.Collections.IEnumerator CustomUpdate()
	{
		while (true)
		{
			for (int i = 0; i < _newVerts.Length; i++)
			{
				Vector3 vertex = _baseHeight[i];
				float s = (Time.time * WaveSpeed + vertex.x + vertex.y + vertex.z) * CustomScale;
				vertex.y += Mathf.Sin(1.25f * s) * WaveAmplitude;
				vertex.y += 0.5f + Mathf.Sin(15f * Mathf.PI * Noise - 2f * i) / (2f + 100f * Noise);
				_newVerts[i] = vertex;
			}
			
			_mesh.vertices = _newVerts;
			_mesh.RecalculateNormals();
			
			yield return new WaitForSeconds(TickFrequency);
		}
	}

}
