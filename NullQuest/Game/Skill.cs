//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace NullQuest.Game
//{
//    public class Skill : IEquatable<Skill>
//    {
//        private readonly int energyCost;
//        private readonly Func<GameWorld, string> action;
//        public string Name { get; set; }

//        public Skill(string name, int energyCost, Func<GameWorld, string> action)
//        {
//            Name = name;
//            this.energyCost = energyCost;
//            this.action = action;
//        }

//        public string Perform(GameWorld world)
//        {
//            if (world.Player.CurrentEnergy < energyCost) 
//            {
//                return Strings.Skill_Perform_Not_enough_energy;
//            }

//            world.Player.CurrentEnergy -= energyCost;
//            return action(world);
//        }

//        public bool Equals(Skill other)
//        {
//            return Name.Equals(other.Name);
//        }

//        public override bool Equals(object obj)
//        {
//            return Name.Equals(((Skill) obj).Name);
//        }

//        public override int GetHashCode()
//        {
//            return Name.GetHashCode();
//        }
//    }
//}
