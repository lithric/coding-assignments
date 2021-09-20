using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllCodingProjectsFromNowUntilTheEnd
{
    class TextAdventure
    {
        public static void Start()
        {
            string playerName = CalcIO.Prompt("what is your name?"); // user inputs their name
            int firewood = CalcIO.Prompt( // prompt for the start of their journey
@"you travel down a dirt road at night.
It's cold and dark.
You see cottages with lit homes and warm fires. 
You wish you were in one of them.
You see some sticks near one of the cottages.
Maybe you can grab some firewood?
How many do you grab?");
            string nextTime = firewood == 0 ? CalcIO.Prompt( // prompt for when you grab no firewood
                $@"you don't feel the need to steal firewood from other people's homes. 
So you keep going, 
feeling the cold biting deeper as you weigh on the standings of your morals.


Press Enter to see how you did..."): firewood < 12 ? CalcIO.Prompt( // prompt for when you grab some firewood
                $@"You don't grab much, {firewood} firewood, just enough to support you on your journey.
Do you know where you're going? 
No. 
But you know that you're cold.


Press Enter to see how you did...") 
                : firewood < 30 ? CalcIO.Prompt( // prompt for when you grab a generous amount of firewood
                    $@"You grab a generous amount of firewood.
About {firewood} firewood.
This will definitely help you on your journey. 
Don't feel like dying to the cold any time soon and you're not going to.
Time to set up camp.


Press Enter to see how you did...") : firewood >= 30 ? CalcIO.Prompt( // prompt for when you grab a lot of firewood
                        $@"Wow, that's alot of firewood. 
{firewood} firewood!?
Who even has that much!? 
anyway, you grab the wood and hope that they don't notice all of their firewood is gone.
Time to set up camp.


Press Enter to see how you did...") : null;
            if ((Literal)firewood)
            {
                Console.WriteLine($"{playerName} survived the night! (scum ending 1/3)"); // user survived through stolen firewood
            } else
            {
                while(nextTime != "give up" && nextTime != "alive") // user is given the choice to keep living or die trying forever
                {
                    switch(nextTime)
                    {
                        case "live":
                            nextTime = CalcIO.Prompt(@"you're holding on for dear life.
Freezing and dying in the cold, you can choose to give up or live.
enter 'give up' or 'live' "); // prompt for the user to choose to live or die
                            break;
                        case "help me": // secret ending
                            nextTime = CalcIO.Prompt(@"calling out for help, 
the house you looked at for firewood is now inviting you in. Go in? (yes or no)"); // user given the choice to go into house
                            if (nextTime == "yes")
                            {
                                nextTime = "alive"; // user accepts help. This wins the game through secret ending
                            }
                            else
                            {
                                CalcIO.Prompt("you decided not to go in. You need to survive another way. (enter to continue)"); // user refruses help
                            }
                            break;
                        default:
                            nextTime = "live"; // keeps user alive
                            break;
                    }
                }
                if (nextTime == "alive")
                {
                    Console.WriteLine($"{playerName} survived the night! (good ending 3/3)"); // secret ending prompt
                    return;
                }
                Console.WriteLine($"{playerName} you did not survive. (bad ending 2/3)"); // bad ending prompt
            }
        }
    }
}
