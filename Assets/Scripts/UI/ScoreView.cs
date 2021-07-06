using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreView : StatView
{
    protected override string Name() => "Score";
    protected override float Value() => UserData.Score;
}
