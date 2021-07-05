using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackSpeedView : StatView
{
    protected override string Name() => "Attack SPD";
    protected override float Value() => UserData.AttackSpeed;
}
