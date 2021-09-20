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
    }
}
