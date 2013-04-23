using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game.Combat
{
    public class WeaponArchetype
    {
        public string Name { get; set; }
        public Magnitude Damage { get; set; }
        public WeaponType WeaponType { get; set; }
        public DamageType DamageType { get; set; }

        public WeaponArchetype(string name)
        {
            Name = name;
        }
    }
}
