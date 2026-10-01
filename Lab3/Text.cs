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
            List<Sentence> copy = Sentances.ToList();
            splitSortbyCount(copy, 0, Sentances.Count - 1);
            foreach (Sentence sentence in copy) {
                sentence.printSentance();
                Console.Write($"({sentence.Words.Count})");
                Console.WriteLine();
            }
        }
        private void splitSortbyCount(List<Sentence> array, int low, int height) {
            if (low < height) {
                int val = array[height].Words.Count;
                ;
                int i = low - 1;
                for (int j = low; j < height; j++) {
                    if (array[j].Words.Count < val) {
                        i++;
                        if (i != j) {
                            Sentence temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }

                int valIdx = i + 1;
                if (valIdx != height) {
                    Sentence temp = array[valIdx];
                    array[valIdx] = array[height];
                    array[height] = temp;
                }

                splitSortbyCount(array, low, valIdx - 1);
                splitSortbyCount(array, valIdx + 1, height);
            }
        }

        public void printByLengthOrder() {
            List<Sentence> copy = Sentances.ToList();
            splitSortbyLength(copy, 0, Sentances.Count - 1);
            foreach (Sentence sentence in copy) {
                sentence.printSentance();
                Console.Write($"({sentence.lengthSentence})");
                Console.WriteLine();
            }
        }
        private void splitSortbyLength(List<Sentence> array, int low, int height) {
            if (low < height) {
                int val = array[height].lengthSentence;
                int i = low - 1;
                for (int j = low; j < height; j++) {
                    if (array[j].lengthSentence < val) {
                        i++;
                        if (i != j) {
                            Sentence temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                    }
                }

                int valIdx = i + 1;
                if (valIdx != height) {
                    Sentence temp = array[valIdx];
                    array[valIdx] = array[height];
                    array[height] = temp;
                }

                splitSortbyLength(array, low, valIdx - 1);
                splitSortbyLength(array, valIdx + 1, height);
            }
        }
        public void findInInInterrogativeSentaceWordsWithLength(int lengthWord) {
            HashSet<string> uniqueWords = new HashSet<string>();
          foreach(Sentence sentence in Sentances) {
                if (sentence.isInterrogativeSentance) {
                    foreach(Word word in sentence.Words) {
                        if(!uniqueWords.Contains(word.word) && word.word.Length == lengthWord) {
                            uniqueWords.Add(word.word);
                            Console.WriteLine(word.word);
                        }
                    }
                }
            }
        }
        public Text deleteAllСonsonantWordsWithLenth(int lengthWord) {
            Text textWithoutConsonantWords = new Text();
            Sentence newSentence = new Sentence(); 
            HashSet<char> consonantHashSet = new HashSet<char>();
            initConsonantHashSet(consonantHashSet);
            foreach (Sentence sentence in Sentances) {
                foreach (Word word in sentence.Words) {
                    string wordString = word.word.ToLower();
                    if (!consonantHashSet.Contains(wordString[0]) || wordString.Length !=lengthWord) {
                        newSentence.Words.Add(word);
                        if (word.hasPunctuationNext)
                            newSentence.addPunctuation(sentence.punctuation.Dequeue());
                    }
                    else
                        if(word.hasPunctuationNext)sentence.punctuation.Dequeue();
                }
                textWithoutConsonantWords.addSentance(newSentence);
                newSentence = new Sentence();
            }
            return textWithoutConsonantWords;
        }
        private void initConsonantHashSet(HashSet<char> set) {
            var chars = new[] { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
                                'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z'};
            set.UnionWith(chars);
        }

        public void replaceWordsInSentanceWithLength(int numSentance, int lengthWord, string substring) {
            foreach(Word word in Sentances[numSentance].Words) {
                if(word.word.Length == lengthWord) {
                    word.word = substring;
                }
            }
            
        }
        public void deleteAllStopWords() {

        }
        public void exportXMLdoc() {

        }

    }
}
