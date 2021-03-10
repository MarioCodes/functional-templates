package es.codes.mario.prueba.service;

import es.codes.mario.prueba.dao.PriceDao;
import es.codes.mario.prueba.dto.PriceReducedDto;
import es.codes.mario.prueba.dto.PriceRequest;
import es.codes.mario.prueba.entity.Price;
import es.codes.mario.prueba.exception.EntityNotFoundException;
import es.codes.mario.prueba.mapper.PriceMapper;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.Optional;

@Slf4j
@Component
public class PriceService {

    @Autowired
    private PriceDao dao;

    @Autowired
    private PriceMapper mapper;

    /**
     * @param request -
     * @return Returns a filled {@link Price} if its found in the database or a {@link Price} with empty variables
     * if no entry was found.
     */
    public PriceReducedDto retrievePrice(final PriceRequest request) {
        final Optional<Price> optPrice = dao.findOneByZonedDateTimeAndProductIdAndBrandId(request.getApplicationDate(),
                request.getProductId(), request.getBrandId());
        final Price price = optPrice.orElseThrow(() -> this.log(request));
        return mapper.map(price);
    }

    private EntityNotFoundException log(final PriceRequest request) {
        final String errorMessage = "The entity with the following data could not be found. " +
                " Date: " + request.getApplicationDate() + ", productId: " + request.getProductId() + ", brandId: " + request.getBrandId();
        log.error(errorMessage);
        return new EntityNotFoundException(errorMessage);
    }

}
