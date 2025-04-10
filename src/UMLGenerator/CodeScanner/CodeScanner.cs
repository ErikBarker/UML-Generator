using System.IO;
using UMLGenerator;

namespace UMLGenerator.CodeScanner;
class CodeScanner
{
    private static Ruleset currentRule;
    private static String currentFile;
    public static String scanLocation;

    private static List<String> files = new List<String>();

    public static void startScan(){
        if (!Directory.Exists(scanLocation)){
            throw new FileNotFoundException();
        }

        searchDirs(scanLocation);

        foreach (String filename in files)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException();
            }

            if (!Ruleset.fileExtentionPairs.Keys.Contains(Path.GetExtension(filename)))
            {
                throw new Exception("No rule key found for type: " + Path.GetExtension(filename));
            }

            currentRule = Ruleset.fileExtentionPairs[Path.GetExtension(filename)];

            StreamReader reader = new StreamReader(filename);
            string currentLine = reader.ReadLine();

            int line = 0;
            while(currentLine != null){
                Lexer.tokenize(currentLine, line);
                line++;
                currentLine = reader.ReadLine();
            }
        }
    }

    private static void searchDirs(String dir){
        if (!Directory.Exists(dir)){
            throw new FileNotFoundException();
        }

        String[] subdirectorys = Directory.GetDirectories(dir);
        String[] subfiles = Directory.GetFiles(dir);

        foreach (String file in subfiles)
        {
            files.Add(file);
        }

        foreach (String subdir in subdirectorys)
        {
            searchDirs(subdir);
        }
    }

    public static Ruleset getCurrentRule(){
        return currentRule;
    }

}