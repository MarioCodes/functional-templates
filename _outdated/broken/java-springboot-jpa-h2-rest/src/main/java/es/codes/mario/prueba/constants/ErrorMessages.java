package es.codes.mario.prueba.constants;

import es.codes.mario.prueba.enums.DateFormats;

/**
 * Static error messages to throw on planned exceptions.
 */
public class ErrorMessages {

    public static String INVALID_DATE_FORMAT = "Bad Request. The exact date format to use is: " + DateFormats.EXPECTED_FORMAT.getFormat();
    public static String INVALID_ID_INPUT = "Bad Request. BrandId and ProductId cannot hold negative values.";

}
