using UnityEngine;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{	
public class doOnHover : MonoBehaviour 
	{
		float startTime;
		float endTime;
		float fadeTime = 1f;
		bool fadeIn = false; 
		float volume; 

	        
		[SerializeField] private VRInteractiveItem m_InteractiveItem;
		
		private void Awake ()
		{
		}

		private void OnEnable()
		{
			m_InteractiveItem.OnOver += HandleOver;
			m_InteractiveItem.OnOut += HandleOut;
			m_InteractiveItem.OnClick += HandleClick;
			m_InteractiveItem.OnDoubleClick += HandleDoubleClick;
		}


		private void OnDisable()
		{
			m_InteractiveItem.OnOver -= HandleOver;
			m_InteractiveItem.OnOut -= HandleOut;
			m_InteractiveItem.OnClick -= HandleClick;
			m_InteractiveItem.OnDoubleClick -= HandleDoubleClick;
		}

		void Update()
		{
			float time = Time.time;
			if (time > startTime && time < endTime){
				volume = Mathf.Lerp  (fadeIn ? 0.09f : 1f, fadeIn ? 1f : 0.09f, (time-startTime)/(endTime-startTime));
//				Debug.Log(fadeIn ? 1f : 0f);
				GetComponent<AudioSource>().volume=volume;
			}
		}


		//Handle the Over event
		private void HandleOver()
		{
			Debug.Log("Show over state");
			fadeIn = true; 
			startTime = Time.time; 
			endTime = Time.time + fadeTime; 


		}


		//Handle the Out event
		private void HandleOut()
		{
			Debug.Log("Show out state");
			fadeIn = false;
			startTime = Time.time; 
			endTime = Time.time + fadeTime; 


		}


		//Handle the Click event
		private void HandleClick()
		{
			Debug.Log("Show click state");
		}


		//Handle the DoubleClick event
		private void HandleDoubleClick()
		{
			Debug.Log("Show double click");
		}
	}

	}