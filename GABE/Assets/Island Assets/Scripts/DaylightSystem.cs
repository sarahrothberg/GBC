using UnityEngine;
using UnityEditor;
using System.Collections;

public class DaylightSystem : MonoBehaviour {

//============================================================================
	
	public Light lightSource;							//Which light source to effect.
	public GameObject secondarySkybox;					//Secondary Skybox in the scene - used to transition into nighttime.
	[Range(0,1)]
	public float cycleSpeed = 1.0f;						//Used to decrease the amount of time during day/night cycle - set to zero to pause effect.

	[Space(12)]
	[Header("Daytime Properties")]
	public Material dayTimeMaterial;					//Skybox material used for daytime.
	public float brightnessDay = 0.85f;					//Value for Sun brightness during daytime.
	public float shadowStrengthDay = 1.0f;				//Value for controlling the shadow strength during the daytime.
	public float skyboxReflectionDay = 1.0f;			//Value for controlling the skybox reflection strength during the daytime.

	[Space(12)]
	[Header("Nighttime Properties")]
	public Material nightTimeMaterial;					//Skybox material used for nighttime.
	public float brightnessNight = 0.35f;				//Value for Sun brightness during nighttime.
	public float shadowStrengthNight = 0.25f;			//Value for controlling the shadow strength during the nighttime.
	public float skyboxReflectionNight = 0.043f;		//Value for controlling the skybox reflection strength during the nighttime.

	
	private GameObject[] torchLights;					//Tiki torch's light emmitter.
	private GameObject[] torchFires;					//Tiki torch's particle emmitter.
	private float fadeValue;
	private float timeOffset;
	private Color dayLight = new Color (0.815f,0.78f,0.619f); 		//(RGBA = 208, 199, 158, 255)
	private Color nightLight = new Color (0.501f,0.486f,0.58f);		//(RGBA = 128, 124, 148, 255)

//============================================================================

	
	// Use this for initialization.
	void Start () 
	{
		//Starts the Daylight System.
		RenderSettings.skybox = dayTimeMaterial;
		lightSource = GetComponent<Light> ();
		lightSource.color = dayLight;
		lightSource.intensity = brightnessDay;
		lightSource.shadowStrength = shadowStrengthDay;
		
		//Sets the tiki torches to off for daytime.
		torchLights = GameObject.FindGameObjectsWithTag ("TorchLight") as GameObject[];
		torchFires = GameObject.FindGameObjectsWithTag ("TorchFire") as GameObject[];
		foreach (GameObject light in torchLights) 
		{
			light.SetActive(false);
		}

		foreach (GameObject fire in torchFires) 
		{
			fire.SetActive(false);
		}
	}


	// Update is called once per frame.
	void Update () 
	{
		//Rotates the "lightSource" by "timeOffset".
		timeOffset = (cycleSpeed * Time.deltaTime) * 10;
		this.transform.Rotate(Vector3.right * timeOffset);

		//Sets the scene to "dayTime". 
		if (this.transform.rotation.eulerAngles.x == 90)
		{
			fadeValue = 1.0f;
			StartCoroutine(FadeEffect(fadeValue, timeOffset / Time.deltaTime));
			StartCoroutine(IntensityCycle(brightnessNight, timeOffset / Time.deltaTime));
			StartCoroutine(ShadowCycle(shadowStrengthNight, timeOffset / Time.deltaTime));
			StartCoroutine(ColorCycle(0.501f, 0.486f, 0.58f, timeOffset / Time.deltaTime));
			StartCoroutine(SkyReflectionCycle(skyboxReflectionNight, timeOffset / Time.deltaTime));
//			Debug.Log ("Day");
		}
		
		//Sets the scene to "nightTime".
		if (this.transform.rotation.eulerAngles.x == 270) 
		{
			fadeValue = 0.0f;
			StartCoroutine(FadeEffect(fadeValue, timeOffset / Time.deltaTime));
			StartCoroutine(IntensityCycle(brightnessDay, timeOffset / Time.deltaTime));
			StartCoroutine(ShadowCycle(shadowStrengthDay, timeOffset / Time.deltaTime));
			StartCoroutine(ColorCycle(0.815f, 0.78f, 0.619f, timeOffset / Time.deltaTime));
			StartCoroutine(SkyReflectionCycle(skyboxReflectionDay, timeOffset / Time.deltaTime));
//			Debug.Log ("Night");
		}	

		//When nighttime, turn ON Tiki Torches.
		if (this.transform.rotation.eulerAngles.x > 180) 
		{
			foreach (GameObject light in torchLights) 
			{
				light.SetActive(true);
			}
			
			foreach (GameObject fire in torchFires) 
			{
				fire.SetActive(true);
			}
		}

		//When daytime, turn OFF Tiki Torches.
		if (this.transform.rotation.eulerAngles.x < 180) 
		{
			foreach (GameObject light in torchLights) 
			{
				light.SetActive(false);
			}
			
			foreach (GameObject fire in torchFires) 
			{
				fire.SetActive(false);
			}
		}

	}


	//Function that controls the light intensity transition.
	private IEnumerator IntensityCycle (float lightValue, float aTime)
	{
		float lightIntensity = lightSource.GetComponent<Light>().intensity;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			float newLight = Mathf.Lerp(lightIntensity,lightValue,t);
			lightSource.intensity = newLight;
			yield return null;
		}
	}


	//Function that controls the shadow strength transition.
	private IEnumerator ShadowCycle (float shadowValue, float aTime)
	{
		float shadowIntensity = lightSource.GetComponent<Light>().shadowStrength;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			float newShadow = Mathf.Lerp(shadowIntensity,shadowValue,t);
			lightSource.shadowStrength = newShadow;
			yield return null;
		}
	}


	//Function that controls the shadow strength transition.
	private IEnumerator SkyReflectionCycle (float reflectionValue, float aTime)
	{
		float reflectionIntensity = RenderSettings.reflectionIntensity;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			float newReflection = Mathf.Lerp(reflectionIntensity,reflectionValue,t);
			RenderSettings.reflectionIntensity = newReflection;
			RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Custom;
			yield return null;
		}
	}

	
	//Function that controls the light color transition.
	private IEnumerator ColorCycle (float rValue, float gValue, float bValue, float aTime)
	{
		float lightColorR = lightSource.GetComponent<Light>().color.r;
		float lightColorG = lightSource.GetComponent<Light>().color.g;
		float lightColorB = lightSource.GetComponent<Light>().color.b;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(Mathf.Lerp(lightColorR,rValue,t), Mathf.Lerp(lightColorG,gValue,t), Mathf.Lerp(lightColorB,bValue,t), 255);
			lightSource.color = newColor;
			yield return null;
		}
	}


	//Function that controls the alpha value (betwee 0 & 1) and the fade lerp time.
	private IEnumerator FadeEffect (float aValue, float aTime)
	{
		float skyboxAlpha = secondarySkybox.GetComponent<Renderer> ().material.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(skyboxAlpha,aValue,t));
			secondarySkybox.GetComponent<Renderer>().material.color = newColor;
			yield return null;
		}
	}

}