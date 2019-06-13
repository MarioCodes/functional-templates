package es.msanchez.frameworks.java.spring.boot.exception;

public class DataTransferException extends RuntimeException {

    private static final long serialVersionUID = 967831159289792636L;

    public DataTransferException(final String message) {
        super(message);
    }

    public DataTransferException(final String message,
                                 final Throwable cause) {
        super(message, cause);
    }

}
