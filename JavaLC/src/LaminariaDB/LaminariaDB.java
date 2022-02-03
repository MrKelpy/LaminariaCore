package LaminariaDB;

import LaminariaCore.PathUtils;
import LaminariaDB.exceptions.InvalidCollectionNameException;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

public class LaminariaDB{

    private final Path path;

    public LaminariaDB(String path)  throws IOException {
        this.path = Path.of(path);
        this.prepareDatabase();
    }

    public Collection addCollection(String name) throws InvalidCollectionNameException, IOException {
        /* Creates a new collection. A Collection is represented
         * by a directory at the database's path, which will contain Documents,
         * that contain JSON-formatted information.
         *
         * Collection names can only contain ASCII letters and numbers in their name, and no two
         * collections with the same name can exist.
         *
         * > Returns True if the collection was successfully created, False otherwise.
         */

        String ascii = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (char c : name.toCharArray()) {

            if (ascii.indexOf(c) == -1)
                throw new InvalidCollectionNameException("Collection names can't contain non-ASCII characters");
        }

        String collectionPath = PathUtils.join(this.path.toString(), name);
        Files.createDirectory(Path.of(collectionPath));
        return new Collection(collectionPath);
    }


    private void prepareDatabase() throws IOException {
        /* Prepares the database for usage by performing
         * any pre-usage tasks.
         */

        if (Files.notExists(this.path))
            Files.createDirectories(this.path);
    }

}
