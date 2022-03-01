using UnityEngine;

namespace WeatherSystem
{
    [ExecuteInEditMode]
    public class WeatherFlag : MonoBehaviour
    {
        public bool mIsRain = false;
        private bool mPreIsRain = false;
        public Material mMaterial;

        void OnEnable()
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if(renderer != null)
            {
                mMaterial = renderer.sharedMaterial;
            }
            WeatherManager.Instance.AddFlag(this);
        }

        void OnDisable()
        {
            WeatherManager.Instance.RemoveFlag(this);
        }

        void Update()
        {
            if(mPreIsRain != mIsRain)
            {
                mPreIsRain = mIsRain;
                WeatherManager.Instance.UpdateFlag();
            }
        }
    }
}
