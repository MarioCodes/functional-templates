package es.codes.mario.prueba.mapper;

import es.codes.mario.prueba.dto.PriceRequest;
import es.codes.mario.prueba.enums.DateFormats;
import es.codes.mario.prueba.validator.PriceValidator;
import org.springframework.stereotype.Component;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

@Component
public class PriceRequestMapper {

    public PriceRequest map(final String date,
                            final Long brandId,
                            final Long productId) {
        final LocalDateTime time = parseDate(date);
        return createRequest(time, brandId, productId);
    }

    /**
     * There's no need to check for exceptions here, as the input was already validated with the same code
     * at {@link PriceValidator#isValid(String)}
     *
     * @param date -
     * @return -
     */
    private LocalDateTime parseDate(final String date) {
        final DateTimeFormatter formatter = DateTimeFormatter.ofPattern(DateFormats.EXPECTED_FORMAT.getFormat());
        return LocalDateTime.parse(date, formatter);
    }

    private PriceRequest createRequest(final LocalDateTime date,
                                       final Long brandId,
                                       final Long productId) {
        final PriceRequest request = new PriceRequest();
        request.setApplicationDate(date);
        request.setBrandId(brandId);
        request.setProductId(productId);
        return request;
    }

}
