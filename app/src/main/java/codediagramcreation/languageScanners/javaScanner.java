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


    

    private static void javaClass(){
        
        String[] accessingDeclaration = new String[2];
        String name = null;
        
        if (currentline.contains(" Class ") || currentline.contains(" class ") || currentline.contains(" Class{") || currentline.contains(" class{")) {
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

    private static void javaMethod() {
        // TODO Auto-generated method stub
        //throw new UnsupportedOperationException("Unimplemented method 'javaMethod'");
    }

    
    private static void javaVar() {
        // TODO Auto-generated method stub
        //throw new UnsupportedOperationException("Unimplemented method 'javaVar'");
    }
}
