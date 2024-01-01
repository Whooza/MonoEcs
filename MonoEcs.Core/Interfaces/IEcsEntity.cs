namespace MonoEcs.Core.Interfaces;

public interface IEcsEntity
{
    int Id { get; }
    string Name { get; }
    bool Exists { get; set; }
    bool Enabled { get; set; }
}
