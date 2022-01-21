using UnityEngine;
using YoukiaCore.Timer;

namespace Framework
{
    public class UnityGetTime : IGetTime
    {
        public void SetTime()
        {
        }

        public float GetUnscaledTime()
        {
            //return Time.fixedUnscaledDeltaTime;
            return Time.unscaledDeltaTime;
        }

        public float GetTime()
        {
            //Debug.LogError($"deltaTime : {Time.deltaTime} fixedDeltaTime : {Time.fixedDeltaTime}");
            //Hugh:
            //fixedDeltaTime 不随任何时间消耗变化
            return Time.deltaTime;
        }
    }
}
