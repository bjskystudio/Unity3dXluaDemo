using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Framework.TimelineExtend
{
    /// <summary>
    ///   <para>Playable that synchronizes a particle system simulation.</para>
    /// </summary>
    public class ParticleControlPlayable : PlayableBehaviour
    {
        private float m_LastTime = -1f;
        private uint m_RandomSeed = 1;
        //private const float kUnsetTime = -1f;
        //private float m_SystemTime;
        bool _needRestart;
        private double m_oldTime;

        public static ScriptPlayable<ParticleControlPlayable> Create(
          PlayableGraph graph,
          ParticleSystem component,
          uint randomSeed)
        {
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                return ScriptPlayable<ParticleControlPlayable>.Null;
            ScriptPlayable<ParticleControlPlayable> scriptPlayable = ScriptPlayable<ParticleControlPlayable>.Create(graph, 0);
            scriptPlayable.GetBehaviour().Initialize(component, randomSeed);
            return scriptPlayable;
        }

        /// <summary>
        ///   <para>Which particle system to control.</para>
        /// </summary>
        public ParticleSystem particleSystem { get; private set; }

        public void Initialize(ParticleSystem ps, uint randomSeed)
        {
            this.m_RandomSeed = Math.Max(1U, randomSeed);
            this.particleSystem = ps;
            //this.m_SystemTime = 0.0f;
            this.SetRandomSeed();
        }

        private void SetRandomSeed()
        {
            this.particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ParticleSystem[] componentsInChildren = this.particleSystem.gameObject.GetComponentsInChildren<ParticleSystem>();
            uint randomSeed = this.m_RandomSeed;
            foreach (ParticleSystem particleSystem in componentsInChildren)
            {
                if (particleSystem.useAutoRandomSeed)
                {
                    particleSystem.useAutoRandomSeed = false;
                    particleSystem.randomSeed = randomSeed;
                    ++randomSeed;
                }
            }
        }

        void ResetSimulation(float time)
        {
            const float maxSimTime = 2.0f / 3;

            if (time < maxSimTime)
            {
                // The target time is small enough: Use the default simulation
                // function (restart and simulate for the given time).
                particleSystem.Simulate(time);
            }
            else
            {
                // The target time is larger than the threshold: The default
                // simulation can be heavy in this case, so use fast-forward
                // (simulation with just a single step) then simulate for a small
                // period of time.
                particleSystem.Simulate(time - maxSimTime, true, true, false);
                particleSystem.Simulate(maxSimTime, true, false, true);
            }
        }

        public override void PrepareFrame(Playable playable, FrameData data)
        {
            if ((UnityEngine.Object) this.particleSystem == (UnityEngine.Object) null)
                return;
            if (!this.particleSystem.gameObject.activeInHierarchy)
            {
                m_oldTime = Convert.ToSingle(playable.GetTime<Playable>());
                _needRestart = true;
                return;
            }
            //float time = (float)playable.GetTime<Playable>();
            //if (!Mathf.Approximately(this.m_LastTime, -1f) && Mathf.Approximately(this.m_LastTime, time))
            //    return;
            //float num1 = Time.fixedDeltaTime * 0.5f;
            //float t1 = time;
            //float num2 = t1 - this.m_LastTime;
            //float num3 = this.particleSystem.main.startDelay.Evaluate((float)this.particleSystem.randomSeed);
            //float num4 = this.particleSystem.main.duration + num3;
            //float num5 = (double)t1 <= (double)num4 ? this.m_SystemTime - num3 : this.m_SystemTime;
            //if ((double)t1 < (double)this.m_LastTime || (double)t1 < (double)num1 || Mathf.Approximately(this.m_LastTime, -1f) || (double)num2 > (double)this.particleSystem.main.duration || (double)Mathf.Abs(num5 - this.particleSystem.time) >= (double)Time.maximumParticleDeltaTime)
            //{
            //    this.particleSystem.Simulate(0.0f, true, true);
            //    this.particleSystem.Simulate(t1, true, false);
            //    this.m_SystemTime = t1;
            //}
            //else
            //{
            //    float num6 = (double)t1 <= (double)num4 ? num4 : this.particleSystem.main.duration;
            //    float num7 = t1 % num6;
            //    float t2 = num7 - this.m_SystemTime;
            //    if ((double)t2 < -(double)num1)
            //        t2 = num7 + num4 - this.m_SystemTime;
            //    this.particleSystem.Simulate(t2, true, false);
            //    this.m_SystemTime += t2;
            //}
            //this.m_LastTime = time;

            // Retrieve the track time (playhead position) from the root playable.
            var time = Convert.ToSingle(playable.GetTime<Playable>() - m_oldTime);
            // Time control
            if (Application.isPlaying)
            {
                // Play mode time control: Only resets the simulation when a large
                // gap between the time variables was found.
                var maxDelta = Mathf.Max(1.0f / 30, Time.smoothDeltaTime * 2);

                if (Mathf.Abs(time - particleSystem.time) > maxDelta)
                {
                    ResetSimulation(time);
                    particleSystem.Play();
                }
            }
            else
            {
                // Edit mode time control
                var minDelta = 1.0f / 240;
                var smallDelta = Mathf.Max(0.1f, Time.fixedDeltaTime * 2);
                var largeDelta = 0.2f;

                // Do full restart on reactivation.
                if (_needRestart)
                {
                    particleSystem.Play();
                    _needRestart = false;
                }

                if (time < particleSystem.time ||
                    time > particleSystem.time + largeDelta)
                {
                    // Backward seek or big leap
                    // Reset the simulation with the current playhead position.
                    ResetSimulation(time);
                }
                else if (time > particleSystem.time + smallDelta)
                {
                    // Fast-forward seek
                    // Simulate without restarting but with fixed steps.
                    particleSystem.Simulate(time - particleSystem.time, true, false, true);
                }
                else if (time > particleSystem.time + minDelta)
                {
                    // Edit mode playback
                    // Simulate without restarting nor fixed step.
                    particleSystem.Simulate(time - particleSystem.time, true, false, false);
                }
                else
                {
                    // Delta time is too small; Do nothing.
                }
            }
        }

        //public override void OnBehaviourPlay(Playable playable, FrameData info)
        //{
        //    this.m_LastTime = -1f;
        //}

        //public override void OnBehaviourPause(Playable playable, FrameData info)
        //{
        //    this.m_LastTime = -1f;
        //}
    }
}
