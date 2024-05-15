package codediagramcreation;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;
import java.net.URL;
import java.util.Scanner;
import java.util.List;
import java.util.ArrayList;
import java.util.Enumeration;

import codediagramcreation.GeneralLanguageInfo.GeneralLanguage;
import codediagramcreation.languageScanners.Supported;
import codediagramcreation.languageScanners.javaScanner;

public class App {
    
    public String getGreeting() {
        try {
            return "Welcome, Please choose a directory you would like to generate a diagram for: this program currently supports:" + supportedLanguages();
        } catch (ClassNotFoundException | IOException e) {
            e.printStackTrace();
        }
        return "";
    }

    public String supportedLanguages() throws ClassNotFoundException, IOException{
        String output = "";

        // Define the package name
        String packageName = "codediagramcreation.languageScanners";

        // Get the ClassLoader
        ClassLoader classLoader = Thread.currentThread().getContextClassLoader();

        // Get all class names in the package
        List<Class<?>> classes = getClassesInPackage(packageName, classLoader);

        // Iterate through the classes and check for @Supported annotation
        for (Class<?> clazz : classes) {
            if (clazz.isAnnotationPresent(Supported.class)) {
                output = output +" " + clazz.getName().replaceFirst("codediagramcreation.languageScanners.", "");
            }
        }
        if (output.isEmpty()) {
            return "none";
        }
        return output;
    }

    private static List<Class<?>> getClassesInPackage(String packageName, ClassLoader classLoader) throws IOException, ClassNotFoundException {
        List<Class<?>> classes = new ArrayList<>();
        String path = packageName.replace('.', '/');
        Enumeration<URL> resources = classLoader.getResources(path);
        while (resources.hasMoreElements()) {
            URL resource = resources.nextElement();
            if (resource.getProtocol().equals("file")) {
                File directory = new File(resource.getFile());
                if (directory.exists()) {
                    // Iterate over .class files in the directory
                    File[] files = directory.listFiles();
                    if (files != null) {
                        for (File file : files) {
                            if (file.isFile() && file.getName().endsWith(".class")) {
                                String className = packageName + '.' + file.getName().substring(0, file.getName().length() - 6);
                                Class<?> clazz = Class.forName(className);
                                classes.add(clazz);
                            }
                        }
                    }
                }
            }
        }
        return classes;
    }
    



    public static void main(String[] args) throws IOException {
        System.out.println(new App().getGreeting());

        if (args.length != 0) {
            directorySearch.Search(args[0]);
        } else {
            Scanner inputScanner = new Scanner(System.in);

            String input = inputScanner.nextLine();

            inputScanner.close();

            directorySearch.Search(input);
        }
        
        for (File file : directorySearch.getFiles()) {
            if (file.getAbsolutePath().endsWith(".java")) {
                javaScanner.scanFile(file);
            }
        }
        
        System.out.println(GeneralLanguage.generateVar(new String[]{"public", "true"}, "testvar", "int"));
        System.out.println(GeneralLanguage.generateMethod(new String[]{"private", "true"}, "void", "testmethod", new String[]{GeneralLanguage.generateVar(new String[]{}, "time", "int"), GeneralLanguage.generateVar(new String[]{}, "isOn", "bool")}));
        System.out.println(GeneralLanguage.generateClass(new String[]{"String"},"protected", "testclass"));
        createDiagram();
    }

    private static void createDiagram(){
        // Create a blank Draw.io diagram XML
        String diagramXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><mxfile host=\"app.diagrams.net\" modified=\"2022-03-24T07:57:48.270Z\" agent=\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36\" etag=\"KMc1E5vus9egApD-T7qc\" version=\"11.9.7\" type=\"device\"><diagram id=\"FbOuxCf-MEXdnhUlct31\"><mxGraphModel dx=\"1322\" dy=\"773\" grid=\"1\" gridSize=\"10\" guides=\"1\" tooltips=\"1\" connect=\"1\" arrows=\"1\" fold=\"1\" page=\"1\" pageScale=\"1\" pageWidth=\"827\" pageHeight=\"1169\" math=\"0\" shadow=\"0\"><root><mxCell id=\"0\"/><mxCell id=\"1\" parent=\"0\"/><mxCell id=\"5\" value=\"Hello\" style=\"shape=process\" vertex=\"1\" parent=\"1\"><mxGeometry x=\"80\" y=\"80\" width=\"120\" height=\"60\" as=\"geometry\"/></mxCell></root></mxGraphModel></diagram></mxfile>";

        // Save the diagram to a file
        String filePath = ".\\output\\blank_diagram.drawio";
        saveDiagramToFile(diagramXml, filePath);

        System.out.println("Blank Draw.io diagram created successfully at: " + filePath);
    }

    private static void saveDiagramToFile(String diagramXml, String filePath) {
        try (FileOutputStream fos = new FileOutputStream(filePath);
             OutputStreamWriter writer = new OutputStreamWriter(fos)) {
            writer.write(diagramXml);
        } catch (IOException e) {
            System.err.println("Error saving diagram to file: " + e.getMessage());
            e.printStackTrace();
        }
    }
}