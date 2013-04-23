using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullQuest.Data
{
    public class DeterministicDungeonNameGenerator : IDungeonNameGenerator
    {
        private readonly Dictionary<int, string> prefixes = new Dictionary<int, string>();
        private readonly Dictionary<int, string> main = new Dictionary<int, string>();
        private readonly Dictionary<int, string> suffixes = new Dictionary<int, string>();

        public DeterministicDungeonNameGenerator()
        {
            SetupWordLists();
        }

        public string GenerateName(int dungeonLevel)
        {
            var rng = new Random(dungeonLevel);

            var format = rng.Next(1, 21);
            string word = "";
            switch (format)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    word = prefixes[rng.Next(1, 101)] + " " + main[rng.Next(1, 101)];
                    break;
                case 6:
                case 7:
                case 8:
                    word = prefixes[rng.Next(1, 101)] + " " + prefixes[rng.Next(1, 101)] + " " + main[rng.Next(1, 101)];
                    break;
                case 9:
                    word = main[rng.Next(1, 101)] + " " + suffixes[rng.Next(1, 101)];
                    break;
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    word = prefixes[rng.Next(1, 101)] + " " + main[rng.Next(1, 101)] + " " + suffixes[rng.Next(1, 101)];
                    break;
                case 17:
                case 18:
                case 19:
                case 20:
                default:
                    word = prefixes[rng.Next(1, 101)] + " " + prefixes[rng.Next(1, 101)] + " " + main[rng.Next(1, 101)] + " " + suffixes[rng.Next(1, 101)];
                    break;
            }

            return word.Replace("- ", "-");
        }

        private void SetupWordLists()
        {
            prefixes.Add(1, "Amazing");
            prefixes.Add(2, "Amazing");
            prefixes.Add(3, "Amethyst");
            prefixes.Add(4, "Black");
            prefixes.Add(5, "Black");
            prefixes.Add(6, "Black");
            prefixes.Add(7, "Boiling");
            prefixes.Add(8, "Brilliant");
            prefixes.Add(9, "Broad");
            prefixes.Add(10, "Brutal");
            prefixes.Add(11, "Brutal");
            prefixes.Add(12, "Chaotic");
            prefixes.Add(13, "Chaotic");
            prefixes.Add(14, "Crazy");
            prefixes.Add(15, "Cursed");
            prefixes.Add(16, "Cursed");
            prefixes.Add(17, "Cursed");
            prefixes.Add(18, "Dagger");
            prefixes.Add(19, "Dangerous");
            prefixes.Add(20, "Dangerous");
            prefixes.Add(21, "Deadly");
            prefixes.Add(22, "Deadly");
            prefixes.Add(23, "Decayed");
            prefixes.Add(24, "Decayed");
            prefixes.Add(25, "Deep");
            prefixes.Add(26, "Deep");
            prefixes.Add(27, "Dire");
            prefixes.Add(28, "Dire");
            prefixes.Add(29, "Dire");
            prefixes.Add(30, "Dire");
            prefixes.Add(31, "Dying");
            prefixes.Add(32, "Emerald");
            prefixes.Add(33, "Eternal");
            prefixes.Add(34, "Eternal");
            prefixes.Add(35, "Evil");
            prefixes.Add(36, "Evil");
            prefixes.Add(37, "False");
            prefixes.Add(38, "Forgotten");
            prefixes.Add(39, "Forgotten");
            prefixes.Add(40, "Forgotten");
            prefixes.Add(41, "Ghostly");
            prefixes.Add(42, "Giant");
            prefixes.Add(43, "Giant");
            prefixes.Add(44, "Great");
            prefixes.Add(45, "Great");
            prefixes.Add(46, "Green");
            prefixes.Add(47, "Grizzly");
            prefixes.Add(48, "Haunted");
            prefixes.Add(49, "Haunted");
            prefixes.Add(50, "Haunted");
            prefixes.Add(51, "Hellish");
            prefixes.Add(52, "Hellish");
            prefixes.Add(53, "Hidden");
            prefixes.Add(54, "Hidden");
            prefixes.Add(55, "Hidden");
            prefixes.Add(56, "Icy");
            prefixes.Add(57, "Infernal");
            prefixes.Add(58, "Infernal");
            prefixes.Add(59, "Jade");
            prefixes.Add(60, "Lawful");
            prefixes.Add(61, "Lawful");
            prefixes.Add(62, "Mighty");
            prefixes.Add(63, "Mighty");
            prefixes.Add(64, "Misty");
            prefixes.Add(65, "Mysterious");
            prefixes.Add(66, "Mysterious");
            prefixes.Add(67, "Rancid");
            prefixes.Add(68, "Red");
            prefixes.Add(69, "Red");
            prefixes.Add(70, "Rough");
            prefixes.Add(71, "Royal");
            prefixes.Add(72, "Royal");
            prefixes.Add(73, "Scary");
            prefixes.Add(74, "Scary");
            prefixes.Add(75, "Secluded");
            prefixes.Add(76, "Secluded");
            prefixes.Add(77, "Sword");
            prefixes.Add(78, "Tiny");
            prefixes.Add(79, "True");
            prefixes.Add(80, "True");
            prefixes.Add(81, "Twisted");
            prefixes.Add(82, "Twisted");
            prefixes.Add(83, "Twisted");
            prefixes.Add(84, "Uncanny");
            prefixes.Add(85, "Uncanny");
            prefixes.Add(86, "Under-");
            prefixes.Add(87, "Under-");
            prefixes.Add(88, "Under-");
            prefixes.Add(89, "Unholy");
            prefixes.Add(90, "Unholy");
            prefixes.Add(91, "Unholy");
            prefixes.Add(92, "Unnamed");
            prefixes.Add(93, "Valiant");
            prefixes.Add(94, "Vicious");
            prefixes.Add(95, "Vicious");
            prefixes.Add(96, "Vicious");
            prefixes.Add(97, "Wailing");
            prefixes.Add(98, "Wailing");
            prefixes.Add(99, "Whispering");
            prefixes.Add(100, "Whispering");

            main.Add(1, "Catacomb");
            main.Add(2, "Catacomb");
            main.Add(3, "Catacomb");
            main.Add(4, "Catacomb");
            main.Add(5, "Catacomb");
            main.Add(6, "Catacomb");
            main.Add(7, "Cave");
            main.Add(8, "Cave");
            main.Add(9, "Cave");
            main.Add(10, "Cave");
            main.Add(11, "Cave");
            main.Add(12, "Cave");
            main.Add(13, "Caverns");
            main.Add(14, "Caverns");
            main.Add(15, "Caverns");
            main.Add(16, "Caverns");
            main.Add(17, "Caverns");
            main.Add(18, "Chamber");
            main.Add(19, "Chamber");
            main.Add(20, "Chambers");
            main.Add(21, "Chambers");
            main.Add(22, "Crypt");
            main.Add(23, "Crypt");
            main.Add(24, "Crypt");
            main.Add(25, "Crypt");
            main.Add(26, "Den");
            main.Add(27, "Den");
            main.Add(28, "Den");
            main.Add(29, "Dungeon");
            main.Add(30, "Dungeon");
            main.Add(31, "Dungeon");
            main.Add(32, "Dungeon");
            main.Add(33, "Dungeon");
            main.Add(34, "Dungeon");
            main.Add(35, "Dungeon");
            main.Add(36, "Dungeon");
            main.Add(37, "Grotto");
            main.Add(38, "Grotto");
            main.Add(39, "Hill");
            main.Add(40, "Hills");
            main.Add(41, "Hole");
            main.Add(42, "Hole");
            main.Add(43, "Hole");
            main.Add(44, "Labyrinth");
            main.Add(45, "Labyrinth");
            main.Add(46, "Labyrinth");
            main.Add(47, "Labyrinth");
            main.Add(48, "Labyrinth");
            main.Add(49, "Labyrinth");
            main.Add(50, "Labyrinth");
            main.Add(51, "Lair");
            main.Add(52, "Lair");
            main.Add(53, "Lair");
            main.Add(54, "Lair");
            main.Add(55, "Lair");
            main.Add(56, "Lair");
            main.Add(57, "Lair");
            main.Add(58, "Lair");
            main.Add(59, "Mausoleum");
            main.Add(60, "Mausoleum");
            main.Add(61, "Mausoleum");
            main.Add(62, "Maze");
            main.Add(63, "Maze");
            main.Add(64, "Maze");
            main.Add(65, "Maze");
            main.Add(66, "Mortuary");
            main.Add(67, "Mortuary");
            main.Add(68, "Mount");
            main.Add(69, "Mountain");
            main.Add(70, "Mountain");
            main.Add(71, "Mountains");
            main.Add(72, "Pit");
            main.Add(73, "Pit");
            main.Add(74, "Pit");
            main.Add(75, "Pit");
            main.Add(76, "Quarter");
            main.Add(77, "Quarter");
            main.Add(78, "Realm");
            main.Add(79, "Realm");
            main.Add(80, "Realm");
            main.Add(81, "Realm");
            main.Add(82, "Tomb");
            main.Add(83, "Tomb");
            main.Add(84, "Tomb");
            main.Add(85, "Tomb");
            main.Add(86, "Tomb");
            main.Add(87, "Tomb");
            main.Add(88, "Tomb");
            main.Add(89, "Tunnel");
            main.Add(90, "Tunnel");
            main.Add(91, "Tunnel");
            main.Add(92, "Tunnel");
            main.Add(93, "Tunnels");
            main.Add(94, "Tunnels");
            main.Add(95, "Tunnels");
            main.Add(96, "Vault");
            main.Add(97, "Vault");
            main.Add(98, "Vaults");
            main.Add(99, "Vaults");
            main.Add(100, "Ways");

            suffixes.Add(1, "Of Ages");
            suffixes.Add(2, "Of Ages");
            suffixes.Add(3, "Of Ages");
            suffixes.Add(4, "Of Bane");
            suffixes.Add(5, "Of Bane");
            suffixes.Add(6, "Of Catastrophe");
            suffixes.Add(7, "Of Chaos");
            suffixes.Add(8, "Of Chaos");
            suffixes.Add(9, "Of Darkness");
            suffixes.Add(10, "Of Darkness");
            suffixes.Add(11, "Of Darkness");
            suffixes.Add(12, "Of Death");
            suffixes.Add(13, "Of Death");
            suffixes.Add(14, "Of Death");
            suffixes.Add(15, "Of Demons");
            suffixes.Add(16, "Of Demons");
            suffixes.Add(17, "Of Despair");
            suffixes.Add(18, "Of Despair");
            suffixes.Add(19, "Of Despair");
            suffixes.Add(20, "Of Destruction");
            suffixes.Add(21, "Of Destruction");
            suffixes.Add(22, "Of Devils");
            suffixes.Add(23, "Of Devils");
            suffixes.Add(24, "Of Disease");
            suffixes.Add(25, "Of Disease");
            suffixes.Add(26, "Of Doom");
            suffixes.Add(27, "Of Doom");
            suffixes.Add(28, "Of Doom");
            suffixes.Add(29, "Of Eternal Night");
            suffixes.Add(30, "Of Eternal Night");
            suffixes.Add(31, "Of Eternity");
            suffixes.Add(32, "Of Eternity");
            suffixes.Add(33, "Of Hatred");
            suffixes.Add(34, "Of Hatred");
            suffixes.Add(35, "Of Horror");
            suffixes.Add(36, "Of Horror");
            suffixes.Add(37, "Of Horror");
            suffixes.Add(38, "Of Illusion");
            suffixes.Add(39, "Of Insanity");
            suffixes.Add(40, "Of Insanity");
            suffixes.Add(41, "Of Lost Souls");
            suffixes.Add(42, "Of Lost Souls");
            suffixes.Add(43, "Of Madness");
            suffixes.Add(44, "Of Madness");
            suffixes.Add(45, "Of Magic");
            suffixes.Add(46, "Of Magic");
            suffixes.Add(47, "Of Magic");
            suffixes.Add(48, "Of Might");
            suffixes.Add(49, "Of Might");
            suffixes.Add(50, "Of Misery");
            suffixes.Add(51, "Of Misery");
            suffixes.Add(52, "Of Mortality");
            suffixes.Add(53, "Of Nightmares");
            suffixes.Add(54, "Of Nightmares");
            suffixes.Add(55, "Of Nightmares");
            suffixes.Add(56, "Of No Escape");
            suffixes.Add(57, "Of No Escape");
            suffixes.Add(58, "Of Power");
            suffixes.Add(59, "Of Power");
            suffixes.Add(60, "Of Ruin");
            suffixes.Add(61, "Of Ruin");
            suffixes.Add(62, "Of Screams");
            suffixes.Add(63, "Of Screams");
            suffixes.Add(64, "Of Secrets");
            suffixes.Add(65, "Of Secrets");
            suffixes.Add(66, "Of Secrets");
            suffixes.Add(67, "Of Shadows");
            suffixes.Add(68, "Of Shadows");
            suffixes.Add(69, "Of Sorrow");
            suffixes.Add(70, "Of Sorrow");
            suffixes.Add(71, "Of Spite");
            suffixes.Add(72, "Of Spite");
            suffixes.Add(73, "Of Suffering");
            suffixes.Add(74, "Of Suffering");
            suffixes.Add(75, "Of Suffering");
            suffixes.Add(76, "Of the Fathers");
            suffixes.Add(77, "Of the Fathers");
            suffixes.Add(78, "Of the Wyrm");
            suffixes.Add(79, "Of the Wyrm");
            suffixes.Add(80, "Of Undeath");
            suffixes.Add(81, "Of Undeath");
            suffixes.Add(82, "Of Undeath");
            suffixes.Add(83, "Of Undeath");
            suffixes.Add(84, "Of Valor");
            suffixes.Add(85, "Of Valor");
            suffixes.Add(86, "Of War");
            suffixes.Add(87, "Of War");
            suffixes.Add(88, "Of War");
            suffixes.Add(89, "Of War");
            suffixes.Add(90, "Of Winter");
            suffixes.Add(91, "Of Winter");
            suffixes.Add(92, "Of Woe");
            suffixes.Add(93, "Of Woe");
            suffixes.Add(94, "Of Woe");
            suffixes.Add(95, "Of Worms");
            suffixes.Add(96, "Of Worms");
            suffixes.Add(97, "Of Worry");
            suffixes.Add(98, "Of Worry");
            suffixes.Add(99, "Of Worry");
            suffixes.Add(100, "Of Worry");
        }
    }
}
