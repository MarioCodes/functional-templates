package es.codes.mario.prueba.exception;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

/**
 * This automatically sends a {@link ParamsValidationException} back to the caller when declared so
 * he has more information on what went wrong and how to fix the call.
 */
@ControllerAdvice
public class ExceptionHandlerAdvice {

    @ExceptionHandler(ParamsValidationException.class)
    public ResponseEntity handleException(ParamsValidationException e) {
        return ResponseEntity.status(e.getHttpStatus()).body(e.getMessage());
    }
}
