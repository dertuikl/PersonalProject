// INHERITANCE
public class PowerupLive : Powerup
{
    // POLYMORPHISM
    protected override void SpecificInfluence(PlayerController player)
    {
        player.UpdateHealth((int)impactValue);
    }
}
