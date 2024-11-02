using System;

[Flags]
public enum EnemyType
{
    Normal = 1,
    Stronger = 2,
    Boos = 4
}

public enum SkillType
{
    BuffGate,
    Blade,
    ShootUpgrade,
    Shield
}