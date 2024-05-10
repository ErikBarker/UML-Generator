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

            javaClass();
        }
    }

    private static void javaClass(){
        
        String[] accessingDeclaration = new String[2];
        String name;
        if (currentline.contains("Class") || currentline.contains("class")) {
            currentlinearray = currentline.split(" ");

            for (int i = 0; i < accessingDeclaration.length; i++) {
                if (currentlinearray[i].equals("public") || currentlinearray[i].equals("private") || currentlinearray[i].equals("protected")) {
                    accessingDeclaration[0] = currentlinearray[i];
                } else if (currentlinearray[i].equals("static")) {
                    accessingDeclaration[1] = currentlinearray[i];
                } else {
                    name = currentlinearray[i];

                    if (name.contains("{")) {
                        name = name.replace("{", "");
                    }
                }
            }

            GeneralLanguage.generateClass();

        }
    }
}
