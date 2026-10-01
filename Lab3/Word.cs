using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3 {
    internal class Word {
        public string word { get; set; }
        public bool hasPunctuationNext { get; }
        public Word(string word,bool punctuation) {
            this.word = word;
            hasPunctuationNext = punctuation;
        }
    }
}
