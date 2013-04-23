using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NullQuest.Game;

namespace NullQuest.Tests
{
    class PlayerTests
    {
        [Test]
        public void ExperienceRequiredForNextLevel_CalculatesExperienceCorrectly()
        {
            Assert.AreEqual(31, Player.ExperienceRequiredForNextLevel(1));
            Assert.AreEqual(115, Player.ExperienceRequiredForNextLevel(2));
            Assert.AreEqual(272, Player.ExperienceRequiredForNextLevel(3));
            Assert.AreEqual(524, Player.ExperienceRequiredForNextLevel(4));
            Assert.AreEqual(891, Player.ExperienceRequiredForNextLevel(5));
        }
    }
}
