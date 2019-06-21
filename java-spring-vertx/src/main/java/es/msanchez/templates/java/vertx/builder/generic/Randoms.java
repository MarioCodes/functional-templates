package es.msanchez.templates.java.vertx.builder.generic;

public interface Randoms {

    Long randomLong();

    Long randomPositiveLong();

    Integer randomInteger();

    Integer randomPositiveInteger();

    Integer randomPositiveInteger(final Integer limit);

    Double randomDouble();

    Float randomFloat();

    String randomAlphanumeric();

    String randomAlphabetic();

    Boolean randomBoolean();

}
