package es.codes.mario.prueba.service;

import com.googlecode.catchexception.apis.BDDCatchException;
import es.codes.mario.prueba.dao.PriceDao;
import es.codes.mario.prueba.dto.PriceReducedDto;
import es.codes.mario.prueba.dto.PriceRequest;
import es.codes.mario.prueba.entity.Price;
import es.codes.mario.prueba.exception.EntityNotFoundException;
import es.codes.mario.prueba.mapper.PriceMapper;
import org.assertj.core.api.BDDAssertions;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.BDDMockito;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.MockitoJUnitRunner;

import java.util.Optional;

@RunWith(MockitoJUnitRunner.class)
public class PriceServiceTest {

    @Mock
    private PriceDao daoMock;

    @Mock
    private PriceMapper mapperMock;

    @InjectMocks
    private PriceService service;

    @Test
    public void givenPriceRequestNotFoundWhenRetrievePriceThenCorrectExceptionThrown() {
        // given
        final PriceRequest request = this.buildPriceRequest();
        final Optional<Price> optPrice = Optional.empty();
        BDDMockito.given(daoMock.findOneByZonedDateTimeAndProductIdAndBrandId(request.getApplicationDate(),
                request.getProductId(), request.getBrandId())).willReturn(optPrice);

        // when
        BDDCatchException.when(service).retrievePrice(request);

        // then
        BDDAssertions.assertThat(BDDCatchException.caughtException())
                .isInstanceOf(EntityNotFoundException.class)
                .hasMessage("The entity with the following data could not be found. " +
                        " Date: " + request.getApplicationDate() + ", productId: " + request.getProductId() + ", brandId: " + request.getBrandId());
    }

    private PriceRequest buildPriceRequest() {
        return new PriceRequest();
    }

    @Test
    public void givenPriceRequestWhenRetrievePriceThenCorrectlyMapped() {
        // given
        final PriceRequest request = this.buildPriceRequest();
        final Price price = this.buildPrice();
        final Optional<Price> optPrice = Optional.of(price);
        BDDMockito.given(daoMock.findOneByZonedDateTimeAndProductIdAndBrandId(request.getApplicationDate(),
                request.getProductId(), request.getBrandId())).willReturn(optPrice);

        final PriceReducedDto dto = this.buildPriceReducedDto();
        BDDMockito.given(this.mapperMock.map(price))
                .willReturn(dto);

        // when
        final PriceReducedDto response = service.retrievePrice(request);

        // then
        BDDAssertions.assertThat(response).isNotNull()
                .isSameAs(dto);
    }

    private Price buildPrice() {
        return new Price();
    }

    private PriceReducedDto buildPriceReducedDto() {
        return new PriceReducedDto();
    }

}