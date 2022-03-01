using Framework.TimelineExtend;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace EngineCenter.Timeline
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(PlayableDirector))]
    public class TimelineMirror : MonoBehaviour
    {
        private bool mPreIsMirror;
        public bool mIsMirror;
        private IEnumerable<TrackAsset> mTrackAssetList;
        private PlayableDirector mPD;
        private TimelineAsset mTA;

        // Start is called before the first frame update
        void OnEnable()
        {
            mPD = GetComponent<PlayableDirector>();
            mTA = mPD.playableAsset as TimelineAsset;
            UpdateTrack();
            mPD.RebuildGraph();
        }

        void UpdateTrack()
        {
            if(mTA == null)
            {
                return;
            }

            mTrackAssetList = mTA.GetOutputTracks();
            if (mTrackAssetList != null)
            {
                foreach (var item in mTrackAssetList)
                {
                    if (item is MecanimControlTrack)
                    {
                        MecanimControlTrack track = item as MecanimControlTrack;
                        track.mIsMirror = mIsMirror;
                    }
                    else if (item is MirrorTrack)
                    {
                        MirrorTrack track = item as MirrorTrack;
                        track.mIsMirror = mIsMirror;
                    }
                    else if (item is MirrorAnimationTrack)
                    {
                        MirrorAnimationTrack track = item as MirrorAnimationTrack;
                        track.mIsMirror = mIsMirror;
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (mIsMirror != mPreIsMirror)
            {
                mPreIsMirror = mIsMirror;
                UpdateTrack();
                mPD.RebuildGraph();
            }
        }
    }

}