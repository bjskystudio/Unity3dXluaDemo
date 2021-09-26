using DG.Tweening;
using Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using XLua;
using YoukiaCore;

public static class GenConfig
{
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
		// unity
		typeof(System.Object),
        typeof(UnityEngine.Object),
        typeof(UnityEngine.CapsuleCollider),
        typeof(Ray2D),
        typeof(GameObject),
        typeof(Component),
        typeof(Behaviour),
        typeof(Transform),
        typeof(Resources),
        typeof(TextAsset),
        typeof(Keyframe),
        typeof(AnimationCurve),
        typeof(AnimationClip),
        typeof(MonoBehaviour),
        typeof(ParticleSystem),
        typeof(SkinnedMeshRenderer),
        typeof(Renderer),
        typeof(SpriteRenderer),
        typeof(WWW),
        typeof(List<int>),
        typeof(Action<string>),
        typeof(UnityEngine.Debug),
        typeof(Delegate),
        //typeof(Dictionary<string, GameObject>),
        typeof(UnityEngine.Events.UnityEvent),
        typeof(UnityEngine.RenderTexture),
        typeof(UnityEngine.RenderTextureFormat),
        //typeof(StoryTimeLine.StoryTimeLineControl),

        // unity结合lua，这部分导出很多功能在lua侧重新实现，没有实现的功能才会跑到cs侧
        typeof(Bounds),
        typeof(Color),
        typeof(LayerMask),
        typeof(Mathf),
        typeof(Plane),
        typeof(Quaternion),
        typeof(Camera),
        typeof(Ray),
        typeof(RaycastHit),

        typeof(Time),
        typeof(Touch),
        typeof(TouchPhase),
        typeof(Vector2),
        typeof(Vector3),
        typeof(Vector4),
        typeof(Matrix4x4),
        typeof(Application),
        typeof(UnityEngine.LogType),
        
        // 渲染
        typeof(RenderMode),
        typeof(AdditionalCanvasShaderChannels),
        typeof(RenderSettings),
        typeof(UnityEngine.MaterialPropertyBlock),
        typeof(UnityEngine.Shader),
        
        // UGUI  
        typeof(UnityEngine.Canvas),
        typeof(UnityEngine.CanvasGroup),
        typeof(UnityEngine.Rect),
        typeof(UnityEngine.RectTransform),
        typeof(UnityEngine.RectOffset),
        typeof(UnityEngine.Sprite),
        typeof(UnityEngine.BoxCollider2D),
        typeof(UnityEngine.Rigidbody2D),
        typeof(UnityEngine.RigidbodyType2D),
        typeof(UnityEngine.Sprites.DataUtility),

        typeof(UnityEngine.Input),
        typeof(UnityEngine.Random),
        typeof(UnityEngine.Font),
        typeof(UnityEngine.UI.CanvasScaler),
        typeof(UnityEngine.UI.CanvasScaler.ScaleMode),
        typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode),
        typeof(UnityEngine.UI.GraphicRaycaster),
        typeof(UnityEngine.UI.Text),
        typeof(UnityEngine.UI.InputField),
        typeof(UnityEngine.UI.Button),
        typeof(UnityEngine.UI.Image),
        typeof(UnityEngine.UI.ScrollRect),
        typeof(UnityEngine.UI.Scrollbar),
        typeof(UnityEngine.UI.Toggle),
        typeof(UnityEngine.UI.ToggleGroup),
        typeof(UnityEngine.UI.Button.ButtonClickedEvent),
        typeof(UnityEngine.UI.ScrollRect.ScrollRectEvent),
        typeof(UnityEngine.UI.GridLayoutGroup),
        typeof(UnityEngine.UI.VerticalLayoutGroup),
        typeof(UnityEngine.UI.HorizontalLayoutGroup),
        typeof(UnityEngine.UI.ContentSizeFitter),
        typeof(UnityEngine.UI.Slider),
        typeof(UnityEngine.TextAnchor),
        typeof(UnityEngine.UI.RawImage),
        typeof(UnityEngine.RectTransformUtility),
        typeof(UnityEngine.UI.Outline),
        typeof(UnityEngine.AI.NavMesh),
        typeof(UnityEngine.AI.NavMeshPath),
        typeof(UnityEngine.AI.NavMeshAgent),
        typeof(UnityEngine.AI.NavMeshData),
        typeof(UnityEngine.AI.NavMeshObstacle),
        typeof(UnityEngine.AI.NavMeshObstacleShape),
        typeof(UnityEngine.AI.OffMeshLink),
        typeof(UnityEngine.AI.OffMeshLinkData),
        typeof(UnityEngine.AI.OffMeshLinkType),
        typeof(UnityEngine.Video.VideoPlayer),
        typeof(UnityEngine.Video.VideoClip),
        
        //YouKia组件
        typeof(TMP_Text),
        typeof(TextMeshProUGUI),
        typeof(TextMeshPro),
        typeof(TMP_TextUtilities),
        typeof(Input),
        //typeof(AorScrollRect),
        //typeof(ImageMapping),

        //Extends
        
        //工具

        //事件监听
        typeof(PointerEventData),
        typeof(BaseEventData),
        typeof(PointerEventData),
        typeof(AxisEventData),
        typeof(EventTrigger),
        typeof(PointerEventData),
        typeof(BaseEventData),
        typeof(PointerEventData),
        typeof(AxisEventData),
        typeof(UnityEngine.EventSystems.EventTrigger.Entry),
        typeof(UnityEngine.EventSystems.EventTrigger.TriggerEvent),
        typeof(UnityEngine.EventSystems.EventTriggerType),
        typeof(UnityEngine.Events.UnityEvent<UnityEngine.EventSystems.BaseEventData>),
        typeof(System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry>),

        // 场景、资源加载
        typeof(UnityEngine.Resources),
        typeof(UnityEngine.ResourceRequest),
        typeof(UnityEngine.SceneManagement.SceneManager),
        typeof(UnityEngine.SceneManagement.Scene),
        //typeof(FResourcesManager),
        //typeof(YoukiaUnity.Scene.SceneManager),
        //typeof(YoukiaUnity.Scene.YoukiaScene),

        // 其它
        typeof(PlayerPrefs),
        typeof(System.GC),
        // typeof(Vector2Wrapper),
        // typeof(Vector3Wrapper),

   

        //XLua
        typeof(System.Type),
        typeof(UnityEngine.Random),
        typeof(UnityEngine.UI.MaskableGraphic),
        typeof(System.Reflection.Missing),
        typeof(UnityEngine.Random.State),
        typeof(UnityEngine.UI.MaskableGraphic.CullStateChangedEvent),
        typeof(UnityEngine.EventSystems.UIBehaviour),

        typeof(UnityEngine.Texture2D),
        typeof(UnityEngine.Texture2D.EXRFlags),
        typeof(UnityEngine.Camera.MonoOrStereoscopicEye),
        typeof(UnityEngine.Camera.StereoscopicEye),
        typeof(UnityEngine.WrapMode),
        typeof(UnityEngine.Physics),

        typeof(UnityEngine.RectTransform.Axis),
        typeof(UnityEngine.RectTransform.Edge),
        typeof(System.DateTime),
        typeof(System.DayOfWeek),
        typeof(System.Diagnostics.Stopwatch),

        typeof(UnityEngine.UI.GraphicRaycaster.BlockingObjects),
        typeof(UnityEngine.UI.InputField.OnChangeEvent),
        typeof(UnityEngine.UI.InputField.SubmitEvent),
        typeof(UnityEngine.UI.InputField.LineType),
        typeof(UnityEngine.UI.InputField.CharacterValidation),
        typeof(UnityEngine.UI.InputField.InputType),
        typeof(UnityEngine.UI.InputField.ContentType),
        typeof(UnityEngine.UI.Selectable),
        typeof(UnityEngine.UI.Selectable.Transition),

        typeof(UnityEngine.AudioListener),
        typeof(UnityEngine.AudioSource),
        typeof(UnityEngine.AudioRolloffMode),
        typeof(UnityEngine.AudioRolloffMode),

        typeof(UnityEngine.EventSystems.BaseRaycaster),
        typeof(UnityEngine.UI.Slider),
        typeof(UnityEngine.UI.Slider.SliderEvent),
        typeof(UnityEngine.UI.Slider.Direction),
        typeof(UnityEngine.UI.Image.Origin360),
        typeof(UnityEngine.UI.Image.Origin180),
        typeof(UnityEngine.UI.Image.Origin90),
        typeof(UnityEngine.UI.Image.OriginVertical),
        typeof(UnityEngine.UI.Image.OriginHorizontal),
        typeof(UnityEngine.UI.Image.FillMethod),
        typeof(UnityEngine.UI.Image.Type),
        typeof(UnityEngine.Events.UnityEvent<System.String>),
        typeof(UnityEngine.Events.UnityEvent<UnityEngine.GameObject,UnityEngine.EventSystems.PointerEventData>),
        typeof(UnityEngine.Events.UnityEventBase),
        typeof(UnityEngine.AudioClip),
        typeof(UnityEngine.BoxCollider),
        typeof(UnityEngine.SphereCollider),
        typeof(UnityEngine.CapsuleCollider),
        typeof(UnityEngine.Collider),
        typeof(UnityEngine.Animation),
        typeof(System.Reflection.BindingFlags),
        typeof(System.Array),
        typeof(UnityEngine.Texture),
        typeof(UnityEngine.Material),
        typeof(UnityEngine.Screen),
        typeof(UnityEngine.EventSystems.PointerEventData.FramePressState),
        typeof(UnityEngine.EventSystems.PointerEventData.InputButton),
        typeof(UnityEngine.EventSystems.AbstractEventData),
        typeof(System.ValueType),
        typeof(UnityEngine.UI.GridLayoutGroup.Constraint),
        typeof(UnityEngine.UI.GridLayoutGroup.Axis),
        typeof(UnityEngine.UI.GridLayoutGroup.Corner),
        typeof(UnityEngine.UI.LayoutGroup),
        typeof(UnityEngine.UI.Toggle.ToggleEvent),
        typeof(UnityEngine.UI.Toggle.ToggleTransition),
        typeof(UnityEngine.Events.UnityEvent<System.Boolean>),
        typeof(UnityEngine.Events.UnityEvent<System.Single>),
        typeof(UnityEngine.UI.ScrollRect.ScrollbarVisibility),
        typeof(UnityEngine.UI.ScrollRect.MovementType),
        typeof(UnityEngine.ParticleSystemRenderer),
        typeof(UnityEngine.MeshRenderer),
        typeof(UnityEngine.UI.Scrollbar.ScrollEvent),
        typeof(UnityEngine.UI.Scrollbar.Direction),
        typeof(System.Collections.Generic.List<UnityEngine.GameObject>),
        typeof(UnityEngine.UI.Shadow),
        typeof(UnityEngine.Transform),
        typeof(System.Reflection.MemberInfo),
        typeof(System.TimeSpan),
        typeof(TMPro.TMP_Dropdown),
        typeof(UnityEngine.UI.Dropdown),
        typeof(TMPro.TextAlignmentOptions),
        typeof(UnityEngine.Playables.PlayableAsset),
        typeof(UnityEngine.Playables.PlayableDirector),
        typeof(Animator),
        typeof(AudioRolloffMode),
        #region 自定义类
        typeof(YoukiaCore.Log.Log),
        typeof(YoukiaCore.IO.ByteBuffer),
        typeof(YoukiaCore.Event.BaseEvent),
        typeof(YoukiaCore.Event.GlobalEvent),
        typeof(YoukiaCore.Utils.TextUtils),
        typeof(YoukiaCore.Utils.EventNumberUtils),
        typeof(YoukiaCore.Utils.TimeUtil),
        typeof(YoukiaCore.Net.TcpConnect),
        typeof(YoukiaCore.Net.ErlTcpConnect),
        typeof(YoukiaCore.Net.TcpConnectFactory),
        typeof(YoukiaCore.Net.ErlTcpConnectFactory),
        typeof(YoukiaCore.Timer.TimerManager),
        //报错注掉 by xin.liu
        //typeof(SDKInterface),
        //typeof(GameManager),
        typeof(Launcher),
        typeof(XLuaManager),
        typeof(UIModel),
        typeof(AorLongPressButton),
        typeof(AorButton),
        typeof(AorDrag),
        typeof(AorImage),
        typeof(AorInputField),
        typeof(AorRawImage),
        typeof(AorRefExtends),
        typeof(AorText),
        typeof(AorTMP),
        typeof(AorTMP3D),
        typeof(AorTMP_InputField),
        typeof(AorTMPExtends),
        typeof(AorGraphicRaycaster),
        typeof(AorGraphicText),
        typeof(EventTriggerListener),
        typeof(SuperScrollView.LoopListView),
        typeof(SuperScrollView.LoopGridView),
        //报错注掉 by xin.liu
        //typeof(WorldSetting),
        //typeof(RoleControler),
        //typeof(RoleControlerManager),
        //typeof(RolePoint),
        //typeof(BattleUpdateManager),
        //typeof(RedBind),
        //typeof(BeHitInfo),
        //typeof(AfterImageCreator),
        //typeof(Framework.TimelineExtend.TimelineExtend),
        typeof(CSTimerManager),
        //报错注掉 by xin.liu
        //typeof(BattleTimerManager),
	    #endregion
        #region 扩展、工具
         //Extends
        typeof(AnimationExtends),
        typeof(AnimatorExtends),
        typeof(AorRefExtends),
        typeof(AorTMPExtends),
        typeof(GameObjectExtends),
        typeof(GlobalExtends),
        typeof(ListEntends),
        typeof(QuaternionExtends),
        typeof(TransformExtends),
        typeof(UnityObjectExtends),
        //报错注掉 by xin.liu
        //typeof(BattleObjectExtends),
        typeof(GraphicExtends),
        //Utils
        //报错注掉 by xin.liu
        //typeof(LuaAudioUtils),
        //typeof(LuaVideoUtils),
        //typeof(LuaCinemachineUtils),
        //typeof(DownloadUtils),
        typeof(MathEx),
        typeof(ShaderBridge),
        typeof(ResUtils),
        typeof(UIUtils),
        #endregion

        #region DoTween
        typeof(ShortcutExtensions),
        // typeof(ShortcutExtensions43),
        // typeof(ShortcutExtensions46),
        typeof(Tween),
        typeof(Tweener),
        typeof(TweenSettingsExtensions),
        typeof(Ease),
        typeof(DOVirtual),
        typeof(DG.Tweening.Sequence),
        typeof(TweenExtensions),
        typeof(DOTween),
        typeof(DOTweenModuleUI),
        //typeof(DOTweenEx),
        //typeof(DialogueRoleEx),
        typeof(AnimationExtends),
        // typeof(Vector2Wrapper),
        // typeof(Vector3Wrapper),
        #endregion
    };


    //自动把LuaCallCSharp涉及到的delegate加到CSharpCallLua列表，后续可以直接用lua函数做callback
    //[CSharpCallLua]
    //public static List<Type> CSharpCallLua
    //{
    //    get
    //    {
    //        var lua_call_csharp = LuaCallCSharp;
    //        var delegate_types = new List<Type>();
    //        var flag = BindingFlags.Public | BindingFlags.Instance
    //            | BindingFlags.Static | BindingFlags.IgnoreCase | BindingFlags.DeclaredOnly;
    //        foreach (var field in (from type in lua_call_csharp select type).SelectMany(type => type.GetFields(flag)))
    //        {
    //            if (typeof(Delegate).IsAssignableFrom(field.FieldType))
    //            {
    //                delegate_types.Add(field.FieldType);
    //            }
    //        }

    //        foreach (var method in (from type in lua_call_csharp select type).SelectMany(type => type.GetMethods(flag)))
    //        {
    //            if (typeof(Delegate).IsAssignableFrom(method.ReturnType))
    //            {
    //                delegate_types.Add(method.ReturnType);
    //            }
    //            foreach (var param in method.GetParameters())
    //            {
    //                var paramType = param.ParameterType.IsByRef ? param.ParameterType.GetElementType() : param.ParameterType;
    //                if (typeof(Delegate).IsAssignableFrom(paramType))
    //                {
    //                    delegate_types.Add(paramType);
    //                }
    //            }
    //        }
    //        return delegate_types.Distinct().ToList();
    //    }
    //}

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
		// unity
        typeof(UnityEngine.Event),
        typeof(UnityEngine.Events.UnityAction),
        typeof(System.Collections.IEnumerator),
        typeof(UnityEngine.Events.UnityAction<Vector2>),
        typeof(UnityEngine.Events.UnityAction<bool>),
        typeof(Action),
        typeof(Action<int>),
        typeof(Action<int,int>),
        typeof(Action<int, float>),
        typeof(Action<int, long>),
        typeof(Action<long, long>),
        typeof(Action<bool,GameObject>),
        typeof(Action<long,UnityEngine.AI.OffMeshLink>),
        typeof(Action<WWW>),
        typeof(Action<Transform, int>),
        typeof(Action<GameObject, int>),
        typeof(Action<Transform, int,bool>),
        typeof(Action<LuaTable, GameObject>),
        typeof(Action<LuaTable>),
        typeof(Action<LuaTable, int>),
        typeof(Action<LuaTable, LuaTable>),
        typeof(Action<GameObject, int>),
        typeof(Action<int, bool, GameObject>),
        typeof(Action<GameObject, GameObject, int, int>),
        typeof(Action<PointerEventData>),
        typeof(Action<int, int, bool>),
        typeof(Action<string>),
        typeof(Action<bool>),
        typeof(Action<long>),
        typeof(Action<float>),
        typeof(Action<float, float>),
        typeof(Action<UnityEngine.UI.Text, int>),
        typeof(Action<float,float,float,float>),
        typeof(Action<bool,float,float,float>),
        typeof(Func<string, int>),
        typeof(Func<string, bool>),
        typeof(Func<string, string>),
        typeof(Func<Vector3, Vector3>),
        typeof(Func<Vector3, float>),
        typeof(Func<int, int, int>),
        typeof(Func<string, GameObject>),
        typeof(Func<int, LuaTable>),
        typeof(Func<string, LuaTable>),
        typeof(UnityEngine.Events.UnityAction<float>),
        typeof(UnityEngine.Events.UnityAction<int>),
        typeof(YoukiaCore.Utils.CallBack),
        typeof(YoukiaCore.Utils.CallBack<float>),
        typeof(YoukiaCore.Utils.CallBack<int, YoukiaCore.Utils.CallBack>),
        typeof(YoukiaCore.Utils.CallBack<LuaTable, YoukiaCore.Utils.CallBack>),
        typeof(YoukiaCore.Event.EventHandle),
        typeof(YoukiaCore.Event.EventHandleSingle),
        typeof(Application.LogCallback),
        typeof(UIGoTable.LuaOnClickBtnAction),
        typeof(UIGoTable.LuaOnClickToggleAction),
        typeof(EventTriggerListener.VoidDelegate),
        typeof(EventTriggerListener.PointerEventDataDelegate),
        typeof(EventTriggerListener.BaseEventDataDelegate),
        typeof(EventTriggerListener.AxisEventDataDelegate),
        typeof(System.Action<EventTriggerListener>),
        typeof(DG.Tweening.Core.DOGetter<float>),
        typeof(DG.Tweening.Core.DOSetter<float>),
    };

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
        new List<string>(){ "System.Xml.XmlNodeList", "ItemOf"},
        new List<string>(){ "UnityEngine.WWW", "movie"},
#if UNITY_WEBGL
        new List<string>(){ "UnityEngine.WWW", "threadPriority"},
#endif
        new List<string>(){ "UnityEngine.Texture", "imageContentsHash"},
        new List<string>(){ "UnityEngine.Texture2D", "alphaIsTransparency"},
        new List<string>(){ "UnityEngine.Security", "GetChainOfTrustValue"},
        new List<string>(){ "UnityEngine.CanvasRenderer", "onRequestRebuild"},
        new List<string>(){ "UnityEngine.Light", "areaSize"},
        new List<string>(){ "UnityEngine.Light", "lightmapBakeType"},
        new List<string>(){ "UnityEngine.WWW", "MovieTexture"},
        new List<string>(){ "UnityEngine.WWW", "GetMovieTexture"},
        new List<string>(){ "UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
#if UNITY_2019_1_OR_NEWER
        new List<string>(){ "UnityEngine.MeshRenderer", "scaleInLightmap"},
        new List<string>(){ "UnityEngine.MeshRenderer", "receiveGI"},
        new List<string>(){ "UnityEngine.MeshRenderer", "stitchLightmapSeams"},
        new List<string>(){ "UnityEngine.ParticleSystemRenderer", "supportsMeshInstancing"},
#endif
       
#if !UNITY_WEBPLAYER
        new List<string>(){ "UnityEngine.Application", "ExternalEval"},
#endif
        new List<string>(){ "UnityEngine.GameObject", "networkView"}, //4.6.2 not support
        new List<string>(){ "UnityEngine.Component", "networkView"},  //4.6.2 not support
        new List<string>(){ "System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
        new List<string>(){ "System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
        new List<string>(){ "System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
        new List<string>(){ "System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
        new List<string>(){ "System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
        new List<string>(){ "System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
        new List<string>(){ "UnityEngine.MonoBehaviour", "runInEditMode"},
        new List<string>(){ "UnityEngine.UI.Text", "OnRebuildRequested"},
        new List<string>(){ "System.Type", "IsSZArray"},
        new List<string>(){ "UnityEngine.Input", "IsJoystickPreconfigured", "System.String"},
        #region Aor组件
        new List<string>(){ "AorImage", "SetGrayEffect", "System.Boolean"},
        new List<string>(){ "AorButton", "SetGrayEffect", "System.Boolean"},
        new List<string>(){ "AorRawImage", "SetGrayEffect", "System.Boolean"},
        new List<string>(){ "AorText", "SetGrayEffect", "System.Boolean"},
        new List<string>(){ "Gradient", "SetGrayEffect", "System.Boolean"},
	    #endregion
        #region 自定义
        //new List<string>(){ "YoukiaCore.Utils.StringUtils", "QueryToCollection", "System.String"},
	    #endregion
    };
}
