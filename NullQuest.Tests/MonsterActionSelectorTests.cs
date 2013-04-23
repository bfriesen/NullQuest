using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NullQuest.Game;
using NullQuest.Game.Combat;
using Moq;

namespace NullQuest.Tests
{
    class MonsterActionSelectorTests
    {
        [Test]
        public void GetWeights_NoActionsHaveTakenPlaceInCombat_EqualToValuesOfAllowedActionsPotentialCombatEffects()
        {
            var dice = new Mock<IDice>();
            var selector = new MonsterActionSelector(dice.Object);
            var context = new CombatContext();
            var monster = new Monster();
            monster.SetCombatContext(context);
            monster.SetActionSelector(selector);
            context.Monster = monster;
            var actionMock1 = new Mock<ICombatAction>();
            var actionMock2 = new Mock<ICombatAction>();
            actionMock1.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 10, Healing = 20 });
            actionMock1.SetupGet(m => m.Name).Returns("Action 1");
            actionMock2.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 60, Healing = 0 });
            actionMock2.SetupGet(m => m.Name).Returns("Action 2");

            var weights = selector.GetWeights(context, new[] { actionMock1.Object, actionMock2.Object });

            Assert.AreEqual(30, weights[actionMock1.Object.CreateHash()]);
            Assert.AreEqual(60, weights[actionMock2.Object.CreateHash()]);
        }

        [Test]
        public void GetWeights_NoActionsHaveTakenPlaceInCombat_DamageAndHealingHaveEqualWeights()
        {
            var dice = new Mock<IDice>();
            var selector = new MonsterActionSelector(dice.Object);
            var context = new CombatContext();
            var monster = new Monster();
            monster.SetCombatContext(context);
            monster.SetActionSelector(selector);
            context.Monster = monster;
            var actionMock1 = new Mock<ICombatAction>();
            var actionMock2 = new Mock<ICombatAction>();
            actionMock1.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 0, Healing = 10 });
            actionMock1.SetupGet(m => m.Name).Returns("Action 1");
            actionMock2.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 10, Healing = 0 });
            actionMock2.SetupGet(m => m.Name).Returns("Action 2");

            var weights = selector.GetWeights(context, new[] { actionMock1.Object, actionMock2.Object });

            Assert.AreEqual(weights[actionMock2.Object.CreateHash()], weights[actionMock1.Object.CreateHash()]);
        }

        [Test]
        public void SelectAction_NoActionsHaveTakenPlaceInCombat_DiceRollCorrespondsToSelection()
        {
            // Arrange
            var actionMock1 = new Mock<ICombatAction>();
            var actionMock2 = new Mock<ICombatAction>();
            var actionMock3 = new Mock<ICombatAction>();
            actionMock1.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 3, Healing = 0 });
            actionMock1.SetupGet(m => m.Name).Returns("Action 1");
            actionMock2.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 5, Healing = 0 });
            actionMock2.SetupGet(m => m.Name).Returns("Action 2");
            actionMock3.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 2, Healing = 0 });
            actionMock3.SetupGet(m => m.Name).Returns("Action 3");
            var diceRollToAction = new Dictionary<int, string>();

            for (int i = 0; i < 10; i++)
            {
                // Arrange
                var dice = new Mock<IDice>();
                dice.Setup(x => x.Random(It.IsAny<int>(), It.IsAny<int>())).Returns((int min, int max) => min + i);
                var selector = new MonsterActionSelector(dice.Object);
                var context = new CombatContext();
                var monster = new Monster();
                monster.SetCombatContext(context);
                monster.SetActionSelector(selector);
                context.Monster = monster;

                // Act
                diceRollToAction.Add(i, selector.SelectAction(context, new[] { actionMock1.Object, actionMock2.Object, actionMock3.Object }).Name);
            }

            // Assert
            Assert.AreEqual(diceRollToAction[0], "Action 1");
            Assert.AreEqual(diceRollToAction[1], "Action 1");
            Assert.AreEqual(diceRollToAction[2], "Action 1");
            Assert.AreEqual(diceRollToAction[3], "Action 2");
            Assert.AreEqual(diceRollToAction[4], "Action 2");
            Assert.AreEqual(diceRollToAction[5], "Action 2");
            Assert.AreEqual(diceRollToAction[6], "Action 2");
            Assert.AreEqual(diceRollToAction[7], "Action 2");
            Assert.AreEqual(diceRollToAction[8], "Action 3");
            Assert.AreEqual(diceRollToAction[9], "Action 3");
        }

        [Test]
        public void SelectAction_DiceRollRequestedIsTheTotalOfAllWeights()
        {
            var dice = new Mock<IDice>();
            int minCalled = -1;
            int maxCalled = -1;
            dice.Setup(x => x.Random(It.IsAny<int>(), It.IsAny<int>())).Returns((int min, int max) =>
                {
                    minCalled = min;
                    maxCalled = max;
                    return min;
                });
            var selector = new MonsterActionSelector(dice.Object);
            var context = new CombatContext();
            var monster = new Monster();
            monster.SetCombatContext(context);
            monster.SetActionSelector(selector);
            context.Monster = monster;
            var actionMock1 = new Mock<ICombatAction>();
            var actionMock2 = new Mock<ICombatAction>();
            actionMock1.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 10, Healing = 20 });
            actionMock1.SetupGet(m => m.Name).Returns("Action 1");
            actionMock2.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 60, Healing = 0 });
            actionMock2.SetupGet(m => m.Name).Returns("Action 2");

            var weights = selector.SelectAction(context, new[] { actionMock1.Object, actionMock2.Object });

            Assert.AreEqual(1, minCalled);
            Assert.AreEqual(90, maxCalled);
        }

        [Test]
        public void GetWeights_MinimumWeightIsOne()
        {
            var dice = new Mock<IDice>();
            var selector = new MonsterActionSelector(dice.Object);
            var context = new CombatContext();
            var monster = new Monster();
            monster.SetCombatContext(context);
            monster.SetActionSelector(selector);
            context.Monster = monster;
            var actionMock1 = new Mock<ICombatAction>();
            var actionMock2 = new Mock<ICombatAction>();
            actionMock1.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 0, Healing = 0 });
            actionMock1.SetupGet(m => m.Name).Returns("Action 1");
            actionMock2.Setup(m => m.GetPotentialCombatOutcome(It.IsAny<Combatant>())).Returns(new CombatOutcome() { Damage = 0, Healing = 0 });
            actionMock2.SetupGet(m => m.Name).Returns("Action 2");

            var weights = selector.GetWeights(context, new[] { actionMock1.Object, actionMock2.Object });

            Assert.AreEqual(1, weights[actionMock1.Object.CreateHash()]);
            Assert.AreEqual(1, weights[actionMock2.Object.CreateHash()]);
        }
    }
}
