using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Environment
{
    public class Torch : MonoBehaviour
    {
        public float minIntensity;
        public float maxIntensity;
        
        public float minFlickerTime;
        public float maxFlickerTime;
        
        [SerializeField] private Light2D _mainLight;
        [SerializeField] private Light2D _flickerLight;
        
        void Awake()
        {
            StartCoroutine(Flicker());
        }

        IEnumerator Flicker()
        {
            while (true)
            {
                float randomIntensity = Random.Range(minIntensity, maxIntensity);
                _flickerLight.intensity = randomIntensity;

                float randomTime = Random.Range(minFlickerTime, maxFlickerTime);
                yield return new WaitForSeconds(randomTime);
            }
        }
    }
}