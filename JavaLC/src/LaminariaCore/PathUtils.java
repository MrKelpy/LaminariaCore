/*
 * This class is distributed as part of the Laminaria Core (Java Version).
 * Get the Source Code in GitHub:
 * https://github.com/MrKelpy/LaminariaCore
 *
 * The LaminariaCore is Open Source and distributed under the
 * MIT License
 */
package LaminariaCore;

import java.util.ArrayList;
import java.util.Collections;

@SuppressWarnings("unused")
public class PathUtils {


    public static String join(String ... chunks) {
        /* Takes in path chunks and forms a path in the correct format from them.
         * This is a vararg method, meaning that there can be as amny path chunks as one can write, and beyond.
         * > Returns a String containing the path.
         */

        StringBuilder path = new StringBuilder();
        for (String chunk: chunks) {

            String slashed_chunk = chunk + "\\";
            path.append(slashed_chunk);  // Adds a path chunk with the slash in the end to the string.
        }
        // Returns the complete path excluding the last character, because it'll always be a '\'.
        return path.deleteCharAt(path.length() - 1).toString();

    }


    public static String getParent(String path) {
        /* Returns the path to the parent directory of a directory/file.
         * > Returns a String containing the path for the Parent Directory of the given path.
         */

        ArrayList<String> result = new ArrayList<>();
        Collections.addAll(result, path.split("\\\\"));  // Converts the path.split() array to an ArrayList

        result.remove(result.size() - 1);  // Removes the last file/directory from the path
        result.removeIf(String::isEmpty);  // Cleans up the resultant list

        return String.join("\\", result);
    }

}
