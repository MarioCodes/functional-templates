package es.msanchez.frameworks.spring.boot.validator

import org.assertj.core.api.BDDAssertions
import org.junit.jupiter.api.Test

internal class KotlinValidatorTest {

    private val validator = KotlinValidator()

    @Test
    internal fun testValidator() {
        // Given

        // When
        val result: String = validator.validate()

        // Then
        BDDAssertions.assertThat(result).isEqualTo("This is the Kotlin validator")
    }

}