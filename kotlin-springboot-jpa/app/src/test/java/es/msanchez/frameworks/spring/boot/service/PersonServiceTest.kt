package es.msanchez.frameworks.spring.boot.service

import es.msanchez.frameworks.spring.boot.dao.PersonDao
import es.msanchez.frameworks.spring.boot.entity.Person
import org.assertj.core.api.BDDAssertions
import org.junit.jupiter.api.Test
import org.mockito.BDDMockito
import org.mockito.Mockito
import java.util.*

internal class PersonServiceTest {

    private val dao = Mockito.mock(PersonDao::class.java)

    private val service = PersonService(this.dao)

    @Test
    internal fun testIsValidCaseDoesntExist() {
        // Given
        val name = "mario"

        BDDMockito.given(this.dao.findOneByName(name))
                .willReturn(Optional.empty())

        // When
        val result = this.service.isValid(name)

        // Then
        BDDAssertions.assertThat(result).isTrue()
    }

    @Test
    internal fun testIsValidCaseAlreadyExists() {
        // Given
        val name = "mario"

        BDDMockito.given(this.dao.findOneByName(name))
                .willReturn(Optional.of(Person()))

        // When
        val result = this.service.isValid(name)

        // Then
        BDDAssertions.assertThat(result).isFalse()
    }

    @Test
    internal fun testSave() {
        // Given
        val person = Person()

        // When
        this.service.save(person)

        // Then
        BDDMockito.verify(this.dao).save(person)
    }
}