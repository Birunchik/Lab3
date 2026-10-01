using System.Text;

namespace Lab3 {
    internal class Sentence {
        public List<object> sentence { get; set; }
        public int lengthSentence { get; private set; }

        public Sentence() {
            lengthSentence = 0;
            sentence = new List<object>();
        }
        public void printSentance() {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < sentence.Count; i++) {
                sb.Append(sentence[i]);
                if (i+1 < sentence.Count && sentence[i+1] is string) { sb.Append(" "); }
            } 
            Console.Write(sb);
        }
        public void addWord(string str) {
            if (str != null) {
                sentence.Add(str);
                lengthSentence += str.Length;
            }
            else
                throw new Exception("String is null");
        }
        public void addPunctuation(char punctuation) {
            sentence.Add(punctuation);
        }
    }
}
