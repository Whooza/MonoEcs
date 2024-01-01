namespace MonoEcs.Core.Interfaces;

public interface IEcsGlobalSystem
{
    bool Enabled { get; set; }

    void Update(float deltaTime);
}
