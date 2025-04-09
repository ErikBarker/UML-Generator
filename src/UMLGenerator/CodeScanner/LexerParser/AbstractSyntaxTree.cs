namespace UMLGenerator.CodeScanner;
class AbstractSyntaxTree
{
    public ASTNode root;

    public static AbstractSyntaxTree parseList(List<Token> tokens){
        AbstractSyntaxTree tree = new AbstractSyntaxTree();

        

        return tree;
    }

    public void add(Token token){
        addChild(root, token);
    }

    public void addChild(ASTNode parent, Token token){
        parent.children.Add(new ASTNode(token));
    }

    public ASTNode getNodeType(Token token){
        return null;
    }

    public void traverseTree(ASTNode node, int depth){

    }

    public void displayTree(){
        root.display();
    }
}