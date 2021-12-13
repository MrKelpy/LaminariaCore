package LaminariaCore;

public enum PlaceholderUtils {

    SMALL_IPSUM("Lorem ipsum dolor sit amet."),
    BIG_IPSUM("Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
            "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi " +
            "ut aliquip ex ea commodo consequat.");


    private final String text;

    PlaceholderUtils(String text) {
        this.text = text;
    }


    @Override
    public String toString() {
        return this.text;
    }

}