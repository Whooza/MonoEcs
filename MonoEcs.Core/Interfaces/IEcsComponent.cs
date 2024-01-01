namespace MonoEcs.Core.Interfaces;

public interface IEcsComponent
{
    int Id { get; }
    bool Exists { get; set; }
}
