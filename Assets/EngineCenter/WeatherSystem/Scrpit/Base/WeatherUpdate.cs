using UnityEngine;

namespace WeatherSystem
{
    [ExecuteInEditMode]
    public class WeatherUpdate : MonoBehaviour
    {
        void Update()
        {
            WeatherManager.Instance.Update();
        }

        void OnDestroy()
        {
            WeatherManager.Instance.Destroy();
        }
    }
}