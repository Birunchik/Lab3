

namespace Lab3 {
    class Programm {
        public static void Main(String[] args) {
            Text parsedText = Parser.parse("text.txt");
            parsedText.printText();

        }
    }
}
