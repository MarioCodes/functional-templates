package es.msanchez.frameworks.spring.boot.service

import es.msanchez.frameworks.spring.boot.dao.PersonDao
import es.msanchez.frameworks.spring.boot.entity.Person
import io.mockk.every
import io.mockk.impl.annotations.InjectMockKs
import io.mockk.impl.annotations.MockK
import io.mockk.junit5.MockKExtension
import io.mockk.verify
import org.assertj.core.api.BDDAssertions
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.extension.ExtendWith
import java.util.*

@ExtendWith(MockKExtension::class)
internal class PersonServiceTest {

    @MockK
    lateinit var dao: PersonDao

    @InjectMockKs
    lateinit var service: PersonService

    @Test
    internal fun testIsValidCaseDoesntExist() {
        // Given
        val name = "mario"

        every { dao.findOneByName(name) } returns Optional.empty()

        // When
        val result = this.service.isValid(name)

        // Then
        BDDAssertions.assertThat(result).isTrue()
    }

    @Test
    internal fun testIsValidCaseAlreadyExists() {
        // Given
        val name = "mario"

        every { dao.findOneByName(name) } returns Optional.of(Person())

        // When
        val result = this.service.isValid(name)

        // Then
        BDDAssertions.assertThat(result).isFalse()
    }

    @Test
    internal fun testSave() {
        // Given
        val person = Person()

        every { dao.save(person) } returns null

        // When
        this.service.save(person)

        // Then
        verify { dao.save(person) }
    }

}