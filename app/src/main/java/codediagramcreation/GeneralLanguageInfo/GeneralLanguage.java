package codediagramcreation.GeneralLanguageInfo;


/**
 * This class is for generating a general language that most languages can fit into,
 * this allows for the option to expand to any language in a easy way.
 * 
 * 
 * 
 * 
 * 
 * 
 * CLASS
 * 
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
    
    
    public static String generateClass(String[] connections, String[] accessingDeclaration, String className, String[] vars, String[] methods){




        return "";
    }

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

        method += isStatic;

        return method;
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
    public static String generateVar(String[] accessingDeclaration, String varName, String varType){
       
        boolean isStatic = false;
        String varOutput = "";
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

        varOutput += accessingDecEnum.toString() + " " + varName;

        if (!varType.isBlank()) {
            varOutput += ":" + varType;
        }

        varOutput += isStatic;

        return  varOutput;
    }
}
