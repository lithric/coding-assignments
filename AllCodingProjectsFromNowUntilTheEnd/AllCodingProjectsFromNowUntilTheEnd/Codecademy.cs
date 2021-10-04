using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllCodingProjectsFromNowUntilTheEnd
{
    class Codecademy
    {
        public class lesson1P6
        {
            public static void Start()
            {
                NamePets("Laika", "Albert");
                VisitPlanets(numberOfPlanets: 8);

            }

            public static void NamePets()
            {
                Console.WriteLine("Aw, you have no spacefaring pets :(");
            }

             public static void NamePets(string pet1, string pet2)
            {
                Console.WriteLine($"Your pets {pet1} and {pet2} will be joining your voyage across space!");
            }

            public static void NamePets(string pet1, string pet2, string pet3)
            {
                Console.WriteLine($"Your pets {pet1}, {pet2}, and {pet3} will be joining your voyage across space!");
            }

             public static void VisitPlanets(
              string adjective = "brave",
              string name = "Cosmonaut",
              int numberOfPlanets = 0,
              double gForce = 4.2)
            {
                Console.WriteLine($"Welcome back, {adjective} {name}.");
                Console.WriteLine($"You visited {numberOfPlanets} new planets...");
                Console.WriteLine($"...while experiencing a g-force of {gForce} g!");
            }
        }
        public class lesson2P6
        {
            public static void Start()
            {
                // Define variables
                string destination = "Neptune";
                string galaxyString = "8";
                int galaxyInt;
                string welcomeMessage;
                bool outcome;

                // Call DecoratePlanet() and TryParse() here
                welcomeMessage = DecoratePlanet(destination);
                outcome = Int32.TryParse(galaxyString, out galaxyInt);

                // Print results
                Console.WriteLine(welcomeMessage);
                Console.WriteLine($"Parsed to int? {outcome}: {galaxyInt}");

            }

            // Define a method that returns a string
            public static string DecoratePlanet(string planet)
            {
                return $"*..*..* Welcome to {planet} *..*..*";
            }

            // Define a method with out
            public static string Whisper(string phrase, out bool wasWhisperCalled)
            {
                wasWhisperCalled = true;
                return phrase.ToLower();
            }
        }
        public class ArraysAndLoops
        {
            public class lesson1P7
            {
                public static void Start() {
                    string[] summerStrut;

                    summerStrut = new string[] { "Juice", "Missing U", "Raspberry Beret", "New York Groove", "Make Me Feel", "Rebel Rebel", "Despacito", "Los Angeles" };

                    int[] ratings = { 5, 4, 4, 3, 3, 5, 5, 4 };

                    string[] summerStrutCopy = new string[8];

                    Array.Copy(summerStrut, summerStrutCopy, 8);
                    Console.WriteLine(summerStrutCopy[0]);
      
                    Array.Reverse(summerStrut);
                    Console.WriteLine(summerStrut[0]);
                    Console.WriteLine(summerStrut[7]);
      
                    Array.Clear(ratings, 0, ratings.Length);
                    Console.WriteLine(ratings[0]);
                }
                public class lesson2P6
                {
                    public static void Start()
                    {
                        string[] websites = { "twitter", "facebook", "gmail" };

                        foreach (string website in websites)
                        {
                            Console.WriteLine(website);
                        }
                    }
                }
            }
        }
        public class ClassesAndObjects
        {
            public class lesson1P12
            {
                class Forest
                {
                    public int age;
                    private string biome;

                    public Forest(string name, string biome)
                    {
                        this.Name = name;
                        this.Biome = biome;
                        Age = 0;
                    }

                    public Forest(string name) : this(name, "Unknown")
                    { }

                    public string Name
                    { get; set; }

                    public int Trees
                    { get; set; }

                    public string Biome
                    {
                        get { return biome; }
                        set
                        {
                            if (value == "Tropical" ||
                                value == "Temperate" ||
                                value == "Boreal")
                            {
                                biome = value;
                            }
                            else
                            {
                                biome = "Unknown";
                            }
                        }
                    }

                    public int Age
                    {
                        get { return age; }
                        private set { age = value; }
                    }

                    public int Grow()
                    {
                        Trees += 30;
                        Age += 1;
                        return Trees;
                    }

                    public int Burn()
                    {
                        Trees -= 20;
                        Age += 1;
                        return Trees;
                    }

                }
                public static void Start()
                {
                    Forest a = new Forest("Amazon");
                    Console.WriteLine(a.Trees);
                    a.Grow();
                    Console.WriteLine(a.Trees);
                }
            }
            public class lesson2P8
            {
                class Forest
                {
                    // FIELDS

                    public int age;
                    private string biome;
                    private static int forestsCreated;
                    private static string treeFacts;

                    // CONSTRUCTORS

                    public Forest(string name, string biome)
                    {
                        this.Name = name;
                        this.Biome = biome;
                        Age = 0;
                        ForestsCreated++;
                    }

                    public Forest(string name) : this(name, "Unknown")
                    { }

                    static Forest()
                    {
                        treeFacts = "Forests provide a diversity of ecosystem services including:\r\n  aiding in regulating climate.\r\n  purifying water.\r\n  mitigating natural hazards such as floods.\n";
                        ForestsCreated = 0;

                    }

                    // PROPERTIES

                    public string Name
                    { get; set; }

                    public int Trees
                    { get; set; }

                    public string Biome
                    {
                        get { return biome; }
                        set
                        {
                            if (value == "Tropical" ||
                                value == "Temperate" ||
                                value == "Boreal")
                            {
                                biome = value;
                            }
                            else
                            {
                                biome = "Unknown";
                            }
                        }
                    }

                    public int Age
                    {
                        get { return age; }
                        private set { age = value; }
                    }

                    public static int ForestsCreated
                    {
                        get { return forestsCreated; }
                        private set { forestsCreated = value; }
                    }

                    public static string TreeFacts
                    {
                        get { return treeFacts; }
                    }

                    // METHODS

                    public int Grow()
                    {
                        Trees += 30;
                        Age += 1;
                        return Trees;
                    }

                    public int Burn()
                    {
                        Trees -= 20;
                        Age += 1;
                        return Trees;
                    }

                    public static void PrintTreeFacts()
                    {
                        Console.WriteLine(TreeFacts);
                    }

                }
                public static void Start()
                {
                    Console.WriteLine(Forest.ForestsCreated);
                    Forest f = new Forest("Congo", "Tropical");
                    Forest r = new Forest("Rendlesham");
                    Console.WriteLine(Forest.ForestsCreated);
                }
            }
        }
    }
}
