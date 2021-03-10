package es.codes.mario.prueba.validator;

import es.codes.mario.prueba.enums.DateFormats;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;
import org.springframework.util.StringUtils;

import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;

@Slf4j
@Component
public class PriceValidator {

    /**
     * @param date -
     * @return true if the param 'date' is a valid {@link ZonedDateTime zonedDateTime} exactly with this format {@link DateFormats#EXPECTED_FORMAT}
     */
    public boolean isValid(final String date) {
        if (StringUtils.isEmpty(date)) {
            log.warn("tried to validate a date, but it's null or empty.");
            return false;
        }

        boolean isValid = true;
        try {
            final DateTimeFormatter formatter = DateTimeFormatter.ofPattern(DateFormats.EXPECTED_FORMAT.getFormat());
            final LocalDateTime time = LocalDateTime.parse(date, formatter);
            final ZoneId zone = ZoneId.of("Europe/London");
            final ZonedDateTime zonedDateTime = ZonedDateTime.of(time, zone);
        } catch (final DateTimeParseException ex) {
            log.error("DateTimeParseException on parsing the date: " + date + ". " + ex.getMessage());
            isValid = false;
        }
        return isValid;
    }

    public boolean isValid(final Long l) {
        return l != null && l >= 0L;
    }

}
