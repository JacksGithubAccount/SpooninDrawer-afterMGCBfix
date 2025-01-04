using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer.Statics
{
    static class WordWrapper
    {
        public static List<string> WordWrap(string text, int lengthToWrapAround)
        {
            int indexof = 0;
            int indexOfHolder = 0;
            string holdertext;            
            int nextLengthToWrapAround = 0;
            List<string> WrappedText = new List<string>();
            int indexfinder = 0;

            for (int i = 1; nextLengthToWrapAround < text.Length; i++)
            {
                indexfinder = lengthToWrapAround * i;
                if (indexfinder > text.Length)
                {
                    indexfinder = text.Length;
                    holdertext = text.Substring(indexOfHolder, indexfinder - indexOfHolder);
                }
                else
                {
                    indexof = text.LastIndexOf(" ", indexfinder);
                    holdertext = text.Substring(indexOfHolder, indexof - indexOfHolder);
                }
                WrappedText.Add(holdertext);

                indexOfHolder = indexof;
                nextLengthToWrapAround += lengthToWrapAround;
            }
            return WrappedText; 
        
        }
        public static List<string> WordWrapByPunctuation(string text, char punctuation) 
        {
            int indexof = 0;
            int indexOfHolder = 0;
            string holdertext;
            int nextLengthToWrapAround = 0;
            List<string> WrappedText = new List<string>();
            int indexfinder = 0;

            for (int i = 1; nextLengthToWrapAround < text.Length; i++)
            {
                indexof = text.LastIndexOf(punctuation, indexfinder);
                holdertext = text.Substring(indexOfHolder, indexof - indexOfHolder);

                indexOfHolder = indexof;
                nextLengthToWrapAround += indexof;
            }

                return WrappedText;
        }

    }
}
