namespace MonoEcs.Core.Components;

public struct StaticSprite : IEcsComponent
{
    public int Id { get; }
    public bool Exists { get; set; }
    public string Path { get; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float OriginX { get; set; }
    public float OriginY { get; set; }
    public float ScaleX { get; set; }
    public float ScaleY { get; set; }

    public StaticSprite(int id, string path, float positionX, float positionY, float originX, float originY, float scaleX, float scaleY)
    {
        Id = id;
        Exists = true;
        Path = path;
        PositionX = positionX;
        PositionY = positionY;
        OriginX = originX;
        OriginY = originY;
        ScaleX = scaleX;
        ScaleY = scaleY;
    }

    public Vector2 GetPositionVector()
    {
        return new Vector2(PositionX, PositionY);
    }
}
