/*
 * This class is distributed as part of the Laminaria Core (Java Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
package LaminariaCore;

import java.io.*;
import java.util.ArrayList;

@SuppressWarnings({"unused", "ResultOfMethodCallIgnored"})
public class IOUtils {


    public static ArrayList<String> readFile(String path) {
        /* Accesses a filepath and reads the content there located.
         * > Returns null if an error occurred or the list is empty.
         * > Returns an ArrayList containing each line in the file.
         */

        ArrayList<String> result = new ArrayList<>();
        try (BufferedReader br = new BufferedReader(new FileReader(path))) {

            // Loops through the existent lines and adds them in order to the result List.
            String line = br.readLine();
            while (line != null) {
                result.add(line);
                line = br.readLine();
            }

            if (result.size() != 0)
                return result;  // Returns an ArrayList for each line in the read file.

            return null;  // Returns null if list is empty

        } catch (IOException e) {return null;}

   }


   public static void writeFile(String path, String text) {
       /* Accesses a filepath and writes content into it all at once. */

       try (BufferedWriter br = new BufferedWriter(new FileWriter(path))) {

           if (text.isBlank())
               return;
           br.write(text);

       } catch (IOException e) { assert true; }  // Write unsuccessful.

   }


    public static void appendFile(String path, String text) {
        /* Accesses a filepath and appends the text to the end of the file. */

        try (BufferedWriter br = new BufferedWriter(new FileWriter(path, true))) {

            if (text.isBlank())
                return;
            br.write(text);

        } catch (IOException e) { assert true; }  // Append unsuccessful.

    }


    public static void appendFileLines(String path, ArrayList<String> text) {
        /* Accesses a filepath and appends the text to the end of the file. */

        try (BufferedWriter br = new BufferedWriter(new FileWriter(path, true))) {

            if (text.size() == 0)
                return;

            // Appends the lines in the given string array into the end of the file
            for (String line: text) {
                br.write(line.strip());
                br.newLine();  // Adds a linebreak to the end of the line
            }

        } catch (IOException e) { assert true; }  // Write unsuccessful.

    }


    public static void writeFileLines(String path, ArrayList<String> text) {
        /* Accesses a filepath and appends content into the end of the file one line at a time. */

        try (BufferedWriter br = new BufferedWriter(new FileWriter(path))) {

            if (text.size() == 0)
                return;

            // Writes the lines in the given string array into the file
            for (String line: text) {
                br.write(line.strip());
                br.newLine();  // Adds a linebreak to the end of the line
            }

        } catch (IOException e) { assert true; }  // Write unsuccessful.

    }


    public static void ensureFilepath(String path) {
        /* Accesses the given filepath and makes sure that not only the file exists,
         * but the directory structure leading up to the file also does.
         */

        File file = new File(path);
        boolean mkd_flag = file.getParentFile().mkdirs();  // Creates all directories up to the file

        if (!mkd_flag)
            return;  // Checks if the directory creation was successful.

        try {
            file.createNewFile();
        } catch (IOException e) { assert true; }  // Ensurance unsuccessful.

    }
}
