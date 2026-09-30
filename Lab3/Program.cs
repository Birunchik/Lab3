

namespace Lab3 {
    class Programm {
        public static void Main(String[] args) {
            Text RandomText = Parser.parse("text.txt");
            RandomText.printByCountOrder();
            Console.WriteLine();
            RandomText.printByLengthOrder();
            Console.WriteLine();
            RandomText.findInInInterrogativeSentaceWordsWithLength(4);    
            RandomText.printText();

        }
    }
}
