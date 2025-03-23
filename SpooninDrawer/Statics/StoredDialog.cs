using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Statics
{
    static class StoredDialog
    {
        private static char[] lowerAlphabet = new char[26] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static char[] upperAlphabet = new char[26] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static char[] specialSymbols = new char[32] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '_', '-', '=', '+', '{', '[', ']', '}', '\\', '|', ';', ':', '"', '\'', ',', '<', '.', '>', '/', '?','`','~' };
        public static string startingDialog = "Roy::Speaker::Blue Guy::Subtitle:: Hey you! Big guy over there! Can you grab that spoon on the table and put it inside the drawer all the way over there on the other side of the room?";
        public static string blueGuyRoyDialog = "Roy::Speaker::Blue Guy::Subtitle:: Grab the spoon off the table and put it into the drawer.";
        public static string glasses = "Fubuki::Speaker::Glasses Fanatic::Subtitle:: Glasses are really versatile. First, you can have glasses-wearing girls take them off and suddenly become beautiful, " +
            "or have girls wearing glasses flashing those cute grins, or have girls stealing the protagonist's glasses and putting them on like, " +
            "\"Haha, got your glasses!\" That's just way too cute! Also, boys with glasses! I really like when their glasses have that suspicious looking " +
            "gleam, and it's amazing how it can look really cool or just be a joke. I really like how it can fulfill all those abstract needs. Being able " +
            "to switch up the styles and colors of glasses based on your mood is a lot of fun too! It's actually so much fun! You have those half rim " +
            "glasses, or the thick frame glasses, everything! It's like you're enjoying all these kinds of glasses at a buffet. I really want Luna to try " +
            "some on or Marine to try some on to replace her eyepatch. We really need glasses to become a thing in hololive and start selling them for " +
            "HoloComi. Don't. You. Think. We. Really. Need. To. Officially. Give. Everyone. Glasses?";
        public static string bigChungus = "Send Help::Speaker::Why::Subtitle::quantum chicken soup grass big chungus";
        public static bool bigChungusBool = false;
        public static List<string> MinigameStrings = new List<string>{"Put the spoon in the drawer", "No, not like that", "You gotta spin it twistways", "Open the drawer first", "Why",
            "Did you just phase the drawer through your hand", "You're doing it wrong", "What are you doing", "Are you okay?", "This is not good", "Oh boy", "Maybe don't quit your day job",
            "The spoon shines", "A thousand years and this is the best we got", "Rainfrog butts", "Remember that one day, you too will die", "Dang, it got bogos binted", "bruh",
            "Life really is suffering", "This is this and that's that", "The Odyssey had a purpose", "Tell me why", "Needs more jpeg", "Quantum chicken soup grass big chungus", "Spoon",
            "In hell we lament", "I have nothing but my sorrow", "Cope", "Well aren't you a spoony bard", "You certainly know how to draw", "Can I go outside?", "My demise is upon me",
            "Do you remember the moment when you first breathed", "I think I asked for too much", "I'm living in a nightmare", "There's butter on the fridge", "Deez", "You were always the slow one huh",
            "If you quit while you suck, you're going to suck forever", "Blue Guy Roy is at it again!", "Somewhere in the distance, you can hear seagulls", "Now this is the peaceful life",
            "Chat are we cooked", "This is the pickle we're in", "The soda is popping off", "Yo, what's your progress so far?", "It's bright outside", "Jazz hands!", "Oof ouch owie my bones",
            "When will this all be over", "Glasses are really versatile.", "How thrilling", "Gotta keep on keeping on"};
        public static string ArmStuck = "Your arm is in the way!";
        public static string DrawerStuck = "Remove your arm from the drawer first!";
        public static string SpooninDrawer = "The spoon is in the drawer!";
        public static string DrawerTooIn = "You pushed the drawer too far in!";
        public static string DrawerTooOut = "Put the drawer back!";
        public static string DrawerExamine = "This is a drawer.";

        public static string SpooninDrawerDialog = "Bob::Speaker::Spoon Weirdo::Subtitle:: You did it! You put the spoon back in the drawer! Now I can finally stop talking!";
        public static string RollCredits = "Congratulations!" + "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" + "Credits: " + "\n\n\n\n\n\n\n" +
            "Programming: BrightTraverser" + "\n" +
            "Art: BrightTraverser" + "\n" +
            "Music: DOVA-SYNDROME \n Warmth of the Sunset by https://kazinchu.com/" + "\n Moonlit Cafe by Yosuke Matsuura" +
            "Sound Effects: FREESOUND.ORG \n Target by RunnerPack \n beep soft 2 by JonnyRuss01 \n wood step sample 3 by Notarget \n Cabinet_sliding_Drawers_Close_wooden_kitchen_01 by MattRuthSound" + "\n\n" +
            "Third Party: \n\n Made with: Monogame Framework \n Monogame Extended library \n Tiled Map Editor " + "\n\n\n\n\n\n\n" + "Thank you for playing!";
        public static string ControlDisplayText;
        private static int ControlDisplayRound = 0;

        public static string WriteControlDisplayText(List<ActionKey> keyboardControls)
        {
            ControlDisplayText = "";
            foreach (ActionKey key in keyboardControls)
            {
                ControlDisplayText += key.key.ToString() + ": " + key.action.ToString() + "   ";
            }
            return ControlDisplayText;
        }
        public static string WriteControlDisplayText2(List<ActionKey> keyboardControls)
        {
            if (ControlDisplayRound == 0)
            {
                ControlDisplayText = "";
                ControlDisplayRound++;
                ControlDisplayText += keyboardControls.Find(x => x.action == Actions.Confirm).key.ToString() + ": " + "Left Hand Foreward" + "   " +
                    keyboardControls.Find(x => x.action == Actions.Cancel).key.ToString() + ": " + "Left Hand Backward" + "   " +
                    keyboardControls.Find(x => x.action == Actions.OpenMenu).key.ToString() + ": " + "Right Hand Foreward" + "   " +
                    keyboardControls.Find(x => x.action == Actions.V).key.ToString() + ": " + "Left Hand Backward";
            }
            else if (ControlDisplayRound == 1)
            {
                ControlDisplayText = "";
                ControlDisplayRound++;
                ControlDisplayText += keyboardControls.Find(x => x.action == Actions.Confirm).key.ToString() + ": " + "What is going on" + "   " +
                    keyboardControls.Find(x => x.action == Actions.Cancel).key.ToString() + ": " + "That's not working." + "   " +
                    keyboardControls.Find(x => x.action == Actions.OpenMenu).key.ToString() + ": " + "Oh no" + "   " +
                    keyboardControls.Find(x => x.action == Actions.V).key.ToString() + ": " + "Send help";
            }
            else if (ControlDisplayRound == 2)
            {
                ControlDisplayRound++;
            }
            else if (ControlDisplayRound == 3)
            {
                ControlDisplayText = "";
                ControlDisplayRound++;
                ControlDisplayText += keyboardControls.Find(x => x.action == Actions.Confirm).key.ToString() + ": " + "This isn't good" + "   " +
                    keyboardControls.Find(x => x.action == Actions.Cancel).key.ToString() + ": " + "The controls aren't working" + "   " +
                    keyboardControls.Find(x => x.action == Actions.OpenMenu).key.ToString() + ": " + "Uhhh" + "   " +
                    keyboardControls.Find(x => x.action == Actions.V).key.ToString() + ": " + "Send help";
            }
            else if (ControlDisplayRound == 4)
            {
                ControlDisplayRound++;
            }
            else if (ControlDisplayRound == 5)
            {
                ControlDisplayText = "";
                ControlDisplayRound++;
                ControlDisplayText += keyboardControls.Find(x => x.action == Actions.Confirm).key.ToString() + ": " + "Oh no" + "   " +
                    keyboardControls.Find(x => x.action == Actions.Cancel).key.ToString() + ": " + "The controls are randomizing" + "   " +
                    keyboardControls.Find(x => x.action == Actions.OpenMenu).key.ToString() + ": " + "with every press" + "   " +
                    keyboardControls.Find(x => x.action == Actions.V).key.ToString() + ": " + "Send help";
            }
            else if (ControlDisplayRound == 5)
            {
                ControlDisplayRound++;
            }
            else if (ControlDisplayRound == 6)
            {
                ControlDisplayRound++;
            }else if (ControlDisplayRound >= 7)
            {
                if (ControlDisplayRound <= 250)
                    ControlDisplayRound++;
                ControlDisplayText = GenerateRandomString(10 + ControlDisplayRound);
            }


            return ControlDisplayText;
        }
        public static string GenerateRandomString(int length)
        {
            string randomstring = "";
            Random rng = new Random();

            for (int i = 0; i < length; i++)
            {
                int a = rng.Next(0, 3);
                if(a == 0)
                {
                    randomstring += lowerAlphabet[rng.Next(0, lowerAlphabet.Length)];
                }
                else if (a == 1)
                {
                    randomstring += upperAlphabet[rng.Next(0, upperAlphabet.Length)];
                }
                else if (a == 2)
                {
                    randomstring += specialSymbols[rng.Next(0, specialSymbols.Length)];
                }
            }

            return randomstring;
        }

    }
}
