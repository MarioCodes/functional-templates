package es.codes.mario.prueba.dto;

import lombok.Getter;
import lombok.Setter;

import java.io.Serializable;
import java.time.ZonedDateTime;

@Getter
@Setter
public class PriceReducedDto implements Serializable {

    private Long productId;

    private Long brandId;

    private Long priceList;

    private ZonedDateTime startDate;

    private ZonedDateTime endDate;

    private String price;

    private String currency;

}
