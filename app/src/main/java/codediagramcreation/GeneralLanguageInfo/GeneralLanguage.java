package codediagramcreation.GeneralLanguageInfo;

import java.io.File;
import java.util.ArrayList;

/**
 * This class is for generating a general language that most languages can fit into,
 * this allows for the option to expand to any language in a easy way.
 * 
 * 
 * 
 * 
 * 
 * <p>
 * CLASS
 * </p>
 * 
 * 
 * <p>
 * METHODS
 * </p>
 *  +|-|# varName( params[optional] ):type static[optonal:Bool]
 * </p>
 *  Examples:
 * <p>
 *  <blockquote><pre>
 *{@code "+ Temp():int true"}
 *{@code "- Temp(currentstate:bool):bool false"}
 *{@code "# foo():CustomClass false"}
 *  </pre></blockquote>
 * </p>
 * <p>
 * 
 * 
 * VARABLES
 * </p>
 *  +|-|# varName:type static[optonal:Bool]
 * </p>
 *  Examples:
 * <p><blockquote><pre>
 *{@code "+ Temp:int true"}
 *{@code "- Temp:bool false"}
 *{@code "# foo:CustomClass false"}
 *  </pre></blockquote></p>
 */
public class GeneralLanguage {
    public static ArrayList<ArrayList<String>> classList = new ArrayList<>();
    
    /**
     * 
     * @param connections
     * @param accessingDeclaration
     * @param className
     * @return
     */
    public static String generateClass(String[] connections, String accessingDeclaration, String className){

        
        
        try {
            String classString = "";
            accesingDeclarations accessingDecEnum = accesingDeclarations.pub;
    
    
            if (accessingDeclaration.equals("public")) {
                accessingDecEnum = accesingDeclarations.pub;
            } else if (accessingDeclaration.equals("private")) {
                accessingDecEnum = accesingDeclarations.prv;
            } else if (accessingDeclaration.equals("protected")) {
                accessingDecEnum = accesingDeclarations.pro;
            }

            for (String string : connections) {
                if (string != null) {
                    classString += string + " ";
                }
            }

            classString += accessingDecEnum + " " + className;

            addClass(classString);

            return classString;
        } catch (NullPointerException e) {
           System.err.println("Null value for class");
        }
        
        return "";
       
    }

    /**
     * Generates a Method to the general language format for the diagram generator to understand
     * 
     * <p>
     * METHODS
     * </p>
     *  +|-|# varName( params[optional] ):type static[optonal:Bool]
     * </p>
     *  Examples:
     * <p>
     *  <blockquote><pre>
     *{@code "+ Temp():int true"}
     *{@code "- Temp(currentstate:bool):bool false"}
     *{@code "# foo():CustomClass false"}
     *  </pre></blockquote>
     * </p>
     * <p>
     * 
     * @param accessingDeclaration an array containing the accesing delcarations for a varible following the format [{@code public}|{@code private}|{@code protected}, {@code static}] 
     * including the part of the array for static is optional
     * @param returntype what the method returns (optonal)
     * @param methodName the name of the method
     * @param params the paramiters for the method, you can use {@code GeneralLanguage.generateVar()} with no accesing methods in the array to make the paramiters (optonal)
     * @return
     */
    public static String generateMethod(String[] accessingDeclaration, String returntype, String methodName, String[] params){
        boolean isStatic = false;
        String method = "";
        accesingDeclarations accessingDecEnum = accesingDeclarations.pub;

        if (accessingDeclaration.length == 2) {
            isStatic = true;
        }

        //sorts the type of accesingDeclaration to a enum
        if (accessingDeclaration[0].equals("public")) {
            accessingDecEnum = accesingDeclarations.pub;
        } else if (accessingDeclaration[0].equals("private")) {
            accessingDecEnum = accesingDeclarations.prv;
        } else if (accessingDeclaration[0].equals("protected")) {
            accessingDecEnum = accesingDeclarations.pro;
        }
        
        method += accessingDecEnum.toString() + " " + methodName + "(";

        //adds each paramiter of the method if any
        for (String varparam : params) {
            method += varparam + " ";
        }

        method += ")";

        if (!returntype.isBlank()) {
            method += ":" + returntype;
        }

        method += " " + isStatic;

        addMethod(method);

        return method;
    }

    public static String generateVar(String[] accessingDeclaration, String varName, String varType){
        return generateVar(accessingDeclaration, varName, varType, true);
    }

    /**
     * Generates a Varable to the general language format for the diagram generator to understand
     * 
     * VARABLES
     * </p>
     *  +|-|# varName:type static[optonal:Bool]
     * </p>
     *  Examples:
     * <p><blockquote><pre>
     *{@code "+ Temp:int true"}
     *{@code "- Temp:bool false"}
     *{@code "# foo:CustomClass false"}
     *  </pre></blockquote></p>
     * 
     * @param accessingDeclaration an array containing the accesing delcarations for a varible following the format [{@code public}|{@code private}|{@code protected}, {@code static}] 
     * including the part of the array for static is optional
     * @param varName the name of the varable being created
     * @param varType the type of varable being created, this is optional for non declaring languages
     * @return the varable in the general language format 
     */
    public static String generateVar(String[] accessingDeclaration, String varName, String varType, boolean full){
       
        boolean isStatic = false;
        String varOutput = "";
        accesingDeclarations accessingDecEnum = accesingDeclarations.pub;

        if (accessingDeclaration.length == 2) {
            isStatic = Boolean.parseBoolean(accessingDeclaration[1]);
        }

        if (accessingDeclaration.length > 0) {  
            //sorts the type of accesingDeclaration to a enum
            if (accessingDeclaration[0].equals("public")) {
                accessingDecEnum = accesingDeclarations.pub;
            } else if (accessingDeclaration[0].equals("private")) {
                accessingDecEnum = accesingDeclarations.prv;
            } else if (accessingDeclaration[0].equals("protected")) {
                accessingDecEnum = accesingDeclarations.pro;
            }

        }
        if (full) {
            varOutput += accessingDecEnum.toString() + " ";
        }

        varOutput += varName;

        if (!varType.isBlank()) {
            varOutput += ":" + varType;
        }

        if (accessingDeclaration.length > 0) {
            varOutput += " " + isStatic;
        }
        
        addVarible(varOutput);

        return  varOutput;
    }

    private static void addClass(String classInfo){
        classList.add(new ArrayList<String>());
        classList.get(classList.size()-1).add(classInfo);
    }

    private static void addMethod(String methodInfo){
        classList.get(classList.size()-1).add(methodInfo);
    }

    private static void addVarible(String VaribleInfo){
        classList.get(classList.size()-1).add(VaribleInfo);
    }


    private static int line = 0; //The lines to represent folder depth, to prevent duplication var is out here
    /**
     * Displays a {@code ArrayList<Object>} and recursivly searches through each subsiquint {@code ArrayList}
     * @param list the {@code ArrayList} to search.
     * @see line a static intiger to keep track of the depth of the recursion.
     */
    public static void display(ArrayList<?> list){

        for (int i = 0; i < list.size(); i++) { //starts scan through array list
            if (i == 0 && list.get(0).getClass().equals(String.class)) { //check to see if object 0 is a string (unessicary, done for extra precution)
                for (int j = 0; j < line; j++) {
                    System.out.print("| ");
                }
                System.out.println("> " + (String)list.get(0));
                line++;
            } else if(list.get(i).getClass().equals(ArrayList.class)){ //checks to see if the object is a array list to start recurtion
                
                display((ArrayList<Object>)list.get(i));
                line--;
                for (int j = 0; j < line; j++) {
                    System.out.print("| ");
                }
                System.out.println("- ");
            } else if (list.get(i).getClass().equals(String.class)) { //checks to see if the object is a file to print
                for (int j = 0; j < line; j++) {
                    System.out.print("| ");
                }
                System.out.println(((String)list.get(i)));
            }  
        }
    }
}
