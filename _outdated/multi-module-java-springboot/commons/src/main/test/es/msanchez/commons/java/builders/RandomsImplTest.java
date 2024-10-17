package es.msanchez.commons.java.builders;

import org.assertj.core.api.BDDAssertions;
import org.testng.annotations.Test;

public class RandomsImplTest {

    private final RandomsImpl randoms = new RandomsImpl();

    @Test
    public void testRandomLong() {
        // @GIVEN

        // @WHEN
        final Long randomLong = this.randoms.randomLong();

        // @THEN
        BDDAssertions.assertThat(randomLong).isInstanceOf(Long.class);
    }

    @Test(invocationCount = 200)
    public void testRandomPositiveLong() {
        // @GIVEN

        // @WHEN
        final Long randomPositive = this.randoms.randomPositiveLong();

        // @THEN
        BDDAssertions.assertThat(randomPositive).isInstanceOf(Long.class);
        BDDAssertions.assertThat(randomPositive).isPositive();
    }

    @Test
    public void testRandomInteger() {
        // @GIVEN

        // @WHEN
        final Integer randomInteger = this.randoms.randomInteger();

        // @THEN
        BDDAssertions.assertThat(randomInteger).isInstanceOf(Integer.class);
    }

    @Test(invocationCount = 200)
    public void testRandomPositiveInteger() {
        // @GIVEN

        // @WHEN
        final Integer randomPositiveInteger = this.randoms.randomPositiveInteger();

        // @THEN
        BDDAssertions.assertThat(randomPositiveInteger).isInstanceOf(Integer.class);
        BDDAssertions.assertThat(randomPositiveInteger).isPositive();
    }

    @Test(invocationCount = 200)
    public void testRandomDouble() {
        // @GIVEN

        // @WHEN
        final Double randomDouble = this.randoms.randomDouble();

        // @THEN
        BDDAssertions.assertThat(randomDouble).isInstanceOf(Double.class).isPositive();
    }

    @Test(invocationCount = 200)
    public void testRandomFloat() {
        // @GIVEN

        // @WHEN
        final Float randomFloat = this.randoms.randomFloat();

        // @THEN
        BDDAssertions.assertThat(randomFloat).isInstanceOf(Float.class).isPositive();
    }

    @Test
    public void testRandomAlphanumericString() {
        // @GIVEN

        // @WHEN
        final String alphanumeric = this.randoms.randomAlphanumeric();

        // @THEN
        BDDAssertions.assertThat(alphanumeric).hasSize(50);
    }

    @Test
    public void testRandomAlphabeticString() {
        // @GIVEN

        // @WHEN
        final String alphabetic = this.randoms.randomAlphabetic();

        // @THEN
        BDDAssertions.assertThat(alphabetic).hasSize(50);
    }

    @Test(invocationCount = 200)
    public void testRandomBoolean() {
        // @GIVEN

        // @WHEN
        final Boolean bool = this.randoms.randomBoolean();

        // @THEN
        BDDAssertions.assertThat(bool).isInstanceOf(Boolean.class);
    }

    @Test(invocationCount = 200)
    public void testRandomPositiveIntegerWithLimit() {
        // @GIVEN
        final Integer upperBound = 3;

        // @WHEN
        final Integer integer = this.randoms.randomPositiveInteger(upperBound);

        // @THEN
        BDDAssertions.assertThat(integer).isExactlyInstanceOf(Integer.class)
                .isGreaterThanOrEqualTo(0).isLessThanOrEqualTo(upperBound);
    }

}