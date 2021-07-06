using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStatsView : StatView
{
    protected override string Name() => string.Empty;
    protected override float Value() => 0;

    private bool show => UserData.ShowShootingStats;

    public override void Refresh()
    {
        gameObject.SetActive(show);
    }
}
