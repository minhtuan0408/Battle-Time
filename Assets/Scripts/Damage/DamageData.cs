[System.Serializable]
public class DamageData
{
    public int damage;
    public DamageType damageType;
    public float effectDuration;
}

public enum DamageType
{
    Normal,
    Fire,
    Poison,
    Ice
}