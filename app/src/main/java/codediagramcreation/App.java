package codediagramcreation;

import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStreamWriter;

public class App {
    
    public String getGreeting() {
        return "Gradle Application is running!";
    }


    public static void main(String[] args) {
        System.out.println(new App().getGreeting());

        // Create a blank Draw.io diagram XML
        String diagramXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><mxfile host=\"app.diagrams.net\" modified=\"2022-03-24T07:57:48.270Z\" agent=\"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36\" etag=\"KMc1E5vus9egApD-T7qc\" version=\"11.9.7\" type=\"device\"><diagram id=\"FbOuxCf-MEXdnhUlct31\"><mxGraphModel dx=\"1322\" dy=\"773\" grid=\"1\" gridSize=\"10\" guides=\"1\" tooltips=\"1\" connect=\"1\" arrows=\"1\" fold=\"1\" page=\"1\" pageScale=\"1\" pageWidth=\"827\" pageHeight=\"1169\" math=\"0\" shadow=\"0\"><root><mxCell id=\"0\"/><mxCell id=\"1\" parent=\"0\"/><mxCell id=\"3\" value=\"Box\" style=\"shape=process\" vertex=\"1\" parent=\"1\"><mxGeometry x=\"80\" y=\"80\" width=\"120\" height=\"60\" as=\"geometry\"/></mxCell></root></mxGraphModel></diagram></mxfile>";

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