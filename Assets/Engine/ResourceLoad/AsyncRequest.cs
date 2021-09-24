

using System.Collections;
using System.Collections.Generic;

namespace ResourceLoad
{
    public class AsyncRequest : IEnumerator
    {
        public AsyncRequest()
        {
            isDone = false;
            progress = 0.0f;
        }

        //lua侧使用自定义协程会使用该变量
        public bool isDone
        {
            get;
            set;
        }

        //lua侧使用自定义协程会使用该变量
        public float progress
        {
            get;
            set;
        }

        public object Current
        {
            set;
            get;
        }

        public System.Object Asset
        {
            get;
            set;
        }

        public List<UnityEngine.Object> Assets
        {
            get;
            set;
        }

        public ResRef ResRef
        {
            get;
            set;
        }

        public bool MoveNext()
        {
            return !isDone;
        }

        public void Reset()
        {
            
        }
    }
}

