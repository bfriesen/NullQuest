using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game.Combat
{
    public class CombatRoundResult
    {
        public IEnumerable<ICombatAction> CombatActions { get; set; }

        //public class CombatAction
        //{
        //    public IEnumerable<CharacterAdjustment> DamageComponents { get; set; }
        //    public IEnumerable<string> AttackTextLines { get; set; }
        //}

        //public class CharacterAdjustment
        //{
        //    public Character Target { get; set; }
        //    public int DamageReceived { get; set; }
        //}
    }
}
