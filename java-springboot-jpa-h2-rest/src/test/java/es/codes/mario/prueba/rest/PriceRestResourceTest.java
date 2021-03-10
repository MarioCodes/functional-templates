package es.codes.mario.prueba.rest;

import com.googlecode.catchexception.apis.BDDCatchException;
import es.codes.mario.prueba.constants.ErrorMessages;
import es.codes.mario.prueba.mapper.PriceRequestMapper;
import es.codes.mario.prueba.service.PriceService;
import es.codes.mario.prueba.dto.PriceReducedDto;
import es.codes.mario.prueba.dto.PriceRequest;
import es.codes.mario.prueba.exception.ParamsValidationException;
import es.codes.mario.prueba.validator.PriceValidator;
import org.assertj.core.api.BDDAssertions;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.BDDMockito;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnitRunner;
import org.springframework.http.ResponseEntity;

@RunWith(MockitoJUnitRunner.class)
public class PriceRestResourceTest {

    @Mock
    private PriceService serviceMock;

    @Mock
    private PriceValidator validatorMock;

    @Mock
    private PriceRequestMapper mapperMock;

    @InjectMocks
    private PriceRestResource resource;

    @Test
    public void givenInvalidDateWhenGetPriceThenCorrectExceptionIsThrown() {
        // given
        final String date = "THIS_IS_AN_INVALID_DATE";
        final Long brandId = 123L;
        final Long productId = 238934L;

        BDDMockito.given(this.validatorMock.isValid(date))
                .willReturn(false);

        // when
        BDDCatchException.when(this.resource).getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(BDDCatchException.caughtException())
                .isInstanceOf(ParamsValidationException.class)
                .hasMessage(ErrorMessages.INVALID_DATE_FORMAT);
    }

    @Test
    public void givenInvalidBrandIdWhenGetPriceThenCorrectExceptionIsThrown() {
        // given
        final String date = "THIS_IS_A_VALID_DATE";
        final Long brandId = 123L;
        final Long productId = 238934L;

        BDDMockito.given(this.validatorMock.isValid(date))
                .willReturn(true);

        BDDMockito.given(this.validatorMock.isValid(brandId))
                .willReturn(false);

        // when
        BDDCatchException.when(this.resource).getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(BDDCatchException.caughtException())
                .isInstanceOf(ParamsValidationException.class)
                .hasMessage(ErrorMessages.INVALID_ID_INPUT);
    }

    @Test
    public void givenInvalidProductIdWhenGetPriceThenCorrectExceptionIsThrown() {
        // given
        final String date = "THIS_IS_A_VALID_DATE";
        final Long brandId = 123L;
        final Long productId = 238934L;

        BDDMockito.given(this.validatorMock.isValid(date))
                .willReturn(true);

        BDDMockito.given(this.validatorMock.isValid(Mockito.anyLong()))
                .willReturn(true, false);

        // when
        BDDCatchException.when(this.resource).getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(BDDCatchException.caughtException())
                .isInstanceOf(ParamsValidationException.class)
                .hasMessage(ErrorMessages.INVALID_ID_INPUT);
    }

    @Test
    public void givenValidDataWhenGetPriceThenCorrectResponse() {
        // given
        final String date = "THIS_IS_A_VALID_DATE";
        final Long brandId = 123L;
        final Long productId = 238934L;

        BDDMockito.given(this.validatorMock.isValid(date))
                .willReturn(true);

        BDDMockito.given(this.validatorMock.isValid(brandId))
                .willReturn(true);

        BDDMockito.given(this.validatorMock.isValid(productId))
                .willReturn(true);

        final PriceRequest priceRequest = this.buildPriceRequest();
        BDDMockito.given(this.mapperMock.map(date, brandId, productId))
                .willReturn(priceRequest);

        final PriceReducedDto dto = this.buildPriceReducedDto();
        BDDMockito.given(this.serviceMock.retrievePrice(priceRequest))
                .willReturn(dto);

        // when
        final ResponseEntity<PriceReducedDto> response = this.resource.getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(response).isNotNull();
        BDDAssertions.assertThat(response.getBody()).isNotNull()
                .isSameAs(dto);
    }

    private PriceRequest buildPriceRequest() {
        return new PriceRequest();
    }

    private PriceReducedDto buildPriceReducedDto() {
        return new PriceReducedDto();
    }

}