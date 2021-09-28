// Decompiled with JetBrains decompiler
// Type: UnityEngine.Timeline.ControlPlayableAsset
// Assembly: UnityEngine.Timeline, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EACF16ED-84B3-40BA-A69F-A1374D0759E3
// Assembly location: E:\Program Files\Unity2018.4.1f1\Editor\Data\UnityExtensions\Unity\Timeline\RuntimeEditor\UnityEngine.Timeline.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Framework.TimelineExtend
{
    /// <summary>
    ///   <para>Asset that generates playables for controlling time-related elements on a GameObject.</para>
    /// </summary>
    [NotKeyable]
    [Serializable]
    public class AnimationParticlePreviewAsset : PlayableAsset, ITimelineClipAsset//,IPropertyPreview, IClipInitializer, IDirectorDriver
    {
        private static readonly List<ParticleSystem> k_EmptyParticlesList = new List<ParticleSystem>(0);
       
        private double m_Duration = PlayableBinding.DefaultDuration;
        private const int k_MaxRandInt = 10000;
        /// <summary>
        ///   <para>Let the particle systems behave the same way on each execution.</para>
        /// </summary>
        private uint particleRandomSeed;
        private bool m_SupportLoop;

#if UNITY_EDITOR
        private PlayableDirector m_pd;
        public TimelineClip tClip;

        public PlayableDirector CurPDirector
        {
            get { return m_pd; }
        }
#endif


        public void OnEnable()
        {
            if (this.particleRandomSeed != 0U)
                return;
            this.particleRandomSeed = (uint)UnityEngine.Random.Range(1, 10000);
        }

        public override double duration
        {
            get
            {
                return this.m_Duration;
            }
        }

        public ClipCaps clipCaps
        {
            get
            {
                return (ClipCaps)(12 | (!this.m_SupportLoop ? 0 : 1));
            }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
        {
            Playable mixer = Playable.Null;
#if UNITY_EDITOR
            List<Playable> playableList = new List<Playable>();

            PlayableDirector pd = go.GetComponent<PlayableDirector>();
            m_pd = pd;
            this.m_Duration = pd.duration;
            this.m_SupportLoop = false;

            if (!Application.isPlaying)
            {
                IList<ParticleSystem> particleSystems = this.GetParticleSystemRoots(go);
                this.SearchHiearchyAndConnectParticleSystem((IEnumerable<ParticleSystem>)particleSystems, graph, playableList);
            }

            mixer = AnimationParticlePreviewAsset.ConnectPlayablesToMixer(graph, playableList);
  
            if (!mixer.IsValid<Playable>())
                mixer = Playable.Create(graph, 0);
#endif
            return mixer;
        }

        private static Playable ConnectPlayablesToMixer(
          PlayableGraph graph,
          List<Playable> playables)
        {
            Playable playable = Playable.Create(graph, playables.Count);
            for (int portIndex = 0; portIndex != playables.Count; ++portIndex)
                AnimationParticlePreviewAsset.ConnectMixerAndPlayable(graph, playable, playables[portIndex], portIndex);
            playable.SetPropagateSetTime<Playable>(true);
            return playable;
        }


        private void SearchHiearchyAndConnectParticleSystem(
          IEnumerable<ParticleSystem> particleSystems,
          PlayableGraph graph,
          List<Playable> outplayables)
        {
            foreach (ParticleSystem particleSystem in particleSystems)
            {
                if ((UnityEngine.Object)particleSystem != (UnityEngine.Object)null)
                    outplayables.Add((Playable)ParticleControlPlayable.Create(graph, particleSystem, this.particleRandomSeed));
            }
        }


        private static void ConnectMixerAndPlayable(
          PlayableGraph graph,
          Playable mixer,
          Playable playable,
          int portIndex)
        {
            graph.Connect<Playable, Playable>(playable, 0, mixer, portIndex);
            mixer.SetInputWeight<Playable, Playable>(playable, 1f);
        }

        internal IList<T> GetComponent<T>(GameObject gameObject)
        {
            List<T> results = new List<T>();
            if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
            {
                if (true)//(this.searchHierarchy)
                    gameObject.GetComponentsInChildren<T>(true, results);
                else
                    gameObject.GetComponents<T>(results);
            }
            return (IList<T>)results;
        }

        [DebuggerHidden]
        private static IEnumerable<MonoBehaviour> GetControlableScripts(GameObject root)
        {
            if (root == null)
            {
                yield break;
            }
            MonoBehaviour[] componentsInChildren = root.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour script in componentsInChildren)
            {
                if (script is ITimeControl)
                {
                    yield return script;
                }
            }
        }

        private static long DoubleToDiscreteTime(double time)
        {
            double num = time / 1E-12 + 0.5;
            if (num < (double)long.MaxValue && num > (double)long.MinValue)
                return (long)num;
            throw new ArgumentOutOfRangeException("Time is over the discrete range.");
        }
        private static double ToDouble(long time)
        {
            return (double)time * 1E-12;
        }

        private void UpdateDurationAndLoopFlag(
          IList<PlayableDirector> directors,
          IList<ParticleSystem> particleSystems)
        {
            if (directors.Count == 0 && particleSystems.Count == 0)
                return;
            double num = double.NegativeInfinity;
            bool flag = false;
            foreach (PlayableDirector director in (IEnumerable<PlayableDirector>)directors)
            {
                if ((UnityEngine.Object)director.playableAsset != (UnityEngine.Object)null)
                {
                    double val2 = director.playableAsset.duration;
                    if (director.playableAsset is TimelineAsset && val2 > 0.0)
                        val2 = (double)(ToDouble(DoubleToDiscreteTime(val2) + 1L));
                    num = Math.Max(num, val2);
                    flag = flag || director.extrapolationMode == DirectorWrapMode.Loop;
                }
            }
            foreach (ParticleSystem particleSystem in (IEnumerable<ParticleSystem>)particleSystems)
            {
                num = Math.Max(num, (double)particleSystem.main.duration);
                flag = flag || particleSystem.main.loop;
            }
            this.m_Duration = !double.IsNegativeInfinity(num) ? num : PlayableBinding.DefaultDuration;
            this.m_SupportLoop = flag;
        }

        private IList<ParticleSystem> GetParticleSystemRoots(GameObject go)
        {
            //if (!this.searchHierarchy)
            //    return this.GetComponent<ParticleSystem>(go);
            List<ParticleSystem> particleSystemList = new List<ParticleSystem>();
            AnimationParticlePreviewAsset.GetParticleSystemRoots(go.transform, (ICollection<ParticleSystem>)particleSystemList);
            return (IList<ParticleSystem>)particleSystemList;
        }

        private static void GetParticleSystemRoots(Transform t, ICollection<ParticleSystem> roots)
        {
            ParticleSystem component = t.GetComponent<ParticleSystem>();
            if ((UnityEngine.Object)component != (UnityEngine.Object)null)
            {
                roots.Add(component);
            }
            else
            {
                for (int index = 0; index < t.childCount; ++index)
                    AnimationParticlePreviewAsset.GetParticleSystemRoots(t.GetChild(index), roots);
            }
        }

        //public void GatherProperties(PlayableDirector director, IPropertyCollector driver)
        //{
        //    if ((UnityEngine.Object)director == (UnityEngine.Object)null || ControlPlayableAsset.s_ProcessedDirectors.Contains(director))
        //        return;
        //    ControlPlayableAsset.s_ProcessedDirectors.Add(director);
        //    GameObject gameObject = this.sourceGameObject.Resolve((IExposedPropertyTable)director);
        //    if ((UnityEngine.Object)gameObject != (UnityEngine.Object)null)
        //    {
        //        if (this.updateParticle)
        //        {
        //            foreach (ParticleSystem componentsInChild in gameObject.GetComponentsInChildren<ParticleSystem>(true))
        //            {
        //                driver.AddFromName<ParticleSystem>(componentsInChild.gameObject, "randomSeed");
        //                driver.AddFromName<ParticleSystem>(componentsInChild.gameObject, "autoRandomSeed");
        //            }
        //        }
        //        if (this.active)
        //            driver.AddFromName(gameObject, "m_IsActive");
        //        if (this.updateITimeControl)
        //        {
        //            foreach (MonoBehaviour controlableScript in ControlPlayableAsset.GetControlableScripts(gameObject))
        //            {
        //                IPropertyPreview propertyPreview = controlableScript as IPropertyPreview;
        //                if (propertyPreview != null)
        //                    propertyPreview.GatherProperties(director, driver);
        //                else
        //                    driver.AddFromComponent(controlableScript.gameObject, (Component)controlableScript);
        //            }
        //        }
        //        if (this.updateDirector)
        //        {
        //            foreach (PlayableDirector director1 in (IEnumerable<PlayableDirector>)this.GetComponent<PlayableDirector>(gameObject))
        //            {
        //                if (!((UnityEngine.Object)director1 == (UnityEngine.Object)null))
        //                {
        //                    TimelineAsset playableAsset = director1.playableAsset as TimelineAsset;
        //                    if (!((UnityEngine.Object)playableAsset == (UnityEngine.Object)null))
        //                        playableAsset.GatherProperties(director1, driver);
        //                }
        //            }
        //        }
        //    }
        //    ControlPlayableAsset.s_ProcessedDirectors.Remove(director);
        //}

        //void IClipInitializer.OnCreate(
        //  TimelineClip newClip,
        //  TrackAsset track,
        //  IExposedPropertyTable table)
        //{
        //    GameObject gameObject = (GameObject)null;
        //    if (table != null)
        //        gameObject = this.sourceGameObject.Resolve(table);
        //    if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null && (UnityEngine.Object)this.prefabGameObject != (UnityEngine.Object)null)
        //        gameObject = this.prefabGameObject;
        //    if (!(bool)((UnityEngine.Object)gameObject))
        //        return;
        //    this.UpdateDurationAndLoopFlag(this.GetComponent<PlayableDirector>(gameObject), this.GetComponent<ParticleSystem>(gameObject));
        //    newClip.displayName = gameObject.name;
        //}

        //IList<PlayableDirector> IDirectorDriver.GetDrivenDirectors(
        //  IExposedPropertyTable resolver)
        //{
        //    if (!this.updateDirector || (UnityEngine.Object)this.prefabGameObject != (UnityEngine.Object)null || resolver == null)
        //        return (IList<PlayableDirector>)new List<PlayableDirector>(0);
        //    GameObject gameObject = this.sourceGameObject.Resolve(resolver);
        //    if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
        //        return (IList<PlayableDirector>)new List<PlayableDirector>(0);
        //    List<PlayableDirector> playableDirectorList = new List<PlayableDirector>();
        //    foreach (PlayableDirector playableDirector in (IEnumerable<PlayableDirector>)this.GetComponent<PlayableDirector>(gameObject))
        //    {
        //        if (playableDirector.playableAsset is TimelineAsset)
        //            playableDirectorList.Add(playableDirector);
        //    }
        //    return (IList<PlayableDirector>)playableDirectorList;
        //}
    }
}
