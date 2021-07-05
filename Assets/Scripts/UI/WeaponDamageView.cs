using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponDamageView : StatView
{
    protected override string Name() => "Weapon DMG";
    protected override float Value() => UserData.WeaponDamage;
}
