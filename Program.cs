using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace cwiczenia2
{
    class Program //program wymaga podania 3 argumentow; nie podanie argumentow zrealizowałem jako podnie argumentow pustych : "" "" ""
                  //przykladowe argumenty:  "C:\Users\Jan\Desktop\csvData.csv" "C:\Users\Jan\Desktop\wynik.xml" xml
    {
        static void Main(string[] args)
        {
            var path = "";
            var errors = new List<String>();//powtórki
            try
            {
                if (args[0].Length > 0)
                {
                    path = args[0];
                }
                else
                {
                    path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\dane.csv";
                }
            }
            catch(IndexOutOfRangeException e1)
            {
                errors.Add("IndexOutOfRangeException - Program wymaga podania 3 argumentow, jeśli nie chcesz ich podawać podaj puste argumenty, np. \"\" ");
                Console.WriteLine(e1.ToString() + "\n\nBłąd zapisano na koniec pliku log.txt na pulpicie.");
            }
    var wynik = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\wynik.xml";
            var errorPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\log.txt";
            Student tmp;
            var hash = new HashSet<Student>(new OwnComparer());
            try
            {
                var lines = File.ReadLines(path);
                foreach (var line in lines)
                {
                    tmp = new Student();
                    tmp.Line = line;
                    tmp.Create();
                    if (!hash.Add(tmp) || !tmp.IsCorrect)
                    {
                        errors.Add(tmp.ToString());
                        if (!tmp.IsCorrect) 
                        {
                            hash.Remove(tmp); //usuwa studentów, który nie mają wszystkich informacji, nie wiedziałem jak to zorobić inaczej, żeby wgl ich nie dodawać więc po prostu usuwam ich na koniec
                        }
                    }
                }
            }
            catch (FileNotFoundException e1)
            {
                errors.Add("FileNotFoundException - Plik dane.svc nie istnieje");
                Console.WriteLine(e1.ToString() + "\n\nBłąd zapisano do log.txt na pulpicie.");
            }
            catch (ArgumentException e1)
            {
                errors.Add("ArgumentException - Program wymaga podania 3 argumentow, jeśli nie chcesz ich podawać podaj puste argumenty, np. \"\" ");
                Console.WriteLine(e1.ToString() + "\n\nBłąd zapisano na koniec pliku log.txt na pulpicie.");
            }
            try
            {
                if (args[2] == "json")
                {
                    Console.WriteLine("Opcja pojawi sie w przyszlosci.");
                }
                else
                {
                    Console.WriteLine("Wybrano format xml.");
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    string today = DateTime.Today.ToShortDateString();

                    ns.Add("createdAt", today);
                    ns.Add("author", "Maciej Trzebiński");
                    FileStream writer = null;
                    if (args[1].Length > 0)
                    {
                        writer = new FileStream(args[1], FileMode.Create);
                    }
                    else
                    {
                        writer = new FileStream(wynik, FileMode.Create);
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(HashSet<Student>),
                                                   new XmlRootAttribute("uczelnia"));
                    serializer.Serialize(writer, hash, ns);
                }
            }catch(IndexOutOfRangeException e1)
            {
                errors.Add("IndexOutOfRangeException - Program wymaga podania 3 argumentow, jeśli nie chcesz ich podawać podaj puste argumenty, np. \"\" ");
                Console.WriteLine(e1.ToString() + "\n\nBłąd zapisano na koniec pliku log.txt na pulpicie.");
            }
            try
            {
                if (File.Exists(errorPath))
                {
                    File.Delete(errorPath);
                }
                if (File.Exists(args[1]))
                {
                    File.Delete(args[1]);
                }
            }catch(IOException e1)
            {
                errors.Add("IOException - Nie można usunąć pliku log.txt lub wynik.xml");
                Console.WriteLine(e1.ToString() + "\n\nBłąd zapisano do log.txt na pulpicie.");
            }
            catch (IndexOutOfRangeException e1)
            {
                errors.Add("IndexOutOfRangeException - Program wymaga podania 3 argumentow, jeśli nie chcesz ich podawać podaj puste argumenty, np. \"\" ");
                Console.WriteLine(e1.ToString() + "\n\nBłąd zapisano na koniec pliku log.txt na pulpicie.");
            }

            File.WriteAllLines(errorPath, errors);

            int count = 0;
            foreach( Student s in hash)
            {
                count++;
            }
            Console.WriteLine("Zapisano " + count + " studentów.");
        }
    }
}
