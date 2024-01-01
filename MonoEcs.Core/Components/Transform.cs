namespace MonoEcs.Core.Components;

public struct Transform : IEcsComponent
{
    public int Id { get; }
    public bool Exists { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float Rotation { get; set; }
    public float ScaleX { get; set; }
    public float ScaleY { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }

    public Transform(int id, bool exists, float positionX, float positionY, float rotation,
        float scaleX = 1f, float scaleY = 1f, float velocityX = 0f, float velocityY = 0f)
    {
        Id = id;
        Exists = exists;
        PositionX = positionX;
        PositionY = positionY;
        Rotation = rotation;
        ScaleX = scaleX;
        ScaleY = scaleY;
        VelocityX = velocityX;
        VelocityY = velocityY;
    }
}
