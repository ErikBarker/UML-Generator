package codediagramcreation;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.util.ArrayList;

public class directorySearch {

    private static ArrayList<Object> files = new ArrayList<Object>();
    
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

        //Start recursive search
    }
}
