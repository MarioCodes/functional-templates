package es.codes.mario.prueba.integrationtest;

import es.codes.mario.prueba.config.SpringConfig;
import es.codes.mario.prueba.dto.PriceReducedDto;
import es.codes.mario.prueba.rest.PriceRestResource;
import org.assertj.core.api.BDDAssertions;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.context.ContextConfiguration;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;

import java.time.ZoneId;
import java.time.ZonedDateTime;

@RunWith(SpringJUnit4ClassRunner.class)
@ContextConfiguration(classes = {SpringConfig.class})
// @DirtiestContext() is important for cleaning Spring context before each Integration test. Otherwise the context
// from one test could impact the following test, and the test may come out as red / wrong when they're in fact, not.
@DirtiesContext(classMode = DirtiesContext.ClassMode.BEFORE_EACH_TEST_METHOD)
public class ExampleIntegrationTests {

    @Autowired
    private PriceRestResource resource;

    @Test
    public void test1() {
        // given
        final String date = "2020-06-14 10:00:00";
        final ZonedDateTime dateAsTime = ZonedDateTime.of(2020, 6, 14, 10, 0, 0, 0, ZoneId.of("Europe/London"));

        final Long productId = 35455L;
        final Long brandId = 1L;

        final String expectedPrice = "35.50";
        final String expectedCurrency = "EUR";
        final Long expectedPriceList = 1L;

        // when
        final ResponseEntity<PriceReducedDto> result = resource.getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(result).isNotNull();
        BDDAssertions.assertThat(result.getBody()).isNotNull();

        final PriceReducedDto resultDto = result.getBody();
        BDDAssertions.assertThat(resultDto.getProductId()).isEqualTo(productId);
        BDDAssertions.assertThat(resultDto.getBrandId()).isEqualTo(brandId);
        BDDAssertions.assertThat(resultDto.getStartDate()).isBeforeOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getEndDate()).isAfterOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getPriceList()).isEqualTo(expectedPriceList);
        BDDAssertions.assertThat(resultDto.getPrice()).isEqualTo(expectedPrice);
        BDDAssertions.assertThat(resultDto.getCurrency()).isEqualTo(expectedCurrency);
    }

    @Test
    public void test2() {
        // given
        final String date = "2020-06-14 16:00:00";
        final ZonedDateTime dateAsTime = ZonedDateTime.of(2020, 6, 14, 16, 0, 0, 0, ZoneId.of("Europe/London"));

        final Long productId = 35455L;
        final Long brandId = 1L;

        final String expectedPrice = "25.45";
        final String expectedCurrency = "EUR";
        final Long expectedPriceList = 2L;

        // when
        final ResponseEntity<PriceReducedDto> result = resource.getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(result).isNotNull();
        BDDAssertions.assertThat(result.getBody()).isNotNull();

        final PriceReducedDto resultDto = result.getBody();
        BDDAssertions.assertThat(resultDto.getProductId()).isEqualTo(productId);
        BDDAssertions.assertThat(resultDto.getBrandId()).isEqualTo(brandId);
        BDDAssertions.assertThat(resultDto.getStartDate()).isBeforeOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getEndDate()).isAfterOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getPriceList()).isEqualTo(expectedPriceList);
        BDDAssertions.assertThat(resultDto.getPrice()).isEqualTo(expectedPrice);
        BDDAssertions.assertThat(resultDto.getCurrency()).isEqualTo(expectedCurrency);
    }

    @Test
    public void test3() {
        // given
        final String date = "2020-06-14 21:00:00";
        final ZonedDateTime dateAsTime = ZonedDateTime.of(2020, 6, 14, 21, 0, 0, 0, ZoneId.of("Europe/London"));

        final Long productId = 35455L;
        final Long brandId = 1L;

        final String expectedPrice = "35.50";
        final String expectedCurrency = "EUR";
        final Long expectedPriceList = 1L;

        // when
        final ResponseEntity<PriceReducedDto> result = resource.getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(result).isNotNull();
        BDDAssertions.assertThat(result.getBody()).isNotNull();

        final PriceReducedDto resultDto = result.getBody();
        BDDAssertions.assertThat(resultDto.getProductId()).isEqualTo(productId);
        BDDAssertions.assertThat(resultDto.getBrandId()).isEqualTo(brandId);
        BDDAssertions.assertThat(resultDto.getStartDate()).isBeforeOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getEndDate()).isAfterOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getPriceList()).isEqualTo(expectedPriceList);
        BDDAssertions.assertThat(resultDto.getPrice()).isEqualTo(expectedPrice);
        BDDAssertions.assertThat(resultDto.getCurrency()).isEqualTo(expectedCurrency);
    }

    @Test
    public void test4() {
        // given
        final String date = "2020-06-15 10:00:00";
        final ZonedDateTime dateAsTime = ZonedDateTime.of(2020, 6, 15, 10, 0, 0, 0, ZoneId.of("Europe/London"));

        final Long productId = 35455L;
        final Long brandId = 1L;

        final String expectedPrice = "30.50";
        final String expectedCurrency = "EUR";
        final Long expectedPriceList = 3L;

        // when
        final ResponseEntity<PriceReducedDto> result = resource.getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(result).isNotNull();
        BDDAssertions.assertThat(result.getBody()).isNotNull();

        final PriceReducedDto resultDto = result.getBody();
        BDDAssertions.assertThat(resultDto.getProductId()).isEqualTo(productId);
        BDDAssertions.assertThat(resultDto.getBrandId()).isEqualTo(brandId);
        BDDAssertions.assertThat(resultDto.getStartDate()).isBeforeOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getEndDate()).isAfterOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getPriceList()).isEqualTo(expectedPriceList);
        BDDAssertions.assertThat(resultDto.getPrice()).isEqualTo(expectedPrice);
        BDDAssertions.assertThat(resultDto.getCurrency()).isEqualTo(expectedCurrency);
    }

    @Test
    public void test5() {
        // given
        final String date = "2020-06-16 21:00:00";
        final ZonedDateTime dateAsTime = ZonedDateTime.of(2020, 6, 16, 21, 0, 0, 0, ZoneId.of("Europe/London"));

        final Long productId = 35455L;
        final Long brandId = 1L;

        final String expectedPrice = "38.95";
        final String expectedCurrency = "EUR";
        final Long expectedPriceList = 4L;

        // when
        final ResponseEntity<PriceReducedDto> result = resource.getPrice(date, productId, brandId);

        // then
        BDDAssertions.assertThat(result).isNotNull();
        BDDAssertions.assertThat(result.getBody()).isNotNull();

        final PriceReducedDto resultDto = result.getBody();
        BDDAssertions.assertThat(resultDto.getProductId()).isEqualTo(productId);
        BDDAssertions.assertThat(resultDto.getBrandId()).isEqualTo(brandId);
        BDDAssertions.assertThat(resultDto.getStartDate()).isBeforeOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getEndDate()).isAfterOrEqualTo(dateAsTime);
        BDDAssertions.assertThat(resultDto.getPriceList()).isEqualTo(expectedPriceList);
        BDDAssertions.assertThat(resultDto.getPrice()).isEqualTo(expectedPrice);
        BDDAssertions.assertThat(resultDto.getCurrency()).isEqualTo(expectedCurrency);
    }

}
