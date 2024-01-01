namespace MonoEcs.Core.Components;

public struct InputData : IEcsComponent
{
    public int Id { get; }
    public bool Exists { get; set; }

    public InputData(int id, bool exists = true)
    {
        Id = id;
        Exists = exists;
    }
}
