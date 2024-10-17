package es.msanchez.frameworks.spring.boot.rest

import es.msanchez.frameworks.spring.boot.validator.KotlinValidator
import org.junit.jupiter.api.Test
import org.mockito.BDDMockito
import org.mockito.Mockito

internal class KotlinRestControllerTest {

    private val validator = Mockito.mock(KotlinValidator::class.java)

    private val controller = KotlinRestController(this.validator)

    @Test
    internal fun testIndex() {
        // Given

        // When
        this.controller.index()

        // Then
        BDDMockito.verify(this.validator).validate()
    }
}