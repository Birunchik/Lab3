

namespace Lab3 {
   class Programm {
      public static void Main(String[] args) {
         Text parsedText = Parser.parse("text.txt");
         parsedText.printText();

         parsedText.printByCountOrder();
         Console.WriteLine();

         parsedText.printByLengthOrder();
         Console.WriteLine();

         parsedText.findInInInterrogativeSentaceWordsWithLength(4);
         Console.WriteLine();

         Text withoutConcosant = parsedText.deleteAllСonsonantWordsWithLenth(5);
         withoutConcosant.printText();
         Console.WriteLine();

         //parsedText.replaceWordsInSentanceWithLength(1, 5, "kgfjbkldfb");
         //parsedText.printText();

         parsedText.deleteAllStopWords();
         parsedText.printText();

         parsedText.exportXMLdoc("res.xml");
         
      }
   }
}
