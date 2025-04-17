namespace UMLGenerator.CodeScanner;
class Token
{
    public String type;
    public String value;
    public int line;
    public int location;
    public bool isConsumed = false;

    public void display(){
        Console.WriteLine("Type: " + type + ", Value: " + value + ", Line: " + line + ", Loc: " + location);
    }
}