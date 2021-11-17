using System;

namespace Day22
{
    public class Drain : ISpell
    {
        private const int Damage = 2;
        private const int HitPointsHealed = 2;

        public Drain(int roundCast)
        {
            RoundCast = roundCast;
        }

        public string Name { get; } = "Drain";
        public int ManaCost { get; } = 73;
        public bool Immediate { get; } = true;
        public int Duration { get; } = 0;
        public int RoundCast { get; }
        public void OnCast(Combat combat)
        {
            combat.PlayerManaPoints -= ManaCost;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += ManaCost;
            combat.BossHitPoints -= Damage;
            combat.PlayerHitPoints += HitPointsHealed;
            combat.Log.Add($"Player casts Drain, dealing {Damage} damage, and healing {HitPointsHealed} hit points.");
        }

        public void OnTick(Combat combat) {}
        public void OnWearsOff(Combat combat) {}
    }
}