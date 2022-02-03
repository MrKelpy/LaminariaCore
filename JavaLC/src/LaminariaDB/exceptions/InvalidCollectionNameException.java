package LaminariaDB.exceptions;

public class InvalidCollectionNameException extends Exception {
    /* Occurs when an invalid name is passed in as a Collection name on creation.
     * Collection names can only contain ASCII letters and numbers in their name.
     */

    private String stacktrace;

    public InvalidCollectionNameException(String stacktrace) {
        super(stacktrace);
    }

}
