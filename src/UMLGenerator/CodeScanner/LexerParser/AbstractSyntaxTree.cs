using System.Text.RegularExpressions;

namespace UMLGenerator.CodeScanner;
class AbstractSyntaxTree
{
    public ASTNode root = null;
    public ASTNode current = null;
    public ASTNode previous = null;
    private static int MAX_WINDOW_SIZE = 15;
    private static int MIN_WINDOW_SIZE = 2;

    public static AbstractSyntaxTree parseList(List<Token> tokens){
        AbstractSyntaxTree tree = new AbstractSyntaxTree();

        Regex regex;

        foreach (string pattern in CodeScanner.getCurrentRule().patterns.Keys)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                ASTNode node;

                //Class pattern
                MAX_WINDOW_SIZE = CodeScanner.getCurrentRule().patterns[pattern].maxTokens;
                MIN_WINDOW_SIZE = CodeScanner.getCurrentRule().patterns[pattern].minTokens;
                regex = new Regex(CodeScanner.getCurrentRule().patterns[pattern].pattern);

                
                for (int currentWindowSize = MIN_WINDOW_SIZE; currentWindowSize < MAX_WINDOW_SIZE; currentWindowSize++)
                {
                    node = null;
                    String window = "";
                    for (int j = i; j < i + currentWindowSize && j < tokens.Count; j++)
                    {
                        window += tokens[j].value + " ";
                    }
    
                    if (regex.Matches(window.TrimEnd()).Count != 0){
                        int matchEnd = regex.Matches(window.TrimEnd())[0].Value.Split(" ").Length;
                        
                        Token acessToken = null;
                        Token nameToken = null;
    
    
                        for (int j = i; j < i + matchEnd && j < tokens.Count; j++)
                        {
                        if (CodeScanner.getCurrentRule().keywords.accessModifiers.Contains(tokens[j].value))
                        {
                            acessToken = tokens[j];
                        }else if(tokens[j].type.Equals("identifier")){
                            nameToken = tokens[j];
                        } else if(CodeScanner.getCurrentRule().symbolSet.openBlock.Contains(tokens[j].value)){
                            tree.previous = tree.current;
                            tree.current = null;
                        } else if(CodeScanner.getCurrentRule().symbolSet.closeBlock.Contains(tokens[j].value)){
                            tree.current = tree.current.parent;
                        }
                        
                        }
    
                        i += matchEnd - 1;
    
                        node = new ASTNode(CodeScanner.getCurrentRule().patterns[pattern].type, nameToken.value);

                        if (acessToken != null)
                        node.modifier = acessToken.value;
                    }
    
    
                    //Attach node if one was created
                    if (node != null)
                    {
                        if(tree.root == null){
                            tree.root = node;
                            tree.current = node;
                        }else if(tree.current == null){
                           tree.addChild(tree.previous, node);
                           tree.current = node;
                        }else
                        {
                            tree.addChild(tree.current, node);
                        }
                    }
                }
            } 
        }
        
        
        return tree;
    }

    public void add(Token token){
        addChild(root, token);
    }

    public void addChild(ASTNode parent, Token token){
        parent.children.Add(new ASTNode(token));
    }

    public void addChild(ASTNode parent, ASTNode node){
        parent.children.Add(node);
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