package es.msanchez.fullstack.kotlin.dao

import es.msanchez.fullstack.kotlin.entity.Course
import org.springframework.stereotype.Repository

@Repository
interface CourseDao : RawDao<Course> {

    fun findAllByUsername(username: String): MutableIterable<Course>

}