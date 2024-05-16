package codediagramcreation;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;

public class directorySearch {

    private static ArrayList<Object> files = new ArrayList<Object>();
    private static ArrayList<File> fileList = new ArrayList<File>();
    
    public static ArrayList<File> getFiles() {
        return fileList;
    }

    /**
     * 
     * @param dir the directory to be searched
     * @throws IOException
     */
    public static void Search(String dir) throws IOException{
        //Test validity of dir string
        if (dir == null) {
            throw new NullPointerException("Null Directory");
        }

        File directory = new File(dir); //Create directory file ref

        //Test existance of dir
        if (!directory.exists()) {
            throw new FileNotFoundException("Directory does not exist");
        }

        //Test for empty dir
        if (directory.list().length == 0) {
            throw new IOException("Directiry is empty");
        }

        //init the array list
        files.add(dir);

        //Start recursive search
        recursiveSearch(directory, files);

        //Display recursive search
        recursiveDisplay(files);
        
    }

    /**
     * Starts with the given root dirctory and add the name to item 0 of the {@code ArrayList}
     * searches through and adds the files to the {@code ArrayList}, if the file is a directory,
     * it creates a new {@code ArrayList} and begins a recursive search putting the name of that directory at item 0 of the
     * newly created {@code ArrayList}.
     * 
     * @param directory The root Directory to search
     * @param list the {@code ArrayList} to save the search to
     */
    private static void recursiveSearch(File directory, ArrayList<Object> list){
        for (String currentFileName : directory.list()) {

            File currentFile = new File(directory.getAbsolutePath() +"\\"+ currentFileName);

            if (currentFile.isFile()) {
                list.add(currentFile);
            } else{
                ArrayList<Object> subList = new ArrayList<Object>();
                subList.add(currentFileName);
                list.add(subList);
                recursiveSearch(currentFile, subList);
            }
        }
    }

    
    private static int line = 0; //The lines to represent folder depth, to prevent duplication var is out here
    /**
     * Displays a {@code ArrayList<Object>} and recursivly searches through each subsiquint {@code ArrayList}
     * @param list the {@code ArrayList} to search.
     * @see line a static intiger to keep track of the depth of the recursion.
     */
    public static void recursiveDisplay(ArrayList<?> list){

        for (int i = 0; i < list.size(); i++) { //starts scan through array list
            if (i == 0 && list.get(0).getClass().equals(String.class)) { //check to see if object 0 is a string (unessicary, done for extra precution)
                for (int j = 0; j < line; j++) {
                    System.out.print("| ");
                }
                System.out.println("> " + (String)list.get(0));
                line++;
            } else if(list.get(i).getClass().equals(ArrayList.class)){ //checks to see if the object is a array list to start recurtion
                
                recursiveDisplay((ArrayList<Object>)list.get(i));
                line--;
                for (int j = 0; j < line; j++) {
                    System.out.print("| ");
                }
                System.out.println("- ");
            } else if (list.get(i).getClass().equals(File.class)) { //checks to see if the object is a file to print
                for (int j = 0; j < line; j++) {
                    System.out.print("| ");
                }
                fileList.add((File)list.get(i));
                System.out.println(((File)list.get(i)).getName());
            }  
        }
    }
}
