using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponSpeedView : StatView
{
    protected override string Name() => "Weapon SPD";
    protected override float Value() => UserData.WeaponSpeed;
}
