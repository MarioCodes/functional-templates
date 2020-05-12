package es.msanchez.templates.java.vertx.entity;

import lombok.Data;
import org.springframework.stereotype.Component;

/**
 * {@code @Component} here is just to test DI works. Delete it for future entities.
 */
@Data
@Component
public class Person {

    private Long id;

    private Integer age;

    private String name;

    private Double doubleField;

    private Float floatField;

    private Boolean booleanField;

}
