﻿using System;
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
            int indexof;
            int indexOfHolder = 0;
            string holdertext;            
            int nextLengthToWrapAround = 0;
            List<string> WrappedText = new List<string>();
            int indexfinder = 0;

            for (int i = 1; nextLengthToWrapAround < text.Length; i++)
            {               
                indexfinder = lengthToWrapAround * i;
                if (indexfinder > text.Length)
                    indexfinder = text.Length;
                indexof = text.LastIndexOf(" ", indexfinder);
                holdertext = text.Substring(indexOfHolder, indexof - indexOfHolder);
                WrappedText.Add(holdertext);

                indexOfHolder = indexof;
                nextLengthToWrapAround += lengthToWrapAround;
            }
            return WrappedText; 
        
        }

    }
}