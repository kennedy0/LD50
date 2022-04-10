public readonly struct Terrain
{
    public readonly string Name;

    public Terrain(string name)
    {
        Name = name;
    }
    
    /// <summary>
    /// Use the name as the hash code; needed by the equality operator.
    /// </summary>
    public override int GetHashCode()
    {
        return Name != null ? Name.GetHashCode() : 0;
    }

    public static Terrain Forest => new Terrain("Forest");
}
