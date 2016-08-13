using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;

namespace VRStandardAssets.Examples
{

    public class personSelect : MonoBehaviour
    {
        
        float startTime;
        float endTime;
        float fadeTime = 1f;
        bool fadeIn = false;
        float volume;
        public GameObject player;
        public GameObject person;

        public GameObject[] otherPeople;
        public GameObject[] otherButtons;



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
        }

        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;

        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
        }

        void Update()
        {
           
        }


        //Handle the Over event
        private void HandleOver()
        {
            for (int i = 0; i < otherPeople.Length; i++)
            {
                otherPeople[i].SetActive(false);
                Renderer temp = otherButtons[i].GetComponent<Renderer>();
                temp.material = m_NormalMaterial;
                GetComponent<AudioSource>().Play();

            }
            Debug.Log("waypoint name is " + person);
            m_Renderer.material = m_OverMaterial;
            person.SetActive(true);

            
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("out");
            //fadeIn = false;
            //startTime = Time.time;
            //endTime = Time.time + fadeTime;


        }
    }
}