using BadSnowstorm;

namespace NullQuest
{
    public class SplashScreen : Screen
    {
        public SplashScreen()
            : base("SplashScreen")
        {
            Content = string.Format(@"{0} _                 _        _       
( (    /||\     /|( \      ( \      
|  \  ( || )   ( || (      | (      
|   \ | || |   | || |      | |      
| (\ \) || |   | || |      | |      
| | \   || |   | || |      | |      
| )  \  || (___) || (____/\| (____/\
|/    )_)(_______)(_______/(_______/

 _______           _______  _______ _________
(  ___  )|\     /|(  ____ \(  ____ \\__   __/
| (   ) || )   ( || (    \/| (    \/   ) (   
| |   | || |   | || (__    | (_____    | |   
| |   | || |   | ||  __)   (_____  )   | |   
| | /\| || |   | || (            ) |   | |   
| (_\ \ || (___) || (____/\/\____) |   | |   
(____\/_)(_______)(_______/\_______)   )_(   {1}




 _____  _               __                                 _ 
/__   \| |__    ___    / /    ___   __ _   ___  _ __    __| |
  / /\/| '_ \  / _ \  / /    / _ \ / _` | / _ \| '_ \  / _` |
 / /   | | | ||  __/ / /___ |  __/| (_| ||  __/| | | || (_| |
 \/    |_| |_| \___| \____/  \___| \__, | \___||_| |_| \__,_|
                                   |___/                     
         __   _    _           
  ___   / _| | |_ | |__    ___ 
 / _ \ | |_  | __|| '_ \  / _ \
| (_) ||  _| | |_ | | | ||  __/
 \___/ |_|    \__||_| |_| \___|

   __  _                              _     ___        _             _ 
  /__\| |_   ___  _ __  _ __    __ _ | |   / _ \ _ __ (_) _ __    __| |
 /_\  | __| / _ \| '__|| '_ \  / _` || |  / /_\/| '__|| || '_ \  / _` |
//__  | |_ |  __/| |   | | | || (_| || | / /_\\ | |   | || | | || (_| |
\__/   \__| \___||_|   |_| |_| \__,_||_| \____/ |_|   |_||_| |_| \__,_|",
                YellowForeground, RedForeground);

            Song = new Song(
                Tempo.FromBpm(150),
                new Note(Tone.C4, Duration.Quarter),
                new Note(Tone.C4, Duration.Eighth),
                new Note(Tone.C4, Duration.Eighth),
                new Note(Tone.G4, Duration.Half),
                new Note(Tone.D4, Duration.Quarter),
                new Note(Tone.D4, Duration.Eighth),
                new Note(Tone.D4, Duration.Eighth),
                new Note(Tone.A4, Duration.Half),
                new Note(Tone.E4, Duration.Quarter),
                new Note(Tone.E4, Duration.Eighth),
                new Note(Tone.E4, Duration.Eighth),
                new Note(Tone.B4, Duration.Quarter),
                new Note(Tone.D5, Duration.Quarter),
                new Note(Tone.E5, Duration.Half));
        }
    }
}