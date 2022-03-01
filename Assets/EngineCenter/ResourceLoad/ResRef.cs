using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResourceLoad
{
    //资源加载返回这个包裹类，可以利用析构函数来释放未引用对象
    public class ResRef
    {
        private HRes mHRes;
        private bool mIsRelease = false;

        public string AssetPathInit
        {
            get
            {
                if(mHRes != null)
                {
                    return mHRes.AssetPathInit;
                }
                else
                {
                    Debug.LogError("ResRef mHRes is null");
                }

                return "";
            }
        }

        public ResRef(HRes hRes)
        {
            mHRes = hRes;
        }

        ~ResRef()
        {
            if(!mIsRelease)
            {
                Release();
            }
        }

        public void Release()
        {
            mIsRelease = true;
            if (mHRes != null)
            {
                //由于析构函数调用的Release是在垃圾回收线程，无法调用unity的api,我们这里处理将释放请求返还给主线程
                ResourceManager.Instance.AddReleaseRequest(mHRes);
            }
        }
    }

}