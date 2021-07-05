using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserData
{
    public const int MaxHealth = 9;

    public static int Health { get; private set; } = MaxHealth;
    public static int Score { get; private set; } = 0;

    public static float AttackSpeed { get; private set; } = 20f;
    public static float WeaponSpeed { get; private set; } = 20f;
    public static float WeaponDamage { get; private set; } = 18f;

    public static bool ShowShootingStats { get; private set; } = true;

    public static Action OnUserDataChanged;

    #region Game Section

    public static void SetPlayerHealth(int health)
    {
        Health = Mathf.Clamp(health, 0, MaxHealth);
        OnUserDataChanged?.Invoke();
    }

    public static void SetPlayerScore(int score)
    {
        Score = score;
        OnUserDataChanged?.Invoke();
    }

    public static void SetAttackSpeed(float attackSpeed)
    {
        AttackSpeed = attackSpeed;
        OnUserDataChanged?.Invoke();
    }

    public static void SetWeaponSpeed(float weaponSpeed)
    {
        WeaponSpeed = weaponSpeed;
        OnUserDataChanged?.Invoke();
    }

    public static void SetWeaponDamage(float weaponDamage)
    {
        WeaponDamage = weaponDamage;
        OnUserDataChanged?.Invoke();
    }

    #endregion

    #region Settings Section

    public static void ShowShootingStatsToggle() => ShowShootingStats = !ShowShootingStats;

    public static void ResetCurrentGameData()
    {
        Health = MaxHealth;
        Score = 0;
        AttackSpeed = 20f;
        WeaponSpeed = 20f;
        WeaponDamage = 18f;
    }

    #endregion
}
