package es.msanchez.fullstack.kotlin.rest

import es.msanchez.fullstack.kotlin.entity.Course
import es.msanchez.fullstack.kotlin.service.CourseService
import io.mockk.every
import io.mockk.impl.annotations.InjectMockKs
import io.mockk.impl.annotations.RelaxedMockK
import io.mockk.junit5.MockKExtension
import io.mockk.verify
import org.assertj.core.api.BDDAssertions
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.extension.ExtendWith
import java.util.*
import javax.persistence.EntityNotFoundException
import kotlin.test.assertFailsWith

@ExtendWith(MockKExtension::class)
internal class CourseRestResourceTest {

    @RelaxedMockK
    lateinit var courseService: CourseService

    @InjectMockKs
    lateinit var restResource: CourseRestResource

    @Test
    internal fun testGetAllCourses() {
        // Given
        val foundCourses = mutableListOf<Course>()

        every { courseService.findAll() } returns foundCourses

        // When
        val result: MutableIterable<Course> = this.restResource.getAllCourses()

        // Then
        BDDAssertions.assertThat(result).isSameAs(foundCourses)
        verify { courseService.findAll() }
    }

    @Test
    internal fun testGetCoursesByUsername() {
        // Given
        val username = "mario"
        val foundCourses = mutableListOf<Course>()

        every { courseService.findAllByUsername(username) } returns foundCourses

        // When
        val result: MutableIterable<Course> = this.restResource.getCoursesByUsername(username)

        // Then
        BDDAssertions.assertThat(foundCourses).isSameAs(result)
        verify { courseService.findAllByUsername(username) }
    }

    @Test
    internal fun testDeleteCourse() {
        // Given
        val id = 1L

        // When
        this.restResource.deleteCourse(id)

        // Then
        verify { courseService.deleteOne(id) }
    }

    @Test
    internal fun testGetCourse() {
        // Given
        val id = 1L

        val optGivenCourse = Optional.of(Course())
        every { courseService.getOne(id) } returns optGivenCourse

        // When
        val course = this.restResource.getCourse(id)

        // Then
        verify { courseService.getOne(id) }
        BDDAssertions.assertThat(course).isEqualTo(optGivenCourse.get());
    }

    @Test
    internal fun testGetCourseCaseEmpty() {
        // Given
        val id = 1L

        val optGivenCourse = Optional.empty<Course>()
        every { courseService.getOne(id) } returns optGivenCourse

        // When
        assertFailsWith<EntityNotFoundException>("EntityNotFound") { this.restResource.getCourse(id) }

        // Then
    }

}