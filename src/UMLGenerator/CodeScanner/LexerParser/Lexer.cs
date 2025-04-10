using System.IO;

namespace UMLGenerator.CodeScanner;
class Lexer
{
    private static List<Token> tokens = new List<Token>();
    private static int currentToken = 0;

    public static void tokenize(String fileData, int lineNumber){

        
        for (int i = 0; i < fileData.Length; i++)
        {
            char c = fileData.ElementAt(i);

            if (char.IsWhiteSpace(c))
            {
                continue;
            }

            if(char.IsLetter(c) || c == '_'){
                int startIndex = i;
                while (i < fileData.Length && char.IsLetter(fileData.ElementAt(i)) || fileData.ElementAt(i) == '_')
                {
                    i++;
                }

                string word = fileData.Substring(startIndex, i-startIndex);
                string type = CodeScanner.getCurrentRule().keywords.contains(word) ? "keyword" : "identifier";

                tokens.Add(new Token{type = type, value = word, location = startIndex});
                continue;
            }

            if (CodeScanner.getCurrentRule().symbolSet.Contains(c.ToString()))
            {
                tokens.Add(new Token{type = "symbol", value = c.ToString(), line = lineNumber, location = i});
                continue;
            }

            tokens.Add(new Token{type = "unknown", value = c.ToString(), line = lineNumber, location = i});
        }
    }

    public static Token readNextToken(){
        currentToken++;
        if (tokens.Count == currentToken)
        {
            return null;
        }

        return tokens.ElementAt(currentToken - 1);
    }

    public static void reset(){
        currentToken = 0;
    }

    public static void clear(){
        currentToken = 0;
        tokens.Clear();
    }

    public static void displayTokens(){

        foreach (Token tok in tokens)
        {
            tok.display();
        }
    }

    public static List<Token> GetTokens() => tokens;

}