using NullQuest.Game.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Game
{
    public class Monster : Combatant
    {
        private CombatContext _combatContext;
        private IActionSelector _actionSelector;
        private string _bareHandsAttackName;

        public MonsterClass Class { get; set; }

        public bool IsBoss
        {
            get { return Class.IsBoss; }
        }

        protected override StatsModifier BaseStatsModifier
        {
            get { return Class; }
        }

        protected override IEnumerable<ICombatAction> AdditionalActions(IContext context)
        {
            yield break;
        }

        public ICombatAction GetCombatAction()
        {
            return _actionSelector.SelectAction(_combatContext, GetAllowedActions(_combatContext));
        }

        public override string BareHandsAttackName
        {
            get { return _bareHandsAttackName ?? "Monster Bare Hands Attack"; }
        }

        public void SetActionSelector(IActionSelector actionSelector)
        {
            _actionSelector = actionSelector;
        }

        public void SetCombatContext(CombatContext combatContext)
        {
            _combatContext = combatContext;
        }

        public void SetBareHandsAttackName(string attackName)
        {
            _bareHandsAttackName = attackName;
        }
    }
}
