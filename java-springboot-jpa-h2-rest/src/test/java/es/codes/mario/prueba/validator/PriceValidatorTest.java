package es.codes.mario.prueba.validator;

import org.assertj.core.api.BDDAssertions;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.mockito.InjectMocks;
import org.mockito.junit.MockitoJUnitRunner;

@RunWith(MockitoJUnitRunner.class)
public class PriceValidatorTest {

    @InjectMocks
    private PriceValidator validator;

    @Test
    public void whenIsValidWithStringDateNullValueThenExpectFalse() {
        // given
        final String date = null;

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithStringDateEmptyValueThenExpectFalse() {
        // given
        final String date = "";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithCorrectStringDateValueThenExpectTrue() {
        // given
        final String date = "2020-06-14 15:00:00";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isTrue();
    }

    @Test
    public void whenIsValidWithRandomStringDateValueThenExpectFalse() {
        // given
        final String date = "THIS_IS_NOT_A_DATE";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithWrongFormatMillisecondsValueThenExpectFalse() {
        // given
        final String date = "2020-06-14 15:00:00.000";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithWrongFormatTValueThenExpectFalse() {
        // given
        final String date = "2020-06-14T15:00:00";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithWrongCharAppendedValueThenExpectFalse() {
        // given
        final String date = "2020-06-14 15:00:00a";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithWrongFormatSwappedDayMonthValueThenExpectFalse() {
        // given
        final String date = "2020-14-06 15:00:00";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithWrongFormatNoSecondsValueThenExpectFalse() {
        // given
        final String date = "2020-06-14 15:00";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithWrongFormatNoTimeValueThenExpectFalse() {
        // given
        final String date = "2020-06-14";

        // when
        final boolean isValid = validator.isValid(date);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithLongNullValueThenExpectFalse() {
        // given
        final Long negativeValue = null;

        // when
        final boolean isValid = this.validator.isValid(negativeValue);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }


    @Test
    public void whenIsValidWithLongNegativeValueThenExpectFalse() {
        // given
        final Long negativeValue = -1L;

        // when
        final boolean isValid = this.validator.isValid(negativeValue);

        // then
        BDDAssertions.assertThat(isValid).isFalse();
    }

    @Test
    public void whenIsValidWithLongZeroValueThenExpectTrue() {
        // given
        final Long negativeValue = 0L;

        // when
        final boolean isValid = this.validator.isValid(negativeValue);

        // then
        BDDAssertions.assertThat(isValid).isTrue();
    }

    @Test
    public void whenIsValidWithLongPositiveValueThenExpectTrue() {
        // given
        final Long negativeValue = 1L;

        // when
        final boolean isValid = this.validator.isValid(negativeValue);

        // then
        BDDAssertions.assertThat(isValid).isTrue();
    }

}