using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3 {
    internal class Word {
        public String word { get; }
        
        bool hasPunctuationNext;
        public Word(string word,bool punctuation) {
            this.word = word;
            hasPunctuationNext = punctuation;
        }
    }
}
