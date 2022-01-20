
public abstract class BaseBehavior
{
    public string Name;
    public WorldItem Item;
    public bool IsOnBegin;
    public abstract void OnBegin();
    public abstract void OnUpdate();
    public abstract void OnEnd();
    public BaseBehavior()
    {
        Name = GetType().Name;
    }
}
