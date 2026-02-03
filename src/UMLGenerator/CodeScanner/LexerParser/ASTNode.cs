namespace UMLGenerator.CodeScanner;
class ASTNode
{
    public string type;
    public string value;
    public string? modifier;
    public string visability;
    public List<ASTNode> children = new List<ASTNode>();
    public ASTNode parent;

    public ASTNode(string type, string value){
        this.type = type;
        this.value = value;
    }

    public ASTNode(Token token){
        this.type = token.type;
        this.value = token.value;
    }

    public void display(){
        System.Diagnostics.Debug.WriteLine("- "+type+" : " + value);
        if (children.Count > 0)
        {
            for (int i = 0; i < children.Count; i++)
            {
                System.Diagnostics.Debug.Write("| ");
                children.ElementAt(i).display();
            }
        }
        
    }
}