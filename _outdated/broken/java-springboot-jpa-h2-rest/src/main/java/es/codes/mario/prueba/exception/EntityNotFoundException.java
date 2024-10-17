package es.codes.mario.prueba.exception;

import org.springframework.http.HttpStatus;

public class EntityNotFoundException extends RuntimeException {

    private HttpStatus httpStatus = HttpStatus.INTERNAL_SERVER_ERROR;

    public HttpStatus getHttpStatus() {
        return httpStatus;
    }

    public EntityNotFoundException(final String message) {
        super(message);
    }

    public EntityNotFoundException(final HttpStatus httpStatus, final String message) {
        super(message);
        this.httpStatus = httpStatus;
    }

}
