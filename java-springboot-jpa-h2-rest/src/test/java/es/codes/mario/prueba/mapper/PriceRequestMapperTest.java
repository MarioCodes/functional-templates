package es.codes.mario.prueba.mapper;

import es.codes.mario.prueba.dto.PriceRequest;
import org.assertj.core.api.BDDAssertions;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.junit.MockitoJUnitRunner;

@RunWith(MockitoJUnitRunner.class)
public class PriceRequestMapperTest {

    @InjectMocks
    private PriceRequestMapper mapper;

    @Test
    public void givenDataWhenMapThenPriceRequestCorrectlyBuilt() {
        // given
        final String date = "2020-02-28 10:00:00";
        final Long brandId = 13212L;
        final Long productId = 58L;

        // when
        final PriceRequest request = this.mapper.map(date, brandId, productId);

        // then
        BDDAssertions.assertThat(request).isNotNull();
        BDDAssertions.assertThat(request.getBrandId()).isEqualTo(brandId);
        BDDAssertions.assertThat(request.getProductId()).isEqualTo(productId);
        BDDAssertions.assertThat(request.getApplicationDate()).isNotNull();
        BDDAssertions.assertThat(request.getApplicationDate().getDayOfMonth()).isEqualTo(28);
        BDDAssertions.assertThat(request.getApplicationDate().getMonthValue()).isEqualTo(2);
        BDDAssertions.assertThat(request.getApplicationDate().getYear()).isEqualTo(2020);
        BDDAssertions.assertThat(request.getApplicationDate().getHour()).isEqualTo(10);
        BDDAssertions.assertThat(request.getApplicationDate().getMinute()).isEqualTo(0);
        BDDAssertions.assertThat(request.getApplicationDate().getSecond()).isEqualTo(0);
    }

}