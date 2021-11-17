using System;

namespace Day22
{
    public class MagicMissile : ISpell
    {
        private const int Damage = 4;

        public MagicMissile(int roundCast)
        {
            RoundCast = roundCast;
        }

        public string Name { get; } = "Magic Missile";
        public int ManaCost { get; } = 53;
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
            combat.Log.Add($"Player casts Magic Missile, dealing {Damage} damage.");
        }
        public void OnTick(Combat combat) {}
        public void OnWearsOff(Combat combat) {}
    }
}