using System.IO;

namespace TimeAndSong
{
    [Flags]
    public enum SongGenre
    {
        Unclassified = 0,
        Pop = 0b1,
        Rock = 0b10,
        Blues = 0b100,
        Country = 0b1_000,
        Metal = 0b10_000,
        Soul = 0b100_000
    }
    public class Song
    {
        public string Artist { get; }
        public string Title { get; }
        public double Length { get; }
        public SongGenre Genre { get; }

        public Song(string title, string artist, double length, SongGenre genre)
        {
            Title = title;
            Artist = artist;
            Length = length;
            Genre = genre;
        }

        public override string ToString()
        {
            return $"{Title} by {Artist} ({Genre}) {Length}mins";
        }
    }

    public static class Library
    {
        private static List<Song> songs = new List<Song>();
        public static void DisplaySongs()
        {
            foreach (Song s in songs)
            {
                Console.WriteLine(s);
            }
        }

        public static void DisplaySongs(double longerThan)
        {
            foreach (Song s in songs)
            {
                if (s.Length > longerThan)
                {
                    Console.WriteLine(s);
                }
                
            }
        }

        public static void DisplaySongs(SongGenre genre)
        {
            foreach(Song s in songs)
            {
                if (s.Genre == genre)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public static void DisplaySongs(string artist)
        {
            foreach (Song s in songs)
            {
                if (s.Artist == artist)
                {
                    Console.WriteLine(s);
                }
            }
        }

        public static void LoadSongs(string fileName)
        {
            TextReader reader = new StreamReader(fileName);
            string artist, length, genre;
            string title = reader.ReadLine();


            while (title != null)
            {
                artist = reader.ReadLine();
                length = reader.ReadLine();
                genre = reader.ReadLine();

                songs.Add(new Song(title, artist, Convert.ToDouble(length), (SongGenre)Enum.Parse(typeof(SongGenre), genre)));

                title = reader.ReadLine();
            }

            reader.Close();
        }
    }

    public enum TimeFormat
    {
        Mil,
        Hour12,
        Hour24
    }

    public class Time
    {
        private static TimeFormat TIME_FORMAT = TimeFormat.Hour12;
        public int Hour { get; }
        public int Minute { get; }
        public Time(int hour = 0, int minute = 0)
        {
            Hour = hour;
            Minute = minute;
        }
        public override string ToString()
        {
            if (TIME_FORMAT == TimeFormat.Mil)
            {
                return $"{Hour.ToString("D2")}{Minute.ToString("D2")}";
            }
            else if (TIME_FORMAT == TimeFormat.Hour12)
            {
                return $"{(Hour%12).ToString("D2")}:{Minute.ToString("D2")} {(Hour / 12 > 0 ? "PM":"AM")}";
            }
            else
            {
                return $"{Hour.ToString("D2")}:{Minute.ToString("D2")}";
            }
            
        }
        public static void SetFormat(TimeFormat timeFormat)
        {
            TIME_FORMAT = timeFormat;
        }
    }

    public class Program
    {
        public static void Main ()
        {
            //CODE FOR SONG AND LIBRARY CLASS
            //To test the constructor and the ToString method
            Console.WriteLine(new Song("Baby", "Justin Bebier", 3.35, SongGenre.Pop));
            //This is first time that you are using the bitwise or. It is used to specify a combination of genres
            Console.WriteLine(new Song("The Promise", "Chris Cornell", 4.26, SongGenre.Country | SongGenre.Rock));

            Library.LoadSongs("Week_03_lab_09_songs4.txt");     //Class methods are invoke with the class name
            Console.WriteLine("\n\nAll songs");
            Library.DisplaySongs();
            SongGenre genre = SongGenre.Rock;
            Console.WriteLine($"\n\n{genre} songs");
            Library.DisplaySongs(genre);
            string artist = "Bob Dylan";
            Console.WriteLine($"\n\nSongs by {artist}");
            Library.DisplaySongs(artist);
            double length = 5.0;
            Console.WriteLine($"\n\nSongs more than {length}mins");
            Library.DisplaySongs(length);


            //CODE FOR TIME CLASS
            //create a list to store the objects
            List<Time> times = new List<Time>()
            {
                new Time(9, 35),
                new Time(18, 5),
                new Time(20, 00),
                new Time(10),
                new Time()
            };

            //display all the objects
            TimeFormat format = TimeFormat.Hour12;
            Console.WriteLine($"\n\nTime format is {format}");
            foreach (Time t in times)
            {
                Console.WriteLine(t);
            }

            //change the format of the output
            format = TimeFormat.Mil;
            Console.WriteLine($"\n\nSetting time format to {format}");
            //SetFormat(TimeFormat) is a class member, so you need the type to access it
            Time.SetFormat(format);
            //again display all the objects
            foreach (Time t in times)
            {
                Console.WriteLine(t);
            }
            //change the format of the output
            format = TimeFormat.Hour24;
            Console.WriteLine($"\n\nSetting time format to {format}");
            //SetFormat(TimeFormat) is a class member, so you need the type to access it
            Time.SetFormat(format);
            foreach (Time t in times)
            {
                Console.WriteLine(t);
            }

        }
    }
}