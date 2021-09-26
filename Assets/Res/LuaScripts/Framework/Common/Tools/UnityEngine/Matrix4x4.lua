local math	= math
local tan   = math.tan
local pow   = Mathf.Pow
local setmetatable = setmetatable
local getmetatable = getmetatable
local rawget = rawget
local rawset = rawset
local Vector3 = Vector3
local Vector4 = Vector4
local order=4 --阶数

---@class Matrix4x4
local Matrix4x4 ={}

Matrix4x4.__index = function(t, k)
    local var=rawget(Matrix4x4,k)

    if var~=nil then
        return var
    end

    if k=="m00" then
        return t[1][1]
    elseif k== "m01" then
        return t[1][2]
    elseif k== "m02" then
        return t[1][3]
    elseif k== "m03" then
        return t[1][4]
    elseif k== "m10" then
        return t[2][1]
    elseif k== "m11" then
        return t[2][2]
    elseif k== "m12" then
        return t[2][3]
    elseif k== "m13" then
        return t[2][4]
    elseif k== "m20" then
        return t[3][1]
    elseif k== "m21" then
        return t[3][2]
    elseif k== "m22" then
        return t[3][3]
    elseif k== "m23" then
        return t[3][4]
    elseif k== "m30" then
        return t[4][1]
    elseif k== "m31" then
        return t[4][2]
    elseif k== "m32" then
        return t[4][3]
    elseif k== "m33" then
        return t[4][4]
    end

    return nil
end

Matrix4x4.__newindex = function(t, k, v)
    if t[k]~=nil then
        t[k]=v
    end
end


function Matrix4x4.New(m00,m01,m02,m03,m10,m11,m12,m13,m20,m21,m22,m23,m30,m31,m32,m33)
    local m =
    {
        {m00 or 0,m01 or 0,m02 or 0,m03 or 0},
        {m10 or 0,m11 or 0,m12 or 0,m13 or 0},
        {m20 or 0,m21 or 0,m22 or 0,m23 or 0},
        {m30 or 0,m31 or 0,m32 or 0,m33 or 0}
    }

    setmetatable(m, Matrix4x4)
    return m
end

local _new=Matrix4x4.New

Matrix4x4.__call = function(t,m00,m01,m02,m03,m10,m11,m12,m13,m20,m21,m22,m23,m30,m31,m32,m33)
    return _new(m00,m01,m02,m03,m10,m11,m12,m13,m20,m21,m22,m23,m30,m31,m32,m33)
end

--获取一个单位矩阵
function Matrix4x4.identity()
    return _new(
            1,0,0,0,
            0,1,0,0,
            0,0,1,0,
            0,0,0,1)
end

function Matrix4x4.zero()
    return _new(
            0,0,0,0,
            0,0,0,0,
            0,0,0,0,
            0,0,0,0)
end

--获取行列式的值
function Matrix4x4:DeterminantSelf()
    return Matrix4x4.Determinant(self)
end

--获取逆矩阵
function Matrix4x4:InverseSelf()
    return Matrix4x4.Inverse(self)
end

--是否是单位矩阵
function Matrix4x4:isIdentity()
    return self[1][1]==1 and self[2][2]==1 and self[3][3]==1 and self[4][4]==1
end

--求nxn的方阵m的行列式的值，order为阶数
function Matrix4x4.DeterminantOfOrder(m,order)
    local sum1,sum2=0,0

    for col=1,order do

        local product=1

        for times=0,order-1 do
            product=product*m[1+times][(col+times-1)%order+1]
        end

        sum1=sum1+product
    end

    for col=1,order do

        local product=1

        for times=0,order-1 do
            product=product*m[order-times][(col+times-1)%order+1]
        end

        sum2=sum2+product
    end

    return sum1-sum2
end

--返回第i列，vector4类型
function Matrix4x4:GetColumn(i)
    return Vector4(self[1][i],self[2][i],self[3][i],self[4][i])
end

--返回第i行，vector4类型
function Matrix4x4:GetRow(i)
    return Vector4(self[i][1],self[i][2],self[i][3],self[i][4])
end

--把一个Vector3的坐标（列向量）左乘矩阵转换到另一个坐标系，返回Vector3类型
--因为是坐标，所以w默认按1处理
function Matrix4x4:MultiplyPoint(v3)
    local v=Vector4(v3.x,v3.y,v3.z,1)
    local v2=self:MultiplyVector4(v)

    if v2.w==0 then
        print("除数无意义")
        v2.x=0
        v2.y=0
        v2.z=0
        return v2
    end

    return Vector3(v2.x/v2.w,v2.y/v2.w,v2.z/v2.w)
end

--把一个Vector4的坐标或向量（列向量）左乘矩阵转换到另一个坐标系，返回Vector4类型
function Matrix4x4:MultiplyVector4(v4)
    local result= Vector4.New(0, 0, 0, 0)
    result.x = v4.x * self[1][1] + v4.y * self[1][2] + v4.z * self[1][3] + v4.w * self[1][4]
    result.y = v4.x * self[2][1] + v4.y * self[2][2] + v4.z * self[2][3] + v4.w * self[2][4]
    result.z = v4.x * self[3][1] + v4.y * self[3][2] + v4.z * self[3][3] + v4.w * self[3][4]
    result.w = v4.x * self[4][1] + v4.y * self[4][2] + v4.z * self[4][3] + v4.w * self[4][4]
    return result
end

--把一个Vector3的向量（列向量）左乘矩阵转换到另一个坐标系，返回Vector3类型
--因为是向量，所以w默认按0处理
function Matrix4x4:MultiplyVector(v3)
    local v=Vector4(v3.x,v3.y,v3.z,0)
    local v2=self:MultiplyVector4(v)

    if v2.w==0 then
        print("除数无意义")
        v2.x=0
        v2.y=0
        v2.z=0
        return v2
    end

    return Vector3(v2.x/v2.w,v2.y/v2.w,v2.z/v2.w)
end

--设置第i列的值为一个Vector4向量
function Matrix4x4:SetColumn(i,v4)
    self[1][i]=v4.x;
    self[2][i]=v4.y;
    self[3][i]=v4.z;
    self[4][i]=v4.w;
end

--设置第i行的值为一个Vector4向量
function Matrix4x4:SetRow(i,v4)
    self[i][1]=v4.x;
    self[i][2]=v4.y;
    self[i][3]=v4.z;
    self[i][4]=v4.w;
end

--设置平移pos(Vector3),旋转q(Quaternion),缩放s(Vector3)矩阵
function Matrix4x4:SetTRS(pos,q,s)
    self=Matrix4x4.TRS(pos,q,s)
end

--求4x4矩阵m的行列式的值
function Matrix4x4.Determinant(m)
    return Matrix4x4.DeterminantOfOrder(m,order)
end

--求4x4矩阵m的第i行第j列的余子式的值，i,j从1开始
function Matrix4x4.GetCofactor(m,i,j)
    local cofactor =
    {
        {0,0,0},
        {0,0,0},
        {0,0,0}
    }

    local arow=1

    for row=1,order-1 do
        if row>=i then
            arow=row+1
        else
            arow=row
        end

        local acol=1

        for col=1,order-1 do
            if col>=j then
                acol=col+1
            else
                acol=col
            end

            cofactor[row][col]=m[arow][acol]
        end
    end

    return  Matrix4x4.DeterminantOfOrder(cofactor,order-1)
end

--求4x4矩阵m的第i行第j列的代数余子式的值，i,j从1开始
function Matrix4x4.GetAlgebraic(m,i,j)
    return pow(-1,i+j)*Matrix4x4.GetCofactor(m,i,j)
end

--求一个4x4矩阵的逆矩阵，如果不可逆，则打印异常
function Matrix4x4.Inverse(m)
    local det=m:DeterminantSelf()

    if det==0 then
        print("不可逆")
        return 1e-6
    end

    local idet=1/det
    local im=Matrix4x4.zero()

    for row=1,order do
        for col=1,order do
            im[row][col]=m.GetAlgebraic(m,row,col)
        end
    end

    return im
end

--获取正交投影矩阵
function Matrix4x4.Ortho(left,right,bottom,top,zNear,zFar)
    local m=Matrix4x4.zero()
    local x =2.0/(right - left)
    local y =2.0/(top - bottom)
    local z =-2.0/(zFar - zNear)
    local a = -(right + left) / (right - left)
    local b = -(top + bottom) / (top - bottom)
    local c =-(zFar + zNear) / (zFar - zNear)

    m[1][1] = x
    m[1][2] = 0
    m[1][3] = 0
    m[1][4] = a
    m[2][1] = 0
    m[2][2] = y
    m[2][3] = 0
    m[2][4] = b
    m[3][1] = 0
    m[3][2] = 0
    m[3][3] = z
    m[3][4] = c
    m[4][1] = 0
    m[4][2] = 0
    m[4][3] = 0
    m[4][4] = 1

    return m
end

--获取透视投影矩阵
function Matrix4x4.Perspective(left,  right,  bottom,  top,  zNear,  zFar)
    local m=Matrix4x4.zero()
    local x = 2.0 * zNear / (right - left)
    local y = 2.0 * zNear / (top - bottom)
    local a = (right + left) / (right - left)
    local b = (top + bottom) / (top - bottom)
    local c = -(zFar + zNear) / (zFar - zNear)
    local d = -(2.0 * zFar * zNear) / (zFar - zNear)
    local e = -1.0
    m[1][1] = x
    m[1][2] = 0
    m[1][3] = a
    m[1][4] = 0
    m[2][1] = 0
    m[2][2] = y
    m[2][3] = b
    m[2][4] = 0
    m[3][1] = 0
    m[3][2] = 0
    m[3][3] = c
    m[3][4] = d
    m[4][1] = 0
    m[4][2] = 0
    m[4][3] = e
    m[4][4] = 0

    return m
end

--获取透视投影矩阵
function Matrix4x4.Perspective( fov,  aspect,  zNear,  zFar)
    local top=zNear*tan(fov*0.5)
    local bottom=-top
    local right=top*aspect
    local left=-right
    return Matrix4x4.Perspective(left,right,bottom,top,zNear,zFar)
end

--创建一个平移矩阵,参数v3(Vector3)
function Matrix4x4.Translation(v3)
    local m=Matrix4x4.identity()
    m[4][1]=v3.x
    m[4][2]=v3.y
    m[4][3]=v3.z
    return m
end

--创建一个绕任意轴旋转的矩阵,q(Quaternion)
function Matrix4x4.Spin(q)
    local m=Matrix4x4.identity()

    local x2=q.x*q.x
    local y2=q.y*q.y
    local z2=q.z*q.z
    local xy=q.x*q.y
    local xz=q.x*q.z
    local yz=q.y*q.z
    local wx=q.w*q.x
    local wy=q.w*q.y
    local wz=q.w*q.z

    m[1][1]=1-2*(y2+z2)
    m[1][2]=2*(xy-wz)
    m[1][3]=2*(xz+wy)
    m[2][1]=2*(xy+wz)
    m[2][2]=1-2*(x2+z2)
    m[2][3]=2*(yz-wx)
    m[3][1]=2*(xz-wy)
    m[3][2]=2*(yz+wx)
    m[3][3]=1-2*(x2+y2)
    return m
end

--创建一个缩放矩阵
function Matrix4x4.Scale(v3)
    local m=Matrix4x4.identity()
    m[1][1]=v3.x
    m[2][2]=v3.y
    m[3][3]=v3.z
    return m
end

--求转置矩阵
function Matrix4x4.Transpose(m)
    local t=Matrix4x4.zero()

    for i=1,order do
        for j=1,order do
            t[i][j]=m[j][i]
        end
    end

    return t
end

-- TODO 与unity Matrix4x4 中SetTRS计算结果不一致
--创建一个平移pos(Vector3),旋转q(Quaternion),缩放s(Vector3)矩阵
function Matrix4x4.TRS(pos,q,s)
    local m1=Matrix4x4.Spin(q)
    local m2=Matrix4x4.Scale(s)
    local m3=m2*m1
    m3[1][4]=pos.x
    m3[2][4]=pos.y
    m3[3][4]=pos.z
    return m3
end

Matrix4x4.__add=function(lhs,rhs)
    local m=Matrix4x4.zero()

    for i=1,order do
        for j=1,order do
            m[i][j]=lhs[i][j]+rhs[i][j]
        end
    end

    return m
end

Matrix4x4.__sub=function(lhs,rhs)
    local m=Matrix4x4.zero()

    for i=1,order do
        for j=1,order do
            m[i][j]=lhs[i][j]-rhs[i][j]
        end
    end

    return m
end

--两个4x4方阵相乘，根据opengl里的左乘
Matrix4x4.__mul=function(lhs,rhs)
    local m=Matrix4x4.zero()

    for i=1,order do
        for j=1,order do
            local sum=0

            for index=1,order do
                sum=sum+lhs[i][index]*rhs[index][j]
            end

            m[i][j]=sum
        end
    end
    return m
end

--两个4x4方阵相除，根据opengl里的左乘rhs的逆矩阵
Matrix4x4.__div=function(lhs,rhs)
    local m_inv=rhs:Inverse()
    return lhs*m_inv
end

--判断两个4x4矩阵是否相等
Matrix4x4.__eq = function(lhs,rhs)
    if lhs ==nil or rhs == nil then
        return false
    end

    for i=1,order do
        for j=1,order do
            if lhs[i][j]~=rhs[i][j] then return false end
        end
    end

    return true
end

Matrix4x4.__tostring = function(self)
    return "["..self[1][1]..self[1][2]..self[1][3]..self[1][4]
            ..self[2][1]..self[2][2]..self[2][3]..self[2][4]
            ..self[3][1]..self[3][2]..self[3][3]..self[3][4]
            ..self[4][1]..self[4][2]..self[4][3]..self[4][4].."]"
end

setmetatable(Matrix4x4, Matrix4x4)
return Matrix4x4