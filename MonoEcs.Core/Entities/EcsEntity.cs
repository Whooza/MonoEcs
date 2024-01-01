namespace MonoEcs.Core.Entities;

public struct EcsEntity : IEcsEntity
{
    public int Id { get; }
    public string Name { get; }
    public bool Exists { get; set; }
    public bool Enabled { get; set; }

    public EcsEntity(int id, string name, bool exists = true, bool enabled = true)
    {
        Id = id;
        Name = name;
        Exists = exists;
        Enabled = enabled;
    }
}
