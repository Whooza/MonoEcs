namespace MonoEcs.Core.Interfaces;

public interface IEcsEntitySystem
{
    bool Enabled { get; set; }

    void AddComponent(int entityId, IEcsComponent component);
    void AddComponent(IEcsEntity entity, IEcsComponent component);
    void RemoveComponent(int entityId);
    void RemoveComponent(IEcsEntity entity);
    void Update(IEcsEntity[] entities, float deltaTime);
    void Draw(IEcsEntity[] entities, float deltaTime);
}
