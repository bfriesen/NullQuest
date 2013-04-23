//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NullQuest
//{
//    class Screen
//    {
//        public bool DebugMode { get; set; }
//        public Character character { get; set; }
//        public int ScreenWidth { get; set; }
//        public int ScreenHeight { get; set; }

//        public Dictionary<byte, char> SpecialCharacters { get; set; }

//        public Screen()
//        {
//            ScreenWidth = 99;
//            ScreenHeight = 44;
//            Console.Clear();
//            Console.SetWindowSize(ScreenWidth, ScreenHeight);
//            Console.BufferWidth = ScreenWidth;
//            Console.BufferHeight = ScreenHeight;

//            character = new Character();

//            SetupSpecialCharacters();

//            Refresh();
//        }

//        public bool Next()
//        {
//            return false;
//        }

//        private void Refresh()
//        {
//            Console.SetCursorPosition(42, 0);
//            Console.Write(new string(SpecialCharacters[175], 2));
//            Console.Write(" NULL QUEST ");
//            Console.Write(new string(SpecialCharacters[174], 2));

//            string subText = "The Legend of the Adventure into the Never-Ending Quest";
//            Console.SetCursorPosition((ScreenWidth - subText.Length) / 2, 1);
//            Console.Write(subText);

//            Console.SetCursorPosition(0, 2);
//            Console.Write(new string(SpecialCharacters[196], ScreenWidth));

//            Console.SetCursorPosition(0, 3);
//            Console.Write("HP [{0}]", CreateBar(((double)character.CurrentHitPoints / character.MaxHitPoints), 20, SpecialCharacters[177], ' '));
//            Console.SetCursorPosition(26, 3);
//            Console.Write("{0:000}", character.CurrentHitPoints);
//            Console.SetCursorPosition(29, 3);
//            Console.Write(" / {0:000}", character.MaxHitPoints);
//            //Console.SetCursorPosition(32, 3);

//            Console.SetCursorPosition(0, 43);
//        }

//        private string CreateBar(double percentage, int length, char fill, char empty)
//        {
//            var s = new string(fill, (int) (length*percentage));
//            return s.PadRight(length, empty);
//        }

//        private void SetupSpecialCharacters()
//        {
//            SpecialCharacters = new Dictionary<byte, char>(255);
//            for (byte i = 0; i < 255; i++)
//            {
//                SpecialCharacters.Add(i, GetPage437Character(i));
//            }
//        }

//        private char GetPage437Character(byte c)
//        {
//            return Encoding.GetEncoding(437).GetChars(new byte[] {c})[0];
//        }
//    }
//}
