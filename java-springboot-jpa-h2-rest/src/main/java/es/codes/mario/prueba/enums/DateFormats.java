package es.codes.mario.prueba.enums;

import lombok.Getter;

@Getter
public enum DateFormats {

    // this format ought to be something agreed with the consumer app, so they send us exactly this format through the REST API.
    EXPECTED_FORMAT("yyyy-MM-dd HH:mm:ss");

    private final String format;

    DateFormats(final String format) {
        this.format = format;
    }

}
