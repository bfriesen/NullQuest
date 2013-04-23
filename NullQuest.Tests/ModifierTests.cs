using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NullQuest.Game;
using NullQuest.Game.Extensions;

namespace NullQuest.Tests
{
    public class ModifierTests
    {
        [TestCase(9)]
        [TestCase(7)]
        [TestCase(1)]
        public void LowValuesReturnNegativeOne(int statValue)
        {
            Assert.That(statValue.GetStatModifier(), Is.EqualTo(-1));
        }

        [TestCase(10)]
        [TestCase(11)]
        public void SixteenAndSeventeenReturnZero(int statValue)
        {
            Assert.That(statValue.GetStatModifier(), Is.EqualTo(0));
        }

        [TestCase(12)]
        [TestCase(13)]
        public void EighteenAndNineteenReturnOne(int statValue)
        {
            Assert.That(statValue.GetStatModifier(), Is.EqualTo(1));
        }

        [TestCase(14)]
        [TestCase(15)]
        public void TwentyAndTwentyOneReturnTwo(int statValue)
        {
            Assert.That(statValue.GetStatModifier(), Is.EqualTo(2));
        }

        [TestCase(16)]
        [TestCase(17)]
        public void TwentyTwoAndTwentyThreeReturnThree(int statValue)
        {
            Assert.That(statValue.GetStatModifier(), Is.EqualTo(3));
        }

        [TestCase(18)]
        [TestCase(19)]
        public void TwentyFourAndTwentyFiveReturnFour(int statValue)
        {
            Assert.That(statValue.GetStatModifier(), Is.EqualTo(4));
        }
    }
}
