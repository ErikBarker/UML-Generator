package codediagramcreation.languageScanners;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

import codediagramcreation.GeneralLanguageInfo.GeneralLanguage;

//@Supported
public class javaScanner {
    private static Scanner fileReader;
    private static String currentline;
    private static String[] currentlinearray;

    public static void scanFile(File file) throws FileNotFoundException {
        System.out.println("reading file " + file.getName());
       
        fileReader = new Scanner(file);

        while (fileReader.hasNext()) {
            currentline = fileReader.nextLine();
            if (currentline.contains("//")) {
                continue;
            } else if (currentline.contains("/*")||currentline.contains("/**")) {
                while (fileReader.hasNext() && !currentline.contains("*/")) {
                    currentline = fileReader.nextLine();
                    continue;
                }
            }
            javaClass();
            javaMethod();
            javaVar();
        }
    }


    private static boolean isJavaClass(){
        if (currentline.contains(" Class ") || currentline.contains(" class ") || currentline.contains(" Class{") || currentline.contains(" class{")) {
            return true;
        }
        return false;
    }

    private static void javaClass(){
        
        String[] accessingDeclaration = new String[2];
        String name = null;
        
        if (isJavaClass()) {
            currentlinearray = currentline.split(" ");

            for (int i = 0; i < accessingDeclaration.length; i++) {
                if (currentlinearray[i].equalsIgnoreCase("public") || currentlinearray[i].equalsIgnoreCase("private") || currentlinearray[i].equalsIgnoreCase("protected")) {
                    accessingDeclaration[0] = currentlinearray[i];
                } else if (currentlinearray[i].equals("static")) {
                    accessingDeclaration[1] = currentlinearray[i];
                } else if(currentlinearray[i].equalsIgnoreCase("class")){
                    name = currentlinearray[i+1];

                    if (name.contains("{")) {
                        name = name.replace("{", "");
                    }
                }
            }

           System.out.println( GeneralLanguage.generateClass(new String[]{}, accessingDeclaration[0], name));

        }
    }

    public static boolean isJavaMethod(){

        currentline = currentline.trim(); // Trim any leading or trailing whitespace

            // Check if the line starts with public, private, protected, or static
        if (currentline.startsWith("public") || currentline.startsWith("private") || currentline.startsWith("protected") || currentline.startsWith("static")) {
        // Check if the line contains an opening parenthesis '(' and a closing parenthesis ')'
        int openParenIndex = currentline.indexOf('(');
        int closeParenIndex = currentline.lastIndexOf(')');
        if (openParenIndex != -1 && closeParenIndex != -1 && openParenIndex < closeParenIndex) {
            // Check if the line contains an opening curly brace '{'
            int openBraceIndex = currentline.indexOf('{', closeParenIndex);
            if (openBraceIndex != -1) {
                return true; // Method signature detected
            }
        }
    }
    return false; // Not a method signature
    }

    private static void javaMethod() {
        // TODO Auto-generated method stub
        //throw new UnsupportedOperationException("Unimplemented method 'javaMethod'");
    }
 
    
    
    private static void javaVar() {
        // TODO Auto-generated method stub
        //throw new UnsupportedOperationException("Unimplemented method 'javaVar'");

        String[] accessingDeclaration = new String[2];
        String varname = null;
        String varType = null;
        
        currentline = currentline.trim();
        if ((currentline.startsWith("public") || currentline.startsWith("private") || currentline.startsWith("protected")) && !(isJavaClass() || isJavaMethod() || isJavaInterface() || isJavaEnum())) {
            currentlinearray = currentline.split(" ");

            for (int i = 0; i < currentlinearray.length; i++) {
                if (currentlinearray[i].equalsIgnoreCase("public") || currentlinearray[i].equalsIgnoreCase("private") || currentlinearray[i].equalsIgnoreCase("protected")) {
                    accessingDeclaration[0] = currentlinearray[i];
                } else if (currentlinearray[i].equals("static")) {
                    accessingDeclaration[1] = currentlinearray[i];
                }else if(varType == null){
                    varType = currentlinearray[i];
                }else{
                    varname = currentlinearray[i].replace(";", "");
                }
            }

            

            System.out.println( GeneralLanguage.generateVar(accessingDeclaration, varname, varType));
        }
    }

    private static boolean isJavaEnum(){
        if (currentline.contains("enum")) {
            return true;
        }
        return false;
    }

    private static void javaEnum(){

    }

    private static boolean isJavaInterface(){
        if (currentline.contains("@interface")) {
            return true;
        }
        return false;
    }

    private static void javaInterface(){

    }

}
