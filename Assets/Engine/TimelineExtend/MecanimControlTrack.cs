using EngineCenter.Timeline;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Framework.TimelineExtend
{
    public enum CinemaCharacterType
    {
        Binding = 0, //绑定角色
        Dynamic,//动态角色
    }

    [TrackColor(1, 0.07f, 0.57f)]
    [TrackClipType(typeof(MecanimPlayableClip))]
    [TrackBindingType(typeof(Animator))]

    public class MecanimControlTrack : TrackAsset
    {
        [SerializeField]
        public CinemaCharacterType bindingType = CinemaCharacterType.Binding;

        [SerializeField]
        public string LoadCommand;

        [SerializeField]
        public GameObject AssetPrefab;

        [SerializeField]
        public bool mIsMirror;

        [SerializeField]
        public List<MirrorDataConfig> mMirrorDataList = new List<MirrorDataConfig>();

        [SerializeField]
        public ExposedReference<Transform> parentLoc;

        [SerializeField]
        public bool mIsEnemy;

#if UNITY_EDITOR
        public GameObject EditorPreviewPrefab;
        [NonSerialized] public PlayableDirector pDirector;
        public bool isPreviewBySampleAnimationClip;
#endif

        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            // before building, update the binding field in the clips assets;
            List<BaseMirror> mirrorList = new List<BaseMirror>();
            var director = go.GetComponent<PlayableDirector>();
            var binding = director.GetGenericBinding(this) as Animator;
            bool isTimelineBind = binding != null;
            Transform parentTrans = parentLoc.Resolve(director);
            if (parentTrans == null)
                parentTrans = go.transform;

            if (Application.isPlaying && !binding)
            {
                if (bindingType == CinemaCharacterType.Dynamic)
                {
                    GameObject obj = TimelineExtend.LoadDynamicCharacter(LoadCommand);
                    
                    if (obj)
                    {
                        //obj.transform.SetParent(parentTrans);// 默认抓取的角色预制体
                        if (mIsEnemy)
                        {
                            List<BaseMirror> enemyMirrorList = MirrorUtility.AddMirrorComponent(obj);
                            MirrorUtility.SwitchMirror(enemyMirrorList, true);
                        }

                        if (mIsMirror)
                        {
                            mirrorList = MirrorUtility.AddMirrorComponent(obj, mMirrorDataList);
                        }
                        binding = obj.GetComponent<Animator>();
                        if(binding != null)
                        {
                            binding.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                        }
                    }
                    else
                    {
                        binding = null;
                    }
                }
                else if (AssetPrefab)
                {
                    GameObject obj = GameObject.Instantiate(AssetPrefab);
                    if (mIsEnemy)
                    {
                        List<BaseMirror> enemyMirrorList = MirrorUtility.AddMirrorComponent(obj);
                        MirrorUtility.SwitchMirror(enemyMirrorList, true);
                    }

                    if (mIsMirror)
                    {
                        mirrorList = MirrorUtility.AddMirrorComponent(obj, mMirrorDataList);
                    }
                    obj.transform.SetParent(parentTrans, false);
                    binding = obj.GetComponent<Animator>();
                    if (binding != null)
                    {
                        binding.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                    }
                }

            }


#if UNITY_EDITOR
            if (TimelineExtend.LoadDynamicCharacterHook == null && binding == null)
            {
                GameObject prefab = AssetPrefab != null ? AssetPrefab : EditorPreviewPrefab;
                if (prefab)
                {
                    GameObject obj = GameObject.Instantiate(prefab);
                    obj.transform.SetParent(parentTrans, false);
                    MecanimControlTrack.SetHideFlagsRecursive(obj);
                    binding = obj.GetComponent<Animator>();
                    if (mIsEnemy)
                    {
                        List<BaseMirror> enemyMirrorList = MirrorUtility.AddMirrorComponent(obj);
                        MirrorUtility.SwitchMirror(enemyMirrorList, true);
                    }

                    if (mIsMirror)
                    {
                        mirrorList = MirrorUtility.AddMirrorComponent(obj, mMirrorDataList);
                    }
                }
            }
#endif

            foreach (var c in GetClips())
            {
                var myAsset = c.asset as MecanimPlayableClip;
                if (myAsset != null)
                {

                    myAsset.animator = binding;

                    if(mirrorList.Count == 1)
                    {
                        myAsset.mMirror = mirrorList[0] as ModelSymmetryMirror;
                    }
#if UNITY_EDITOR
                    myAsset.tClip = c;
                    myAsset.isPreviewBySampleAnimationClip = isPreviewBySampleAnimationClip;
#endif

                }          

            }

#if UNITY_EDITOR
            pDirector = director;
            var playable = ScriptPlayable<MecanimPlayableMixBehaviour>.Create(graph, inputCount);
            var bh = playable.GetBehaviour();
            if (binding != null)
            {
            }

            if (parentTrans != null)
            {
            }
            bh.animator = binding;
            bh.mdirector = director;
            bh.isTimelineBind = isTimelineBind;
            bh.isPreviewBySampleAnimationClip = isPreviewBySampleAnimationClip;
            bh.parentTrans = parentTrans;
            bh.mIsMirror = mIsMirror;
            bh.mMirrorList = mirrorList;

            return playable;
#else
            var playable = ScriptPlayable<MecanimPlayableMixBehaviour>.Create(graph, inputCount);
            var bh = playable.GetBehaviour();
            if (binding != null)
            {
            }

            if (parentTrans != null)
            {
            }

            bh.animator = binding;
            bh.isTimelineBind = isTimelineBind;
            bh.parentTrans = parentTrans;
            bh.mIsMirror = mIsMirror;
            bh.mMirrorList = mirrorList;

            return playable;//base.CreateTrackMixer(graph, go, inputCount);
#endif
        }


        private static void SetHideFlagsRecursive(GameObject gameObject)
        {
            gameObject.hideFlags = HideFlags.DontSaveInEditor | HideFlags.DontSaveInBuild;
            //if (!Application.isPlaying)
            //    gameObject.hideFlags |= HideFlags.HideInHierarchy;
            IEnumerator enumerator = gameObject.transform.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                    MecanimControlTrack.SetHideFlagsRecursive(((Component)enumerator.Current).gameObject);
            }
            finally
            {
                if (enumerator is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}