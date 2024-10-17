package es.codes.mario.prueba.rest;

import es.codes.mario.prueba.constants.ErrorMessages;
import es.codes.mario.prueba.dto.PriceReducedDto;
import es.codes.mario.prueba.dto.PriceRequest;
import es.codes.mario.prueba.mapper.PriceRequestMapper;
import es.codes.mario.prueba.service.PriceService;
import es.codes.mario.prueba.exception.ParamsValidationException;
import es.codes.mario.prueba.validator.PriceValidator;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RestController;

@Slf4j
@RestController
public class PriceRestResource {

    @Autowired
    private PriceService service;

    @Autowired
    private PriceValidator validator;

    @Autowired
    private PriceRequestMapper mapper;

    @GetMapping("/price/{date}/{product_id}/{brand_id}")
    public ResponseEntity<PriceReducedDto> getPrice(@PathVariable("date") final String date,
                                                    @PathVariable("product_id") final Long productId,
                                                    @PathVariable("brand_id") final Long brandId) {
        log.debug("called rest service with values date: " + date + ", product_id: " + productId + ", brandId: " + brandId);

        if (!this.validator.isValid(date)) {
            log.error("tried to validate date with value: " + date + ". This date hasn't the right format.");
            throw new ParamsValidationException(HttpStatus.UNPROCESSABLE_ENTITY, ErrorMessages.INVALID_DATE_FORMAT);
        }
        if (!this.validator.isValid(brandId) || !this.validator.isValid(productId)) {
            log.error("tried to validate either brandId: " + brandId + " and productId: " + productId + ". One or both of them is not valid.");
            throw new ParamsValidationException(HttpStatus.BAD_REQUEST, ErrorMessages.INVALID_ID_INPUT);
        }

        final PriceRequest request = this.mapper.map(date, brandId, productId);
        final PriceReducedDto price = service.retrievePrice(request);
        return ResponseEntity.ok(price);
    }

}
