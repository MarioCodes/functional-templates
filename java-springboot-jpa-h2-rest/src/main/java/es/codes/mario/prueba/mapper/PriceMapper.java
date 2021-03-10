package es.codes.mario.prueba.mapper;

import es.codes.mario.prueba.dto.PriceReducedDto;
import es.codes.mario.prueba.entity.Price;
import org.springframework.beans.BeanUtils;
import org.springframework.stereotype.Component;

@Component
public class PriceMapper {

    public PriceReducedDto map(final Price entity) {
        final PriceReducedDto dto = new PriceReducedDto();
        BeanUtils.copyProperties(entity, dto);
        return dto;
    }

}
