namespace MonoEcs.Core.Systems.Bases;

public readonly struct EcsEntitySystemConfig
{
    public readonly int MaxComponentCount;
    public readonly bool ShouldUpdate;
    public readonly bool ShouldDraw;

    public EcsEntitySystemConfig(int maxComponentCount, bool shouldUpdate, bool shouldDraw)
    {
        MaxComponentCount = maxComponentCount;
        ShouldUpdate = shouldUpdate;
        ShouldDraw = shouldDraw;
    }
}
