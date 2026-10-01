using System.Text;
using System.Xml.Serialization;

namespace Lab3 {
   public class Sentence {
      [XmlIgnore]
      public List<object> sentence { get; set; } = new List<object>();
      [XmlElement("Word")]
      public string[] XmlWords {
         get {
            return sentence.OfType<string>().ToArray();
         }
         set {
         }
      }
      [XmlIgnore]
      public int countWords { get; private set; }
      [XmlIgnore]
      public int lengthSentence { get; private set; }
      [XmlIgnore]
      public bool isInterrogativeSentance { get; private set; }

      public Sentence() {
         lengthSentence = 0;
         countWords = 0;
         isInterrogativeSentance = false;
         sentence = new List<object>();
      }
      public void printSentance() {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < sentence.Count; i++) {
            sb.Append(sentence[i]);
            if (i + 1 < sentence.Count && sentence[i + 1] is string) { sb.Append(" "); }
         }
         Console.Write(sb);
      }
      public void addWord(string str) {
         if (str != null) {
            sentence.Add(str);
            lengthSentence += str.Length;
            countWords++;
         }
         else
            throw new Exception("String is null");
      }
      public void addPunctuation(char punctuation) {
         sentence.Add(punctuation);
         if (punctuation == '?')
            isInterrogativeSentance = true;
      }
   }
}
