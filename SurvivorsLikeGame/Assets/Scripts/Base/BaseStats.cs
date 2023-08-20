using UnityEngine;
using System;

public enum BonusType
{
    Bonus,
    Boost,
}

public enum StatsType
{
    Attack,
    Defense,
    MoveSpeed,
    HP,
    MaxHP,
    XP,
    MaxXP,
    PickUpRange,
    AliveTime,
    SpawnCount,
    SpawnTimerMin,
    SpawnTimerMax,
}

[Serializable]
public class BonusStats
{
    public BonusType Type;
    public StatsType Key;
    public float Value;
}

public class BaseStats
{
    public string Title;

    public int Id;

    public int Lv;

    public string Name;

    [TextArea] public string Description;

    public float Attack;

    public float Defense;

    public float HP;

    public float MaxHP;

    public float XP;

    public float MaxXP;

    public float MoveSpeed;

    public float PickUpRange;

    public float AliveTime;

    public float this[StatsType key]
    {
        get
        {
            if(key == StatsType.Attack) return Attack;
            else if (key == StatsType.Defense) return Defense;
            else if (key == StatsType.HP) return HP;
            else if (key == StatsType.MaxHP) return MaxHP;
            else if (key == StatsType.XP) return XP;
            else if (key == StatsType.MaxXP) return MaxXP;
            else if (key == StatsType.PickUpRange) return PickUpRange;
            else if (key == StatsType.AliveTime) return AliveTime;
            else return 0;
        }
        set
        {
            if(key == StatsType.Attack) Attack = value;
            else if (key == StatsType.Defense) Defense = value;
            else if (key == StatsType.HP) HP = value;
            else if (key == StatsType.MaxHP) MaxHP = value;
            else if (key == StatsType.XP) XP = value;
            else if (key == StatsType.MaxXP) MaxXP = value;
            else if (key == StatsType.PickUpRange) PickUpRange = value;
            else if (key == StatsType.AliveTime) AliveTime = value;
        }
    }

    protected float applyBonus(float currentValue, float value, BonusType type)
    {
        if(BonusType.Bonus == type)
        {
            return currentValue + value;
        }
        else if(BonusType.Boost == type)
        {
            return currentValue * (1 + value);
        }
        return currentValue;
    }

    protected void addBonus(BonusStats bonus)
    {
        float value = applyBonus(this[bonus.Key], bonus.Value, bonus.Type);

        if(StatsType.HP == bonus.Key)
        {
            value = Mathf.Clamp(value, 0, MaxHP);
        }
        else if(StatsType.XP == bonus.Key)
        {
            value = Mathf.Clamp(value, 0 , MaxXP);
        }

        this[bonus.Key] = value;
    }

    public BaseStats GetCopy()
    {
        return (BaseStats)MemberwiseClone();
    }
}