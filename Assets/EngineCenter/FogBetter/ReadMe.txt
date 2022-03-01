1.功能:
优化得雾效，合并了距离雾效 和 高度雾效。

2.使用：
(1)将FogBetter这个prefab拖动到场景中，在FogBetterMgr脚本上点击mEnableFog 开启雾效,调整参数就可以设置整体场景得雾效了。
(2)如果某个物体需要单独控制雾效，需要给那个物体挂上FogBetterSingleControl这个脚本，然后新建一个单独得材质拖动到这个物体上，
这个脚本就可以不受到FogBetterMgr得控制，而由材质球上得参数来进行控制。
（注意：场景物体得shader需要添加雾效得代码才有效果，可以参考这里得SceneObjFogBetter这个shader得使用方法）

3.参数
FogBetterMgr
(1)mEnableFog
雾效总开关

(2)mFogDistColor
距离雾效颜色

(3)mFogHeightColor
高度雾效颜色

(4)mFogDisturbTex
扰动贴图

(5)mFogDisturbIntensity
扰动强度

(6)mFogDisturbSpeedX
x方向扰动速度

(7)mFogDisturbSpeedX
y方向扰动速度

(8)mFogDistDensity
距离雾效密度，根据距离判断雾的多少。 此时使用的是 距离雾的颜色

(9)mFogDistIntensity
距离雾效整体强度

(10)mFogHeightDensity
高度雾效密度，根据高度判断雾的多少。此时使用的颜色是 高度雾颜色

(11)mFogHeightDistDensity
高度雾效下，根据距离修改雾效密度。 此时使用的颜色是 高度雾颜色

(12)mFogHeightIntensity
高度雾效的整体强度

(13)mFogHeightBase
修改高度雾的基准值，会改变雾效的高度方向上的密度

(14)mFogDistOffset
雾效的整体起始点偏移

FogBetterSingleControl
参数跟上面一样



2021.9.11
 增加了对shadergraph得支持，直接将FogBetterShaderSubGraph 拖动到shadergraph中就行了，输入一个场景得颜色，输出得颜色就是混合了雾效得。
 

