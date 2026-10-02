using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Lab3 {

   [XmlRoot("Text")]
   public class Text {
      [XmlElement("Sentence")]
      public List<Sentence> Sentances { get; set; }

      public Text() {
         Sentances = new List<Sentence>();
      }

      public void printText() {
         foreach (Sentence sentence in Sentances) {
            sentence.printSentance();
         }
         Console.WriteLine("\n---------------------------------------");
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
         Console.WriteLine("Sort by count words in senteneces:");
         foreach (Sentence sentence in copy) {
            sentence.printSentance();
            Console.Write($"({sentence.countWords})");
            Console.WriteLine();
         }
         Console.Write("\n---------------------------------------");
      }


      public void printByLengthOrder() {
         List<Sentence> copy = Sentances.ToList();
         splitSortbyLength(copy, 0, Sentances.Count - 1);
         Console.WriteLine("Sort by count words in senteneces:");
         foreach (Sentence sentence in copy) {
            sentence.printSentance();
            Console.Write($"({sentence.lengthSentence})");
            Console.WriteLine();
         }
         Console.Write("\n---------------------------------------");
      }


      public void findInInInterrogativeSentaceWordsWithLength(int lengthWord) {
         HashSet<string> uniqueWords = new HashSet<string>();
         Console.WriteLine($"uniqueWords in InterrogativeSentace with length:{lengthWord}");
         foreach (Sentence sentence in Sentances) {
            if (sentence.isInterrogativeSentance) {
               foreach (var word in sentence.sentence) {
                  if (word is string str) {
                     if (!uniqueWords.Contains(str) && str.Length == lengthWord) {
                        uniqueWords.Add(str);
                        Console.WriteLine(str);
                     }
                  }
               }
            }
         }
         Console.Write("\n---------------------------------------");
      }

      public Text deleteAllСonsonantWordsWithLenth(int lengthWord) {
         Text textWithoutConsonantWords = new Text();
         Sentence newSentence = new Sentence();
         HashSet<char> consonantHashSet = new HashSet<char>();
         initConsonantHashSet(consonantHashSet);
         Console.WriteLine($"All Consonant Words with lengt:{lengthWord} has been deleted:");
         foreach (Sentence sentence in Sentances) {
            foreach (var word in sentence.sentence) {
               if (word is string str) {
                  if (!consonantHashSet.Contains(str[0]) || str.Length != lengthWord) {
                     newSentence.addWord(str);
                  }
               }
               if (word is char punc) {
                  newSentence.addPunctuation(punc);
               }
            }
            textWithoutConsonantWords.addSentance(newSentence);
            newSentence = new Sentence();
         }
         return textWithoutConsonantWords;
      }
      public void replaceWordsInSentanceWithLength(int numSentance, int lengthWord, string substring) {
         int idx = numSentance - 1;
         if (idx < 0 || idx > Sentances.Count)
            return;
         Console.WriteLine($"All words with length:{lengthWord} in {numSentance} sentance has been replaced by \"{substring}\"");
         foreach (var word in Sentances[idx].sentence) {
            if (word is string str) {
               if (str.Length == lengthWord) {
                  str = substring;
               }
            }
         }
         for (int i = 0; i < Sentances[idx].sentence.Count; i++) {
            if (Sentances[idx].sentence[i] is string str && str.Length == lengthWord) {
               Sentances[idx].sentence[i] = substring;
            }
         }
      }



      public void deleteAllStopWords() {

         HashSet<string> stopWords = new HashSet<string>();
         Sentence newSentence = new Sentence();
         initStopWords(stopWords);
         Console.WriteLine("All stop words has been deleted");
         for (int i = 0; i < Sentances.Count; i++) {
            foreach (var word in Sentances[i].sentence) {
               if (word is string str && !stopWords.Contains(str.ToLowerInvariant())) {
                  newSentence.addWord(str);
               }
               if (word is char punc) {
                  newSentence.addPunctuation(punc);
               }
            }
            Sentances[i] = newSentence;
            newSentence = new Sentence();
         }
      }
      public void exportXMLdoc(string path) {
         XmlSerializer serializer = new XmlSerializer(typeof(Text));
         var settings = new System.Xml.XmlWriterSettings
         {
            Indent = true
         };
         Console.WriteLine($"Text has been serialize to file \"{path}\"");
         using (var writer = System.Xml.XmlWriter.Create(path,settings)) {
            serializer.Serialize(writer, this);
         }
      }
      internal class WordInfo {
         public int Frequency { get; set; } = 0;
         public SortedSet<int> SentenceNumbers { get; set; } = new SortedSet<int>();
      }
      private SortedDictionary<string, WordInfo> BuildConcordance() {
         SortedDictionary<string, WordInfo> concordance = new SortedDictionary<string, WordInfo>(StringComparer.OrdinalIgnoreCase);
         for (int i = 0; i < Sentances.Count; i++) {
            int sentenceNumber = i + 1;

            foreach (var word in Sentances[i].sentence) {
               if (word is string str) {
                  string lowerWord = str.ToLowerInvariant();

                  if (!concordance.ContainsKey(lowerWord)) {
                     concordance[lowerWord] = new WordInfo();
                  }
                  concordance[lowerWord].Frequency++;
                  concordance[lowerWord].SentenceNumbers.Add(sentenceNumber);
               }
            }
         }
         return concordance;
      }
      public void PrintConcordance() {
         SortedDictionary<string, WordInfo> concordance = BuildConcordance();

         foreach (var element in concordance) {
            string word = element.Key;
            int freq = element.Value.Frequency;
            string sentenceNums = string.Join(" ", element.Value.SentenceNumbers);
            int totalLength = 25;
            string padding = new string('.', Math.Max(1, totalLength - word.Length));

            Console.WriteLine($"{word}{padding}{freq}: {sentenceNums}");
         }
      }

      private void initStopWords(HashSet<string> set) {
         var stopwords = new[] { "а", "е", "и", "ж", "м", "о", "на", "не", "ни", "об", "но", "он", "мне", "мои", "мож", "она", "они", "оно", "мной", "много", "многочисленное",
    "многочисленная", "многочисленные", "многочисленный", "мною", "мой", "мог", "могут", "можно", "может", "можхо", "мор", "моя", "моё", "мочь", "над", "нее", "оба",
    "нам", "нем", "нами", "ними", "мимо", "немного", "одной", "одного", "менее", "однажды", "однако", "меня", "нему", "меньше", "ней", "наверху", "него", "ниже",
    "мало", "надо", "один", "одиннадцать", "одиннадцатый", "назад", "наиболее", "недавно", "миллионов", "недалеко", "между", "низко", "меля", "нельзя", "нибудь",
    "непрерывно", "наконец", "никогда", "никуда", "нас", "наш", "нет", "нею", "неё", "них", "мира", "наша", "наше", "наши", "ничего", "начала", "нередко", "несколько",
    "обычно", "опять", "около", "мы", "ну", "нх", "от", "отовсюду", "особенно", "нужно", "очень", "отсюда", "в", "во", "вон", "вниз", "внизу", "вокруг", "вот", "восемнадцать",
    "восемнадцатый", "восемь", "восьмой", "вверх", "вам", "вами", "важное", "важная", "важные", "важный", "вдали", "везде", "ведь", "вас", "ваш", "ваша", "ваше", "ваши",
    "впрочем", "весь", "вдруг", "вы", "все", "второй", "всем", "всеми", "времени", "время", "всему", "всего", "всегда", "всех", "всею", "всю", "вся", "всё", "всюду", "г",
    "год", "говорил", "говорит", "года", "году", "где", "да", "ее", "за", "из", "ли", "же", "им", "до", "по", "ими", "под", "иногда", "довольно", "именно", "долго", "позже",
    "более", "должно", "пожалуйста", "значит", "иметь", "больше", "пока", "ему", "имя", "пор", "пора", "потом", "потому", "после", "почему", "почти", "посреди", "ей", "два",
    "две", "двенадцать", "двенадцатый", "двадцать", "двадцатый", "двух", "его", "дел", "или", "без", "день", "занят", "занята", "занято", "заняты", "действительно", "давно",
    "девятнадцать", "девятнадцатый", "девять", "девятый", "даже", "алло", "жизнь", "далеко", "близко", "здесь", "дальше", "для", "лет", "зато", "даром", "первый", "перед",
    "затем", "зачем", "лишь", "десять", "десятый", "ею", "её", "их", "бы", "еще", "при", "был", "про", "процентов", "против", "просто", "бывает", "бывь", "если", "люди",
    "была", "были", "было", "будем", "будет", "будете", "будешь", "прекрасно", "буду", "будь", "будто", "будут", "ещё", "пятнадцать", "пятнадцатый", "друго", "другое",
    "другой", "другие", "другая", "других", "есть", "пять", "быть", "лучше", "пятый", "к", "ком", "конечно", "кому", "кого", "когда", "которой", "которого", "которая",
    "которые", "который", "которых", "кем", "каждое", "каждая", "каждые", "каждый", "кажется", "как", "какой", "какая", "кто", "кроме", "куда", "кругом", "с", "т", "у",
    "я", "та", "те", "уж", "со", "то", "том", "снова", "тому", "совсем", "того", "тогда", "тоже", "собой", "тобой", "собою", "тобою", "сначала", "только", "уметь", "тот",
    "тою", "хорошо", "хотеть", "хочешь", "хоть", "хотя", "свое", "свои", "твой", "своей", "своего", "своих", "свою", "твоя", "твоё", "раз", "уже", "сам", "там", "тем", "чем",
    "сама", "сами", "теми", "само", "рано", "самом", "самому", "самой", "самого", "семнадцать", "семнадцатый", "самим", "самими", "самих", "саму", "семь", "чему", "раньше",
    "сейчас", "чего", "сегодня", "себе", "тебе", "сеаой", "человек", "разве", "теперь", "себя", "тебя", "седьмой", "спасибо", "слишком", "так", "такое", "такой", "такие",
    "также", "такая", "сих", "тех", "чаще", "четвертый", "через", "часто", "шестой", "шестнадцать", "шестнадцатый", "шесть", "четыре", "четырнадцать", "четырнадцатый",
    "сколько", "сказал", "сказала", "сказать", "ту", "ты", "три", "эта", "эти", "что", "это", "чтоб", "этом", "этому", "этой", "этого", "чтобы", "этот", "стал", "туда",
    "этим", "этими", "рядом", "тринадцать", "тринадцатый", "этих", "третий", "тут", "эту", "суть", "чуть", "тысяч",

    "a", "a's", "able", "about", "above", "according", "accordingly", "across", "actually", "after", "afterwards", "again", "against", "ain't", "all", "allow", "allows",
    "almost", "alone", "along", "already", "also", "although", "always", "am", "among", "amongst", "an", "and", "another", "any", "anybody", "anyhow", "anyone", "anything",
    "anyway", "anyways", "anywhere", "apart", "appear", "appreciate", "appropriate", "are", "aren't", "around", "as", "aside", "ask", "asking", "associated", "at", "available",
    "away", "awfully", "b", "be", "became", "because", "become", "becomes", "becoming", "been", "before", "beforehand", "behind", "being", "believe", "below", "beside",
    "besides", "best", "better", "between", "beyond", "both", "brief", "but", "by", "c", "c'mon", "c's", "came", "can", "can't", "cannot", "cant", "cause", "causes",
    "certain", "certainly", "changes", "clearly", "co", "com", "come", "comes", "concerning", "consequently", "consider", "considering", "contain", "containing",
    "contains", "corresponding", "could", "couldn't", "course", "currently", "d", "definitely", "described", "despite", "did", "didn't", "different", "do", "does",
    "doesn't", "doing", "don't", "done", "down", "downwards", "during", "e", "each", "edu", "eg", "eight", "either", "else", "elsewhere", "enough", "entirely", "especially",
    "et", "etc", "even", "ever", "every", "everybody", "everyone", "everything", "everywhere", "ex", "exactly", "example", "except", "f", "far", "few", "fifth", "first",
    "five", "followed", "following", "follows", "for", "former", "formerly", "forth", "four", "from", "further", "furthermore", "g", "get", "gets", "getting", "given",
    "gives", "go", "goes", "going", "gone", "got", "gotten", "greetings", "h", "had", "hadn't", "happens", "hardly", "has", "hasn't", "have", "haven't", "having", "he",
    "he's", "hello", "help", "hence", "her", "here", "here's", "hereafter", "hereby", "herein", "hereupon", "hers", "herself", "hi", "him", "himself", "his", "hither",
    "hopefully", "how", "howbeit", "however", "i", "i'd", "i'll", "i'm", "i've", "ie", "if", "ignored", "immediate", "in", "inasmuch", "inc", "indeed", "indicate",
    "indicated", "indicates", "inner", "insofar", "instead", "into", "inward", "is", "isn't", "it", "it'd", "it'll", "it's", "its", "itself", "j", "just", "k", "keep",
    "keeps", "kept", "know", "knows", "known", "l", "last", "lately", "later", "latter", "latterly", "least", "less", "lest", "let", "let's", "like", "liked", "likely",
    "little", "look", "looking", "looks", "ltd", "m", "mainly", "many", "may", "maybe", "me", "mean", "meanwhile", "merely", "might", "more", "moreover", "most", "mostly",
    "much", "must", "my", "myself", "n", "name", "namely", "nd", "near", "nearly", "necessary", "need", "needs", "neither", "never", "nevertheless", "new", "next", "nine",
    "no", "nobody", "non", "none", "noone", "nor", "normally", "not", "nothing", "novel", "now", "nowhere", "o", "obviously", "of", "off", "often", "oh", "ok", "okay", "old",
    "on", "once", "one", "ones", "only", "onto", "or", "other", "others", "otherwise", "ought", "our", "ours", "ourselves", "out", "outside", "over", "overall", "own", "p",
    "particular", "particularly", "per", "perhaps", "placed", "please", "plus", "possible", "presumably", "probably", "provides", "q", "que", "quite", "qv", "r", "rather",
    "rd", "re", "really", "reasonably", "regarding", "regardless", "regards", "relatively", "respectively", "right", "s", "said", "same", "saw", "say", "saying", "says",
    "second", "secondly", "see", "seeing", "seem", "seemed", "seeming", "seems", "seen", "self", "selves", "sensible", "sent", "serious", "seriously", "seven", "several",
    "shall", "she", "should", "shouldn't", "since", "six", "so", "some", "somebody", "somehow", "someone", "something", "sometime", "sometimes", "somewhat", "somewhere",
    "soon", "sorry", "specified", "specify", "specifying", "still", "sub", "such", "sup", "sure", "t", "t's", "take", "taken", "tell", "tends", "th", "than", "thank",
    "thanks", "thanx", "that", "that's", "thats", "the", "their", "theirs", "them", "themselves", "then", "thence", "there", "there's", "thereafter", "thereby",
    "therefore", "therein", "theres", "thereupon", "these", "they", "they'd", "they'll", "they're", "they've", "think", "third", "this", "thorough", "thoroughly",
    "those", "though", "three", "through", "throughout", "thru", "thus", "to", "together", "too", "took", "toward", "towards", "tried", "tries", "truly", "try",
    "trying", "twice", "two", "u", "un", "under", "unfortunately", "unless", "unlikely", "until", "unto", "up", "upon", "us", "use", "used", "useful", "uses",
    "using", "usually", "uucp", "v", "value", "various", "very", "via", "viz", "vs", "w", "want", "wants", "was", "wasn't", "way", "we", "we'd", "we'll", "we're",
    "we've", "welcome", "well", "went", "were", "weren't", "what", "what's", "whatever", "when", "whence", "whenever", "where", "where's", "whereafter", "whereas",
    "whereby", "wherein", "whereupon", "wherever", "whether", "which", "while", "whither", "who", "who's", "whoever", "whole", "whom", "whose", "why", "will", "willing",
    "wish", "with", "within", "without", "won't", "wonder", "would", "would", "wouldn't", "x", "y", "yes", "yet", "you", "you'd", "you'll", "you're", "you've", "your",
    "yours", "yourself", "yourselves", "z", "zero" };
         set.UnionWith(stopwords);

      }
      private void initConsonantHashSet(HashSet<char> set) {
         var chars = new[] { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
                          'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z'};
         set.UnionWith(chars);
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
      private void splitSortbyCount(List<Sentence> array, int low, int height) {
         if (low < height) {
            int val = array[height].countWords;
            ;
            int i = low - 1;
            for (int j = low; j < height; j++) {
               if (array[j].countWords < val) {
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
   }
}
