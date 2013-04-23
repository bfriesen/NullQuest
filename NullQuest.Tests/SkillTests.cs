//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUnit.Framework;
//using NullQuest.Game;

//namespace NullQuest.Tests
//{
//    class SkillTests
//    {
//        [Test]
//        public void Perform_CharacterDoesNotHaveEnoughEnergy_ReturnsNotEnoughEnergyMessage()
//        {
//            var hero = new Player() {CurrentEnergy = 0};
//            var world = new GameWorld() {Player = hero};
//            var skill = new Skill("Nothing", 5, w => "This skill successfully does nothing");

//            Assert.AreEqual(Strings.Skill_Perform_Not_enough_energy, skill.Perform(world));
//        }

//        [Test]
//        public void Perform_CharacterHasEnoughEnergy_PerformsPassedInAction()
//        {
//            var hero = new Player() { CurrentEnergy = 10 };
//            var world = new GameWorld() {Player = hero};
//            var skill = new Skill("Nothing", 5, w => "This skill successfully does nothing");

//            Assert.AreEqual("This skill successfully does nothing", skill.Perform(world));
//        }

//        [Test]
//        public void Perform_SkillPerformed_CharacterEnergyReducedByCost()
//        {
//            var hero = new Player() { CurrentEnergy = 10 };
//            var world = new GameWorld() {Player = hero};
//            var skill = new Skill("Nothing", 8, w => "This skill successfully does nothing");

//            skill.Perform(world);
//            Assert.AreEqual(2, world.Player.CurrentEnergy);
//        }
//    }
//}
