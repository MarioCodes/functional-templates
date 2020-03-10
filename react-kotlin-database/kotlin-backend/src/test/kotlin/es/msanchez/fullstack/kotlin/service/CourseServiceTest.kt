package es.msanchez.fullstack.kotlin.service

import es.msanchez.fullstack.kotlin.dao.CourseDao
import es.msanchez.fullstack.kotlin.entity.Course
import io.mockk.every
import io.mockk.impl.annotations.InjectMockKs
import io.mockk.impl.annotations.MockK
import io.mockk.junit5.MockKExtension
import io.mockk.verify
import org.assertj.core.api.Assertions.assertThat
import org.assertj.core.api.BDDAssertions
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.extension.ExtendWith
import java.util.*

@ExtendWith(MockKExtension::class)
internal class CourseServiceTest {

    @MockK
    lateinit var dao: CourseDao

    @InjectMockKs
    lateinit var service: CourseService

    @Test
    internal fun testFindAllByUsername() {
        // Given
        val username = "mario"

        val foundCourses = mutableListOf<Course>()
        every { dao.findAllByUsername(username) } returns foundCourses

        // When
        val result: MutableIterable<Course> = this.service.findAllByUsername(username)

        // Then

        verify { dao.findAllByUsername(username) }

        assertThat(result).isSameAs(foundCourses)
    }

    @Test
    internal fun testFindAll() {
        // Given
        val foundCourses = mutableListOf<Course>()
        every { dao.findAll() } returns foundCourses

        // When
        val result: MutableIterable<Course> = this.service.findAll()

        // Then
        BDDAssertions.assertThat(result).isSameAs(foundCourses)
        verify { dao.findAll() }
    }

    @Test
    internal fun testGetOne() {
        // Given
        val id = 1L

        val givenCourse = Optional.of(Course())
        every { dao.findById(id) } returns givenCourse

        // When
        val course = this.service.getOne(id)

        // Then
        verify { dao.findById(id) }
        BDDAssertions.assertThat(course).isPresent.isSameAs(course)
    }

    @Test
    internal fun testGetOneCaseEmpty() {
        // Given
        val id = 1L

        val emptyCourse = Optional.empty<Course>()
        every { dao.findById(id) } returns emptyCourse

        // When
        val course = this.service.getOne(id)

        // Then
        verify { dao.findById(id) }
        BDDAssertions.assertThat(course).isEmpty
    }

    @Test
    internal fun testDeleteOne() {
        // Given
        val id = 1L
        every { dao.deleteById(id) } returns Unit

        // When
        this.service.deleteOne(id)

        // Then
        verify { dao.deleteById(id) }
    }

}