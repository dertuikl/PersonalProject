public class PowerupLive : Powerup
{
    protected override void SpecificInfluence(PlayerController player)
    {
        player.UpdateHealth((int)impactValue);
    }
}
