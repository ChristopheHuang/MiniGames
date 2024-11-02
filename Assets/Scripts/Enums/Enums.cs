using System;

[Flags]
public enum EnemyType
{
    Normal = 1,
    Stronger = 2,
    Boos = 4
}

[Flags]
public enum SkillType
{
    BuffGate = 1,
    Blade = 2,
    ShootUpgrade = 4,
    Shield = 8
}