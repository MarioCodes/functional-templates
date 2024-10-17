package es.msanchez.commons.java.builders;

import java.security.SecureRandom;
import java.util.Random;

public class RandomsImpl implements Randoms {

    private final static int DEFAULT_STRING_LENGTH = 50;

    protected Random randomGenerator() {
        return new SecureRandom();
    }

    /**
     * @return not null, positive or negative Long
     */
    @Override
    public Long randomLong() {
        final Random random = this.randomGenerator();
        return random.nextLong();
    }

    /**
     * @return not null, positive Long
     */
    @Override
    public Long randomPositiveLong() {
        final Long randomLong = this.randomLong();
        return randomLong > 0L ? randomLong : randomLong * -1L;
    }

    /**
     * @return not null, positive or negative Integer
     */
    @Override
    public Integer randomInteger() {
        final Random random = this.randomGenerator();
        return random.nextInt();
    }

    /**
     * @return not null, positive Integer
     */
    @Override
    public Integer randomPositiveInteger() {
        final Integer randomInt = this.randomInteger();
        return randomInt > 0 ? randomInt : randomInt * -1;
    }

    /**
     * @param limit positive integer
     * @return not null, positive integer between 0 and {@code limit}, both included
     */
    @Override
    public Integer randomPositiveInteger(final Integer limit) {
        final Random random = this.randomGenerator();
        return random.nextInt(limit + 1);
    }

    /**
     * @return not null, positive double
     */
    @Override
    public Double randomDouble() {
        final Random random = this.randomGenerator();
        return random.nextDouble();
    }

    @Override
    public Float randomFloat() {
        final Random random = this.randomGenerator();
        return random.nextFloat();
    }

    /**
     * @return not null, random String with length = 50, which contains from a to Z and 0 to 9
     */
    @Override
    public String randomAlphanumeric() {
        final String source = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        return this.buildRandomString(source);
    }

    /**
     * @return not null, random String with length = 50, which contains from a to Z
     */
    @Override
    public String randomAlphabetic() {
        final String source = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        return this.buildRandomString(source);
    }

    private String buildRandomString(final String source) {
        final Random random = this.randomGenerator();
        final StringBuilder sb = new StringBuilder();
        for (int i = 0; i < DEFAULT_STRING_LENGTH; i++) {
            sb.append(source.charAt(random.nextInt(source.length())));
        }
        return sb.toString();
    }

    @Override
    public Boolean randomBoolean() {
        return this.randomPositiveInteger(1) == 1;
    }
}
