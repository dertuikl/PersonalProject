using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthView : StatView
{
    protected override string Name() => "Health";
    protected override float Value() => UserData.Health;

    private int maxHealth => UserData.MaxHealth;

    public override void Refresh()
    {
        text.text = $"{Value()}/{maxHealth}";
    }
}
