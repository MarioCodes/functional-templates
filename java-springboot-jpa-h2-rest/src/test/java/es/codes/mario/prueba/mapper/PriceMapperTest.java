package es.codes.mario.prueba.mapper;

import es.codes.mario.prueba.entity.Price;
import es.codes.mario.prueba.dto.PriceReducedDto;
import org.assertj.core.api.BDDAssertions;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.junit.MockitoJUnitRunner;

import java.time.ZoneId;
import java.time.ZonedDateTime;

@RunWith(MockitoJUnitRunner.class)
public class PriceMapperTest {

    @InjectMocks
    private PriceMapper mapper;

    @Test
    public void givenEntityWhenMapThenAllVariablesAreMapped() {
        // given
        final Price entity = this.buildPrice();

        // when
        final PriceReducedDto dto = mapper.map(entity);

        // then
        BDDAssertions.assertThat(dto).isNotNull();
        BDDAssertions.assertThat(dto.getBrandId()).isEqualTo(entity.getBrandId());
        BDDAssertions.assertThat(dto.getStartDate()).isEqualTo(entity.getStartDate());
        BDDAssertions.assertThat(dto.getEndDate()).isEqualTo(entity.getEndDate());
        BDDAssertions.assertThat(dto.getPriceList()).isEqualTo(entity.getPriceList());
        BDDAssertions.assertThat(dto.getPrice()).isEqualTo(entity.getPrice());
        BDDAssertions.assertThat(dto.getCurrency()).isEqualTo(entity.getCurrency());
    }

    private Price buildPrice() {
        final Price price = new Price();
        price.setBrandId(123L);
        price.setStartDate(ZonedDateTime.of(1980, 12, 20, 12, 0, 0, 0, ZoneId.of("Europe/London")));
        price.setEndDate(ZonedDateTime.of(1981, 2, 20, 14, 0, 0, 0, ZoneId.of("Europe/London")));
        price.setPriceList(1L);
        price.setPrice("123.0");
        price.setCurrency("EUR");
        return price;
    }

}