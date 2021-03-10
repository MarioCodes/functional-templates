package es.codes.mario.prueba.entity;

import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.io.Serializable;
import java.time.ZonedDateTime;

@Entity
@Data
@NoArgsConstructor
@Table(name = "PRICES")
public class Price implements Serializable {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long priceList;

    private Long brandId;

    private ZonedDateTime startDate;

    private ZonedDateTime endDate;

    private Long productId;

    private Long priority;

    private String price;

    @Column(name = "CURR")
    private String currency;

}
