using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22
{
    public interface ISpell
    {
        public string Name { get; }
        public int ManaCost { get; }
        public bool Immediate { get; }
        public int Duration { get; }
        public int RoundCast { get; }
        public void OnCast(Combat combat);
        public void OnTick(Combat combat);
        public void OnWearsOff(Combat combat);

        // editor's note: trying to make use of C# 8's new feature here
        public static int GetSpellTimer(Combat combat, string spell)
        {
            var shield = combat.ActiveSpells.Single(x => x.Name == spell);
            var remainingRounds = shield.Duration - (combat.Round - shield.RoundCast);
            return remainingRounds;
        }

        public static ISpell Cast(string name, int round)
        {
            switch (name)
            {
                case "Magic Missile":
                    return new MagicMissile(round);
                case "Drain":
                    return new Drain(round);
                case "Shield":
                    return new Shield(round);
                case "Poison":
                    return new Poison(round);
                case "Recharge":
                    return new Recharge(round);
                default:
                    throw new Exception($"Invalid spell name: '{name}'");
            }
        }

        public static SpellCatalogItem[] GetSpellCatalog()
        {
            var spells = new string[]
            {
                "Magic Missile",
                "Drain",
                "Shield",
                "Poison",
                "Recharge"
            };

            var catalog = new List<SpellCatalogItem>();
            foreach (var name in spells)
            {
                var spell = Cast(name, 0); // apology: inelegant/hacky way to access instance properties we need
                catalog.Add(new SpellCatalogItem(spell.Name, spell.ManaCost, spell.Duration));
            }

            return catalog.ToArray();
        }
    }

    public record SpellCatalogItem(string Name, int ManaCost, int Duration);
}
