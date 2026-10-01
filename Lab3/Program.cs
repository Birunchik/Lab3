

namespace Lab3 {
    class Programm {
        public static void Main(String[] args) {
            Text RandomText = Parser.parse("text.txt");
            RandomText.printByCountOrder();
            Console.WriteLine();
            RandomText.printByLengthOrder();
            Console.WriteLine();

            RandomText.findInInInterrogativeSentaceWordsWithLength(4);    

            Text testF = RandomText.deleteAllСonsonantWordsWithLenth(5);
            testF.printText();


            RandomText.replaceWordsInSentanceWithLength(2, 5,"fffff");
            RandomText.printText();
            Console.Clear();
            RandomText.deleteAllStopWords();
            RandomText.printText();

        }
    }
}
