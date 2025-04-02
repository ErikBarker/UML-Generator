using System.IO;

namespace UMLGenerator.CodeScanner;
class Lexer
{
    private List<Token> tokens;
    private int currentToken;


    public void tokenize(String fileData){
        //CodeScanner.getCurrentRule();
        String[] tokenData = fileData.Replace(", ", " ").Split();

        for (int i = 0; i < tokenData.Length; i++)
        {
            if (true)
            {
                
            }            
        }
    }

}