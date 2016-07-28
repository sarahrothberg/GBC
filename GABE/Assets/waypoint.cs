using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{

    public class waypoint : MonoBehaviour
    {

        float startTime;
        float endTime;
        float fadeTime = 1f;
        bool fadeIn = false;
        float volume;
        public GameObject player;

        [SerializeField]
        private Material m_NormalMaterial;
        [SerializeField]
        private Material m_OverMaterial;
        [SerializeField]
        private VRInteractiveItem m_InteractiveItem;
        [SerializeField]
        private Renderer m_Renderer;

        private Vector3 waypointPos;

        private void Awake()
        {
            m_Renderer.material = m_NormalMaterial;
            waypointPos = this.transform.position;

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
            //float time = Time.time;
            //if (time > startTime && time < endTime)
            //{
            //    volume = Mathf.Lerp(fadeIn ? 0.01f : 1f, fadeIn ? 1f : 0.01f, (time - startTime) / (endTime - startTime));
            //    //				Debug.Log(fadeIn ? 1f : 0f);
            //    GetComponent<AudioSource>().volume = volume;
            //}
        }


        //Handle the Over event
        private void HandleOver()
        {
            Debug.Log("waypoint name is " + this.gameObject.name);
            m_Renderer.material = m_OverMaterial;
            Vector3 temp = new Vector3(waypointPos.x, waypointPos.y, waypointPos.z);
            player.transform.position = temp;
            //fadeIn = true;
            //startTime = Time.time;
            //endTime = Time.time + fadeTime;


        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;
            //fadeIn = false;
            //startTime = Time.time;
            //endTime = Time.time + fadeTime;


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