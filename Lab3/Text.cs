using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3 {
    internal class Text {
        List<Sentence> Sentances;
        
        public Text() {
            Sentances = new List<Sentence>();
        }

        public void printText() {
            foreach (Sentence sentence in Sentances) {
                sentence.printSentance();
            }
        }

        public void addSentance(Sentence sentance) {
            if (sentance != null)
                Sentances.Add(sentance);
            else
                Console.WriteLine("Sentance is empty");
        }
        public void printByCountOrder() {

        }
        public void printByLengthOrder() {

        }
        public void findInInInterrogativeSentaceWordsWithLength(int lengthWord) {
          
        }
        public void deleteAllСonsonantWordsWithLenth(int lengthWord) {

        }
        public void replaceWordsInSentanceWithLength(int numSentance, int lengthWord) {

        }
        public void deleteAllStopWords() {

        }
        public void exportXMLdoc() {

        }

    }
}
