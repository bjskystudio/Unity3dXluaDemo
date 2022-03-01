using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public class WeatherBase : IWeather
    {
        public virtual void OnInit(Transform parent)
        {
        }

        public virtual void OnClear()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnHigh()
        {
        }

        public virtual void OnLow()
        {
        }

        public virtual void OnMedium()
        {
        }

        public virtual void OnRefreshFlag(Dictionary<int, WeatherFlag> flags)
        {
        }
    }
}
