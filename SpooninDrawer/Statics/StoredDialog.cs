using SpooninDrawer.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Statics
{
    static class StoredDialog
    {
        public static string startingDialog = "Bob::Speaker::Spoon Weirdo::Subtitle:: Hey you! Big guy over there! Can you grab that spoon on the table and put it inside the drawer all the way over there on the other side of the room?";
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
            "Life really is suffering", "This is this and that's that", "The Odyssey had a purpose", "Tell me why", "Needs more jpeg", "Quantum chicken soup grass big chungus"};
        public static string ArmStuck = "Your arm is in the way!";
        public static string DrawerStuck = "Remove your arm from the drawer first!";
        public static string SpooninDrawer = "The spoon is in the drawer!";
        public static string DrawerTooIn = "You pushed the drawer too far in!";
        public static string DrawerTooOut = "Put the drawer back!";

        public static string SpooninDrawerDialog = "Bob::Speaker::Spoon Weirdo::Subtitle:: You did it! You put the spoon back in the drawer! Now I can finally stop talking!";
        public static string RollCredits = "Thank you for playing! \n";
        public static string ControlDisplayText;

        public static string WriteControlDisplayText(List<ActionKey> keyboardControls)
        {
            //ActionKey holder = 
            foreach (ActionKey key in keyboardControls)
            {
                ControlDisplayText += key.key.ToString() + ": " + key.action.ToString() + "   ";
            }
            //ControlDisplayText = keyboardControls.Find(x => x.action == Actions.MoveUp).key.ToString();
            return ControlDisplayText;
        }


    }
}
