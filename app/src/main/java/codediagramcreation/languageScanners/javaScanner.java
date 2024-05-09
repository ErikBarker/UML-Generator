package codediagramcreation.languageScanners;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.Scanner;

//@Supported
public class javaScanner {
    


    public static void scanFile(File file) throws FileNotFoundException {
        System.out.println("reading file " + file.getName());
        Scanner fileReader = new Scanner(file);
        String currentline;
        String[] currentlinearray;

        String[] accessingDeclaration = new String[2];

        while (fileReader.hasNext()) {
            currentline = fileReader.nextLine();

            if (currentline.contains("Class") || currentline.contains("class")) {
                currentlinearray = currentline.split(" ");

                for (int i = 0; i < accessingDeclaration.length; i++) {
                    if (currentlinearray[i].equals("public") || currentlinearray[i].equals("private") || currentlinearray[i].equals("protected")) {
                        accessingDeclaration[0] = currentlinearray[i];
                    } else if (currentlinearray[i].equals("static")) {
                        accessingDeclaration[1] = currentlinearray[i];
                    } else if (condition) {
                        
                    }
                }
            }
        }
    }
}
