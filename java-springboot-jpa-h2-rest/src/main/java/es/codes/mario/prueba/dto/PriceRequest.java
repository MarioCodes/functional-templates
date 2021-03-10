package es.codes.mario.prueba.dto;

import lombok.Getter;
import lombok.Setter;

import java.io.Serializable;
import java.time.LocalDateTime;

@Getter
@Setter
public class PriceRequest implements Serializable {

    private LocalDateTime applicationDate;

    private Long productId;

    private Long brandId;

}
