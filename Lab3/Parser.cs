using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3 {
    internal class Parser {

        static private HashSet<char> endSentncePunctuation = new HashSet<char>();
        static public Text parse(string path) {
            Text parsedText = new Text();

            endSentncePunctuation.Add('.');
            endSentncePunctuation.Add('?');
            endSentncePunctuation.Add(';');
            endSentncePunctuation.Add('!');

            using (StreamReader reader = new StreamReader(path)) {
                string? line;
                Sentence sentence = new Sentence();
                StringBuilder word = new StringBuilder();
                while ((line = reader.ReadLine()) != null) {
                    for (int i = 0; i < line.Length; i++) {
                        char symbol = line[i];
                        // склеивание слова 
                        if (char.IsLetterOrDigit(symbol) || symbol == '-' || symbol == '\'') {
                            word.Append(symbol);
                        }
                        // конец предложения, инициализация нового 
                        else if (endSentncePunctuation.Contains(symbol)) {
                            if (word.Length > 0) {
                                sentence.addWord(word.ToString());
                                word.Clear();
                            }
                            sentence.addPunctuation(symbol);

                            parsedText.addSentance(sentence);
                            sentence = new Sentence();
                        }
                        // пунктуация
                        else {
                            if (word.Length > 0) {
                                sentence.addWord(word.ToString());
                                word.Clear();
                            }
                            if (symbol != ' ')
                                sentence.addPunctuation(symbol);
                            ;
                        }
                    }
                    if (word.Length > 0) {
                        sentence.addWord(word.ToString());
                        word.Clear();
                    }
                }
                if (sentence.lengthSentence > 0) {
                    parsedText.addSentance(sentence);
                }
            }
            return parsedText;
        }

    }
}
