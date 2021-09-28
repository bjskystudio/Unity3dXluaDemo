using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace EngineCenter.Timeline
{
    public static class MirrorUtility
    {
        public static BaseMirror AddMirrorComponent(MirrorData mirrorData)
        {
            MirrorType mirrorType = mirrorData.mMirrorType;
            switch (mirrorType)
            {
                case MirrorType.eCameraSymmetryMirror:
                    {
                        CameraSymmetryMirror mirror = mirrorData.gameObject.GetComponent<CameraSymmetryMirror>();
                        if(mirror != null)
                        {
                            mirror.Reset();
                            return mirror;
                        }
                        else
                        {
                            return mirrorData.gameObject.AddComponent<CameraSymmetryMirror>();
                        }
                    }
                case MirrorType.eLightSymmetryMirror:
                    {
                        LightSymmetryMirror mirror = mirrorData.gameObject.GetComponent<LightSymmetryMirror>();
                        if(mirror != null)
                        {
                            mirror.Reset();
                            return mirror;
                        }
                        else
                        {
                            return mirrorData.gameObject.AddComponent<LightSymmetryMirror>();
                        }

                    }
                case MirrorType.eModelSymmetryMirror:
                    {
                        ModelSymmetryMirror mirror = mirrorData.gameObject.GetComponent<ModelSymmetryMirror>();
                        if (mirror != null)
                        {
                            mirror.Reset();
                            return mirror;
                        }
                        else
                        {
                            return mirrorData.gameObject.AddComponent<ModelSymmetryMirror>();
                        };
                    }
                case MirrorType.eScaleMirror:
                    {
                        ScaleMirror mirror = mirrorData.gameObject.GetComponent<ScaleMirror>();
                        if (mirror != null)
                        {
                            mirror.Reset();
                            return mirror;
                        }
                        else
                        {
                            return mirrorData.gameObject.AddComponent<ScaleMirror>();
                        };
                    }
                case MirrorType.eEffectSymmetryMirror:
                    {
                        EffectSymmetryMirror mirror = mirrorData.gameObject.GetComponent<EffectSymmetryMirror>();
                        if (mirror != null)
                        {
                            mirror.Reset();
                            return mirror;
                        }
                        else
                        {
                            return mirrorData.gameObject.AddComponent<EffectSymmetryMirror>();
                        };
                    }
            }

            return null;
        }

        //使用timeline中的配置来覆盖obj自身的配置，因为timeline中有不同的需求，主要是在MirrorData.Copy中实现
        public static List<BaseMirror> AddMirrorComponent(GameObject obj, List<MirrorDataConfig> mirrorDataConfigList)
        {
            List<BaseMirror> mirrorList = new List<BaseMirror>();
            MirrorData[] mirrorDataList = obj.GetComponentsInChildren<MirrorData>();
            if(mirrorDataList.Length != mirrorDataConfigList.Count)
            {
                Debug.LogError(string.Format("{0}对象的MirrorData数量 和 对应Track上的MirrorData数量不匹配，请重新选中Track让MirrorData数量匹配!!!", obj.name));
                return mirrorList;
            }

            for(int i = 0; i < mirrorDataList.Length; i++)
            {
                MirrorData mirrorData = mirrorDataList[i];
                MirrorDataConfig mirrorDataConfig = mirrorDataConfigList[i];
                MirrorData.Copy(mirrorData, mirrorDataConfig);
                BaseMirror mirror = AddMirrorComponent(mirrorDataList[i]);
                if (mirror != null)
                {
                    mirrorList.Add(mirror);
                }
            }

            return mirrorList;
        }

        public static List<BaseMirror> AddMirrorComponent(PlayableGraph graph, List<TransformWrap> transList)
        {
            List<BaseMirror> mirrorList = new List<BaseMirror>();
            for (int i = 0; i < transList.Count; i++)
            {
                Transform trans = transList[i].mTrans.Resolve(graph.GetResolver());
                if(trans != null)
                {
                    MirrorData mirrorData = trans.GetComponent<MirrorData>();
                    if(mirrorData != null)
                    {
                        BaseMirror mirror = MirrorUtility.AddMirrorComponent(mirrorData);
                        if (mirror != null)
                        {
                            mirrorList.Add(mirror);
                        }
                    }
                }
            }

            return mirrorList;
        }

        public static List<BaseMirror> AddMirrorComponent(GameObject obj)
        {
            List<BaseMirror> mirrorList = new List<BaseMirror>();
            MirrorData[] mirrorDataList = obj.GetComponentsInChildren<MirrorData>();
            for (int i = 0; i < mirrorDataList.Length; i++)
            {
                BaseMirror mirror = AddMirrorComponent(mirrorDataList[i]);
                if(mirror != null)
                {
                    mirrorList.Add(mirror);
                }
            }

            return mirrorList;
        }

        public static void SwitchMirror(List<BaseMirror> mirrorList, bool isActivate)
        { 
            if(mirrorList == null)
            {
                return;
            }

            for (int i = 0; i < mirrorList.Count; i++)
            {
                if (isActivate)
                {
                    mirrorList[i].IsMirror = true;

                }
                else
                {
                    mirrorList[i].IsMirror = false;
                    GameObject.DestroyImmediate(mirrorList[i]);
                }
            }
        }
    }

}