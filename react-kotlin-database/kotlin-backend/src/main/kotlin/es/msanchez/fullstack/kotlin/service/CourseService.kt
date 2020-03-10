package es.msanchez.fullstack.kotlin.service

import es.msanchez.fullstack.kotlin.dao.CourseDao
import es.msanchez.fullstack.kotlin.entity.Course
import org.springframework.stereotype.Service
import java.util.*

@Service
class CourseService(private val courseDao: CourseDao) {

    fun initializeTestData() {
        val course1 = Course("msanchez", "Learn Full stack with Spring boot and angular")
        val course2 = Course("msanchez", "Learn Full stack with Spring boot and react")
        val course3 = Course("msanchez", "Learn Full stack with Spring boot and spring cloud")
        this.courseDao.save(course1)
        this.courseDao.save(course2)
        this.courseDao.save(course3)
    }

    fun findAllByUsername(username: String): MutableIterable<Course> {
        return this.courseDao.findAllByUsername(username)
    }

    fun findAll(): MutableIterable<Course> {
        return this.courseDao.findAll()
    }

    fun save(course: Course) {
        this.courseDao.save(course)
    }

    fun getOne(id: Long): Optional<Course> {
        return this.courseDao.findById(id)
    }

    fun deleteOne(id: Long) {
        this.courseDao.deleteById(id)
    }

}