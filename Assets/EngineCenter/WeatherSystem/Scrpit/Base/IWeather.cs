using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeatherSystem
{
    public interface IWeather
    {
        void OnInit(Transform parent);
        void OnClear();
        void OnRefreshFlag(Dictionary<int, WeatherFlag> flags);
        void OnUpdate();
        void OnHigh();
        void OnMedium();
        void OnLow();
    }
}
