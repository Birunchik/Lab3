using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab3 {
    internal class Sentence {
        List<Word> Words;
        bool isInterrogativeSentance;
        private Queue<char> punctuation;
        public int lengthSentence { get; private set; }

        public Sentence() {
            lengthSentence = 0;
            punctuation = new Queue<char>();  
            Words = new List<Word>();
        }
        public void printSentance() {
            for(int i = 0; i < Words.Count; i++) {
                Console.Write(Words[i].word);
                char p;
                if(Words[i].hasPunctuationNext && punctuation.TryDequeue(out p)) Console.Write(p);
                Console.Write(" ");
            } 
        }
        public void addWord(Word word) {
            if (word != null) {
                Words.Add(word);
                lengthSentence += word.word.Length;
            }
            else Console.WriteLine("Word can't be null");
        }
        public void addPunctuation(char punctuation) {
            this.punctuation.Enqueue(punctuation);
            lengthSentence++;
            if( punctuation == '?') isInterrogativeSentance = true;
        }
    }
}
