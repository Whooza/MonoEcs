namespace MonoEcs.Core.Systems.Bases;

public abstract class EcsGlobalSystem<TSystem> : IEcsGlobalSystem where TSystem : IEcsGlobalSystem
{
    public bool Enabled { get; set; }

    protected readonly EcsContainer SystemContainer;

    protected EcsGlobalSystem(EcsContainer systemContainer)
    {
        SystemContainer = systemContainer;
        Enabled = true;
    }

    public void Update(float deltaTime)
    {
        if (Enabled)
        {
            UpdateLogic(deltaTime);
        }
    }

    protected virtual void UpdateLogic(float deltaTime) { }
}
